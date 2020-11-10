using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameOfLifeCell : MonoBehaviour
{
    public Image image;
    public State cellState; // this variable stores the state of the cell as an enum defined below 
    public enum State
    {      
        Dead,
        Alive,
    }

    public void SetAlive()
    { 
        image.color = Color.white;
        cellState = State.Alive;
    }

    public void SetDead()
    {
        image.color = Color.black;
        cellState = State.Dead;
    }

    public void InvertState()
    {
        if (cellState == State.Alive)
            SetDead();

        else if (cellState == State.Dead)
            SetAlive();
    }
}
