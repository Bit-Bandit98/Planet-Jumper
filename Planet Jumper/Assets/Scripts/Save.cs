using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class Save 
{
    public GM.GameStats GameStats = new GM.GameStats();
    public GM.MultiplyersAndNumbers Multiplyers = new GM.MultiplyersAndNumbers();
    public List<Facility.FacilityStats> FacilityStats = new List<Facility.FacilityStats>();
    public int ColourPoint = 0;
    public bool PassiveIncomeRoutine = false;
    public float ScrollingSpeed = 1;
    public List<float> TurretRate = new List<float>();
    public List<bool> TurretSwitch = new List<bool>();
    public List<bool> TurretObjectState = new List<bool>();
    public List<bool> HasReachedPlanet = new List<bool>();
    public CrewMember.Character[] CrewMemberStates = new CrewMember.Character[25];
    public DateTime TimeAtSave = DateTime.Now;
   

}




