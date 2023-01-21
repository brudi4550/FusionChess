using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Helpers;
using System;

public class Chessman : MonoBehaviour
{
    //References to objects in our Unity Scene
    public GameObject controller;
    public GameObject movePlate;

    //Position for this Chesspiece on the Board
    //The correct position will be set later
    private int xBoard = -1;
    private int yBoard = -1;

    //Variable for keeping track of the player it belongs to "black" or "white"
    private string player;

    //References to all the possible Sprites that this Chesspiece could be
    public Sprite black_queen, black_knight, black_bishop, black_king, black_rook, black_pawn, black_pnight, black_knishop, black_rawn, black_knook, black_rishop,
        black_kneen, black_pishop;
    public Sprite white_queen, white_knight, white_bishop, white_king, white_rook, white_pawn, white_pnight, white_knishop, white_rawn, white_knook, white_rishop,
        white_kneen, white_pishop;

    public void Activate()
    {
        //Get the game controller
        controller = GameObject.FindGameObjectWithTag("GameController");

        //Take the instantiated location and adjust transform
        SetCoords();

        //Choose correct sprite based on piece's name
        switch (this.name)
        {
            case "black_queen": this.GetComponent<SpriteRenderer>().sprite = black_queen; player = "black"; break;
            case "black_knight": this.GetComponent<SpriteRenderer>().sprite = black_knight; player = "black"; break;
            case "black_bishop": this.GetComponent<SpriteRenderer>().sprite = black_bishop; player = "black"; break;
            case "black_king": this.GetComponent<SpriteRenderer>().sprite = black_king; player = "black"; break;
            case "black_rook": this.GetComponent<SpriteRenderer>().sprite = black_rook; player = "black"; break;
            case "black_pawn": this.GetComponent<SpriteRenderer>().sprite = black_pawn; player = "black"; break;
            case "black_pnight": this.GetComponent<SpriteRenderer>().sprite = black_pnight; player = "black"; break;
            case "black_knishop": this.GetComponent<SpriteRenderer>().sprite = black_knishop; player = "black"; break;
            case "black_rawn": this.GetComponent<SpriteRenderer>().sprite = black_rawn; player = "black"; break;
            case "black_knook": this.GetComponent<SpriteRenderer>().sprite = black_knook; player = "black"; break;
            case "black_rishop": this.GetComponent<SpriteRenderer>().sprite = black_rishop; player = "black"; break;

            case "black_kneen": this.GetComponent<SpriteRenderer>().sprite = black_kneen; player = "black"; break;
            case "black_pishop": this.GetComponent<SpriteRenderer>().sprite = black_pishop; player = "black"; break;

            case "white_queen": this.GetComponent<SpriteRenderer>().sprite = white_queen; player = "white"; break;
            case "white_knight": this.GetComponent<SpriteRenderer>().sprite = white_knight; player = "white"; break;
            case "white_bishop": this.GetComponent<SpriteRenderer>().sprite = white_bishop; player = "white"; break;
            case "white_king": this.GetComponent<SpriteRenderer>().sprite = white_king; player = "white"; break;
            case "white_rook": this.GetComponent<SpriteRenderer>().sprite = white_rook; player = "white"; break;
            case "white_pawn": this.GetComponent<SpriteRenderer>().sprite = white_pawn; player = "white"; break;
            case "white_pnight": this.GetComponent<SpriteRenderer>().sprite = white_pnight; player = "white"; break;
            case "white_knishop": this.GetComponent<SpriteRenderer>().sprite = white_knishop; player = "white"; break;
            case "white_rawn": this.GetComponent<SpriteRenderer>().sprite = white_rawn; player = "white"; break;
            case "white_knook": this.GetComponent<SpriteRenderer>().sprite = white_knook; player = "white"; break;
            case "white_rishop": this.GetComponent<SpriteRenderer>().sprite = white_rishop; player = "white"; break;
            case "white_kneen": this.GetComponent<SpriteRenderer>().sprite = white_kneen; player = "white"; break;
            case "white_pishop": this.GetComponent<SpriteRenderer>().sprite = white_pishop; player = "white"; break;

        }
    }

    public void SetCoords()
    {
        //Get the board value in order to convert to xy coords
        float x = xBoard;
        float y = yBoard;

        //Adjust by variable offset
        x *= 0.66f;
        y *= 0.66f;

        //Add constants (pos 0,0)
        x += -2.3f;
        y += -2.3f;

        //Set actual unity values
        this.transform.position = new Vector3(x, y, -1.0f);
    }

    public int GetXBoard()
    {
        return xBoard;
    }

    public int GetYBoard()
    {
        return yBoard;
    }

    public void SetXBoard(int x)
    {
        xBoard = x;
    }

    public void SetYBoard(int y)
    {
        yBoard = y;
    }


    private void OnMouseUp()
    {
        if (!controller.GetComponent<Game>().IsGameOver() && controller.GetComponent<Game>().GetCurrentPlayer() == player)
        {
            //Remove all moveplates relating to previously selected piece
            DestroyMovePlates();

            //Create new MovePlates
            InitiateMovePlates();
        }
    }

    public void DestroyMovePlates()
    {
        //Destroy old MovePlates
        GameObject[] movePlates = GameObject.FindGameObjectsWithTag("MovePlate");
        for (int i = 0; i < movePlates.Length; i++)
        {
            Destroy(movePlates[i]); //Be careful with this function "Destroy" it is asynchronous
        }
    }

    public void InitiateMovePlates()
    {
        switch (this.name)
        {
            case "black_queen":
            case "white_queen":
                LineMovePlate(1, 0);
                LineMovePlate(0, 1);
                LineMovePlate(1, 1);
                LineMovePlate(-1, 0);
                LineMovePlate(0, -1);
                LineMovePlate(-1, -1);
                LineMovePlate(-1, 1);
                LineMovePlate(1, -1);
                break;
            case "black_knight":
            case "white_knight":
                LMovePlate();
                break;
            case "black_bishop":
            case "white_bishop":
                LineMovePlate(1, 1);
                LineMovePlate(1, -1);
                LineMovePlate(-1, 1);
                LineMovePlate(-1, -1);
                break;
            case "black_king":
            case "white_king":
                SurroundMovePlate();
                CastleMovePlate();
                break;
            case "black_rook":
            case "white_rook":
                LineMovePlate(1, 0);
                LineMovePlate(0, 1);
                LineMovePlate(-1, 0);
                LineMovePlate(0, -1);
                break;
            case "black_pawn":
                PawnMovePlate(xBoard, yBoard - 1);
                break;
            case "white_pawn":
                PawnMovePlate(xBoard, yBoard + 1);
                break;
            case "black_pnight":
                PawnMovePlate(xBoard, yBoard - 1);
                LMovePlate();
                break;
            case "white_pnight":
                PawnMovePlate(xBoard, yBoard + 1);
                LMovePlate();
                break;
            case "black_knishop":
            case "white_knishop":
                LMovePlate();
                LineMovePlate(1, 1);
                LineMovePlate(1, -1);
                LineMovePlate(-1, 1);
                LineMovePlate(-1, -1);
                break;
            case "black_rawn":
                PawnMovePlate(xBoard, yBoard - 1);
                LineMovePlate(1, 0);
                LineMovePlate(0, 1);
                LineMovePlate(-1, 0);
                LineMovePlate(0, -1);
                break;
            case "white_rawn":
                PawnMovePlate(xBoard, yBoard + 1);
                LineMovePlate(1, 0);
                LineMovePlate(0, 1);
                LineMovePlate(-1, 0);
                LineMovePlate(0, -1);
                break;
            case "black_knook":
            case "white_knook":
                LineMovePlate(1, 0);
                LineMovePlate(0, 1);
                LineMovePlate(-1, 0);
                LineMovePlate(0, -1);
                LMovePlate();
                break;
            case "black_rishop":
            case "white_rishop":
                LineMovePlate(1, 1);
                LineMovePlate(1, -1);
                LineMovePlate(-1, 1);
                LineMovePlate(-1, -1);
                LineMovePlate(1, 0);
                LineMovePlate(0, 1);
                LineMovePlate(-1, 0);
                LineMovePlate(0, -1);
                break;

            case "white_kneen":
            case "black_kneen":
                LineMovePlate(1, 0);
                LineMovePlate(0, 1);
                LineMovePlate(1, 1);
                LineMovePlate(-1, 0);
                LineMovePlate(0, -1);
                LineMovePlate(-1, -1);
                LineMovePlate(-1, 1);
                LineMovePlate(1, -1);
                LMovePlate();
                break;
            case "white_pishop":
                LineMovePlate(1, 1);
                LineMovePlate(1, -1);
                LineMovePlate(-1, 1);
                LineMovePlate(-1, -1);
                PawnMovePlate(xBoard, yBoard + 1);
                break;
            case "black_pishop":
                LineMovePlate(1, 1);
                LineMovePlate(1, -1);
                LineMovePlate(-1, 1);
                LineMovePlate(-1, -1);
                PawnMovePlate(xBoard, yBoard - 1);
                break;

        }
    }

    public void LineMovePlate(int xIncrement, int yIncrement)
    {
        Game sc = controller.GetComponent<Game>();

        int x = xBoard + xIncrement;
        int y = yBoard + yIncrement;

        while (sc.isPositionOnBoard(x, y) && sc.GetPosition(x, y) == null)
        {
            MovePlateSpawn(x, y);
            x += xIncrement;
            y += yIncrement;
        }

        if (sc.isPositionOnBoard(x, y) && sc.GetPosition(x, y).GetComponent<Chessman>().player != player)
        {
            MovePlateAttackSpawn(x, y, false);
        }
        if (sc.isPositionOnBoard(x, y) && sc.GetPosition(x, y).GetComponent<Chessman>().player == player && this.name != "white_queen" && this.name != "black_queen" &&
            this.name != "white_rawn" && this.name != "black_rawn" && this.name != "white_pnight" && this.name != "black_pnight" &&
            this.name != "white_knishop" && this.name != "black_knishop" &&
            this.name != "white_knook" && this.name != "black_knook" &&
            this.name != "white_rishop" && this.name != "black_rishop" &&
            this.name != "white_kneen" && this.name != "black_kneen" &&
            this.name != "white_pishop" && this.name != "black_pishop" &&
            this.name != "white_king" && this.name != "black_king" &&
            !sc.GetPosition(x, y).GetComponent<Chessman>().name.EndsWith("king"))
        {
            Debug.Log("LineMovePlate");
            Debug.Log(this.name);
            Debug.Log("Name");
            Debug.Log("sc.GetPosition(x, y).GetComponent<Chessman>().player");
            Debug.Log(sc.GetPosition(x, y).GetComponent<Chessman>().player);
            Debug.Log("Player");
            Debug.Log(player);

            MovePlateMergeSpawn(x, y);
        }
    }

    public void LMovePlate()
    {
        PointMovePlate(xBoard + 1, yBoard + 2);
        PointMovePlate(xBoard - 1, yBoard + 2);
        PointMovePlate(xBoard + 2, yBoard + 1);
        PointMovePlate(xBoard + 2, yBoard - 1);
        PointMovePlate(xBoard + 1, yBoard - 2);
        PointMovePlate(xBoard - 1, yBoard - 2);
        PointMovePlate(xBoard - 2, yBoard + 1);
        PointMovePlate(xBoard - 2, yBoard - 1);
    }

    public void SurroundMovePlate()
    {
        PointMovePlate(xBoard, yBoard + 1);
        PointMovePlate(xBoard, yBoard - 1);
        PointMovePlate(xBoard - 1, yBoard + 0);
        PointMovePlate(xBoard - 1, yBoard - 1);
        PointMovePlate(xBoard - 1, yBoard + 1);
        PointMovePlate(xBoard + 1, yBoard + 0);
        PointMovePlate(xBoard + 1, yBoard - 1);
        PointMovePlate(xBoard + 1, yBoard + 1);
    }

    public void CastleMovePlate()
    {
        if (controller.GetComponent<Game>().LongCastlePossible(player))
        {
            if (player.Equals("white"))
            {
                CastleMovePlateSpawn(2, 0, "long");
            }
            else
            {
                CastleMovePlateSpawn(2, 7, "long");
            }
        }
        if (controller.GetComponent<Game>().ShortCastlePossible(player))
        {
            if (player.Equals("white"))
            {
                CastleMovePlateSpawn(6, 0, "short");
            }
            else
            {
                CastleMovePlateSpawn(6, 7, "short");
            }
        }
    }

    public void PointMovePlate(int x, int y)
    {
        Game sc = controller.GetComponent<Game>();

        if (sc.isPositionOnBoard(x, y))
        {
            GameObject cp = sc.GetPosition(x, y);

            if (cp == null)
            {
                MovePlateSpawn(x, y);
            }
            else if (cp.GetComponent<Chessman>().player != player)
            {
                MovePlateAttackSpawn(x, y, false);
            }
            else if (cp.GetComponent<Chessman>().player == player && cp != null && this.name != "white_king" && this.name != "black_king" &&
                this.name != "white_rawn" && this.name != "black_rawn" && this.name != "white_pnight" && this.name != "black_pnight" &&
                this.name != "white_knishop" && this.name != "black_knishop" &&
                this.name != "white_knook" && this.name != "black_knook" &&
                this.name != "white_rishop" && this.name != "black_rishop" &&
                this.name != "white_kneen" && this.name != "black_kneen" &&
                this.name != "white_pishop" && this.name != "black_pishop" &&
                !sc.GetPosition(x, y).GetComponent<Chessman>().name.EndsWith("king"))
            {
                Debug.Log("PointMovePLate");
                Debug.Log(this.name);
                Debug.Log("Name");
                MovePlateMergeSpawn(x, y);

            }
            //else if (cp.GetComponent<Chessman>().player == player && cp != null && this.name != "white_king" && this.name != "black_king")
            //{
            //    MovePlateMergeSpawn(x, y);
            //}
        }
    }

    public void PawnMovePlate(int x, int y)
    {
        Game game = controller.GetComponent<Game>();
        if (game.isPositionOnBoard(x, y))
        {

            //checking for first move for pawns
            if (game.GetPosition(x, y) == null)
            {
                if (y == 7 && player.Equals("white"))
                {
                    PromotionMovePlateSpawn(x, y);
                }
                else if (y == 0 && player.Equals("black"))
                {
                    PromotionMovePlateSpawn(x, y);
                }
                else
                {
                    MovePlateSpawn(x, y);
                }
                if (y == 2 && player == "white" && game.GetPosition(x, y + 1) == null)
                {
                    MovePlateSpawn(x, y + 1);
                }
                else if (y == 5 && player == "black" && game.GetPosition(x, y - 1) == null)
                {
                    MovePlateSpawn(x, y - 1);
                }
            }

            checkForEnPassant(game, x, y);

            if (game.isPositionOnBoard(x + 1, y) && game.GetPosition(x + 1, y) != null && game.GetPosition(x + 1, y).GetComponent<Chessman>().player != player)
            {
                MovePlateAttackSpawn(x + 1, y, false);
            }

            if (game.isPositionOnBoard(x - 1, y) && game.GetPosition(x - 1, y) != null && game.GetPosition(x - 1, y).GetComponent<Chessman>().player != player)
            {
                MovePlateAttackSpawn(x - 1, y, false);
            }

            if (game.GetPosition(x, y) != null && game.GetPosition(x, y).GetComponent<Chessman>().player == player && this.name != "white_bishop" && this.name != "black_bishop" &&
                this.name != "white_rawn" && this.name != "black_rawn" && this.name != "white_pnight" && this.name != "black_pnight" &&
                this.name != "white_knishop" && this.name != "black_knishop" &&

                this.name != "white_rishop" && this.name != "black_rishop" &&
                this.name != "white_kneen" && this.name != "black_kneen" &&
                this.name != "white_pishop" && this.name != "black_pishop" &&
                this.name != "white_king" && this.name != "black_king" &&
                !game.GetPosition(x, y).GetComponent<Chessman>().name.EndsWith("king"))
            {
                Debug.Log("PawnMovePlate");
                Debug.Log(this.name);
                Debug.Log("Name");

                if (this.name != "white_rishop" && this.name != "black_rishop")
                {

                    MovePlateMergeSpawn(x, y);
                }

            }
        }
    }

    public void checkForEnPassant(Game game, int x, int y)
    {
        GameObject possiblePosition1, possiblePosition2;
        if (player == "white" && y == 5)
        {
            possiblePosition1 = game.GetPosition(x + 1, y - 1);
            possiblePosition2 = game.GetPosition(x - 1, y - 1);
            Move lastMove = game.GetLastMove();
            if (possiblePosition1 != null && possiblePosition1.name == "black_pawn")
            {
                if (lastMove.getPiece().name == possiblePosition1.name && lastMove.getToX() == x + 1)
                {
                    MovePlateAttackSpawn(x + 1, y, true);
                }
            }
            if (possiblePosition2 != null && possiblePosition2.name == "black_pawn")
            {
                if (lastMove.getPiece().name == possiblePosition2.name && game.GetLastMove().getToX() == x - 1)
                {
                    MovePlateAttackSpawn(x - 1, y, true);
                }
            }
        }
        else if (player == "black" && y == 2)
        {
            possiblePosition1 = game.GetPosition(x + 1, y + 1);
            possiblePosition2 = game.GetPosition(x - 1, y + 1);
            Move lastMove = game.GetLastMove();
            if (possiblePosition1 != null && possiblePosition1.name == "white_pawn")
            {
                if (lastMove.getPiece().name == possiblePosition1.name && lastMove.getToX() == x + 1)
                {
                    MovePlateAttackSpawn(x + 1, y, true);
                }
            }
            if (possiblePosition2 != null && possiblePosition2.name == "white_pawn")
            {
                if (lastMove.getPiece().name == possiblePosition2.name && game.GetLastMove().getToX() == x - 1)
                {
                    MovePlateAttackSpawn(x - 1, y, true);
                }
            }
        }
    }

    public void CastleMovePlateSpawn(int matrixX, int matrixY, string type)
    {
        //simulate move and check if the move is legal before spawning plate
        //king is not allowed to be in check in when castling and also not in the two positions next to him
        Game game = controller.GetComponent<Game>();
        GameObject[,] copy1 = getBoardCopy();
        GameObject[,] copy2 = getBoardCopy();
        GameObject[,] copy3 = getBoardCopy();
        copy2[xBoard, yBoard] = null;
        copy2[matrixX - 1, matrixY] = game.CreatePiece(player + "_king");
        copy3[xBoard, yBoard] = null;
        copy3[matrixX, matrixY] = game.CreatePiece(player + "_king");
        if (game.isKingInCheck(player, copy1) ||
            game.isKingInCheck(player, copy2) ||
            game.isKingInCheck(player, copy3))
            return;

        //Get the board value in order to convert to xy coords
        float x = matrixX;
        float y = matrixY;

        //Adjust by variable offset
        x *= 0.66f;
        y *= 0.66f;

        //Add constants (pos 0,0)
        x += -2.3f;
        y += -2.3f;

        //Set actual unity values
        GameObject mp = Instantiate(movePlate, new Vector3(x, y, -3.0f), Quaternion.identity);
        MovePlate mpScript = mp.GetComponent<MovePlate>();
        mpScript.SetReference(gameObject);
        if (type.Equals("short"))
        {
            mpScript.shortCastle = true;
        }
        else
        {
            mpScript.longCastle = true;
        }
        mpScript.SetCoords(matrixX, matrixY);
    }

    public void PromotionMovePlateSpawn(int matrixX, int matrixY)
    {
        //simulate move and check if the move is legal before spawning plate
        Game game = controller.GetComponent<Game>();
        GameObject[,] copy = getBoardCopy();
        copy[xBoard, yBoard] = null;
        copy[matrixX, matrixY] = game.Create(player + "_queen", matrixX, matrixY);
        if (game.isKingInCheck(player, copy))
            return;

        //Get the board value in order to convert to xy coords
        float x = matrixX;
        float y = matrixY;

        //Adjust by variable offset
        x *= 0.66f;
        y *= 0.66f;

        //Add constants (pos 0,0)
        x += -2.3f;
        y += -2.3f;

        //Set actual unity values
        GameObject mp = Instantiate(movePlate, new Vector3(x, y, -3.0f), Quaternion.identity);
        MovePlate mpScript = mp.GetComponent<MovePlate>();
        mpScript.SetReference(gameObject);
        mpScript.promotion = true;
        mpScript.SetCoords(matrixX, matrixY);
    }

    public void MovePlateSpawn(int matrixX, int matrixY)
    {
        //simulate move and check if the move is legal before spawning plate
        Game game = controller.GetComponent<Game>();
        GameObject[,] copy = getBoardCopy();
        GameObject piece = copy[xBoard, yBoard];
        copy[xBoard, yBoard] = null;
        copy[matrixX, matrixY] = piece;
        if (game.isKingInCheck(player, copy))
            return;

        //Get the board value in order to convert to xy coords
        float x = matrixX;
        float y = matrixY;

        //Adjust by variable offset
        x *= 0.66f;
        y *= 0.66f;

        //Add constants (pos 0,0)
        x += -2.3f;
        y += -2.3f;

        //Set actual unity values
        GameObject mp = Instantiate(movePlate, new Vector3(x, y, -3.0f), Quaternion.identity);

        MovePlate mpScript = mp.GetComponent<MovePlate>();
        mpScript.SetReference(gameObject);
        mpScript.SetCoords(matrixX, matrixY);
    }

    public void MovePlateAttackSpawn(int matrixX, int matrixY, bool enPassant)
    {
        //simulate move and check if the move is legal before spawning plate
        Game game = controller.GetComponent<Game>();
        GameObject[,] copy = getBoardCopy();
        GameObject piece = copy[xBoard, yBoard];
        copy[xBoard, yBoard] = null;
        copy[matrixX, matrixY] = piece;
        if (game.isKingInCheck(player, copy))
            return;

        //Get the board value in order to convert to xy coords
        float x = matrixX;
        float y = matrixY;

        //Adjust by variable offset
        x *= 0.66f;
        y *= 0.66f;

        //Add constants (pos 0,0)
        x += -2.3f;
        y += -2.3f;

        //Set actual unity values
        GameObject mp = Instantiate(movePlate, new Vector3(x, y, -3.0f), Quaternion.identity);

        MovePlate mpScript = mp.GetComponent<MovePlate>();
        mpScript.attack = true;
        mpScript.enPassant = enPassant;
        mpScript.SetReference(gameObject);
        mpScript.SetCoords(matrixX, matrixY);
    }

    // Methode to merge 
    public void MovePlateMergeSpawn(int matrixX, int matrixY)
    {
        //simulate move and check if the move is legal before spawning plate
        Game game = controller.GetComponent<Game>();
        GameObject[,] copy = getBoardCopy();
        GameObject piece = copy[xBoard, yBoard];
        string pieceName1 = piece.GetComponent<Chessman>().name.Split("_")[1];
        string pieceName2 = copy[matrixX, matrixY].GetComponent<Chessman>().name.Split("_")[1];
        string combo = getCombination(pieceName1, pieceName2);
        Debug.Log(pieceName1);
        Debug.Log(pieceName2);
        Debug.Log(combo);
        copy[xBoard, yBoard] = null;
        copy[matrixX, matrixY] = game.CreatePiece(player + "_" + combo);
        if (game.isKingInCheck(player, copy))
            return;

        //Get the board value in order to convert to xy coords
        float x = matrixX;
        float y = matrixY;

        //Adjust by variable offset
        x *= 0.66f;
        y *= 0.66f;

        //Add constants (pos 0,0)
        x += -2.3f;
        y += -2.3f;

        //Set actual unity values
        GameObject mp = Instantiate(movePlate, new Vector3(x, y, -3.0f), Quaternion.identity);

        MovePlate mpScript = mp.GetComponent<MovePlate>();
        mpScript.merge = true;
        mpScript.SetReference(gameObject);
        Debug.Log("MovePlateMergeSpawn");
        Debug.Log(mpScript.GetReference());
        Debug.Log(controller.GetComponent<Game>().GetPosition(matrixX, matrixY));

        mpScript.SetCoords(matrixX, matrixY);
    }

    public GameObject[,] getBoardCopy()
    {
        Game game = controller.GetComponent<Game>();
        GameObject[,] currBoard = game.GetPositions();
        return currBoard.Clone() as GameObject[,];
    }

    public string getCombination(string pieceName1, string pieceName2)
    {
        switch (pieceName1)
        {
            case "pawn":
                switch (pieceName2)
                {
                    case "knight": return "pnight";
                    case "bishop": return "pishop";
                    case "rook": return "rawn";
                }
                break;
            case "rook":
                switch (pieceName2)
                {
                    case "pawn": return "rawn";
                    case "knight": return "knook";
                    case "bishop": return "rishop";
                }
                break;
            case "knight":
                switch (pieceName2)
                {
                    case "pawn": return "pnight";
                    case "rook": return "knook";
                    case "bishop": return "knishop";
                    case "queen": return "kneen";
                }
                break;
            case "bishop":
                switch (pieceName2)
                {
                    case "pawn": return "pishop";
                    case "knight": return "knishop";
                    case "rook": return "rishop";
                }
                break;
            case "queen":
                switch (pieceName2)
                {
                    case "knight": return "kneen";
                }
                break;
        }
        return "";
    }
}
