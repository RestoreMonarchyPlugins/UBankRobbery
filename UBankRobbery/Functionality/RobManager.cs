using Rocket.Unturned;
using Rocket.Unturned.Chat;
using Rocket.Unturned.Events;
using Rocket.Unturned.Player;
using SDG.Unturned;
using Steamworks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UBankRobbery.Regions;
using UnityEngine;

namespace UBankRobbery.Functionality
{
    public class RobManager
    {
        public RobManager()
        {
            U.Events.OnPlayerDisconnected += Disconnected;
            UnturnedPlayerEvents.OnPlayerDeath += Death;
        }

        public ICollection<RunningRobbery> RunningRobberies { get; } = new List<RunningRobbery>();
        public IDictionary<string, DateTime> Cooldowns { get; } = new Dictionary<string, DateTime>();

        public bool Rob(Player robber, BankRobberyRegionConfig configuration)
        {
            if (configuration == null)
                throw new ArgumentNullException(nameof(configuration));

            if (RunningRobberies.Any(c => c.Region.RegionId == configuration.RegionId))
                throw new Exception($"Robbery already running on region: {configuration.RegionId}");

            var robbery = new RunningRobbery(robber, configuration);
            RunningRobberies.Add(robbery);
            UnturnedChat.Say(Plugin.Instance.Translate("robbing", robber.channel.owner.playerID.characterName, configuration.RegionId), Color.yellow);

            return true;
        }

        public void Fail(RunningRobbery robbery)
        {
            UnturnedChat.Say(Plugin.Instance.Translate("ended", robbery.Robber.channel.owner.playerID.characterName, robbery.Region.RegionId));
            RunningRobberies.Remove(robbery);
            Cooldowns.Add(robbery.Region.RegionId, DateTime.UtcNow.AddSeconds(robbery.Region.RobbingInterval));
        }

        private void Death(UnturnedPlayer player, EDeathCause cause, ELimb limb, CSteamID murderer)
        {
            var found = RunningRobberies.SingleOrDefault(c => c.Robber.channel.owner.playerID.steamID == player.CSteamID);
            if (found == null)
                return;
            Fail(found);
        }

        private void Disconnected(UnturnedPlayer player)
        {
            var found = RunningRobberies.SingleOrDefault(c => c.Robber.channel.owner.playerID.steamID == player.CSteamID);
            if (found == null)
                return;
            Fail(found);
        }

        internal void FixedUpdate()
        {
            var completed = RunningRobberies.Where(c => c.IsCompleted).ToArray();

            foreach (var item in completed)
            {
                item.IssueReward();
                Cooldowns.Add(item.Region.RegionId, DateTime.UtcNow.AddSeconds(item.Region.RobbingInterval));
                RunningRobberies.Remove(item);
            }
        }
    }
}
