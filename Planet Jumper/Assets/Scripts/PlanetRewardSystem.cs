using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlanetRewardSystem : MonoBehaviour
{

    public static PlanetRewardSystem Singleton;
    [SerializeField]
    GameObject[] SelectionBox;
    [SerializeField]
    Image[] BoxImages;
    [SerializeField]
    TextMeshProUGUI[] Names;
    [SerializeField]
    TextMeshProUGUI[] Descriptions;

     [SerializeField]
    Reward[] RewardBoxes;

    Reward TempReward;
    CrewMember.Character TempChar;
    int tempNum;

    [System.Serializable]
    public class Reward
    {
      public Sprite RewardIcon;
      public string Name;
      public string Description;
      public int Placement;
      
      public enum RewardType { AddTurret, NewCrewMember };

      public RewardType TypeOfReward;
       
    public Reward(Sprite Picture, string RewardName, string RewardDescription, int Place )
        {
            RewardIcon = Picture;
            Name = RewardName;
            Description = RewardDescription;
        }
    }

    private void Awake()
    {
        Singleton = this;
       
    }
   

    private void OnEnable()
    {
        DisplayWindow();
    }

    public void DisplayWindow()
    {
        
        for (int i = 0; i < CrewMember.Singleton.PPerson.Length; i++)
        {
            CrewMember.Singleton.PPerson[i].PHasChosen = false;
        }

        RandomizeBoxes();
    }

    Reward GetRandomReward()
    {
        TempChar = null;
        tempNum = 0;
        //THIS CAUSES A CRASH!!!
        while (TempChar == null) {

        tempNum = Random.Range(0, CrewMember.Singleton.PPerson.Length - 1);
     
        TempChar = CrewMember.Singleton.PPerson[tempNum];
            if (TempChar.PUnlocked || TempChar.PHasChosen) TempChar = null;                      
        }
        CrewMember.Singleton.PPerson[tempNum].PHasChosen = true;
//        print(CrewMember.Singleton.PPerson[tempNum].PHasChosen);
        Reward NewReward = new Reward(CrewMember.Singleton.PersonPhoto[tempNum], TempChar.PName, TempChar.PDescription, tempNum);
        return NewReward;
    }

    void RandomizeBoxes()
    {
        for(int i = 0; i < SelectionBox.Length; i++)
        {
            TempReward = GetRandomReward();
            BoxImages[i].sprite = TempReward.RewardIcon;
            Names[i].text = TempReward.Name;
            Descriptions[i].text = TempReward.Description;
            TempReward.TypeOfReward = Reward.RewardType.NewCrewMember;
            TempReward.Placement = tempNum;
            RewardBoxes[i] =  TempReward;


        }
    }

    public void RewardPlayer(int SelectionBox)
    {

        if (RewardBoxes[SelectionBox].TypeOfReward == Reward.RewardType.NewCrewMember)
        {
            CrewMember.Singleton.AddCrewMember(RewardBoxes[SelectionBox].Placement);
        }
        else if (RewardBoxes[SelectionBox].TypeOfReward == Reward.RewardType.AddTurret)
        {
            GM.Singleton.PlayerStats.PAvailableTurretSlots++;
        }
        gameObject.SetActive(false);
        PlanetStructure.Singleton.StartNextJourney();
    }




}
