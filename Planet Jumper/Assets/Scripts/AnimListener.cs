using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimListener : MonoBehaviour
{
    [SerializeField]
    GameObject Parent, VictoryText;
    bool GameWon;
   public void DisableText (){
        Parent.SetActive(false);
    }

    public void OpenRewardWindow()
    {
        PlayerUI.Singleton.RewardScreen.SetActive(!GameWon); 
    }

    public void IsFinalPlanet()
    {
        if (PlanetStructure.Singleton.Planets[PlanetStructure.Singleton.Planets.Length - 1].PHasReached)
        {
            GameWon = true;
            VictoryText.SetActive(GameWon);
            if (gameObject == VictoryText) VictoryText.GetComponent<Animator>().speed = 0;
        }
        else
        {
            GameWon = false;

        }
                
    }
}
