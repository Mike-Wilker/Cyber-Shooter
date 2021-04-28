using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    private void Awake()
    {
        Cursor.lockState = CursorLockMode.None;
    }
    public void startGame()
    {
        SceneManager.LoadScene("Level1");
    }
    public void showInstructions()
    {
        Instantiate(Resources.Load<Transform>(@"Prefabs\Menus\Instructions"), transform);
    }
    public void showCredits()
    {
        Instantiate(Resources.Load<Transform>(@"Prefabs\Menus\Credits"), transform);
    }
    public void exit()
    {
        Application.Quit();
    }
}
