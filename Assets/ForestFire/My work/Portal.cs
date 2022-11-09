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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("portal ");
            player.transform.position = fooled.position;
            
            System.Timers.Timer time = new System.Timers.Timer(3000);//实例化Timer类，规定每隔3秒执行一次
            time.Elapsed += new System.Timers.ElapsedEventHandler(Never);//当达到规定的时间内执行aa 这个方法
            time.AutoReset = false;//false 执行一次，true 一直执行
            time.Enabled = true;//设置是否执行time.Elapsed 时间

        }



    }
    private void Never(object source, System.Timers.ElapsedEventArgs e)
    {
        Debug.Log("Time down");
        
        //audio.Play();
        Debug.Log("music");
        firstEndingPanel.SetActive(true);
        Debug.Log("panel");
    }
}
