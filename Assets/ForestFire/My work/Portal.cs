using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public Transform player;

    public Transform fooled;

    public GameObject firstEndingPanel;

    private new AudioSource audio;

    
    void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == ("Player"))
        {
            Debug.Log("portal ");
            player.transform.position = fooled.position;

            StartCoroutine(NeverCoroutine());
            
        }



    }

    public IEnumerator NeverCoroutine()
    {
        yield return new WaitForSeconds(3f);
        Debug.Log("Time down");

        audio.Play();
        Debug.Log("music");
        firstEndingPanel.SetActive(true);
        Debug.Log("panel");
    }

}
