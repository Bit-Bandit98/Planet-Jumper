using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AsteroidUI : MonoBehaviour
{   
    public Slider AsteroidHealth;
    [SerializeField]
    Image FillBar;
    [SerializeField]
    Asteroid ConnectedAsteroid;


    public void ShowHealthBar(bool Set)
    {
      AsteroidHealth.gameObject.SetActive(Set);
    }

    public void UpdateAsteroidHealth()
    {
        AsteroidHealth.value = (float)ConnectedAsteroid.AsteroidStats.Health / (float)ConnectedAsteroid.AsteroidStats.MaxHealth;
        if(AsteroidHealth.value > 0.66f)
        {
            FillBar.color = Color.green;
        } else if(AsteroidHealth.value > 0.33f && AsteroidHealth.value < 0.66f)
        {
            FillBar.color = Color.yellow;
        } else
        {
            FillBar.color = Color.red;
        }
    }



}
