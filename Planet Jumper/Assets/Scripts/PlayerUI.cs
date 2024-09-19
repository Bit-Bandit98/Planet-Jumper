using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class PlayerUI : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI Credits, KMPerSecond, ETAText, TargetKM, PlanetReached, TargetPlanetName, PreviousPlanetName;
    public TextMeshProUGUI PPlanetReached { get { return PlanetReached; } set { PlanetReached = value; } }
    [SerializeField]
    Image TargetPlanet, PreviousPlanet;
    [SerializeField]
    Slider DistanceSlider;
    [SerializeField]
    Sprite People;
    [SerializeField]
    Image[] CrewImages;
    //[SerializeField]
    //bool[] IsShowingImage;
    [SerializeField]
    GameObject CrewMemberPanel;
    public Slider PDistanceSlider
    {
        get { return DistanceSlider; }
        set { DistanceSlider = value; }
    }
    public Image SliderFill, WhiteScreen;
    int ColourTemp = 0;
    public static PlayerUI Singleton;

    public GameObject RewardScreen;
 
    public GameObject UpgradeMenu, GameplayUI;
    
    public void UpdatePlayerUI(string PreviousPlanet, string TargetPlanet)
    {
        UpdateCreditsText();
        UpdateKMPerSecondText();
        UpdateETAText();
        UpdateDistanceSlider();
        UpdatePlanetNames(PreviousPlanet, TargetPlanet);
        UpdateTargetDistance();
        UpdatePlanetImages();
        SetCrewImages();
    }

    [SerializeField]
    Color[] ProgressionColours;

    public void SetCrewImages()
    {
        ResetIsShowingOfEachPerson();

        for (int i = 0; i < CrewImages.Length; i++)
        {
            for (int ii = 0; ii < CrewMember.Singleton.PPerson.Length; ii++)
            {
                if (CrewMember.Singleton.PPerson[ii].PUnlocked && !CrewMember.Singleton.PPerson[ii].IsBeingShown)
                {
                    CrewImages[i].sprite = CrewMember.Singleton.PersonPhoto[ii];
                    CrewImages[i].gameObject.SetActive(true);
                    CrewMember.Singleton.PPerson[ii].IsBeingShown = true;
                    break;
                }
               
                else if (ii == CrewMember.Singleton.PPerson.Length - 1) CrewImages[i].gameObject.SetActive(false);
            }


        }
    }

    private static void ResetIsShowingOfEachPerson()
    {
        for (int iii = 0; iii < CrewMember.Singleton.PPerson.Length; iii++)
        {
            CrewMember.Singleton.PPerson[iii].IsBeingShown = false;
        }
    }

    private void Awake(){Singleton = this;}

    private void Start()
    {
        UpdateSliderColour();
        UpdatePlayerUI(PlanetStructure.Singleton.Planets[GM.Singleton.PlayerStats.CurrentPlanet].PName,
           PlanetStructure.Singleton.Planets[GM.Singleton.PlayerStats.TargetPlanet].PName);  
    }

   

    public void UpdateCreditsText()
    {
        Credits.text = "<color=green>Credits: </color>" + NumberCruncher.GetCreditsUnit(GM.Singleton.PlayerStats.PCredits);
    }

    public void UpdateKMPerSecondText()
    {
        KMPerSecond.text = NumberCruncher.GetDistanceUnit(GM.Singleton.ReturnSpeedWithMultiplyer()) + "/S";
    }
    
    public void UpdateETAText()
    {
        ETAText.text = "<color=red>ETA:</color> " + NumberCruncher.GetTimeUnit(GM.Singleton.GiveETA());
    }

    public void UpdateDistanceSlider()
    {
       if(GM.Singleton.PlayerStats.CurrentPlanet != PlanetStructure.Singleton.Planets.Length - 1) { 
            DistanceSlider.value = 1-(float)(GM.Singleton.PlayerStats.PDistanceLeft / PlanetStructure.Singleton.Planets[GM.Singleton.PlayerStats.TargetPlanet].PTargetDistance);
        } else
        {
            DistanceSlider.value = 1;
        }
    }

    public void UpdatePlanetNames(string PreviousPlanet, string TargetPlanet)
    {
        if(PreviousPlanet != null) {
            PreviousPlanetName.text = PreviousPlanet;
        } else
        {
            PreviousPlanetName.text = "Start";
        }
        
        TargetPlanetName.text = TargetPlanet;
    }

    public void UpdateTargetDistance()
    {
       TargetKM.text = NumberCruncher.GetDistanceUnit(PlanetStructure.Singleton.Planets[GM.Singleton.PlayerStats.TargetPlanet].PTargetDistance).ToString();
    }
    
    public void UpdateSliderColour()
    {
        SliderFill.color = ProgressionColours[ColourTemp];
        ColourTemp++;
    }

    public void UpdatePlanetImages()
    {
        if (PreviousPlanet.gameObject.activeInHierarchy == false) PreviousPlanet.gameObject.SetActive(true);
        TargetPlanet.sprite = PlanetStructure.Singleton.Planets[GM.Singleton.PlayerStats.TargetPlanet].PlanetIcon;
        if (GM.Singleton.PlayerStats.TargetPlanet == 0) {
            PreviousPlanet.sprite = null ;
            PreviousPlanet.gameObject.SetActive(false);
        }
        else PreviousPlanet.sprite = PlanetStructure.Singleton.Planets[GM.Singleton.PlayerStats.TargetPlanet - 1].PlanetIcon;
    }

    public void DisplayPlanetReached()
    {
        PlanetReached.text = "Welcome to  " + "<color=green>"+PlanetStructure.Singleton.Planets[GM.Singleton.PlayerStats.TargetPlanet].PName +"</color>";
        PlanetReached.gameObject.SetActive(true);
       // yield return new WaitForSeconds(4f);
        //PlanetReached.gameObject.SetActive(false);
    }

    public void DisplayUpgradeScreen(bool State)
    {
        UpgradeMenu.SetActive(State);
        GameplayUI.SetActive(!State);
       
        GM.Singleton.PPlayerIsInUI = State;
    }

   

}
