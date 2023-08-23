using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoadManager 
{
    


    public static void loadLevel(int levelIndex)
    {
        SceneManager.LoadScene(levelIndex);
    }
}
