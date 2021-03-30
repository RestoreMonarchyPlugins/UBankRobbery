using Rocket.API;
using Rocket.Unturned.Chat;
using Rocket.Unturned.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace UBankRobbery.Commands
{
    public class CommandRobbank : IRocketCommand
    {
        public AllowedCaller AllowedCaller { get; } = AllowedCaller.Player;
        public string Name { get; } = "robbank";
        public string Help { get; } = "";
        public string Syntax { get; } = "";
        public List<string> Aliases { get; } = new List<string>();
        public List<string> Permissions { get; } = new List<string>()
        {
            "robbank"
        };

        public void Execute(IRocketPlayer caller, string[] command)
        {
            var player = (UnturnedPlayer)caller;

            var foundBank = Plugin.Instance.RegionManager.GetRegion(player.Position);

            if(foundBank == null)
            {
                UnturnedChat.Say(caller, Plugin.Instance.Translate("no_region_found"), Color.red);
                return;
            }

            if(Plugin.Instance.RobManager.RunningRobberies.Any(c => c.Region.RegionId == foundBank.RegionId))
            {
                UnturnedChat.Say(caller, Plugin.Instance.Translate("already_robbing", foundBank.RegionId), Color.red);
                return;
            }

            var cooldown = Plugin.Instance.RobManager.Cooldowns.SingleOrDefault(c => c.Key == foundBank.RegionId);
            if(DateTime.UtcNow < cooldown.Value)
            {
                UnturnedChat.Say(caller, Plugin.Instance.Translate("on_cooldown", (int)(cooldown.Value - DateTime.UtcNow).TotalSeconds), Color.red);
                return;
            }

            Plugin.Instance.RobManager.Cooldowns.Remove(foundBank.RegionId);
            Plugin.Instance.RobManager.Rob(player.Player, foundBank);
        }
    }
}
