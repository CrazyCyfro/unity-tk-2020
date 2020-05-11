using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerScript : MonoBehaviour
{
    public void LoadScene(int scene)
    {
        StartCoroutine(LoadSceneAsync(scene));
    }

    public void LoadScene(string scene)
    {
        StartCoroutine(LoadSceneAsync(scene));
    }

    IEnumerator LoadSceneAsync(int scene)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(scene);

        while (!asyncLoad.isDone) yield return null;
    }

    IEnumerator LoadSceneAsync(string scene)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(scene);

        while (!asyncLoad.isDone) yield return null;
    }
}
