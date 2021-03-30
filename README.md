## UBankRobbery

### Features
* Supports AdvancedZones and AdvancedRegions
* Can use XP or Uconomy for rewards
* Players have to survive in bank for `RobbingDuration` seconds to get reward in range of `MinimumReward` and `MaximumReward`

### Commands
- **/robbank - Starts the robbery the player is currently standing in**

### Configuration
```xml
<?xml version="1.0" encoding="utf-8"?>
<Configuration xmlns:xsd="http://www.w3.org/2001/XMLSchema" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance">
  <UseUconomy>false</UseUconomy>
  <Banks>
    <BankRobberyRegionConfiguration>
      <RegionId>bank1</RegionId>
      <RobbingInterval>60</RobbingInterval>
      <MinimumReward>2500</MinimumReward>
      <MaximumReward>5000</MaximumReward>
      <RobbingDuration>20</RobbingDuration>
    </BankRobberyRegionConfiguration>
  </Banks>
</Configuration>
```

### Translations
```xml
<?xml version="1.0" encoding="utf-8"?>
<Translations xmlns:xsd="http://www.w3.org/2001/XMLSchema" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance">
  <Translation Id="succesfully_started" Value="{0} is getting robbed by {1}!" />
  <Translation Id="ended" Value="{0} robbery on {1} has failed!" />
  <Translation Id="finished" Value="{0} robbery succeded and he got away!" />
  <Translation Id="no_region_found" Value="There is no bank here! color=red" />
  <Translation Id="on_cooldown" Value="You're not allowed to rob this bank for another {0} seconds!" />
  <Translation Id="already_robbing" Value="{0} is already getting robbed!" />
  <Translation Id="robbing" Value="{0} is robbing bank {1}!" />
</Translations>
```