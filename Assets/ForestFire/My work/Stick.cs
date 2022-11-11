using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stick : MonoBehaviour
{
    public GameObject fire; //get the fire

    public GameObject bonfire1; //get the first bonfire

    public GameObject bonfire2; // get the second bonfire

    private bool isFireActive; //a bool to check if the fire is active

    private bool isBonfireActive; //a bool to check if the bonfire is active

    public GameObject lid; //get the lid of pot

    private Rigidbody lidRigidbody; //rigidbody of lid

    private new AudioSource audio; //new audio

    
    void Start()
    {
        audio = GetComponent<AudioSource>(); //get the audio source component
        isFireActive = false; //the fire is not active
        isBonfireActive = false; // the bonfire is not active
    }
    
    public void OnTriggerEnter(Collider other)
    {
        if (other.name == "Fire VFX(Clone)") // check the name of trigger object
        {
            Debug.Log("trigger");
            fire.SetActive(true); //set the fire to active
            isFireActive = true; //change the bool to true
        }

        else if (other.name == "FireDetectedCube1") // check the name of trigger object
        {
            if (isFireActive == true) // check if the fire is active
            {
                bonfire1.SetActive(true); //set the bonfire to active
            }
        }

        else if (other.name == "FireDetectedCube2") // check the name of trigger object
        {
            if (isFireActive == true) // check if the fire is active
            {
                bonfire2.SetActive(true); //set the bonfire to active
                StartCoroutine(FlyCoroutine()); //start a time down
            }
                
        }
    }

    public IEnumerator FlyCoroutine()
    {
        lidRigidbody = lid.GetComponent<Rigidbody>(); //get the rigidbody of lid
        yield return new WaitForSeconds(5f); //after 5 seconds
        audio.Play(); //play a music
        lidRigidbody.AddForce(Vector3.up * 70); //make the lid flyyyy
    }
     
}