using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EndScreenManager : MonoBehaviour
{
    [SerializeField] private TMP_Text _winnerText;

    private static EndScreenManager instance;
    public static EndScreenManager Instance { get { return instance; } }

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetWinnerText(string name)
    {
        _winnerText.text = $"The winner is:\r\n{name}!";
    }

    public void MainMenu()
    {
        GameSceneManager.GoToScene(Scene.MainMenu);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
