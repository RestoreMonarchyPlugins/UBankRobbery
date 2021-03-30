using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace UBankRobbery.Regions
{
    public interface IRegionManager
    {
        BankRobberyRegionConfiguration GetRegion(Vector3 position);
    }
}
