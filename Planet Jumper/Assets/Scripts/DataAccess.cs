using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class DataAccess
{
    [DllImport("__Internal")]
    private static extern void SyncFiles();

    [DllImport("__Internal")]
    private static extern void WindowAlert(string message);

    public static void Save()
    {
        Save SaveGame = SaveManager.Singleton.CreateSaveData();
        string dataPath = string.Format("{0}/GameDetails.dat", Application.persistentDataPath);
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        FileStream fileStream;
        ;
        try
        {
            if (File.Exists(dataPath))
            {
                File.WriteAllText(dataPath, string.Empty);
                fileStream = File.Open(dataPath, FileMode.Open);
            }
            else
            {
                fileStream = File.Create(dataPath);
            }

            binaryFormatter.Serialize(fileStream, SaveGame);
            fileStream.Close();

            if (Application.platform == RuntimePlatform.WebGLPlayer)
            {
                SyncFiles();
            }
            //            Debug.Log("Ran");
            Debug.Log("Game Saved to " + Application.persistentDataPath);
        }
        catch (Exception e)
        {
            PlatformSafeMessage("Failed to Save: " + e.Message);
        }
    }


    public static Save Load()
    {
        Save SaveGame = null;
        string dataPath = string.Format("{0}/GameDetails.dat", Application.persistentDataPath);

        try
        {
            if (File.Exists(dataPath))
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                FileStream fileStream = File.Open(dataPath, FileMode.Open);
                
                SaveGame = (Save)binaryFormatter.Deserialize(fileStream);
                
                fileStream.Close();
               
                GM.Singleton.PlayerStats = SaveGame.GameStats;
                GM.Singleton.PlayerMultiplyersAndValues = SaveGame.Multiplyers;
                //Debug.Log("HI");
                ScrollingBackground.Singleton.SetScrollSpeed(SaveGame.ScrollingSpeed);
                Spawner.Singleton.PColourPoint = SaveGame.ColourPoint;
                CrewMember.Singleton.PPerson = SaveGame.CrewMemberStates;
                
                for (int i = 0; i < PlanetStructure.Singleton.Planets.Length; i++)
                {
                    PlanetStructure.Singleton.Planets[i].PHasReached = SaveGame.HasReachedPlanet[i];
                }
                
                Facility[] GameCache = Resources.FindObjectsOfTypeAll<Facility>();

                //potential error below
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


              //      GameCache[i].GetComponentInChildren<FacilityUI>().UpdateFacilityUI();

                }
//                Debug.Log("HI");
                PlayerUI.Singleton.UpdatePlayerUI(PlanetStructure.Singleton.Planets[GM.Singleton.PlayerStats.CurrentPlanet].PName, PlanetStructure.Singleton.Planets[GM.Singleton.PlayerStats.TargetPlanet].PName);
                BackgroundManager.Singleton.SetPlanetImages();
                if (!GM.Singleton.PlayerStats.PIsMoving)
                {
                    if (PlayerUI.Singleton.RewardScreen.activeInHierarchy)
                    {
                        PlayerUI.Singleton.RewardScreen.SetActive(false);
                        PlayerUI.Singleton.RewardScreen.SetActive(true);
                    } else
                        PlayerUI.Singleton.RewardScreen.SetActive(true);
                } else
                {
                    PlayerUI.Singleton.RewardScreen.SetActive(false);
                }
                BackgroundManager.Singleton.StartCoroutine("ChangeBackgroundMat");
                ShipModelChange.Singleton.ChangeShipModel(GM.Singleton.PlayerStats.PShipModeStage);
                Debug.Log("Game Loaded from " + Application.persistentDataPath);

                TimeSpan SpanSinceSave = DateTime.Now.Subtract(SaveGame.TimeAtSave);
               // GM.Singleton.RemoveTime(SpanSinceSave);
                //Debug.Log(SpanSinceSave.TotalSeconds);
                Debug.Log(GM.Singleton.PlayerStats.PDistanceLeft);
              GM.Singleton.PlayerStats.PDistanceLeft -= SpanSinceSave.TotalSeconds * GM.Singleton.ReturnSpeedWithMultiplyer();
                if (GM.Singleton.PlayerStats.PPassiveIncomeAvailable)
                {
                    if (SpanSinceSave.TotalHours > GM.Singleton.PlayerStats.PPassiveMoneyHoursLimit)
                    {
                        GM.Singleton.AddCredits(GM.Singleton.PlayerStats.PBasePassiveMoney * (GM.Singleton.PlayerStats.PPassiveMoneyHoursLimit * (60 * 60)));
                        Spawner.Singleton.SpawnPassiveMoneyText(GM.Singleton.PlayerStats.PBasePassiveMoney * (GM.Singleton.PlayerStats.PPassiveMoneyHoursLimit * (60 * 60)));
                    }
                    else
                    {
                        GM.Singleton.AddCredits(GM.Singleton.PlayerStats.PBasePassiveMoney * (Math.Round(SpanSinceSave.TotalSeconds)));
                        Spawner.Singleton.SpawnPassiveMoneyText(GM.Singleton.PlayerStats.PBasePassiveMoney * (Math.Round(SpanSinceSave.TotalSeconds)));
                    }
                    }

                GameObject[] Facilties = GameObject.FindGameObjectsWithTag("Facility");
                for(int i = 0; i < Facilties.Length; i++)
                {
                    Facilties[i].GetComponent<Facility>().UpdateFacilityUI();
                }

            }
        }
        catch (Exception e)
        {
            PlatformSafeMessage("Failed to Load: " + e.Message);
        }

        return SaveGame;
    }

    private static void PlatformSafeMessage(string message)
    {
        if (Application.platform == RuntimePlatform.WebGLPlayer)
        {
            WindowAlert(message);
        }
        else
        {
            Debug.Log(message);
        }
    }

   
   
}
