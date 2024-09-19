using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField]
    public Stats AsteroidStats;
    [SerializeField]
    AsteroidUI ConnectedUI;
    [SerializeField]
    Rigidbody2D AsteroidBody;

    [SerializeField]
    GameObject AsteroidModel;
    public GameObject PAsteroidModel { get { return AsteroidModel; } }

    Vector2 Speed = new Vector2(0, 0);
    Vector3 RotationEulers = new Vector3(0, 0, 0);

    [SerializeField]  
    float temp;

    [System.Serializable]
    public class Stats
    {
        public int Health, MaxHealth;
        public double Value;
        public float MinSpeed, MaxSpeed, MinTorque, MaxTorque, TempTorque;
        public int AsteroidLevel;
        
    }

    private void Start()
    {
        SetStats();
        ChangeSpriteOnLevel();
       
    }

    private void Update()
    {
        RotateAsteroid();
    }

    private void RotateAsteroid()
    {
        
        RotationEulers.z = AsteroidStats.TempTorque;
        AsteroidModel.transform.Rotate((RotationEulers * temp) * Time.deltaTime);
    }

    private static float RandomSign()
    {
        
        float temp = Random.Range(0f, 1f);
        if (Mathf.Round(temp) >= 1)
        {
            temp = 1;
        }
        else
        {
            temp = -1;
        }

        return temp;
    }

    private void OnEnable()
    {
        SetStats();
        SetRotationAndSpeed();
      
        ConnectedUI.UpdateAsteroidHealth();
        ConnectedUI.ShowHealthBar(false);

    }

    public void SetStats()
    {
       AsteroidStats.MaxHealth = 30 + (int)(NumberCruncher.ExpenentialEquation(GM.Singleton.PlayerStats.CurrentPlanet,3));
       AsteroidStats.Value = 150 -1  + (NumberCruncher.ExpenentialEquation(GM.Singleton.PlayerStats.CurrentPlanet + 1, 5) ) * GM.Singleton.PlayerMultiplyersAndValues.PValueMultiplyer;

       if (Spawner.Singleton.PColourPoint >= Spawner.Singleton.AsteroidSprites.Length){ Spawner.Singleton.PColourPoint -= Spawner.Singleton.AsteroidSprites.Length; }

        ChangeSpriteOnLevel();
        AsteroidStats.Health = AsteroidStats.MaxHealth;
    }

    private void ChangeSpriteOnLevel()
    {
        if (AsteroidStats.AsteroidLevel != Spawner.Singleton.PColourPoint)
        {
            AsteroidStats.AsteroidLevel = Spawner.Singleton.PColourPoint;
            PAsteroidModel.GetComponent<SpriteRenderer>().sprite = ((AsteroidStats.AsteroidLevel >= 0) ? Spawner.Singleton.AsteroidSprites[AsteroidStats.AsteroidLevel] : Spawner.Singleton.AsteroidSprites[AsteroidStats.AsteroidLevel + 1] );

            
           

        }
    }

    private void SetRotationAndSpeed()
    {
        temp = RandomSign();
        AsteroidStats.TempTorque = Random.Range(AsteroidStats.MinTorque, AsteroidStats.MaxTorque);
        SetAsteroidVelocity();
    }

    private void SetAsteroidVelocity()
    {
        Speed.y = -Random.Range(AsteroidStats.MinSpeed, AsteroidStats.MaxSpeed);
        AsteroidBody.AddForce(Speed);
    }

    

    void Damage(int DamageValue)
    {
        AsteroidStats.Health -= DamageValue;
        ConnectedUI.UpdateAsteroidHealth();
        SoundManager.Singleton.ASounds.PlayHitSound();
        Spawner.Singleton.SpawnDamageText(DamageValue, gameObject.transform.position);
        ConnectedUI.ShowHealthBar(true);
        

        if(AsteroidStats.Health <= 0) {Die(true);}
    }


    void Die(bool ByPlayer)
    {
        gameObject.SetActive(false);

        if (ByPlayer)
        {
            SoundManager.Singleton.ASounds.PlayDestroySound();
            GM.Singleton.AddCredits(AsteroidStats.Value);
            Spawner.Singleton.SpawnMoneyText(AsteroidStats.Value * GM.Singleton.PlayerMultiplyersAndValues.PMoneyMultiplyer, gameObject.transform.position);
            Spawner.Singleton.SpawnExplosion(gameObject.transform.position);

        }

    }


}
