﻿using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Helpers;
using System;


public class Counter
{
    public static int roundCounter = 0;
    public static int winCounterPlayerA = 0;
    public static int winCounterPlayerB = 0;
}



public class Game : MonoBehaviour
{
    //Reference from Unity IDE
    public GameObject chesspiece;
    public string playerA;
    public string playerB;


    //Matrices needed, positions of each of the GameObjects
    //Also separate arrays for the players in order to easily keep track of them all
    //Keep in mind that the same objects are going to be in "positions" and "playerBlack"/"playerWhite"
    private GameObject[,] positions = new GameObject[8, 8];
    private GameObject[] playerBlack = new GameObject[16];
    private GameObject[] playerWhite = new GameObject[16];

    //current turn
    private string currentPlayer = "white";

    private List<Move> moves = new List<Move>();

    //Game Ending
    private bool gameOver = false;


    //Timer 
    public float timeRemainingWhite;
    public bool timerIsRunningWhite = false;
    public Text timeTextWhite;
    public float timeRemainingBlack;
    public bool timerIsRunningBlack = false;
    public Text timeTextBlack;

    //Unity calls this right when the game starts, there are a few built in functions
    //that Unity can call for you
    public void Start()
    {

        timerIsRunningWhite = true;
        timerIsRunningBlack = true;
        Counter.roundCounter++;
        if (Counter.roundCounter % 2 == 0) //No of game round is even -> 2,4,6 etc. player A=black and player B=white
        {
            playerA = "black";
            playerB = "white";
            GameObject.FindGameObjectWithTag("PlayerWhite").GetComponent<Text>().enabled = true;
            GameObject.FindGameObjectWithTag("PlayerWhite").GetComponent<Text>().text = "Player B = white \n won " + Counter.winCounterPlayerB + " x";
            GameObject.FindGameObjectWithTag("PlayerBlack").GetComponent<Text>().enabled = true;
            GameObject.FindGameObjectWithTag("PlayerBlack").GetComponent<Text>().text = "Player A = black \n  won " + Counter.winCounterPlayerA + " x";
        }
        else
        {
            playerA = "white";
            playerB = "black";
            GameObject.FindGameObjectWithTag("PlayerWhite").GetComponent<Text>().enabled = true;
            GameObject.FindGameObjectWithTag("PlayerWhite").GetComponent<Text>().text = "Player A = white \n won " + Counter.winCounterPlayerA + " x";
            GameObject.FindGameObjectWithTag("PlayerBlack").GetComponent<Text>().enabled = true;
            GameObject.FindGameObjectWithTag("PlayerBlack").GetComponent<Text>().text = "Player B = black \n won " + Counter.winCounterPlayerB + " x";
        }
        GameObject.FindGameObjectWithTag("GameRound").GetComponent<Text>().enabled = true;
        GameObject.FindGameObjectWithTag("GameRound").GetComponent<Text>().text = "Game round " + Counter.roundCounter;
        /*GameObject.FindGameObjectWithTag("WinCounterA").GetComponent<Text>().enabled = true;
        GameObject.FindGameObjectWithTag("WinCounterA").GetComponent<Text>().text = "Player A won " + Counter.winCounterPlayerA + " times";
        GameObject.FindGameObjectWithTag("WinCounterB").GetComponent<Text>().enabled = true;
        GameObject.FindGameObjectWithTag("WinCounterB").GetComponent<Text>().text = "Player B won " + Counter.winCounterPlayerB + " times";*/



        playerWhite = new GameObject[] { Create("white_rook", 0, 0), Create("white_knight", 1, 0),
            Create("white_bishop", 2, 0), Create("white_queen", 3, 0), Create("white_king", 4, 0),
            Create("white_bishop", 5, 0), Create("white_knight", 6, 0), Create("white_rook", 7, 0),
            Create("white_pawn", 0, 1), Create("white_pawn", 1, 1), Create("white_pawn", 2, 1),
            Create("white_pawn", 3, 1), Create("white_pawn", 4, 1), Create("white_pawn", 5, 1),
            Create("white_pawn", 6, 1), Create("white_pawn", 7, 1)};
        playerBlack = new GameObject[] { Create("black_rook", 0, 7), Create("black_knight",1,7),
            Create("black_bishop",2,7), Create("black_queen",3,7), Create("black_king",4,7),
            Create("black_bishop",5,7), Create("black_knight",6,7), Create("black_rook",7,7),
            Create("black_pawn", 0, 6), Create("black_pawn", 1, 6), Create("black_pawn", 2, 6),
            Create("black_pawn", 3, 6), Create("black_pawn", 4, 6), Create("black_pawn", 5, 6),
            Create("black_pawn", 6, 6), Create("black_pawn", 7, 6)};

        //Set all piece positions on the positions board
        for (int i = 0; i < playerBlack.Length; i++)
        {
            SetPosition(playerBlack[i]);
            SetPosition(playerWhite[i]);
        }



    }

    public GameObject Create(string name, int x, int y)
    {
        GameObject obj = Instantiate(chesspiece, new Vector3(0, 0, -1), Quaternion.identity);
        Chessman cm = obj.GetComponent<Chessman>(); //We have access to the GameObject, we need the script
        cm.name = name; //This is a built in variable that Unity has, so we did not have to declare it before
        cm.SetXBoard(x);
        cm.SetYBoard(y);
        cm.Activate(); //It has everything set up so it can now Activate()
        return obj;
    }

    public void SetPosition(GameObject obj)
    {
        Chessman cm = obj.GetComponent<Chessman>();
        //Overwrites either empty space or whatever was there
        positions[cm.GetXBoard(), cm.GetYBoard()] = obj;
    }

    public void SetPositionEmpty(int x, int y)
    {
        positions[x, y] = null;
    }

    public GameObject GetPosition(int x, int y)
    {
        try
        {
            return positions[x, y];
        }
        catch (IndexOutOfRangeException e)
        {
            return null;
        }
    }

    public GameObject[,] GetPositions()
    {
        return positions;
    }

    public bool isPositionOnBoard(int x, int y)
    {
        if (x < 0 || y < 0 || x >= positions.GetLength(0) || y >= positions.GetLength(1)) return false;
        return true;
    }

    public string GetCurrentPlayer()
    {
        return currentPlayer;
    }

    public bool IsGameOver()
    {
        return gameOver;
    }

    public void AddMove(Move m)
    {
        moves.Add(m);
    }

    public bool LongCastlePossible(String player)
    {
        if (player.Equals("white"))
        {
            if (!pieceHasMoved(0, 0) && !pieceHasMoved(4, 0))
            {
                return
                    GetPosition(1, 0) == null &&
                    GetPosition(2, 0) == null &&
                    GetPosition(3, 0) == null;
            }
            else
            {
                return false;
            }
        }
        else
        {
            if (!pieceHasMoved(0, 7) && !pieceHasMoved(4, 7))
            {
                return
                    GetPosition(1, 7) == null &&
                    GetPosition(2, 7) == null &&
                    GetPosition(3, 7) == null;
            }
            else
            {
                return false;
            }
        }
    }

    public bool ShortCastlePossible(String player)
    {
        if (player.Equals("white"))
        {
            if (!pieceHasMoved(7, 0) && !pieceHasMoved(4, 0))
            {
                return
                    GetPosition(5, 0) == null &&
                    GetPosition(6, 0) == null;
            }
            else
            {
                return false;
            }
        }
        else
        {
            if (!pieceHasMoved(7, 7) && !pieceHasMoved(4, 7))
            {

                return
                    GetPosition(5, 7) == null &&
                    GetPosition(6, 7) == null;
            }
            else
            {
                return false;
            }
        }
    }

    public bool pieceHasMoved(int orgX, int orgY)
    {
        foreach (Move m in moves)
        {
            if (m.getFromX() == orgX && m.getFromY() == orgY)
            {
                return true;
            }
        }
        return false;
    }

    public bool isKingInCheck(String playerOfKing, GameObject[,] positions)
    {
        Chessman currPiece;
        bool kingIsInCheck = false;
        for (int i = 0; i < positions.GetLength(0); i++)
        {
            for (int k = 0; k < positions.GetLength(1); k++)
            {
                if (positions[i, k] == null)
                    continue;
                GameObject g = positions[i, k];
                currPiece = g.GetComponent<Chessman>();
                if (currPiece.name.StartsWith(playerOfKing))
                    continue;
                String pieceName = currPiece.name.Split("_")[1];
                switch (pieceName)
                {
                    case "pawn":
                        kingIsInCheck = pawnCheck(playerOfKing, g, positions);
                        break;
                    case "rook":
                        kingIsInCheck = rookCheck(playerOfKing, g, positions);
                        break;
                    case "knight":
                        kingIsInCheck = knightCheck(playerOfKing, g, positions);
                        break;
                    case "bishop":
                        kingIsInCheck = bishopCheck(playerOfKing, g, positions);
                        break;
                    case "queen":
                        kingIsInCheck = queenCheck(playerOfKing, g, positions);
                        break;
                    case "pnight":
                        kingIsInCheck = knightCheck(playerOfKing, g, positions) || pawnCheck(playerOfKing, g, positions);
                        break;
                    case "knishop":
                        kingIsInCheck = knightCheck(playerOfKing, g, positions) || bishopCheck(playerOfKing, g, positions);
                        break;
                    case "rawn":
                        kingIsInCheck = rookCheck(playerOfKing, g, positions) || pawnCheck(playerOfKing, g, positions);
                        break;
                    case "knook":
                        kingIsInCheck = rookCheck(playerOfKing, g, positions) || knightCheck(playerOfKing, g, positions);
                        break;
                    case "rishop":
                        kingIsInCheck = rookCheck(playerOfKing, g, positions) || bishopCheck(playerOfKing, g, positions);
                        break;
                    case "kneen":
                        kingIsInCheck = queenCheck(playerOfKing, g, positions) || knightCheck(playerOfKing, g, positions);
                        break;
                    case "pishop":
                        kingIsInCheck = bishopCheck(playerOfKing, g, positions) || pawnCheck(playerOfKing, g, positions);
                        break;
                }
                if (kingIsInCheck)
                {
                    return true;
                }
            }
        }
        return false;
    }

    private bool pawnCheck(String playerOfKing, GameObject g, GameObject[,] positions)
    {
        Tuple<int, int> kingCoords = getKingCoords(playerOfKing, positions);
        int xKing = kingCoords.Item1;
        int yKing = kingCoords.Item2;
        Tuple<int, int> pieceCoords = getPieceCoords(g, positions);
        int xPiece = pieceCoords.Item1;
        int yPiece = pieceCoords.Item2;
        if (playerOfKing.Equals("white"))
        {
            return (xPiece + 1 == xKing || xPiece - 1 == xKing) && yPiece - 1 == yKing;
        }
        else
        {
            return (xPiece + 1 == xKing || xPiece - 1 == xKing) && yPiece + 1 == yKing;
        }
    }

    private bool rookCheck(String playerOfKing, GameObject g, GameObject[,] positions)
    {
        Tuple<int, int> kingCoords = getKingCoords(playerOfKing, positions);
        int xKing = kingCoords.Item1;
        int yKing = kingCoords.Item2;
        Tuple<int, int> pieceCoords = getPieceCoords(g, positions);
        int xPiece = pieceCoords.Item1;
        int yPiece = pieceCoords.Item2;
        //check right of piece
        for (int i = xPiece + 1; i < positions.GetLength(0); i++)
        {
            if (i == xKing && yPiece == yKing)
            {
                return true;
            }
            if (positions[i, yPiece] != null)
            {
                break;
            }
        }
        //check left of piece
        for (int i = xPiece - 1; i >= 0; i--)
        {
            if (i == xKing && yPiece == yKing)
            {
                return true;
            }
            if (positions[i, yPiece] != null)
            {
                break;
            }
        }
        //check up vertically
        for (int i = yPiece + 1; i < positions.GetLength(1); i++)
        {
            if (i == yKing && xPiece == xKing)
            {
                return true;
            }
            if (positions[xPiece, i] != null)
            {
                break;
            }
        }
        //check down vertically
        for (int i = yPiece - 1; i >= 0; i--)
        {
            if (i == yKing && xPiece == xKing)
            {
                return true;
            }
            if (positions[xPiece, i] != null)
            {
                break;
            }
        }
        return false;
    }

    private bool knightCheck(String playerOfKing, GameObject g, GameObject[,] positions)
    {
        Tuple<int, int> kingCoords = getKingCoords(playerOfKing, positions);
        int xKing = kingCoords.Item1;
        int yKing = kingCoords.Item2;
        Tuple<int, int> pieceCoords = getPieceCoords(g, positions);
        int xPiece = pieceCoords.Item1;
        int yPiece = pieceCoords.Item2;
        if ((xPiece + 1 == xKing || xPiece - 1 == xKing) && yPiece + 2 == yKing)
        {
            return true;
        }
        else if ((xPiece + 1 == xKing || xPiece - 1 == xKing) && yPiece - 2 == yKing)
        {
            return true;
        }
        else if ((yPiece + 1 == yKing || yPiece - 1 == yKing) && xPiece + 2 == xKing)
        {
            return true;
        }
        else if ((yPiece + 1 == yKing || yPiece - 1 == yKing) && xPiece - 2 == xKing)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private bool bishopCheck(String playerOfKing, GameObject g, GameObject[,] positions)
    {
        Tuple<int, int> kingCoords = getKingCoords(playerOfKing, positions);
        int xKing = kingCoords.Item1;
        int yKing = kingCoords.Item2;
        Tuple<int, int> pieceCoords = getPieceCoords(g, positions);
        int xPiece = pieceCoords.Item1;
        int yPiece = pieceCoords.Item2;
        int currX = xPiece;
        int currY = yPiece;
        //check up left
        while (currX >= 0 && currY < positions.GetLength(1))
        {
            currX--;
            currY++;
            if (currX == xKing && currY == yKing)
            {
                return true;
            }
            else if (isPositionOnBoard(currX, currY) && positions[currX, currY] != null)
            {
                break;
            }
        }
        currX = xPiece;
        currY = yPiece;
        //check up right
        while (currX < positions.GetLength(0) && currY < positions.GetLength(1))
        {
            currX++;
            currY++;
            if (currX == xKing && currY == yKing)
            {
                return true;
            }
            else if (isPositionOnBoard(currX, currY) && positions[currX, currY] != null)
            {
                break;
            }
        }
        currX = xPiece;
        currY = yPiece;
        //check down left
        while (currX >= 0 && currY >= 0)
        {
            currX--;
            currY--;
            if (currX == xKing && currY == yKing)
            {
                return true;
            }
            else if (isPositionOnBoard(currX, currY) && positions[currX, currY] != null)
            {
                break;
            }
        }
        currX = xPiece;
        currY = yPiece;
        //check down right
        while (currX < positions.GetLength(0) && currY >= 0)
        {
            currX++;
            currY--;
            if (currX == xKing && currY == yKing)
            {
                return true;
            }
            else if (isPositionOnBoard(currX, currY) && positions[currX, currY] != null)
            {
                break;
            }
        }
        return false;
    }

    private bool queenCheck(String playerOfKing, GameObject g, GameObject[,] positions)
    {
        return bishopCheck(playerOfKing, g, positions) || rookCheck(playerOfKing, g, positions);
    }

    public Tuple<int, int> getKingCoords(String player, GameObject[,] positions)
    {
        for (int i = 0; i < positions.GetLength(0); i++)
        {
            for (int j = 0; j < positions.GetLength(1); j++)
            {
                if (positions[i, j] == null)
                    continue;
                Chessman c = positions[i, j].GetComponent<Chessman>();
                if (c.name.Equals(player + "_king"))
                {
                    return Tuple.Create(i, j);
                }
            }
        }
        return null;
    }

    public Tuple<int, int> getPieceCoords(GameObject g, GameObject[,] positions)
    {
        for (int i = 0; i < positions.GetLength(0); i++)
        {
            for (int j = 0; j < positions.GetLength(1); j++)
            {
                if (positions[i, j] == null)
                    continue;
                if (positions[i, j] == g)
                {
                    return Tuple.Create(i, j);
                }
            }
        }
        return null;
    }

    public Move GetLastMove()
    {
        return moves.DefaultIfEmpty(null).Last();
    }

    public void NextTurn()
    {
        if (currentPlayer == "white")
        {
            currentPlayer = "black";
        }
        else
        {
            currentPlayer = "white";
        }
    }

    public void Update()
    {
        if (gameOver == true && Input.GetMouseButtonDown(0))
        {
            gameOver = false;

            //Using UnityEngine.SceneManagement is needed here
            SceneManager.LoadScene("Game"); //Restarts the game by loading the scene over again
        }



        if (timerIsRunningWhite && currentPlayer == "white")
        {
            if (timeRemainingWhite > 0)
            {

                timeRemainingWhite -= Time.deltaTime;
                DisplayTimeWhite(timeRemainingWhite);
            }
            else
            {
                Debug.Log("Time has run out!");
                timeRemainingWhite = 0;
                timerIsRunningWhite = false;
                Winner("PlayerBlack");
            }
        }

        if (timerIsRunningBlack && currentPlayer == "black")
        {
            if (timeRemainingBlack > 0)
            {
                timeRemainingBlack -= Time.deltaTime;
                DisplayTimeBlack(timeRemainingBlack);
            }
            else
            {
                Debug.Log("Time has run out!");
                timeRemainingBlack = 0;
                timerIsRunningBlack = false;
                Winner("PlayerWhite");
            }
        }
    }


    void DisplayTimeBlack(float timeToDisplay)
    {
        timeToDisplay += 1;
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        GameObject.FindGameObjectWithTag("TimerBlack").GetComponent<Text>().enabled = true;
        GameObject.FindGameObjectWithTag("TimerBlack").GetComponent<Text>().text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    void DisplayTimeWhite(float timeToDisplay)
    {
        timeToDisplay += 1;
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        GameObject.FindGameObjectWithTag("TimerWhite").GetComponent<Text>().enabled = true;
        GameObject.FindGameObjectWithTag("TimerWhite").GetComponent<Text>().text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }


    public void Winner(string playerWinner)
    {
        gameOver = true;

        //Using UnityEngine.UI is needed here
        //GameObject.FindGameObjectWithTag("WinnerText").GetComponent<Text>().enabled = true;
        //GameObject.FindGameObjectWithTag("WinnerText").GetComponent<Text>().text = playerWinner + " is the winner";

        //GameObject.FindGameObjectWithTag("RestartText").GetComponent<Text>().enabled = true;

        if (playerWinner == "white" && playerA == "white") //player A won
        {
            Counter.winCounterPlayerA++;
            GameObject.FindGameObjectWithTag("PlayerWhite").GetComponent<Text>().text = "Player A = white \n won " + Counter.winCounterPlayerA + " x";
            GameObject.FindGameObjectWithTag("TrophyWinnerWhite").GetComponent<Image>().enabled = true;
            GameObject.FindGameObjectWithTag("WinnerWhite").GetComponent<Text>().enabled = true;
            GameObject.FindGameObjectWithTag("WinnerWhite").GetComponent<Text>().text = "--> WINNER";
        }
        else if (playerWinner == "white" && playerB == "white") //player B won
        {
            Counter.winCounterPlayerB++;
            GameObject.FindGameObjectWithTag("PlayerWhite").GetComponent<Text>().text = "Player B = white \n won " + Counter.winCounterPlayerB + " x";
            GameObject.FindGameObjectWithTag("TrophyWinnerWhite").GetComponent<Image>().enabled = true;
            GameObject.FindGameObjectWithTag("WinnerWhite").GetComponent<Text>().enabled = true;
            GameObject.FindGameObjectWithTag("WinnerWhite").GetComponent<Text>().text = "--> WINNER";

        }
        else if (playerWinner == "black" && playerA == "black") //player A won
        {
            Counter.winCounterPlayerA++;
            GameObject.FindGameObjectWithTag("PlayerBlack").GetComponent<Text>().text = "Player A = black \n won " + Counter.winCounterPlayerA + " x";
            GameObject.FindGameObjectWithTag("TrophyWinnerBlack").GetComponent<Image>().enabled = true;
            GameObject.FindGameObjectWithTag("WinnerBlack").GetComponent<Text>().enabled = true;
            GameObject.FindGameObjectWithTag("WinnerBlack").GetComponent<Text>().text = "--> WINNER";
        }
        else //player B won
        {
            Counter.winCounterPlayerB++;
            GameObject.FindGameObjectWithTag("PlayerBlack").GetComponent<Text>().text = "Player B = black \n won " + Counter.winCounterPlayerB + " x";
            GameObject.FindGameObjectWithTag("TrophyWinnerBlack").GetComponent<Image>().enabled = true;
            GameObject.FindGameObjectWithTag("WinnerBlack").GetComponent<Text>().enabled = true;
            GameObject.FindGameObjectWithTag("WinnerBlack").GetComponent<Text>().text = "--> WINNER";
        }



    }


}
