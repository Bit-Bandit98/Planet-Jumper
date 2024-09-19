using System.Collections;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;
using System.Linq;
public class SaveManager : MonoBehaviour
{
        public static SaveManager Singleton;
        private void Awake()
        {
            Singleton = this;
        }

    private void Start()
    {
       // LoadGame();
    }
    public void SaveGame()
        {
            Save SaveGame = CreateSaveData();

            BinaryFormatter BF = new BinaryFormatter();
            FileStream FileS = File.Create(Application.persistentDataPath + "/gamesave.save");
            BF.Serialize(FileS, SaveGame);
            FileS.Close();

            Debug.Log("Game Saved to "+Application.persistentDataPath);
        }

        public void LoadGame()
        {
            if (File.Exists(Application.persistentDataPath + "/gamesave.save"))
            {
                BinaryFormatter BF = new BinaryFormatter();
                FileStream Files = File.Open(Application.persistentDataPath + "/gamesave.save", FileMode.Open);
           
                Save SaveGame = (Save)BF.Deserialize(Files);
                Files.Close();
                GM.Singleton.PlayerStats = SaveGame.GameStats;
                GM.Singleton.PlayerMultiplyersAndValues = SaveGame.Multiplyers;
                ScrollingBackground.Singleton.SetScrollSpeed(SaveGame.ScrollingSpeed);
                Spawner.Singleton.PColourPoint= SaveGame.ColourPoint;

                for(int i=0; i <PlanetStructure.Singleton.Planets.Length; i++)
            {
                PlanetStructure.Singleton.Planets[i].PHasReached = SaveGame.HasReachedPlanet[i];
            }
            
            Facility[] GameCache = Resources.FindObjectsOfTypeAll<Facility>();
           

            int TurretI = 0;
            for (int i = 0; i < GameCache.Length; i++)
                {
                GameCache[i].GetComponent<Facility>().Stats = SaveGame.FacilityStats[i];
                SaveGame.FacilityStats.Add(GameCache[i].GetComponent<Facility>().Stats);
                Turret TurretCache = GameCache[i].GetComponent<Turret>();
                
                if (TurretCache != null)
                {
                    TurretCache.gameObject.SetActive(SaveGame.TurretObjectState[TurretI]);
                    TurretCache.PFireRate = SaveGame.TurretRate[TurretI];
                    TurretCache.PSwitch = SaveGame.TurretSwitch[TurretI];
                    TurretI++;
                   
                }
                GameCache[i].GetComponentInChildren<FacilityUI>().UpdateFacilityUI();
            }
           
            PlayerUI.Singleton.UpdatePlayerUI(PlanetStructure.Singleton.Planets[GM.Singleton.PlayerStats.CurrentPlanet].PName,PlanetStructure.Singleton.Planets[GM.Singleton.PlayerStats.TargetPlanet].PName);
            Debug.Log("Game Loaded from " +Application.persistentDataPath);
                
            }
            else
            {
                Debug.Log("Load Failed");
            }
        }

        public Save CreateSaveData()
        {
            Save SaveGame = new Save();

            SaveGame.GameStats = GM.Singleton.PlayerStats;
            SaveGame.Multiplyers = GM.Singleton.PlayerMultiplyersAndValues;
            SaveGame.ColourPoint = Spawner.Singleton.PColourPoint;
             SaveGame.CrewMemberStates = CrewMember.Singleton.PPerson;
           // SaveGame.SavedPlanets = PlanetStructure.Singleton.Planets.ToList();
            SaveGame.ScrollingSpeed = ScrollingBackground.Singleton.ReturnScrollingSpeed();
        //  GameObject[] GameCache = GameObject.FindGameObjectsWithTag("Facility");

        for (int i = 0; i < PlanetStructure.Singleton.Planets.Length; i++)
        {
            SaveGame.HasReachedPlanet.Add(PlanetStructure.Singleton.Planets[i].PHasReached);
        }

        Facility[] GameCache = Resources.FindObjectsOfTypeAll<Facility>();

        SaveGame.TimeAtSave = DateTime.Now;
        int TurretI = 0;
        for (int i = 0; i < GameCache.Length; i++)
        {
            SaveGame.FacilityStats.Add(GameCache[i].GetComponent<Facility>().Stats);
            Turret TurretCache = GameCache[i].GetComponent<Turret>();

            if (TurretCache != null)
            {
                SaveGame.TurretObjectState.Add(TurretCache.gameObject.activeInHierarchy);
                SaveGame.TurretSwitch.Add(TurretCache.PSwitch);
                SaveGame.TurretRate.Add(TurretCache.PFireRate);

            }


            SaveGame.TimeAtSave = DateTime.Now;
        }


        return SaveGame;

        }
    }
