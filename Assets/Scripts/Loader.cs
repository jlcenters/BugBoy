using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;




public enum Scenes
{
    MainMenu,
    Loading,
    Prototype
}
public static class Loader
{
    private static Scenes targetScene;



    public static void Load(Scenes targetScene)
    {
        Loader.targetScene = targetScene;

        SceneManager.LoadScene(Scenes.Loading.ToString());
    }



    public static IEnumerator LoaderCallback()
    {
        Debug.Log("Load called back");
        yield return new WaitForSeconds(2f);
        Debug.Log("waited");

        SceneManager.LoadScene(targetScene.ToString());
        Debug.Log("scene loaded");

    }
}
