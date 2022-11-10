using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BBQ : MonoBehaviour
{
    private float ftime;
    public bool start;

    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (start)
        {
            ftime += Time.deltaTime;
            if (ftime >= 3f)
            {


            }

            if (ftime >= 7f)
            {


            }

            if (ftime >= 10f)
            {

                
            }
        }
    }
}
