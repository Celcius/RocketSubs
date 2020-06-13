using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using AmoaebaAds;
using AmoaebaUtils;
using UnityEngine.SceneManagement;

public class EndGameMenu : MonoBehaviour
{
    [SerializeField]
    private BoolVar wonGame;

    [SerializeField]
    private Button videoAdsbutton;

    [SerializeField]
    private SceneVar menuScene;

    [SerializeField]
    private GameMode currentGameMode;

    [SerializeField]
    private StringVar placementIdWin;

    [SerializeField]
    private StringVar placementIdLose;

    private MenuAdHelper adHelper = null;

    private void Awake()
    {
        adHelper = GetComponent<MenuAdHelper>();
    }

    
    public void WatchAds()
    {
        if(adHelper == null)
        {
            return;
        }

        Action<string, AdsResult> callback = (string placement, AdsResult result) => 
        { 
            if(result == AdsResult.Finished)
            {
                currentGameMode.HandleAdReward(placement);
            }
        };

        adHelper.WatchAds(wonGame? placementIdWin.Value : placementIdLose.Value, 
                          callback);
    }

    public void Exit()
    {
        SceneManager.LoadScene(menuScene.Value.handle, LoadSceneMode.Single);
    }

    public void Replay()
    {
        currentGameMode.PlayGameMode();
    }
    
}
