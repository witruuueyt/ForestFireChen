using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ignition : MonoBehaviour
{
    public GameObject fire;
    
    
    public void OnTriggerEnter(Collider other)
    {
        if (other.name == "Fire VFX(Clone)")
        {
            Debug.Log("trigger");
            fire.SetActive(true);
        }
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.name == "Fire VFX(Clone)")
    //    {
    //        Debug.Log("collision");
    //        fire.SetActive(true);
    //    }
    //}
}