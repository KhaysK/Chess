using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Controller : MonoBehaviour
{
    public GameObject chessPiece;
    public GameObject PanelDead;
    public Text WinText;

    // Массивы сетки позиционирования и фигур каждого игрока
    public GameObject[,] position = new GameObject[8, 8];
    private GameObject[] playerBlack = new GameObject[16];
    private GameObject[] playerWhite = new GameObject[16];

    public string currentPlayer = "white";

    private bool gameOver = false;

    void Start()
    {
        // Заполняем массивы фигурами для каждого игрока
        playerWhite = new GameObject[]
        {
            Create("white_rook", 0, 0), Create("white_knight", 1, 0), Create("white_bishop", 2, 0), Create("white_queen", 3, 0),
            Create("white_king", 4, 0), Create("white_bishop", 5, 0), Create("white_knight", 6, 0), Create("white_rook", 7, 0),
            Create("white_pawn", 0, 1), Create("white_pawn", 1, 1), Create("white_pawn", 2, 1), Create("white_pawn", 3, 1),
            Create("white_pawn", 4, 1), Create("white_pawn", 5, 1), Create("white_pawn", 6, 1), Create("white_pawn", 7, 1)
        };

        playerBlack = new GameObject[]
        {
            Create("black_rook", 0, 7), Create("black_knight", 1, 7), Create("black_bishop", 2, 7), Create("black_queen", 3, 7),
            Create("black_king", 4, 7), Create("black_bishop", 5, 7), Create("black_knight", 6, 7), Create("black_rook", 7, 7),
            Create("black_pawn", 0, 6), Create("black_pawn", 1, 6), Create("black_pawn", 2, 6), Create("black_pawn", 3, 6),
            Create("black_pawn", 4, 6), Create("black_pawn", 5, 6), Create("black_pawn", 6, 6), Create("black_pawn", 7, 6)
        };

        for (int i = 0; i < playerWhite.Length; i++)
        {
            SetPosition(playerWhite[i]);
            SetPosition(playerBlack[i]);
        }
        
    }

    public GameObject Create(string name, int x, int y)
    {
        // Создает фигуру, задает ей имя и кординаты, а затем растовляет эту фигуру на доске
        GameObject obj = Instantiate(chessPiece, new Vector3(0, 0, -1), Quaternion.identity);
        ChessPiece cp = obj.GetComponent<ChessPiece>();
        cp.name = name;
        cp.xBoard = x;
        cp.yBoard = y;
        cp.Activate();
        return obj;
    }

    public void SetPosition(GameObject obj)
    {
        // Заполняет массив сетки позиционирования ( помещает фигуру в массив соответсвенно ее координатам на доске)
        ChessPiece cp = obj.GetComponent<ChessPiece>();

        position[cp.xBoard, cp.yBoard] = obj;
    }  

    public bool PositionOnBoard(int x, int y)
    {
        // Проверяет находятся ли данные координаты на доске или они за пределами ее
        if (x < 0 || y < 0 || x >= position.GetLength(0) || y >= position.GetLength(1)) return false;
        return true;
    }

    public void SetPositionEmpty(int x, int y)
    {
        // Удаляет объект записанный в данной ячейке
        position[x, y] = null;
    }

    public bool IsGameOver()
    {
        // Возыращает переменную gameOver для проверки состояния игры
        return gameOver;
    }

    public void NextTurn()
    {
        // Переключение ходов
        if (currentPlayer == "white") currentPlayer = "black";
        else currentPlayer = "white";
    }

    public void Winner(string playerWinner)
    {
        // Выводит на экране панель с текстом 
        gameOver = true;

        WinText.text = playerWinner + " выйграли!";

        PanelDead.SetActive(true);
    }

    public void Restart()
    {
        // Перезапуск сцены
        SceneManager.LoadScene("Game");
    }

    public void GoToMenu()
    {
        // Выход в главное меню
        SceneManager.LoadScene("Menu");
    }
}
