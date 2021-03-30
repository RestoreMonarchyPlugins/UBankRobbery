using Rocket.Unturned.Chat;
using Rocket.Unturned.Player;
using SDG.Unturned;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UBankRobbery.Helpers;
using UBankRobbery.Regions;
using UnityEngine;

namespace UBankRobbery.Functionality
{
    public class RunningRobbery
    {
        public RunningRobbery(Player robber, BankRobberyRegionConfig region)
        {
            Robber = robber ?? throw new ArgumentNullException(nameof(robber));
            Region = region ?? throw new ArgumentNullException(nameof(region));
            StartedAtUtc = DateTime.UtcNow;
        }
        public Player Robber { get; }
        public BankRobberyRegionConfig Region { get; }
        public DateTime StartedAtUtc { get; }

        public bool IsCompleted => (DateTime.UtcNow - StartedAtUtc).TotalSeconds > Region.RobbingDuration;

        public void IssueReward()
        {
            var random = new System.Random();
            var reward = random.Next(Region.MinimumReward, Region.MaximumReward);
            var uPlayer = UnturnedPlayer.FromPlayer(Robber);

            if (Plugin.Instance.Configuration.Instance.UseUconomy)
            {
                if (!Plugin.IsDependencyLoaded("Uconomy"))
                {
                    Rocket.Core.Logging.Logger.LogWarning("Uconomy plugin not found, giving reward in experience");
                    uPlayer.Experience += (uint)reward;
                } else
                {
                    UconomyHelper.Pay(uPlayer.Id, reward);
                }
            } else
            {
                uPlayer.Experience += (uint)reward;
            }
            
            UnturnedChat.Say(Plugin.Instance.Translate("finished", uPlayer.CharacterName), Color.yellow);
        }
    }
}
