using Rocket.Unturned.Player;
using RocketRegions;
using RocketRegions.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace UBankRobbery.Regions
{
    public class RocketRegionsManager : IRegionManager
    {
        public RocketRegionsManager()
        {
            RegionsPlugin.Instance.RegionLeave += RegionLeave;
        }

        public BankRobberyRegionConfiguration GetRegion(Vector3 position)
        {

            var found = RegionsPlugin.Instance.GetRegionAt(position);
            if (found == null)
                return null;
            return Plugin.Instance.Configuration.Instance.Banks.SingleOrDefault(c => c.RegionId == found.Name);
        }

        private void RegionLeave(UnturnedPlayer player, Region region)
        {
            var found = Plugin.Instance.RobManager.RunningRobberies.SingleOrDefault(c => c.Robber == player.Player && c.Region.RegionId == region.Name);
            if (found == null)
                return;
            Plugin.Instance.RobManager.Fail(found);
        }
    }
}
