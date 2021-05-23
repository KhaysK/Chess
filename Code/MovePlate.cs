using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlate : MonoBehaviour
{

    public GameObject controller;
    public GameObject reference = null;

    int coordX;
    int coordY;

    public bool atack = false;

    void Start()
    {
        // Если мы можем сделать ход атаки то рамка на этом ходе будет красного цвета
        if (atack)
        {
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1.0f, 0.0f, 0.0f, 1.0f);
        }
    }

    public void OnMouseUp()
    {
        // Отслеживает нажатие мыши если игрок атакует удаляет фигуру опонента,
        // обнуляет ячейку где была записанна данная фигура, а затем ставит
        // нашу фигуру на место фигуры опонента, если просто перемещение, то
        // двигает фигуру на выбранное место
        controller = GameObject.FindGameObjectWithTag("GameController");

        if (atack)
        {
            GameObject cp = controller.GetComponent<Controller>().position[coordX, coordY];

            if (cp.name == "white_king") controller.GetComponent<Controller>().Winner("Черные");
            if (cp.name == "black_king") controller.GetComponent<Controller>().Winner("Белые");

            Destroy(cp);
        }

        controller.GetComponent<Controller>().SetPositionEmpty(reference.GetComponent<ChessPiece>().xBoard,
            reference.GetComponent<ChessPiece>().yBoard);

        reference.GetComponent<ChessPiece>().xBoard = coordX;
        reference.GetComponent<ChessPiece>().yBoard = coordY;
        reference.GetComponent<ChessPiece>().SetCoords();

        controller.GetComponent<Controller>().SetPosition(reference);

        controller.GetComponent<Controller>().NextTurn(); 

        reference.GetComponent<ChessPiece>().DestroyMovePlates();
    }

    public void SetCoords(int x, int y)
    {
        // Устанваливает координаты объекта
        coordX = x;
        coordY = y;
    }
}
