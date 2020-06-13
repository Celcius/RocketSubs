using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using AmoaebaUtils;

public class GameMode : ScriptableObject
{
    [SerializeField]
    private SceneVar sceneVar;
    protected bool isRunning = false;

    private void OnEnable()
    {
        isRunning = false;
        StopGameMode();
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnLevelFinishedLoading;
    }

    protected Scene GetScene()
    {
        return SceneManager.GetSceneByBuildIndex(sceneVar.Value.handle);
    }
    private void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        Scene actualScene = GetScene();
        
        if(actualScene != null && scene.name.CompareTo(actualScene.name) == 0)
        {
            StartGameMode();
        }
        else if(isRunning)
        {
            StopGameMode();
        }
    }

    public void PlayGameMode()
    {
        SceneManager.LoadScene(sceneVar.Value.handle, LoadSceneMode.Single);
    }

    public virtual void HandleAdReward(string placementId)
    {

    }
    
    public virtual void StopGameMode()
    {
        if(!isRunning)
        {
            return;
        }
        isRunning = false;
    }
    public virtual void StartGameMode()
    {
        if(isRunning)
        {
            return;
        }
        isRunning = true;
    }
}
