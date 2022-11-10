using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public GameObject portal;

    public GameObject text1;

    public GameObject text2;

    public GameObject text3;

    public GameObject dialog;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(PortalCoroutine());
        Debug.Log("npc working");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator PortalCoroutine()
    {
        yield return new WaitForSeconds(1f);
        Debug.Log("start text1");
        text1.SetActive(true);
        dialog.SetActive(true);

        yield return new WaitForSeconds(5f);
        text1.SetActive(false);
        Debug.Log("start text2");
        text2.SetActive(true);

        yield return new WaitForSeconds(5f);
        text2.SetActive(false);
        Debug.Log("start text3");
        text3.SetActive(true);
        Debug.Log("portal active");
        portal.SetActive(true);
    }
}
