using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public GameObject DeadPanel;
    public void Play()
    {
        // Загрузка сцены с игрой
        SceneManager.LoadScene("Game");
    }

    public void Info()
    {
        // Отображения панели уведомления
        StartCoroutine(WaitAndShow());
    }

    IEnumerator WaitAndShow()
    {
        // Дополняет Info реализует зажержку
        DeadPanel.SetActive(true);
        yield return new WaitForSeconds(3);
        DeadPanel.SetActive(false);
    }
}
