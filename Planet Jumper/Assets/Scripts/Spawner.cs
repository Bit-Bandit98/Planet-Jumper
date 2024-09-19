using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
   
    public static Spawner Singleton;
    Vector3 ZeroZ = new Vector3(1, 1, 0);
    [SerializeField]
    GameObject Anchor;

    int ColourPoint;

    public int PColourPoint
    {
        get { return ColourPoint; }
        set {  ColourPoint = value;}
    }
    public Sprite[] AsteroidSprites;

    private void Awake()
    {
        ColourPoint = 0;
        Singleton = this;
    }

    private void Start()
    {
        StartCoroutine("SpawnAsteroid");
     
    }


    public IEnumerator SpawnAsteroid()
    {
        while (true)
        {
          
            
            GameObject Asteroid = ObjectPooler.Singleton.GetPooledObject("Asteroid");
            if (Asteroid != null && GM.Singleton.PlayerStats.PIsMoving)
            {
             //   Asteroid.GetComponent<Asteroid>().PAsteroidModel.GetComponent<Renderer>().material.SetColor("_Color", Colours[PlanetStructure.Singleton.CurrentPlanet + 1]);
                Asteroid.transform.position = new Vector3(Random.Range(-2f, 2f), 6f, -2f );
                Asteroid.transform.rotation = Quaternion.identity;
                Asteroid.SetActive(true);
            }
            yield return new WaitForSeconds(Random.Range(0f, 2f));
        }
    }

    public void SpawnDamageText(int DamageValue, Vector3 Position)
    {
        
        GameObject DamageText = ObjectPooler.Singleton.GetPooledObject("DamageText");
        
        if (DamageText != null)
        {
            DamageText.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = DamageValue.ToString();
            // DamageText.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + Vector3.up/2;
            DamageText.transform.position = Position + Vector3.up / 2;
            DamageText.transform.position = Vector3.Scale(DamageText.transform.position, ZeroZ);
            DamageText.SetActive(true);

        }
    }

    public void SpawnPassiveMoneyText(double Money)
    {
        GameObject MoneyText = ObjectPooler.Singleton.GetPooledObject("PassiveMoneyText");
        if(MoneyText != null)
        {
            MoneyText.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = "$" + NumberCruncher.GetCreditsUnit(Money);
            MoneyText.transform.position = Anchor.transform.position;
            MoneyText.SetActive(true);

        }
    }

    public void SpawnMoneyText(double Money, Vector3 SpawnPos)
    {

        GameObject MoneyText = ObjectPooler.Singleton.GetPooledObject("MoneyText");

        if (MoneyText != null)
        {
            SoundManager.Singleton.PlayMoneySound();
            MoneyText.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = "$" + NumberCruncher.GetCreditsUnit(Money);
            SetSpawnedObject(MoneyText, SpawnPos);

        }
    }

    public void SpawnExplosion(Vector3 SpawnPos)
    {

        GameObject ExplosionObject = ObjectPooler.Singleton.GetPooledObject("Explosion");

        if (ExplosionObject != null)
        {
          
            SetSpawnedObject(ExplosionObject, SpawnPos);

        }
    }

    public void SpawnMoneyLossText(double Money)
    {
        GameObject MoneyText = ObjectPooler.Singleton.GetPooledObject("MoneyLossText");

        if(MoneyText != null)
        {
            MoneyText.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = "-$" + NumberCruncher.GetCreditsUnit(Money);
            MoneyText.transform.position = new Vector3(0, 4.12f, 0);
            MoneyText.SetActive(true);
        }
    }

    public void SpawnLevelUpText(string Skill, float Increase, Vector3 SpawnPos)
    {
        GameObject LevelUpText = ObjectPooler.Singleton.GetPooledObject("LevelUpText");
        if (LevelUpText != null)
        {
            if(Increase != 0)
            {
                LevelUpText.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = Skill + Increase + "!";
            }
            else
            {
                LevelUpText.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = Skill + "!";
            }
          
            LevelUpText.transform.position = SpawnPos;
            LevelUpText.SetActive(true);
        }
    }

    private void SetSpawnedObject(GameObject SpawnedObject, Vector3 Position)
    {
        SpawnedObject.transform.position = Position;
        SpawnedObject.transform.position = (SpawnedObject.transform.position);
        SpawnedObject.SetActive(true);
    }
}
