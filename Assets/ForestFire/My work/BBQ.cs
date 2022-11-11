using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BBQ : MonoBehaviour
{
    private Material m_material; //Create a new material

    float bbqTime = 0; // Creat a timer for BBQ

    public GameObject markBoard0; // Get the first mark board

    public GameObject markBoard1; // Get the second mark board

    public GameObject ending2; // Get the ending panel
    private void Awake()
    {
        m_material = GetComponent<Renderer>().material; //Get the material of this game object
    }

    
    private void OnTriggerStay(Collider other)
    {
        
        if (other.tag == "Flame") //do something if the trigger object's tag equals to "Flame"
        {
            
            bbqTime += Time.deltaTime; //bbqTime +1 every 1 second

            if (bbqTime >= 5f)
            {
                m_material.color = Color.magenta; //change the color of material
            }

            if (bbqTime >= 10f)
            {
                m_material.color = Color.black; //change the color of material
            }
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "MarkSystem") //go on if the trigger object's name equals to "MarkSystem"
        {
            StartCoroutine(EndCoroutine()); // start a time down
            
            if(bbqTime <5f)
            {
                markBoard0.SetActive(true); //game object set to active
            }

            if(bbqTime >= 5f && bbqTime < 10f)
            {
                markBoard1.SetActive(true); //game object set to active
            }

            if (bbqTime >= 10f)
            {
                markBoard0.SetActive(true); //game object set to active
            }
        }
    }

    public IEnumerator EndCoroutine()
    {
        yield return new WaitForSeconds(5f); //wait for 5 second
        ending2.SetActive(true); //game object set to active

    }
}