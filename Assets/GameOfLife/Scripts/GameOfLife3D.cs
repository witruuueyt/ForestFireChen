using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

// class that controls the forest fire cellular automaton
public class GameOfLife3D : MonoBehaviour
{
    public int gridSizeX; // x size of the grid
    public int gridSizeY; // y size of the grid
    public int xC, yC; // used for picking random x, y points

    public GameObject cellPrefab; // sprite used to represent a cell on the grid
    public Text gameRunningText; // text used to display whether the game is running    

    public GameOfLifeCell[,] gameOfLifeCells = new GameOfLifeCell[0, 0]; // array of gameOfLifeCell objects
    public GameOfLifeCell.State[,] gameOfLifeCellsNextGenStates = new GameOfLifeCell.State[0, 0]; // array of cell states to be used in the next generation of the game 

    public GameObject[,] cellGameObjects = new GameObject[0, 0]; // an array to hold references to each gameobject that make up grid
    public bool gameRunning = false; // bool controlling whether the game is currently running

    [Range(0.01f, 3f)]
    public float updateRate; // used to define how often will the game update (in seconds)
    private float _gameTimer; // a variable that will be used detect when the game should update 

    public GameObject gameCanvas;        

    // Start is a built-in Unity function that is called before the first frame update
    private void Start()
    {
        CreateGrid(gridSizeX, gridSizeY);
        PauseGame(true);
        UpdateGridVisuals();
    }

    // this function controls whether or not to pause the game
    private void PauseGame(bool setGamePause)
    {
        // if setGamePause is true the game should stop running
        if (setGamePause)
        {
            gameRunning = false;
            gameRunningText.text = "Game Paused";
            gameRunningText.color = Color.red;
        }
        else // else if setGamePause is false unpause the game
        {
            gameRunning = true;
            gameRunningText.text = "Game Running";
            gameRunningText.color = Color.green;
        }
    }

    // Update is a built-in Unity function that is called once per frame 
    private void Update()
    {
        // check if the spacebar key has been pressed. this key will toggle between whether the game is currently running or paused
        if (Keyboard.current.rKey.wasPressedThisFrame)
        {
            // if the gameRunning is true, pause the game
            if (gameRunning)
            {
                PauseGame(true);
            }
            else // if the gameRunning is false, unpause the game
            {
                PauseGame(false);
            }
        }

        // check if the R key has been pressed. this key will clear the grid and pause the game
        if (Keyboard.current.rKey.wasPressedThisFrame)
        {
            RandomiseGrid();
        }

        // update the visual state of each cell
        UpdateGridVisuals();

        // if the game is not running, return here to prevent the rest of the code in this Update function from running    
        if (gameRunning == false)
            return;

        if (_gameTimer < updateRate)
        {
            _gameTimer += Time.deltaTime;
        }
        else
        {
            UpdateCells();
            _gameTimer = 0f;
        }
    }

    public void RandomiseGrid()
    {
        // iterate through every cell in the cell in the grid and set its state to dead
        for (int xCount = 0; xCount < gridSizeX; xCount++)
        {
            for (int yCount = 0; yCount < gridSizeY; yCount++)
            {
                // cast a random integer value into a GameOfLifeCell enum state.
                gameOfLifeCells[xCount, yCount].cellState =  (GameOfLifeCell.State) UnityEngine.Random.Range(0, 2);
            }
        }
    }


    // update the status of each cell on grid according to the rules of the game
    private void UpdateCells()
    {
        // iterate through each cell in the rows and columns
        for (int xCount = 0; xCount < gridSizeX; xCount++)
        {
            for (int yCount = 0; yCount < gridSizeY; yCount++)
            {
                // find out the number of alight neighbours this cell has
                int aliveNeighbourCells = CountAlightNeighbourCells(xCount, yCount);

                if (gameOfLifeCells[xCount, yCount].cellState == GameOfLifeCell.State.Alive) // if the cell is currently alive
                {

                    // Any live cell with fewer than two live neighbours dies, as if by underpopulation.
                    // Any live cell with more than three live neighbours dies, as if by overpopulation.
                    if (aliveNeighbourCells < 2 || aliveNeighbourCells > 3)
                    {
                        // cell die from under population or over population 
                        gameOfLifeCellsNextGenStates[xCount, yCount] = GameOfLifeCell.State.Dead;
                    }
                    else // if the cell doesn't die from underpopulation or overpopulation, assign it to be alive for the next generation of the game
                    {
                        gameOfLifeCellsNextGenStates[xCount, yCount] = GameOfLifeCell.State.Alive;
                    }
                }
                else // the cell is currently dead
                {
                    // Any dead cell with exactly three live neighbours becomes an  alive cell, as if by reproduction.
                    if (aliveNeighbourCells == 3)
                    {
                        gameOfLifeCellsNextGenStates[xCount, yCount] = GameOfLifeCell.State.Alive;
                    }
                    else // if the cell is dead, keep it dead for the next generation of the game
                    {
                        gameOfLifeCellsNextGenStates[xCount, yCount] = GameOfLifeCell.State.Dead;
                    }
                }
            }
        }

        // now the state of each cell has been calculated, apply the results by setting the current game array values to that of the next generation
        for (int xCount = 0; xCount < gridSizeX; xCount++)
        {
            for (int yCount = 0; yCount < gridSizeY; yCount++)
            {
                gameOfLifeCells[xCount, yCount].cellState = gameOfLifeCellsNextGenStates[xCount, yCount];
            }
        }
    }

    // count the alight cells surrounding a specified cell on the grid 
    private int CountAlightNeighbourCells(int cellPositionX, int cellPositionY)
    {
        // create local variable to keep track of alight neighbour cells
        int alightNeighbourCells = 0;

        // the code below tries to iterate through the neighbour cells immediately surrounding the specified cell on the grid as well as the specified cell
        //
        // On the grid the it would like this
        //
        //  N N N
        //  N C N
        //  N N N
        //
        // N = neighbour C = cell that's neighbours are being counted

        for (int xPosition = cellPositionX - 1; xPosition < cellPositionX + 2; xPosition++)
        {
            for (int yPosition = cellPositionY - 1; yPosition < cellPositionY + 2; yPosition++)
            {
                // only continue if the neighbour is valid
                if (IsNeighbourValid(xPosition, yPosition))
                {
                    // if the cell is currently alive (1), increase the count of alive neighbours by one
                    if (gameOfLifeCells[xPosition, yPosition].cellState == GameOfLifeCell.State.Alive)
                    {
                        alightNeighbourCells++;

                        // we don't want to check if the specified cell is alive, only its neighbours so if it was added, subtract it
                        if (xPosition == cellPositionX && yPosition == cellPositionY)
                        {
                            alightNeighbourCells--;
                        }
                    }
                }
            }
        }

        // return the number of alight neighbour cells
        return alightNeighbourCells;
    }

    // make sure that the cell we are trying to count is not beyond the range of the game grid (edges of the game board)
    private bool IsNeighbourValid(int cellPositionX, int cellPositionY)
    {
        if (cellPositionX < 0 || cellPositionY < 0)
            return false;

        if (cellPositionX >= gridSizeX || cellPositionY >= gridSizeY)
            return false;

        return true;
    }

    // this function creates the grid of the game
    private void CreateGrid(int sizeX, int sizeY)
    {
        // initialise the game array and array containing their states for the next generation to the size of grid
        gameOfLifeCells = new GameOfLifeCell[sizeX, sizeY];
        gameOfLifeCellsNextGenStates = new GameOfLifeCell.State[sizeX, sizeY];

        // initialise the array of gameobjects that will hold the game objects on the grid
        cellGameObjects = new GameObject[sizeX, sizeY];       

        int xSpacing = 0;
        int ySpacing = 0;

        for (int xCount = 0; xCount < sizeX; xCount++)
        {
            for (int yCount = 0; yCount < sizeY; yCount++)
            {
                // create cell object and name it according to its position (3 adds extra space between cells)
                GameObject newCell = Instantiate(cellPrefab, new Vector3(xCount, 0, yCount), Quaternion.identity);
                newCell.name = "cell " + xCount + " " + yCount;

                newCell.transform.SetParent(gameCanvas.transform);

                RectTransform rect = newCell.GetComponent<RectTransform>();

                rect.localPosition = new Vector3(0, 0, 0);
                rect.anchoredPosition = new Vector2(0, 0);
                rect.localScale = new Vector3(2, 2, 2);
                rect.localRotation = Quaternion.identity;

                //position the cell on the grid, spacing them out using the x and y count as coordinates 
                //  newCell.transform.position = new Vector3(xCount + xSpacing, 0, yCount + ySpacing);

                rect.anchoredPosition = new Vector2(xSpacing, ySpacing);


                // add the gameobject of the cell to the array that stores references of the cell sprites
                cellGameObjects[xCount, yCount] = newCell;

                // add to array
                GameOfLifeCell gameOfLifeCell = newCell.GetComponent<GameOfLifeCell>();
                gameOfLifeCells[xCount, yCount] = gameOfLifeCell;

                ySpacing += 35;
            }

            ySpacing = 0;
            xSpacing += 35;
        }
    }

    // udpate the grid sprites colours according to their current state
    // this function will be called every frame of the game so the grid is always up to date 
    private void UpdateGridVisuals()
    {
        // iterate through each cell in the rows and columns
        for (int xCount = 0; xCount < gridSizeX; xCount++)
        {
            // check current state of cell an update visual
            for (int yCount = 0; yCount < gridSizeY; yCount++)
            {
                if (gameOfLifeCells[xCount, yCount].cellState == GameOfLifeCell.State.Alive)
                {
                    gameOfLifeCells[xCount, yCount].SetAlive();                   
                }
                else if (gameOfLifeCells[xCount, yCount].cellState == GameOfLifeCell.State.Dead)
                {
                    gameOfLifeCells[xCount, yCount].SetDead();
                }
            }
        }
    }

    // UI Helper methods
    public void TogglePause()
    {
        PauseGame(gameRunning);
    }

    public void Clear()
    {
        PauseGame(true);

        // iterate through every cell in the cell in the grid and set its state to dead
        for (int xCount = 0; xCount < gridSizeX; xCount++)
        {
            for (int yCount = 0; yCount < gridSizeY; yCount++)
            {
                gameOfLifeCells[xCount, yCount].SetDead();
            }
        }
    }

    public void SetUpdateRate(float rate)
    {
        updateRate = rate;
    }
}