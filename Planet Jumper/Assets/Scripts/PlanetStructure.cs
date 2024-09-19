using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlanetStructure : MonoBehaviour
{
  //  public int TargetPlanet, CurrentPlanet;
    //[SerializeField]
    public static PlanetStructure Singleton;
    [SerializeField]
    public Planet[] Planets;

    [System.Serializable]
    public class Planet
    {
        [SerializeField]
        string Name;
        public string PName { get { return Name; } }
        //[SerializeField]
        //int PossibleAsteroids;
        [SerializeField]
        double TargetDistance;
        public double PTargetDistance { get { return TargetDistance; } set { TargetDistance = value; } }
        public Sprite PlanetIcon;
        public Sprite PlanetGlow;
        bool HasReached;
        public bool PHasReached
        {
            get
            {
                return HasReached;
            }
            set
            {
                HasReached = value;
            }
        }
    }

    public void SetDistances()
    {
        for(int i = 1; i < Planets.Length; i++)
        {
            Planets[i].PTargetDistance = 6000*(NumberCruncher.ExpenentialEquation(i, 8));
        }
        

        
    }

    private void Awake()
    {
        Singleton = this;
        InitialiseTargetAndCurrentPlanet();
        SetDistances();

    }

    private void InitialiseTargetAndCurrentPlanet()
    {
        GM.Singleton.PlayerStats.TargetPlanet = 1;
        GM.Singleton.PlayerStats.CurrentPlanet = 0;
    }

    private void Start()
    {
        GM.Singleton.PlayerStats.PDistanceLeft = Planets[GM.Singleton.PlayerStats.TargetPlanet].PTargetDistance;
        PlayerUI.Singleton.UpdatePlanetImages();
        PlayerUI.Singleton.UpdatePlanetNames("Earth", Planets[GM.Singleton.PlayerStats.CurrentPlanet + 1].PName);
    }
    public void PlanetReached()
    {
        HasReached();
        
    }

    public void StartNextJourney()
    {
        if (GM.Singleton.PlayerStats.PAvailableTurretSlots <= 4)
        {
            GM.Singleton.PlayerStats.PAvailableTurretSlots++;
           // GM.Singleton.PlayerStats.FirstTurretGot = true;
        }
        GM.Singleton.PlayerStats.PIsMoving = true;
        PlayerUI.Singleton.PPlanetReached.gameObject.SetActive(false);
        IncrementTargetAndCurrentPlanet();
        //BackgroundManager.Singleton.StartCoroutine("ChangeBackgroundMat");
        Spawner.Singleton.PColourPoint += 1;
        if (GM.Singleton.PlayerStats.TargetPlanet % 5 == 0)
        {            
            GM.Singleton.PlayerStats.PCurrentSolarSystem++;
        }
        if (GM.Singleton.PlayerStats.TargetPlanet != Planets.Length)
        {
            UpdateOverallDistanceUI();
            BackgroundManager.Singleton.SetPlanetImages();
        }
    }

    private void UpdateOverallDistanceUI()
    {
        PlayerUI.Singleton.UpdatePlanetNames(Planets[GM.Singleton.PlayerStats.CurrentPlanet].PName, Planets[GM.Singleton.PlayerStats.CurrentPlanet + 1].PName);
        GM.Singleton.PlayerStats.PDistanceLeft = Planets[GM.Singleton.PlayerStats.TargetPlanet].PTargetDistance;
        PlayerUI.Singleton.UpdateTargetDistance();
        PlayerUI.Singleton.UpdatePlanetImages();
    }

    private void HasReached()
    {
        Planets[GM.Singleton.PlayerStats.TargetPlanet].PHasReached = true;
        // PlayerUI.Singleton.RewardScreen.SetActive(true);

        SoundManager.Singleton.PlayPlanetReachedSound();
        PlayerUI.Singleton.DisplayPlanetReached();  


        SoundManager.Singleton.PlayPlanetReachedSound();
    }

    private void IncrementTargetAndCurrentPlanet()
    {
        GM.Singleton.PlayerStats.TargetPlanet++;
        GM.Singleton.PlayerStats.CurrentPlanet++;
    }


}
