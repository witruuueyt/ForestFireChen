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
        Debug.Log("Time down");
        yield return new WaitForSeconds(6f);
        firstEndingPanel.SetActive(true);
        Debug.Log("panel");
        yield return new WaitForSeconds(1f);
        audio.Play();
        Debug.Log("music");
        
    }

}
