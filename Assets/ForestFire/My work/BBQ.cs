using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BBQ : MonoBehaviour
{
    private Material m_material;

    float bbqTime = 0;

    public GameObject markBoard0;

    public GameObject markBoard1;

    public GameObject ending2;
    private void Awake()
    {
        m_material = GetComponent<Renderer>().material;
    }

    
    private void OnTriggerStay(Collider other)
    {
        
        if (other.name == "FireDetectedCube1")
        {
            
            bbqTime += Time.deltaTime;

            if (bbqTime >= 5f)
            {
                m_material.color = Color.magenta;
            }

            if (bbqTime >= 10f)
            {
                m_material.color = Color.black;
            }
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "MarkSystem")
        {
            StartCoroutine(EndCoroutine());
            
            if(bbqTime <5f)
            {
                markBoard0.SetActive(true);
            }

            if(bbqTime >= 5f && bbqTime < 10f)
            {
                markBoard1.SetActive(true);
            }

            if (bbqTime >= 10f)
            {
                markBoard0.SetActive(true);
            }
        }
    }

    public IEnumerator EndCoroutine()
    {
        yield return new WaitForSeconds(5f);
        ending2.SetActive(true);

    }
}