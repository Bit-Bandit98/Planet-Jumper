using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrier : MonoBehaviour
{
    private void OnDrawGizmos()
    {
        Gizmos.DrawCube(gameObject.transform.position, gameObject.transform.lossyScale);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Asteroid"){collision.SendMessage("Die", false); }
    }
}
