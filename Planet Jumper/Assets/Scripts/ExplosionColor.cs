using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ExplosionColor : MonoBehaviour
{
    [SerializeField]
    Color[] ExplosionColours;
    [SerializeField]
    Image ExplosionImage;

    private void OnEnable()
    {
        ExplosionImage.color = ExplosionColours[GM.Singleton.PlayerStats.CurrentPlanet];
    }
}
