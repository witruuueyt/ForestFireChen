using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Reset : MonoBehaviour
{
    
    public void Click()
    {
        SceneManager.LoadScene(0); //reload the game
    }
}
