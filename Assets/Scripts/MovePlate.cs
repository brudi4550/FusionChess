using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Helpers;

public class MovePlate : MonoBehaviour
{
    //Some functions will need reference to the controller
    public GameObject controller;

    //The Chesspiece that was tapped to create this MovePlate
    GameObject reference = null;
    //Location on the board
    int matrixX;
    int matrixY;

    //false: movement, true: attacking
    public bool attack = false;
    // false: no mering possible, true: merging
    public bool merge = false;

    public bool enPassant = false;

    public void Start()
    {
        if (attack)
        {
            //Set to red
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1.0f, 0.0f, 0.0f, 1.0f);
        }

        if (merge)
        {
            //Set to green
            gameObject.GetComponent<SpriteRenderer>().color = new Color(0.0f, 1.0f, 0.0f, 1.0f);
        }
    }

    public void OnMouseUp()
    {
        controller = GameObject.FindGameObjectWithTag("GameController");

        //Destroy the victim Chesspiece
        if (attack)
        {
            GameObject cp;
            if (enPassant)
            {
                int factor = controller.GetComponent<Game>().GetCurrentPlayer() == "white" ? -1 : 1;
                cp = controller.GetComponent<Game>().GetPosition(matrixX, matrixY + factor);
            }
            else
            {
                cp = controller.GetComponent<Game>().GetPosition(matrixX, matrixY);
            }

            if (cp.name == "white_king") controller.GetComponent<Game>().Winner("black");
            if (cp.name == "black_king") controller.GetComponent<Game>().Winner("white");

            Destroy(cp);
            OnMouseUpReference(reference);
        }
        else if (merge)
        {
            GameObject cp = controller.GetComponent<Game>().GetPosition(matrixX, matrixY);

            //Check with asset we need 
            //PNIGHT
            if ((cp.name == "white_knight" && reference.GetComponent<Chessman>().name == "white_pawn") | (reference.GetComponent<Chessman>().name == "white_knight" && cp.name == "white_pawn"))
            {
                Destroy(cp);
                //Debug.Log(cp.name);
                //Debug.Log(reference.GetComponent<Chessman>().name);
                GameObject newReference = controller.GetComponent<Game>().Create("white_pnight", reference.GetComponent<Chessman>().GetXBoard(), reference.GetComponent<Chessman>().GetYBoard());
                Destroy(reference);

                OnMouseUpReference(newReference);
            }
            if ((cp.name == "black_knight" && reference.GetComponent<Chessman>().name == "black_pawn") | (reference.GetComponent<Chessman>().name == "black_knight" && cp.name == "black_pawn"))
            {
                Destroy(cp);
                GameObject newReference = controller.GetComponent<Game>().Create("black_pnight", reference.GetComponent<Chessman>().GetXBoard(), reference.GetComponent<Chessman>().GetYBoard());
                Destroy(reference);

                OnMouseUpReference(newReference);
            }
            //KNISHOP
            if ((cp.name == "white_knight" && reference.GetComponent<Chessman>().name == "white_bishop") | (reference.GetComponent<Chessman>().name == "white_knight" && cp.name == "white_bishop"))
            {
                Destroy(cp);
                Debug.Log(cp.name);
                Debug.Log(reference.GetComponent<Chessman>().name);
                GameObject newReference = controller.GetComponent<Game>().Create("white_knishop", reference.GetComponent<Chessman>().GetXBoard(), reference.GetComponent<Chessman>().GetYBoard());
                Destroy(reference);

                OnMouseUpReference(newReference);
            }
            if ((cp.name == "black_knight" && reference.GetComponent<Chessman>().name == "black_bishop") | (reference.GetComponent<Chessman>().name == "black_knight" && cp.name == "black_bishop"))
            {
                Destroy(cp);
                GameObject newReference = controller.GetComponent<Game>().Create("black_knishop", reference.GetComponent<Chessman>().GetXBoard(), reference.GetComponent<Chessman>().GetYBoard());
                Destroy(reference);

                OnMouseUpReference(newReference);
            }

            //RAWN
            if ((cp.name == "white_pawn" && reference.GetComponent<Chessman>().name == "white_rook") | (reference.GetComponent<Chessman>().name == "white_pawn" && cp.name == "white_rook"))
            {
                Destroy(cp);
                //Debug.Log(cp.name);
                //Debug.Log(reference.GetComponent<Chessman>().name);
                GameObject newReference = controller.GetComponent<Game>().Create("white_rawn", reference.GetComponent<Chessman>().GetXBoard(), reference.GetComponent<Chessman>().GetYBoard());
                Destroy(reference);

                OnMouseUpReference(newReference);
            }
            if ((cp.name == "black_pawn" && reference.GetComponent<Chessman>().name == "black_rook") | (reference.GetComponent<Chessman>().name == "black_pawn" && cp.name == "black_rook"))
            {
                Destroy(cp);
                GameObject newReference = controller.GetComponent<Game>().Create("black_rawn", reference.GetComponent<Chessman>().GetXBoard(), reference.GetComponent<Chessman>().GetYBoard());
                Destroy(reference);

                OnMouseUpReference(newReference);
            }

            //KNOOK
            if ((cp.name == "white_knight" && reference.GetComponent<Chessman>().name == "white_rook") | (reference.GetComponent<Chessman>().name == "white_knight" && cp.name == "white_rook"))
            {
                Destroy(cp);
                Debug.Log(cp.name);
                Debug.Log(reference.GetComponent<Chessman>().name);
                GameObject newReference = controller.GetComponent<Game>().Create("white_knook", reference.GetComponent<Chessman>().GetXBoard(), reference.GetComponent<Chessman>().GetYBoard());
                Destroy(reference);

                OnMouseUpReference(newReference);
            }
            if ((cp.name == "black_knight" && reference.GetComponent<Chessman>().name == "black_rook") | (reference.GetComponent<Chessman>().name == "black_knight" && cp.name == "black_rook"))
            {
                Destroy(cp);
                GameObject newReference = controller.GetComponent<Game>().Create("black_knook", reference.GetComponent<Chessman>().GetXBoard(), reference.GetComponent<Chessman>().GetYBoard());
                Destroy(reference);

                OnMouseUpReference(newReference);
            }
            //RISHOP
            if ((cp.name == "white_rook" && reference.GetComponent<Chessman>().name == "white_bishop") | (reference.GetComponent<Chessman>().name == "white_rook" && cp.name == "white_bishop"))
            {
                Destroy(cp);
                Debug.Log(cp.name);
                Debug.Log(reference.GetComponent<Chessman>().name);
                GameObject newReference = controller.GetComponent<Game>().Create("white_rishop", reference.GetComponent<Chessman>().GetXBoard(), reference.GetComponent<Chessman>().GetYBoard());
                Destroy(reference);

                OnMouseUpReference(newReference);
            }
            if ((cp.name == "black_rook" && reference.GetComponent<Chessman>().name == "black_bishop") | (reference.GetComponent<Chessman>().name == "black_rook" && cp.name == "black_bishop"))
            {
                Destroy(cp);
                GameObject newReference = controller.GetComponent<Game>().Create("black_rishop", reference.GetComponent<Chessman>().GetXBoard(), reference.GetComponent<Chessman>().GetYBoard());
                Destroy(reference);

                OnMouseUpReference(newReference);
            }
            //KNEEN
            //PISHOP


        }
        else
        {
            OnMouseUpReference(reference);
        }

    }

    private void OnMouseUpReference(GameObject references)
    {
        Game game = controller.GetComponent<Game>();
        Chessman piece = references.GetComponent<Chessman>();
        if (enPassant)
        {
            int factor = game.GetCurrentPlayer() == "white" ? -1 : 1;
            game.SetPositionEmpty(piece.GetXBoard(), piece.GetYBoard() + factor);
        }
        else
        {

            game.SetPositionEmpty(piece.GetXBoard(), piece.GetYBoard());
        }

        //Add move to movelist, necessary for checking for en passant
        Move m = new Move(references, piece.GetXBoard(), piece.GetYBoard(), matrixX, matrixY);
        game.AddMove(m);

        //Move reference chess piece to this position
        references.GetComponent<Chessman>().SetXBoard(matrixX);
        references.GetComponent<Chessman>().SetYBoard(matrixY);
        references.GetComponent<Chessman>().SetCoords();

        //Update the matrix
        game.SetPosition(references);

        //Switch Current Player
        game.NextTurn();

        //Destroy the move plates including self
        references.GetComponent<Chessman>().DestroyMovePlates();

    }

    public void SetCoords(int x, int y)
    {
        matrixX = x;
        matrixY = y;
    }

    public void SetReference(GameObject obj)
    {
        reference = obj;
    }

    public GameObject GetReference()
    {
        return reference;
    }
}
