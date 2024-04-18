using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Control : MonoBehaviour
{
    public Animator animate;
    public GameObject ButtonStart;
    public GameObject ButtonQuit;
    void Start(){
        animate.SetBool("isTransition",false);
    }
    public void ReloadScene(){
        Resources.UnloadUnusedAssets();
        // StartCoroutine(LoadSceneAsync("Menu"));
        SceneManager.LoadScene("Menu");
       
    }
    private IEnumerator LoadSceneAsync(string name){
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(name);
        while (!asyncLoad.isDone){
            float proress = Mathf.Clamp01(asyncLoad.progress/0.9f);
            if (asyncLoad.progress >=0.9f){
                asyncLoad.allowSceneActivation = true;
            }
            yield return null;
        }
    }
    public void OnStart(){
        animate.SetBool("isTransition",true);
        ButtonQuit.SetActive(false);
        ButtonStart.SetActive(false);
        Invoke("ChageScene",4f);
    }
    void ChageScene(){
           StartCoroutine(LoadSceneAsync("GamePlay"));
    }
    public void Quit(){
        Application.Quit();
    }
}
