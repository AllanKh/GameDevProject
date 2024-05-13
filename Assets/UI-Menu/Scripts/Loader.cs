using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class Loader
{

    public enum Scene //ADD ALL THE SCENES USED IN THE GAME!!! Check spelling.
    {
        MainMenuScene,
        LoadingScene,
        Paulo,
        Dennis
    }



    private static Scene targetScene;

    public static void Load(Scene targetScene)
    {
        Loader.targetScene = targetScene; 

        SceneManager.LoadScene(Scene.LoadingScene.ToString()); //Initiate the loading scene so the game is not just frozen on the main menu

    }



    public static void LoaderCallback()
    {
        SceneManager.LoadScene(targetScene.ToString()); //After the loaderScene has been initiated, load the actual scene that we want to get to.
    }



}
