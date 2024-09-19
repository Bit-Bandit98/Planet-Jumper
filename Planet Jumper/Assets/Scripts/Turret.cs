using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
   
    int CurrentLevel, MaxLevel;
    [SerializeField]
    int Damage;
    int Layermask = 1 << 8;

    [SerializeField]
    float Range = 15f;
    [SerializeField]
    float FireRate;
    public float PFireRate
    {
        get { return FireRate; }
        set { FireRate = value; }
    }
    RaycastHit2D Hit;
    GameObject Target;
    public Turret InstanceOfTurret;
    bool Switch = false;
    public bool PSwitch
    {
        get { return Switch; }
        set
        {
            Switch = value;
            switch (Switch)
            {
                case true:
                    StartCoroutine("FindTarget");
                    break;

                case false:
                    StopAllCoroutines();
                    break;
            }
        }
    }
    [SerializeField]
    GameObject MuzzleFlash;
    private void Awake()
    {
        InstanceOfTurret = this;
    }


    IEnumerator FindTarget()
    {
        while (Target == null)
        {
             Hit = Physics2D.CircleCast(transform.position, Range, Vector3.zero, Mathf.Infinity, Layermask);

            if (Hit.collider != null) {
               
                Target = Hit.collider.gameObject;
                
                yield return null;
            }
            else {yield return new WaitForSeconds(0.2f);}
        }

        StartCoroutine("LookAtTarget");
        StartCoroutine("Fire");
        
    }



   
    void ClearTarget()
    {
        Target = null;
    }

    IEnumerator LookAtTarget()
    {
        while(Target != null)
        {
            transform.LookAt(Target.transform.position);
           // transform.eulerAngles = new Vector3(transform.rotation.x, -90, transform.rotation.z);
            yield return null;
        }
    }

    IEnumerator Fire()
    {
        while(Hit.collider != null && Hit.collider.gameObject != null && Hit.collider.gameObject.activeInHierarchy && Vector2.Distance(gameObject.transform.position, Target.transform.position) < Range)
        {
          Target.SendMessage("Damage", Damage *GM.Singleton.PlayerMultiplyersAndValues.PDamageNumber);
            //   transform.rotation = Quaternion.Euler(0.0f, 0.0f, Vector3.Angle(Vector3.Scale(transform.position, Vector3.forward), Vector3.Scale(Target.transform.position, Vector3.right)));

            //transform.eulerAngles = new Vector3(0f ,0f , transform.eulerAngles.z);
            //transform.rotation *= new Quaternion(1,0,0,1));
            if (MuzzleFlash.activeInHierarchy) MuzzleFlash.SetActive(false);
            MuzzleFlash.SetActive(true);
            yield return new WaitForSeconds(FireRate);
        }
        Target = null;
        StartCoroutine("FindTarget");
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, Range);
    }
}
