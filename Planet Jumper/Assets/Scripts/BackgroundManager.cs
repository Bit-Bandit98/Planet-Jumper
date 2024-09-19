using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class BackgroundManager : MonoBehaviour
{
  
   
   
    public static BackgroundManager Singleton;

    [SerializeField]
    Texture2D[] Backgrounds;
   
    [SerializeField]
    GameObject[] PlanetImages;
    [SerializeField]
    SpriteRenderer[] SRender;
    [SerializeField]
    SpriteRenderer[] PlanetGlows;
    [SerializeField]
    Transform[] PlanetAnchors;
    [SerializeField]
    Material BackgroundMaterial;
    [SerializeField]
    Color[] FlashLerp;


    private void Awake()
    {
        Singleton = this;
                
    
    }

    private void Start()
    {
        
        SetPlanetImages();
     // ChangeBackgroundMat();
    }

    public IEnumerator ChangeBackgroundMat()
    {

        float t = 0;
        while(t < 1)
        {
            PlayerUI.Singleton.WhiteScreen.color = Color.Lerp(FlashLerp[0], FlashLerp[1], t);
            t += Time.deltaTime;
            yield return null;
        }
        BackgroundMaterial.SetTexture("_Emission", Backgrounds[GM.Singleton.PlayerStats.PCurrentSolarSystem]);
        BackgroundMaterial.SetTexture("_MainTex", Backgrounds[GM.Singleton.PlayerStats.PCurrentSolarSystem]);
        while (t > 0)
        {
            PlayerUI.Singleton.WhiteScreen.color = Color.Lerp(FlashLerp[0], FlashLerp[1], t);
            t -= Time.deltaTime;
            yield return null;
        }

    }


   

    private void Update()
    {
      
      InterpolatePlanets();
    }

    public void SetPlanetImages()
    {



        SRender[1].sprite = PlanetStructure.Singleton.Planets[GM.Singleton.PlayerStats.CurrentPlanet].PlanetIcon;
        PlanetGlows[1].sprite = PlanetStructure.Singleton.Planets[GM.Singleton.PlayerStats.CurrentPlanet].PlanetGlow;
        SRender[0].sprite = PlanetStructure.Singleton.Planets[GM.Singleton.PlayerStats.TargetPlanet].PlanetIcon;
        PlanetGlows[0].sprite = PlanetStructure.Singleton.Planets[GM.Singleton.PlayerStats.TargetPlanet].PlanetGlow;
    }

    public void InterpolatePlanets()
    {
        PlanetImages[0].transform.position = Vector3.Lerp(PlanetAnchors[0].position, PlanetAnchors[1].position, PlayerUI.Singleton.PDistanceSlider.value);
       
        PlanetImages[1].transform.position = Vector3.Lerp(PlanetAnchors[1].position, PlanetAnchors[2].position, PlayerUI.Singleton.PDistanceSlider.value);
    }

  
}
