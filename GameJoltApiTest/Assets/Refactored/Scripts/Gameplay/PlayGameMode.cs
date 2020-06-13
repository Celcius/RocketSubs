using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AmoaebaUtils;

#if UNITY_ENGINE
using UnityEditor;
#endif

public class PlayGameMode : GameMode
{
    
    [SerializeField]
    private BoolVar isInputBlocked;

    [SerializeField]
    protected FloatVar fuel;

    [SerializeField]
    protected FloatVar health;

    [SerializeField]
    protected FloatVar maxHealth;

    [SerializeField]
    protected IntVar skipCount;

    [SerializeField]
    protected BoolVar usingFuel;

    [SerializeField]
    protected BoolVar isPlaying;

    [SerializeField]
    protected BoolVar wonGame;

    [SerializeField]
    protected Transform endGameMenu;

    [SerializeField]
    protected IntVar collectedParts;

    [SerializeField]
    protected IntVar totalParts;

    [SerializeField]
    protected FloatVar totalTime;

    [SerializeField]
    protected FloatVar time;
    private CoroutineRunner runner;
    public override void StartGameMode()
    {
#if UNITY_ENGINE
        bool appRunning = Application.isPlaying || EditorApplication.isPlayingOrWillChangePlaymode;
#else
        bool appRunning = true;
#endif
        if(isRunning || !appRunning)
        {
            return;
        }

        fuel.Reset();
        health.Value = maxHealth.Value;
        time.Value = totalTime.Value;
        usingFuel.Value = false;
        
        skipCount.Value = 0;
        collectedParts.Value = 0;

        time.OnChange += CheckEndGame;
        health.OnChange += CheckEndGame;

        isPlaying.Value = isRunning = true;
        isInputBlocked.Value = false;
        wonGame.Value = false;

        if(runner != null)
        {
            Destroy(runner);
        }

        runner = CoroutineRunner.Instantiate(this.name);
        runner.StartCoroutine(RunGameTimer());
    }

    public override void StopGameMode()
    {
        if(isRunning || !Application.isPlaying)
        {
            return;
        }

        Destroy(runner);
        runner = null;
        time.OnChange -= CheckEndGame;
        health.OnChange -= CheckEndGame;

        isPlaying.Value = isRunning = false;
        isInputBlocked.Value = true;
    }

    private void CheckEndGame(float oldVal, float newVal)
    {
        if(isRunning && oldVal > 0 && newVal <= 0)
        {
            EndGame(health.Value > 0);
        }
    }

    public void EndGame(bool won)
    {
        StopGameMode();
        wonGame.Value = won;
        Instantiate(endGameMenu);
    }

    private IEnumerator RunGameTimer()
    {
        while(time.Value > 0)
        {
            time.Value -= Time.deltaTime;
            yield return new WaitForEndOfFrame(); 
        }
        CheckEndGame(0,time.Value);
    }
}
