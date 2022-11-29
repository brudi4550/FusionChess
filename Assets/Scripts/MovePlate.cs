using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            GameObject cp = controller.GetComponent<Game>().GetPosition(matrixX, matrixY);

            if (cp.name == "white_king") controller.GetComponent<Game>().Winner("black");
            if (cp.name == "black_king") controller.GetComponent<Game>().Winner("white");

            Destroy(cp);
            OnMouseUpReference(reference);
        }
        else if (merge)
        {
            GameObject cp = controller.GetComponent<Game>().GetPosition(matrixX, matrixY);
            Destroy(cp);
            //Check with asset we need 
            if (cp.name.Contains("white_knight")  | cp.name.Contains("white_pawn"))
            {
                GameObject newReference = controller.GetComponent<Game>().Create("white_pnight", reference.GetComponent<Chessman>().GetXBoard(), reference.GetComponent<Chessman>().GetYBoard());
                Destroy(reference);
                OnMouseUpReference(newReference);
            }
            if (cp.name.Contains("black_knight") | cp.name.Contains("black_pawn")) 
            {
                GameObject newReference = controller.GetComponent<Game>().Create("black_pnight", reference.GetComponent<Chessman>().GetXBoard(), reference.GetComponent<Chessman>().GetYBoard());
                Destroy(reference);
                OnMouseUpReference(newReference);
            }
            
            //@Magda Create the merge figures 
            

        }
        else 
        {
            OnMouseUpReference(reference);
        }
       
    }

    private void OnMouseUpReference(GameObject references)
    {
        controller.GetComponent<Game>().SetPositionEmpty(references.GetComponent<Chessman>().GetXBoard(),
                  references.GetComponent<Chessman>().GetYBoard());

         //Move reference chess piece to this position
         references.GetComponent<Chessman>().SetXBoard(matrixX);
         references.GetComponent<Chessman>().SetYBoard(matrixY);
         references.GetComponent<Chessman>().SetCoords();

         //Update the matrix
         controller.GetComponent<Game>().SetPosition(references);

         //Switch Current Player
         controller.GetComponent<Game>().NextTurn();

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
