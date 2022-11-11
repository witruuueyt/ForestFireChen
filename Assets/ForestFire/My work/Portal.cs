using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public Transform player; //get the player object

    public Transform fooled; //get the transform object

    public GameObject firstEndingPanel; // get the ending panel

    private new AudioSource audio; //Audio source component


    void Start()
    {
        audio = GetComponent<AudioSource>(); // get the Audio source component
    }

    
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == ("Player")) //check the trigger object's tag
        {
            Debug.Log("portal ");
            player.transform.position = fooled.position;//teleport player

            StartCoroutine(NeverCoroutine()); // start a time down
            
        }



    }

    public IEnumerator NeverCoroutine()
    {
        Debug.Log("Time down");
        yield return new WaitForSeconds(6f); //wait 6 seconds
        firstEndingPanel.SetActive(true); //set ending panel to active
        Debug.Log("panel");
        yield return new WaitForSeconds(1f); //wait 1 second
        audio.Play(); //play music
        Debug.Log("music");
        
    }

}
