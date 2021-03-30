using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UBankRobbery.Regions
{
    public class BankRobberyRegionConfig
    {
        public string RegionId { get; set; }

        public int MinimumReward { get; set; }
        public int MaximumReward { get; set; }

        public int RobbingInterval { get; set; }
        public int RobbingDuration { get; set; }
    }
}
