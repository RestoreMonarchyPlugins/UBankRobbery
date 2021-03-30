using Game4Freak.AdvancedZones;
using Rocket.Unturned.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace UBankRobbery.Regions.RocketRegions
{
    public class AdvancedZonesManager : IRegionManager
    {
        public AdvancedZonesManager()
        {
            AdvancedZones.onZoneLeave += ZoneLeave;
        }

        public BankRobberyRegionConfig GetRegion(Vector3 position)
        {
            var found = AdvancedZones.Instance.getPositionZones(position);
            if (found == null || found.Count == 0)
                return null;

            BankRobberyRegionConfig config;
            foreach (var zone in found)
            {
                config = Plugin.Instance.Configuration.Instance.Banks.FirstOrDefault(x => x.RegionId == zone.name);
                if (config != null)
                    return config;
            }

            return null;
        }

        private void ZoneLeave(UnturnedPlayer player, Zone zone, Vector3 lastPos)
        {
            var found = Plugin.Instance.RobManager.RunningRobberies.SingleOrDefault(c => c.Robber == player.Player && c.Region.RegionId == zone.name);
            if (found == null)
                return;
            Plugin.Instance.RobManager.Fail(found);
        }
    }
}
