using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrewMember : MonoBehaviour
{
    [SerializeField]
    Character[] Person;
    public Character[] PPerson
    {
        get { return Person; }
        set { Person = value; }
    }
    public static CrewMember Singleton;
    public Sprite[] PersonPhoto;

    [System.Serializable]
    public class Character
    {
        [SerializeField]
        string Name;
        public string PName { get { return Name; } set { Name = value;} }
        [SerializeField, TextArea]
        string Description;
        public string PDescription { get { return Description; } set { Description = value; } }
        [SerializeField]
        float IncreaseValue;
        public float PIncreaseValue { get { return IncreaseValue; } }

        [SerializeField]
        bool Unlocked;
        public bool PUnlocked
        {
            get { return Unlocked; }
            set { Unlocked = value; }
        }
        bool HasChosen;
        public bool IsBeingShown;
        public bool PHasChosen { get { return HasChosen; } set { HasChosen = value; } }
        
       public enum IncreaseAbility { Damage, Value, MoneyMultiplyer, Speed, BasePassiveIncome, AddTurret};
       
        public IncreaseAbility Ability;
        
       

        public Character()
        {
            Name = "John Smith";
            Description = "Increases money Multiplyer by X5";
         
            Unlocked = false;
            Ability = IncreaseAbility.MoneyMultiplyer;
        }

    }

    private void Awake()
    {
        Singleton = this;
        
       
    }

 
    public void AddStats(int Num)
    {
        switch (PPerson[Num].Ability)
        {
            case Character.IncreaseAbility.Damage:
                GM.Singleton.PlayerMultiplyersAndValues.PDamageNumber *= PPerson[Num].PIncreaseValue;
                break;
        
            case Character.IncreaseAbility.Value:
                GM.Singleton.PlayerMultiplyersAndValues.PValueMultiplyer *= PPerson[Num].PIncreaseValue;
                break;
            case Character.IncreaseAbility.MoneyMultiplyer:
                GM.Singleton.PlayerMultiplyersAndValues.PMoneyMultiplyer *= PPerson[Num].PIncreaseValue;
                break;
            case Character.IncreaseAbility.Speed:
                GM.Singleton.PlayerMultiplyersAndValues.PSpeedMultiplyer *= PPerson[Num].PIncreaseValue;
                break;
            case Character.IncreaseAbility.BasePassiveIncome:
                GM.Singleton.PlayerStats.PBasePassiveMoney *= PPerson[Num].PIncreaseValue;
                break;

            case Character.IncreaseAbility.AddTurret:
                GM.Singleton.PlayerStats.PAvailableTurretSlots++;
                break;

        }
    }

    public void AddCrewMember(int Num)
    {
        PPerson[Num].PUnlocked = true;
        PlayerUI.Singleton.SetCrewImages();
        AddStats(Num);
        //TempChar.PUnlocked = true;

        // return TempChar;
    }

}
