using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Facility : MonoBehaviour
{

    [System.Serializable]
   public class FacilityStats {
        [SerializeField]
        int CurrentLevel, MaxLevel;

        [SerializeField]
        int LevelsPast;

        public int PMaxLevel { get { return MaxLevel; } set { MaxLevel = value; } }
        public int PLevelsPast
        {
            get { return LevelsPast; }
            set
            {
                LevelsPast = value;
                if (LevelsPast >= 5)
                {
                    LevelsPast = 0;
                    PMainFrameLevelRequirement += 1;

                }
            }
        }

        [SerializeField]
        double NextLevelCost;
        public double PNextLevelCost { get { return NextLevelCost; } set { NextLevelCost = value; } }

        [SerializeField]
        int MainFrameLevelRequirement;
        public int PMainFrameLevelRequirement { get { return MainFrameLevelRequirement; } set { MainFrameLevelRequirement = value; } }

        public int PCurrentLevel
        {
            get
            {
                return CurrentLevel;
            }
            set
            {
                if (!(value > MaxLevel))
                {
                    CurrentLevel = value;

                   // ConnectedUI.UpdateLevelText();
                    
                }
            }
        }
        public int BaseCost;
    }

    [SerializeField]
    public FacilityStats Stats = new FacilityStats();
   
    public bool IsMainframe, IsTurret;
    Vector3 NearTopPos = new Vector3(0.1f, 2.3f, 2f);
    [SerializeField]
    FacilityUI ConnectedUI;
    [SerializeField]
    Turret ConnectedTurret;
  

    public enum Facilitys { Damage, MoneyMaker, Speed, Mainframe, AutoMine, Turret, AddTurret}
    [SerializeField]
    Facilitys FacilityType;
    [SerializeField]
    public Facility InstanceOfFacility;
    public Facilitys PFacilityType { get{ return FacilityType; }  }

    private void OnEnable()
    {

        //if (Stats.PMaxLevel < 50) Stats.PMaxLevel = 50;
        if (IsTurret) Stats.PNextLevelCost = Stats.BaseCost * NumberCruncher.ExpenentialEquation(GM.Singleton.PlayerStats.PAvailableTurretSlots, 10);
        else Stats.PNextLevelCost = Stats.BaseCost;
        
        // if (PFacilityType == Facilitys.Mainframe) Stats.PNextLevelCost = 100000;
        InstanceOfFacility = this;
    }

    private void Start()
    {
        ConnectedUI.UpdateDescription(FacilityType);
    }

    void IncreaseStats(Facilitys SelectedStat)
    {
        if (Stats.PCurrentLevel == Stats.PMaxLevel) return;

        if (GM.Singleton.PlayerStats.PCredits >= Stats.PNextLevelCost && Stats.PCurrentLevel < Stats.PMaxLevel && Stats.PMainFrameLevelRequirement <= GM.Singleton.PlayerStats.MainFrameLevel)
        {
            GM.Singleton.RemoveCredits(Stats.PNextLevelCost);
            // Spawner.Singleton.SpawnMoneyLossText(Stats.PNextLevelCost);   

            switch (SelectedStat)
            {
                case Facilitys.Damage:

                    GM.Singleton.PlayerMultiplyersAndValues.PDamageNumber *= 1.25f;
                    
                    //Spawner.Singleton.SpawnLevelUpText("Damage +", 5, NearTopPos);
                    break;

                case Facilitys.MoneyMaker:

                    GM.Singleton.PlayerMultiplyersAndValues.PMoneyMultiplyer *= 1.25f;
                    // Spawner.Singleton.SpawnLevelUpText("Money Multiplyer x", GM.Singleton.PlayerMultiplyersAndValues.PMoneyMultiplyer, NearTopPos);
                    break;

                case Facilitys.Speed:

                    GM.Singleton.PlayerMultiplyersAndValues.PSpeedMultiplyer *= 1.5f;
                    //  Spawner.Singleton.SpawnLevelUpText("Speed x", 1.25f, NearTopPos);
                    ScrollingBackground.Singleton.IncreaseScrollingSpeed(ScrollingBackground.Singleton.AdditionSpeed);
                    PlayerUI.Singleton.UpdateKMPerSecondText();
                    break;

                case Facilitys.Mainframe:

                    GM.Singleton.PlayerStats.MainFrameLevel += 1;
                    GM.Singleton.PlayerStats.PShipModeStage++;
                    //   Spawner.Singleton.SpawnLevelUpText("Mainframe Level +", 1, NearTopPos);
                    break;

                case Facilitys.AutoMine:

                    GM.Singleton.PlayerStats.PBasePassiveMoney += (NumberCruncher.ExpenentialEquation(Stats.PCurrentLevel, 2));
                    GM.Singleton.PlayerStats.PPassiveMoneyHoursLimit += 0.5f;
                    // Spawner.Singleton.SpawnLevelUpText("Passive Income x", 2, NearTopPos);
                    break;

                //  case Facilitys.AddTurret:
                //    GM.Singleton.PlayerStats.PAvailableTurretSlots++;
                //  break;
                case Facilitys.Turret:

                    if (ConnectedTurret.PSwitch != true)
                    {
                        //    Spawner.Singleton.SpawnLevelUpText("Turet Activated", 0, NearTopPos);
                        ConnectedTurret.PSwitch = true;
                    }

                    else
                    {
                        ConnectedTurret.PFireRate /= 1.5f;
                        //  Spawner.Singleton.SpawnLevelUpText("Turret Fire Rate x", 2f, NearTopPos);
                    }

                    break;

            }

            Stats.PCurrentLevel += 1;

            //todo Change PnextLevelCost to Level
            if (PFacilityType == Facilitys.Mainframe) Stats.PNextLevelCost += (100 * NumberCruncher.ExpenentialEquation(80f, Stats.PCurrentLevel));
            else if (PFacilityType == Facilitys.Turret) Stats.PNextLevelCost += Stats.BaseCost * (NumberCruncher.ExpenentialEquation(GM.Singleton.PlayerStats.PAvailableTurretSlots, 10));
            else Stats.PNextLevelCost += 10*(NumberCruncher.ExpenentialEquation(2.5f, Stats.PCurrentLevel));
            SoundManager.Singleton.PlayLevelUpSound();
            print(Stats.PNextLevelCost);

            Stats.PLevelsPast += 1;
            ConnectedUI.UpdateDescription(SelectedStat);
            UpdateFacilityUI();
        }
        else { SoundManager.Singleton.PlayErrorSound(); }
    }

    public void UpdateFacilityUI()
    {
        ConnectedUI.UpdateLevelText();
        ConnectedUI.UpdateMainFrameRequirement();
        ConnectedUI.UpdateCostText();
    }

    public void IncreaseStats()
    {
        IncreaseStats(PFacilityType);
    }
   
       
    
    

}
