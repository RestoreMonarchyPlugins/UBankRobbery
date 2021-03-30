using ImperialPlugins.AdvancedRegions;
using SDG.Unturned;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace UBankRobbery.Regions
{
    public class AdvancedRegionsManager : IRegionManager
    {
        public AdvancedRegionsManager()
        {
            AdvancedRegionsPlugin.Instance.RegionsManager.OnPlayerLeaveRegion += LeavedRegion;
        }

        private void LeavedRegion(Region region, Player player)
        {
            var found = Plugin.Instance.RobManager.RunningRobberies.SingleOrDefault(c => c.Robber == player && c.Region.RegionId == region.RegionInfo.Name);
            if (found == null)
                return;
            Plugin.Instance.RobManager.Fail(found);
        }

        public BankRobberyRegionConfiguration GetRegion(Vector3 position)
        {
            var found = ImperialPlugins.AdvancedRegions.AdvancedRegionsPlugin.Instance.RegionsManager.GetRegionsAtPoint(position);
            return Plugin.Instance.Configuration.Instance.Banks.SingleOrDefault(c => found.Any(k => c.RegionId == k.RegionInfo.Name));
        }
    }
}
