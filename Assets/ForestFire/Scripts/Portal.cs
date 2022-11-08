using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public Transform player;

    public Transform fooled;

    public GameObject firstEndingPanel;

    new AudioSource audio;

    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("portal ");
            player.transform.position = fooled.position;
            //firstEndingPanel.SetActive(true);
            audio.Play();
        }



    }
}
