using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stick : MonoBehaviour
{
    public GameObject fire;

    public GameObject bonfire1;

    public GameObject bonfire2;

    private bool isFireActive;

    public bool isBonfireActive;

    public GameObject lid;

    public Rigidbody lidRigidbody;

    public int test;
    void Start()
    {
        test = 1;
        isFireActive = false;
        isBonfireActive = false;
    }
    
    public void OnTriggerEnter(Collider other)
    {
        if (other.name == "Fire VFX(Clone)")
        {
            Debug.Log("trigger");
            fire.SetActive(true);
            isFireActive = true;
        }

        else if (other.name == "FireDetectedCube1")
        {
            if (isFireActive == true)
            {
                bonfire1.SetActive(true);
            }
        }

        else if (other.name == "FireDetectedCube2")
        {
            if (isFireActive == true)
            {
                bonfire2.SetActive(true);
                StartCoroutine(FlyCoroutine());
            }
                
        }
    }

    public IEnumerator FlyCoroutine()
    {
        lidRigidbody = lid.GetComponent<Rigidbody>();
        yield return new WaitForSeconds(5f);

        lidRigidbody.AddForce(Vector3.up * 70);
    }
   
    
}