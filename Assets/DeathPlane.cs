using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathPlane : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        IDestructible destructible = collision.gameObject.GetComponent<IDestructible>();

        if(destructible != null)
        {
            destructible.Destroy();
        }
    }
}