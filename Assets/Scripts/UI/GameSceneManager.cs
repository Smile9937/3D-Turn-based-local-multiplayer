using System;
using UnityEngine.SceneManagement;
public static class GameSceneManager
{
    private static Scene _activeScene;
    public static void SetScene()
    {
        _activeScene = (Scene)Enum.Parse(typeof(Scene), SceneManager.GetActiveScene().name);
    }
    public static void GoToScene(Scene scene)
    {
        _activeScene = scene;
        SceneManager.LoadScene(_activeScene.ToString());
    }

    public static void GoToLevel(int level)
    {
        _activeScene = (Scene)Enum.Parse(typeof(Scene), $"Level{level}");
        SceneManager.LoadScene(_activeScene.ToString());
    }

    public static bool IsInLevel()
    {
        return _activeScene.ToString().Contains("Level");
    }
    public static Scene GetScene()
    {
        return _activeScene;
    }

    public static int GetLevel()
    {
        string[] sceneName = _activeScene.ToString().Split("Level");
        int level = int.Parse(sceneName[1]);
        return level;
    }
}