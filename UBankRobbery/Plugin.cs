using Rocket.API.Collections;
using Rocket.Core.Plugins;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UBankRobbery.Functionality;
using UBankRobbery.Regions;

namespace UBankRobbery
{
    public class Plugin : RocketPlugin<Configuration>
    {
        public static Plugin Instance { get; private set; }
        public UBankRobbery.Regions.IRegionManager RegionManager { get; private set; }
        public RobManager RobManager { get; private set; }
        protected override void Load()
        {            
            Instance = this;

            RobManager = new RobManager();
            if (IsDependencyLoaded("AdvancedRegions"))
            {
                Rocket.Core.Logging.Logger.Log("AdvancedRegions plugin found!");
                RegionManager = new UBankRobbery.Regions.AdvancedRegions.AdvancedRegionsManager();
            }
            else if (IsDependencyLoaded("AdvancedZones"))
            {
                Rocket.Core.Logging.Logger.Log("AdvancedZones plugin found!");
                RegionManager = new UBankRobbery.Regions.RocketRegions.AdvancedZonesManager();
            }
            else
            {

                Rocket.Core.Logging.Logger.LogError("No regions plugin was found!");
                UnloadPlugin();
            }

            Rocket.Core.Logging.Logger.Log($"{Name} {Assembly.GetName().Version} has been loaded!", ConsoleColor.Yellow);
        }

        protected override void Unload()
        {
            Rocket.Core.Logging.Logger.Log($"{Name} has been unloaded!", ConsoleColor.Yellow);
        }

        public override TranslationList DefaultTranslations => new TranslationList()
        {
            {
                "succesfully_started",
                "{0} is getting robbed by {1}!"
            },
            {
                "ended",
                "{0} robbery on {1} has failed!"
            },
            {
                "finished",
                "{0} robbery succeded and he got away!"
            },
            {
                "no_region_found",
                "There is no bank here! color=red"
            },
            {
                "on_cooldown",
                "You're not allowed to rob this bank for another {0} seconds!"
            },
            {
                "already_robbing",
                "{0} is already getting robbed!"
            },
            {
                "robbing",
                "{0} is robbing bank {1}!"
            },
            { 
                "finished_reward", 
                "You robbed ${0} from the bank!"
            }
        };

        private void FixedUpdate()
        {
            RobManager.FixedUpdate();
        }
    }
}
