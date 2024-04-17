using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Control : MonoBehaviour
{
    public Animator animate;
    public GameObject Button;
    public void ReloadScene(){
        Resources.UnloadUnusedAssets();
        StartCoroutine(LoadSceneAsync("Menu"));
       
    }
    private IEnumerator LoadSceneAsync(string name){
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(name);
        while (!asyncLoad.isDone){
            yield return null;
        }
    }
    public void OnStart(){
        animate.SetBool("isTransition",true);
        // StartCoroutine(LoadSceneAsync("GamePlay"));
    }
    public void Quit(){
        Application.Quit();
    }
}
