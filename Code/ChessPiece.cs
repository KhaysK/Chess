using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessPiece : ChessPieceMovement
{
    private GameObject controller;
    public GameObject movePlate;

    public int xBoard = -1;
    public int yBoard = -1;

    public string player;

    public Sprite black_queen, black_knight, black_bishop, black_king, black_rook, black_pawn;
    public Sprite white_queen, white_knight, white_bishop, white_king, white_rook, white_pawn;

    public void Activate()
    {
        // В зависимости от имени объекта устанавливает ему соответствующую картику(спрайт)
        controller = GameObject.FindGameObjectWithTag("GameController");

        SetCoords();

        switch (this.name)
        {
            case "black_queen": this.GetComponent<SpriteRenderer>().sprite = black_queen; player = "black"; break;
            case "black_knight": this.GetComponent<SpriteRenderer>().sprite = black_knight; player = "black"; break;
            case "black_bishop": this.GetComponent<SpriteRenderer>().sprite = black_bishop; player = "black"; break;
            case "black_king": this.GetComponent<SpriteRenderer>().sprite = black_king; player = "black"; break;
            case "black_rook": this.GetComponent<SpriteRenderer>().sprite = black_rook; player = "black"; break;
            case "black_pawn": this.GetComponent<SpriteRenderer>().sprite = black_pawn; player = "black"; break;

            case "white_queen": this.GetComponent<SpriteRenderer>().sprite = white_queen; player = "white"; break;
            case "white_knight": this.GetComponent<SpriteRenderer>().sprite = white_knight; player = "white"; break;
            case "white_bishop": this.GetComponent<SpriteRenderer>().sprite = white_bishop; player = "white"; break;
            case "white_king": this.GetComponent<SpriteRenderer>().sprite = white_king; player = "white"; break;
            case "white_rook": this.GetComponent<SpriteRenderer>().sprite = white_rook; player = "white"; break;
            case "white_pawn": this.GetComponent<SpriteRenderer>().sprite = white_pawn; player = "white"; break;
        }
    }

    public void SetCoords()
    {
        // Высчитывает кординаты для объекта исходя из размера сцены проекта и помещает каждую фигуру на свое место 
        float x = xBoard;
        float y = yBoard;

        x *= 2f;
        y *= 1.98f;

        this.transform.position = new Vector3(x, y, -1.0f);
    }

    private void OnMouseUp()
    {
        // Отслеживает нажатие мыши

        if(!controller.GetComponent<Controller>().IsGameOver() && controller.GetComponent<Controller>().currentPlayer == player)
        {
            DestroyMovePlates();

            InitiateMovePlates();
        }
    }

    public void DestroyMovePlates()
    {
        // Удаляет все подсказки 
        GameObject[] movePlates = GameObject.FindGameObjectsWithTag("MovePlate");
        for (int i = 0; i < movePlates.Length; i++)
        {
            Destroy(movePlates[i]);
        }
    }

    public void InitiateMovePlates()
    {
        // Создает подсказки для выбранной фигуры(куда можно ее поставить)
        switch (this.name)
        {
            case "black_queen":
            case "white_queen":
                LineMovePlate(1, 0, xBoard, yBoard, controller, player, movePlate, gameObject);
                LineMovePlate(0, 1, xBoard, yBoard, controller, player, movePlate, gameObject);
                LineMovePlate(1, 1, xBoard, yBoard, controller, player, movePlate, gameObject);
                LineMovePlate(-1, 0, xBoard, yBoard, controller, player, movePlate, gameObject);
                LineMovePlate(0, -1, xBoard, yBoard, controller, player, movePlate, gameObject);
                LineMovePlate(-1, -1, xBoard, yBoard, controller, player, movePlate, gameObject);
                LineMovePlate(-1, 1, xBoard, yBoard, controller, player, movePlate, gameObject);
                LineMovePlate(1, -1, xBoard, yBoard, controller, player, movePlate, gameObject);
                break;
            case "black_knight":
            case "white_knight":
                LMovePlate(xBoard, yBoard, controller, player, movePlate, gameObject);
                break;
            case "black_bishop":
            case "white_bishop":
                LineMovePlate(1, 1, xBoard, yBoard, controller, player, movePlate, gameObject);
                LineMovePlate(1, -1, xBoard, yBoard, controller, player, movePlate, gameObject);
                LineMovePlate(-1, 1, xBoard, yBoard, controller, player, movePlate, gameObject);
                LineMovePlate(-1, -1, xBoard, yBoard, controller, player, movePlate, gameObject);
                break;
            case "black_king":
            case "white_king":
                SurroundMovePlate(xBoard, yBoard, controller, player, movePlate, gameObject);
                break;
            case "black_rook":
            case "white_rook":
                LineMovePlate(1, 0, xBoard, yBoard, controller, player, movePlate, gameObject);
                LineMovePlate(0, 1, xBoard, yBoard, controller, player, movePlate, gameObject);
                LineMovePlate(-1, 0, xBoard, yBoard, controller, player, movePlate, gameObject);
                LineMovePlate(0, -1, xBoard, yBoard, controller, player, movePlate, gameObject);
                break;
            case "black_pawn":
                for(int i = 0; i < 8; i++)
                {
                    if (controller.GetComponent<Controller>().position[i, 6] == this.gameObject)
                    {
                        PawnMovePlate(xBoard, yBoard - 2, controller, player, movePlate, gameObject);
                        PawnMovePlate(xBoard, yBoard - 2, controller, player, movePlate, gameObject);
                    }
                    else
                        PawnMovePlate(xBoard, yBoard - 1, controller, player, movePlate, gameObject);
                }
                break;
            case "white_pawn":
                for (int i = 0; i < 8; i++)
                {
                    if (controller.GetComponent<Controller>().position[i, 1] == this.gameObject)
                    {
                        PawnMovePlate(xBoard, yBoard + 2, controller, player, movePlate, gameObject);
                        PawnMovePlate(xBoard, yBoard + 2, controller, player, movePlate, gameObject);
                    }
                    else
                        PawnMovePlate(xBoard, yBoard + 1, controller, player, movePlate, gameObject);
                }
                break;

        }
    }
}
