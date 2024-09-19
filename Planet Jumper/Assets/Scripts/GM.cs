using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GM : MonoBehaviour
{
    Ray RayShot;
    RaycastHit2D HitThing, ClickHitThing;
    public static GM Singleton;
    bool LeftClickHeldDown = false;
    bool PlayerIsInUI;
    public bool PPlayerIsInUI { get { return PlayerIsInUI; } set { PlayerIsInUI = value; } }
   
    public GameStats PlayerStats = new GameStats();
    public MultiplyersAndNumbers PlayerMultiplyersAndValues = new MultiplyersAndNumbers();
    [SerializeField]
    GameObject[] TurretUpgradeMenus;
    public GameObject[] PTurretUpgradeMenus { get { return TurretUpgradeMenus; } }
    [SerializeField]
    GameObject[] Turrets = new GameObject[4];
    public GameObject[] PTurrets
    {
        get { return Turrets; }
    }
    [SerializeField]
    int SecondsBeforeSave;

    [System.Serializable]
    public class GameStats
    {
        double DistanceLeft;
        public double PDistanceLeft {get{return DistanceLeft; }
        set { DistanceLeft = value;

                if (DistanceLeft < 0 && PlanetStructure.Singleton.Planets[GM.Singleton.PlayerStats.TargetPlanet].PHasReached == false)
                {
                    PIsMoving = false;
                    DistanceLeft = 0;
                    PlanetStructure.Singleton.PlanetReached();
                     PlayerUI.Singleton.UpdateETAText();
                     PlayerUI.Singleton.UpdateDistanceSlider();
                }
            }
        }
        int ShipModelStage;
        public int PShipModeStage
        {
            get { return ShipModelStage; }
            set { ShipModelStage = value;
                if (ShipModelStage >= ShipModelChange.Singleton.PPlayerModels.Length) ShipModelStage = ShipModelChange.Singleton.PPlayerModels.Length - 1;
                else if (ShipModelStage < 0) ShipModelStage = 0;
                ShipModelChange.Singleton.ChangeShipModel(ShipModelStage);
            }
        }
        public int TargetPlanet, CurrentPlanet;
        int CurrentSolarSystem = 0;
        public int PCurrentSolarSystem
        { get { return CurrentSolarSystem; }
            set {
                CurrentSolarSystem = value;
                BackgroundManager.Singleton.StartCoroutine("ChangeBackgroundMat");
                PlayerUI.Singleton.UpdateSliderColour();
            } }
        bool IsMoving = true;
        public bool PIsMoving { get { return IsMoving; } set { IsMoving = value; } }
        public bool FirstTurretGot;
        double Credits = 0;
        public double PCredits
        {
            get
            {
                return Credits;

            }
            set
            {

                Credits = value;

                if (Credits < 0)
                {
                    Credits = 0;
                }
                PlayerUI.Singleton.UpdateCreditsText();
            }

        }
        [SerializeField]
        double BasePassiveMoney;
        public double PBasePassiveMoney
        {
            get
            {
                return BasePassiveMoney;
            }
            set
            {
                BasePassiveMoney = value;
                if (BasePassiveMoney > 1)
                {
                    PPassiveIncomeAvailable = true;
                } else
                {
                    PPassiveIncomeAvailable = false;
                }


            }
        }
        [SerializeField]
        float PassiveMoneyHoursLimit;
        public float PPassiveMoneyHoursLimit { get { return PassiveMoneyHoursLimit; } set { PassiveMoneyHoursLimit = value; } }

        [SerializeField]
        double KMSpeed;
        public double PKMSpeed
        {
            get
            {
                return KMSpeed;
            }

            set
            {
                KMSpeed = value;
                PlayerUI.Singleton.UpdateKMPerSecondText();
            }
        }

        public int MainFrameLevel;
        

        int AvailableTurretSlots;
        public int PAvailableTurretSlots
        {
            get { return AvailableTurretSlots; }
            set { AvailableTurretSlots = value;
                if (AvailableTurretSlots >= 5) AvailableTurretSlots = 4;
                else if (AvailableTurretSlots < 0) AvailableTurretSlots = 0;
                else Singleton.SetTurrets();
            }
        }

        float CreditsPerSecond;
        [SerializeField]
        float DamageRate = 0.2f;
        public float PDamageRate
        {
            get
            {
                return DamageRate;
            }
            set
            {
                DamageRate = value;
            }
        }

        bool PassiveIncomeAvailable;
        public bool PPassiveIncomeAvailable
        {
            get
            {
                return PassiveIncomeAvailable;

            }
            set
            {
                PassiveIncomeAvailable = value;
                if (PassiveIncomeAvailable && !PassiveCoroutineStarted)
                {
                    Singleton.StartCoroutine("PassiveIncome");

                }
                else if(!PassiveIncomeAvailable)
                {
                    Singleton.StopCoroutine("PassiveIncome");

                }
            }
        }
        public bool PassiveCoroutineStarted;
       
    }

    [System.Serializable]
    public class MultiplyersAndNumbers
    {
       float DamageNumber;
        [SerializeField]
       float SpeedMultiplyer;
        float ValueMultiplyer;
        public float PValueMultiplyer { get { return ValueMultiplyer; } set { ValueMultiplyer = value; } }
        [SerializeField]
       float MoneyMultiplyer;

        public float PDamageNumber
        {
            get {
                return DamageNumber;
            }
            set
            {
                DamageNumber = value;
                if(DamageNumber < 1)
                {
                    DamageNumber = 1;
                }
            }
        }

        public float PSpeedMultiplyer
        {
            get
            {
                return SpeedMultiplyer;
            }
            set
            {
                SpeedMultiplyer = value;
                if (SpeedMultiplyer < 1)
                {
                    SpeedMultiplyer = 1;
                }
            }
        }

        public float PMoneyMultiplyer
        {
            get
            {
                return MoneyMultiplyer;
            }
            set
            {
                MoneyMultiplyer = value;
                if (MoneyMultiplyer < 1)
                {
                    MoneyMultiplyer = 1;
                }
            }
        }

       public MultiplyersAndNumbers()
        {
            PValueMultiplyer = 1;
            PDamageNumber = 5;
            PSpeedMultiplyer = 1;
            PMoneyMultiplyer = 1;
            
        }
    }

    private void Awake()
    {
        Singleton = this;
        PlayerStats.MainFrameLevel = 1;
        FalsifyPassiveAvailability();
               //Singleton.PlayerStats.PBasePassiveMoney = 1;
    }

    private void FalsifyPassiveAvailability()
    {
        PlayerStats.PPassiveIncomeAvailable = false;
        PlayerStats.PassiveCoroutineStarted = false;
    }


    private static void InitialiseUI()
    {
        PlayerUI.Singleton.UpdateCreditsText();
        PlayerUI.Singleton.UpdateKMPerSecondText();
        PlayerUI.Singleton.UpdateTargetDistance();
    }

    private void Start()
    {
        
        InitialiseUI();
        //  SaveManager.Singleton.LoadGame();
        
        StartCoroutine("SaveGameProcess");
    }

    public void SetTurrets()
    {
        SoundManager.Singleton.PlayTurretGet();
        for(int i = 0; i < PlayerStats.PAvailableTurretSlots; i++)
        {
            PTurrets[i].SetActive(true);
            PTurrets[i].GetComponent<Turret>().PSwitch = true;  
            PTurretUpgradeMenus[i].SetActive(true);
        }
    }



    private void Update()
    {
       if(Debug.isDebugBuild) DeveloperCheats();

        RayShot = Camera.main.ScreenPointToRay((Vector2)Input.mousePosition);

     if (!LeftClickHeldDown && Input.GetMouseButtonDown(0) && !PPlayerIsInUI) StartCoroutine("HitSomething");
     if(GM.Singleton.PlayerStats.CurrentPlanet != PlanetStructure.Singleton.Planets.Length -1)   DistanceTickDown();    }

    private void DeveloperCheats()
    {
        if (Debug.isDebugBuild) {

            if (Input.GetKeyDown(KeyCode.E))
            {
               // PlayerStats.PAvailableTurretSlots++;
                PlayerMultiplyersAndValues.PSpeedMultiplyer *= 2;
                ScrollingBackground.Singleton.IncreaseScrollingSpeed(ScrollingBackground.Singleton.AdditionSpeed);
                PlayerUI.Singleton.UpdateKMPerSecondText();
            
            }
            if (Input.GetKeyDown(KeyCode.F))
            {
            
                AddCredits(100000);
            
            }
            if (Input.GetKeyDown(KeyCode.G))
            {
                PlayerStats.PAvailableTurretSlots++;
                PlanetStructure.Singleton.StartNextJourney();
            }

            if (Input.GetKeyDown(KeyCode.Q))
            {
                AddCredits(1E36f);
            }

        if (Input.GetKeyDown(KeyCode.R))
        {
           
                UnityEngine.SceneManagement.SceneManager.LoadScene(0);
            
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
                // SaveManager.Singleton.LoadGame();
                DataAccess.Load();
        }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                //SaveManager.Singleton.SaveGame();
                DataAccess.Save();
            }

        }
    }

    public void RemoveTime(System.TimeSpan TimeOnLoad)
    {
        
    }

    private void DistanceTickDown()
    {
        if (PlayerStats.PIsMoving) { 
        PlayerStats.PDistanceLeft -= ((ReturnSpeedWithMultiplyer()) * Time.deltaTime);
        PlayerUI.Singleton.UpdateETAText();
        PlayerUI.Singleton.UpdateDistanceSlider();
        }
    }

    public double ReturnSpeedWithMultiplyer() { return PlayerStats.PKMSpeed * PlayerMultiplyersAndValues.PSpeedMultiplyer;  }

    public double GiveETA()
    {
        return ((PlayerStats.PDistanceLeft / ReturnSpeedWithMultiplyer()));
    }

    public void AddCredits(double AddedCredits) {PlayerStats.PCredits += (AddedCredits * PlayerMultiplyersAndValues.PMoneyMultiplyer); }

    public void RemoveCredits(double RemovedCredits){ PlayerStats.PCredits -= RemovedCredits; }

    void ClickUI()
    {
        ClickHitThing = Physics2D.Raycast(RayShot.origin, RayShot.direction);

        if(ClickHitThing.collider != null) { 
            if(ClickHitThing.collider.gameObject.tag == "Facility" || ClickHitThing.collider.gameObject.tag == "Turret") { 
                ClickHitThing.collider.SendMessage("IncreaseStats", ClickHitThing.collider.GetComponent<Facility>().PFacilityType);
                
            }
        }
    }

   
    IEnumerator PassiveIncome()
    {
        PlayerStats.PassiveCoroutineStarted = true;
        while (PlayerStats.PPassiveIncomeAvailable)
        {
            yield return new WaitForSeconds(1f);
            AddCredits(PlayerStats.PBasePassiveMoney);
            Spawner.Singleton.SpawnPassiveMoneyText(PlayerStats.PBasePassiveMoney *PlayerMultiplyersAndValues.PMoneyMultiplyer );
          }  
    }

    IEnumerator HitSomething()
    {
        LeftClickHeldDown = true;
        ClickUI();
        while (Input.GetMouseButton(0))
        {
            HitThing = Physics2D.Raycast(RayShot.origin, RayShot.direction);
            
            if(HitThing) switch (HitThing.collider.gameObject.tag)
             {
                    case "Asteroid":
                        HitThing.collider.SendMessage("Damage", PlayerMultiplyersAndValues.PDamageNumber);
                        yield return new WaitForSeconds(PlayerStats.PDamageRate);
                    break;
            }
                    yield return null;   
            }
        LeftClickHeldDown = false;
        }

    public IEnumerator SaveGameProcess()
    {
        yield return null;
      if(!Debug.isDebugBuild)  DataAccess.Load();
        while (true)
        {

            yield return new WaitForSeconds(SecondsBeforeSave);
            DataAccess.Save();
        }
    }
    }

