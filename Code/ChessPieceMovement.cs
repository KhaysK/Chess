using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessPieceMovement : MonoBehaviour
{
    public void LineMovePlate(int xIncrement, int yIncrement, int pieceCoordX, int pieceCoordY,
        GameObject controller, string player, GameObject movePlate, GameObject piece)
    {
        // Реализует движение фигур по прямым линиям или диагоналям 
        Controller contr = controller.GetComponent<Controller>();

        int x = pieceCoordX + xIncrement;
        int y = pieceCoordY + yIncrement;

        while(contr.PositionOnBoard(x,y) && contr.position[x,y] == null)
        {
            MovePlateSpawn(x, y, movePlate, piece);
            x += xIncrement;
            y += yIncrement;
        }

        if(contr.PositionOnBoard(x, y) && contr.position[x,y].GetComponent<ChessPiece>().player != player)
        {
            MovePlateAttackSpawn(x, y, movePlate, piece);
        }
    }

    public void LMovePlate(int pieceCoordX, int pieceCoordY, GameObject controller, 
        string player, GameObject movePlate, GameObject piece)
    {
        // Реализация движения коня
        PointMovePlate(pieceCoordX + 1, pieceCoordY + 2, controller, player, movePlate, piece);
        PointMovePlate(pieceCoordX - 1, pieceCoordY + 2, controller, player, movePlate, piece);
        PointMovePlate(pieceCoordX + 2, pieceCoordY + 1, controller, player, movePlate, piece);
        PointMovePlate(pieceCoordX + 2, pieceCoordY - 1, controller, player, movePlate, piece);
        PointMovePlate(pieceCoordX + 1, pieceCoordY - 2, controller, player, movePlate, piece);
        PointMovePlate(pieceCoordX - 1, pieceCoordY - 2, controller, player, movePlate, piece);
        PointMovePlate(pieceCoordX - 2, pieceCoordY + 1, controller, player, movePlate, piece);
        PointMovePlate(pieceCoordX - 2, pieceCoordY - 1, controller, player, movePlate, piece);
    }

    public void SurroundMovePlate(int pieceCoordX, int pieceCoordY, GameObject controller,
        string player, GameObject movePlate, GameObject piece)
    {
        PointMovePlate(pieceCoordX, pieceCoordY + 1, controller, player, movePlate, piece);
        PointMovePlate(pieceCoordX, pieceCoordY - 1, controller, player, movePlate, piece);
        PointMovePlate(pieceCoordX + 1, pieceCoordY, controller, player, movePlate, piece);
        PointMovePlate(pieceCoordX - 1, pieceCoordY, controller, player, movePlate, piece);
        PointMovePlate(pieceCoordX - 1, pieceCoordY - 1, controller, player, movePlate, piece);
        PointMovePlate(pieceCoordX - 1, pieceCoordY + 1, controller, player, movePlate, piece);
        PointMovePlate(pieceCoordX + 1, pieceCoordY - 1, controller, player, movePlate, piece);
        PointMovePlate(pieceCoordX + 1, pieceCoordY + 1, controller, player, movePlate, piece);
    }

    public void PointMovePlate(int x, int y, GameObject controller, string player, GameObject movePlate, GameObject piece)
    {
        // Проверяет находится ли в клетке движения фигура, и если да, то смотрит враг это или нет
        Controller contr = controller.GetComponent<Controller>();

        if (contr.PositionOnBoard(x, y))
        {
            GameObject cp = contr.position[x, y];

            if(cp == null)
            {
                MovePlateSpawn(x, y, movePlate, piece);
            }
            else if (cp.GetComponent<ChessPiece>().player != player)
            {
                MovePlateAttackSpawn(x, y, movePlate, piece);
            }
        }
    }

    public void PawnMovePlate(int x, int y, GameObject controller, string player, GameObject movePlate, GameObject piece)
    {
        // Реализация движения пешки
        Controller contr = controller.GetComponent<Controller>();

        if (contr.PositionOnBoard(x, y))
        {
            if (contr.position[x, y] == null)
            {
                MovePlateSpawn(x, y, movePlate, piece);
            }

            if (contr.PositionOnBoard(x + 1, y) && contr.position[x + 1, y] != null &&
                contr.position[x + 1, y].GetComponent<ChessPiece>().player != player)
            {
                MovePlateAttackSpawn(x + 1, y, movePlate, piece);
            }
            if (contr.PositionOnBoard(x - 1, y) && contr.position[x - 1, y] != null &&
                contr.position[x - 1, y].GetComponent<ChessPiece>().player != player)
            {
                MovePlateAttackSpawn(x - 1, y, movePlate, piece);
            }
        }
    }

    public void MovePlateSpawn(int coordX,int coordY, GameObject movePlate, GameObject piece)
    {
        // Создание коретки движения
        float x = coordX;
        float y = coordY;

        x *= 2f;
        y *= 1.98f;

        GameObject mp = Instantiate(movePlate, new Vector3(x, y, -3.0f), Quaternion.identity);

        MovePlate mpScript = mp.GetComponent<MovePlate>();
        mpScript.reference = piece;
        mpScript.SetCoords(coordX, coordY);
    }

    public void MovePlateAttackSpawn(int coordX, int coordY, GameObject movePlate, GameObject piece)
    {
        // Создание коретки атаки
        float x = coordX;
        float y = coordY;

        x *= 2f;
        y *= 1.98f;

        GameObject mp = Instantiate(movePlate, new Vector3(x, y, -3.0f), Quaternion.identity);

        MovePlate mpScript = mp.GetComponent<MovePlate>();
        mpScript.atack = true;
        mpScript.reference = piece;
        mpScript.SetCoords(coordX, coordY);
    }
}
