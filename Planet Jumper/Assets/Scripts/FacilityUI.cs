using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FacilityUI : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI LevelText, CostText, MainframeLevelReqText, DescriptionText;
    [SerializeField, TextArea]
    string Description;
    [SerializeField]
    Facility ConnectedFacility;


    
    void Start()
    {
        UpdateLevelText();
        UpdateCostText();

       if(!ConnectedFacility.IsMainframe) UpdateMainFrameRequirement();
    }

  public void UpdateFacilityUI()
    {
        UpdateLevelText();
        UpdateCostText();
        if (!ConnectedFacility.IsMainframe) UpdateMainFrameRequirement();
    }


    public void UpdateDescription(Facility.Facilitys FacilityThing)
    {
        switch (FacilityThing)
        {
            case Facility.Facilitys.Damage:
                {
                    DescriptionText.text = Description + " (<color=orange> +" +GM.Singleton.PlayerMultiplyersAndValues.PDamageNumber.ToString("0.00") + " Damage Per Hit </color=orange>)";
                    break;
                }
            case Facility.Facilitys.Mainframe:
                {
                    DescriptionText.text = Description + " (<color=orange>" + (GM.Singleton.PlayerStats.MainFrameLevel) + "</color=orange>)";
                    break;
                }
            case Facility.Facilitys.MoneyMaker:
                {
                    DescriptionText.text = Description + " (<color=orange>X" + GM.Singleton.PlayerMultiplyersAndValues.PMoneyMultiplyer.ToString("0.00") + "</color=orange>)";
                    break;
                }
            case Facility.Facilitys.Speed:
                {
                    DescriptionText.text = Description + " (<color=orange>X" + GM.Singleton.PlayerMultiplyersAndValues.PSpeedMultiplyer.ToString("0.00") + "</color=orange>)";
                    break;
                }

            case Facility.Facilitys.AutoMine:
                {
                    DescriptionText.text = Description + " (<color=orange>$" + GM.Singleton.PlayerStats.PBasePassiveMoney + " Per Sec, " + GM.Singleton.PlayerStats.PPassiveMoneyHoursLimit.ToString("0.00") + " Hour Limit.</color=orange>)";
                    break;
                }
        }
    }
   public void UpdateLevelText()
    {
        LevelText.text = "Lv " +(ConnectedFacility.Stats.PCurrentLevel).ToString();
    }

    public void UpdateCostText()
    {
        if(ConnectedFacility.Stats.PCurrentLevel != ConnectedFacility.Stats.PMaxLevel ) { 
        CostText.text = "Cost $" +(NumberCruncher.GetCreditsUnit(ConnectedFacility.Stats.PNextLevelCost));
        } else
        {
            CostText.text = "MAX LEVEL";
        }
    }

    public void UpdateMainFrameRequirement()
    {
        if (!ConnectedFacility.IsMainframe || !ConnectedFacility.IsTurret) { 
            if (ConnectedFacility.Stats.PCurrentLevel != ConnectedFacility.Stats.PMaxLevel +1 && !ConnectedFacility.IsMainframe && !ConnectedFacility.IsTurret) { 
            MainframeLevelReqText.text = "<color=red> M Lv Rq </color=red>"+ConnectedFacility.Stats.PMainFrameLevelRequirement.ToString();
            } else
                {
                   if(MainframeLevelReqText != null) MainframeLevelReqText.text = "";
                }
        }
    }


}
