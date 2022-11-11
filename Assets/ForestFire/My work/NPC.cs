using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public GameObject portal; //get the portal object

    public GameObject text1; //get the first text

    public GameObject text2; //get the second text

    public GameObject text3; // get the final text

    public GameObject dialog; // the background picture of dialog
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(PortalCoroutine()); //start a time down
        Debug.Log("npc working");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator PortalCoroutine()
    {
        yield return new WaitForSeconds(1f); //after 1 second
        Debug.Log("start text1");
        text1.SetActive(true); //game object set to active
        dialog.SetActive(true);//game object set to active

        yield return new WaitForSeconds(5f); //after 5 seconds
        text1.SetActive(false);//game object set to not active
        Debug.Log("start text2");
        text2.SetActive(true);//game object set to active

        yield return new WaitForSeconds(5f); //after 5 seconds
        text2.SetActive(false);//game object set to not active
        Debug.Log("start text3");
        text3.SetActive(true);//game object set to active
        Debug.Log("portal active");
        portal.SetActive(true);//game object set to active
    }
}
