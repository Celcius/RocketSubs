using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    protected Transform endGameMenu;

    public override void StartGameMode()
    {
        if(isRunning)
        {
            return;
        }
        
        fuel.Reset();
        health.Value = maxHealth.Value;
        skipCount.Value = 0;
        usingFuel.Value = false;

        health.OnChange += OnHealthChanged;

        isPlaying.Value = isRunning = true;
        isInputBlocked.Value = false;
    }

    public override void StopGameMode()
    {
        if(!isRunning)
        {
            return;
        }
        
        health.OnChange -= OnHealthChanged;

        isPlaying.Value = isRunning = false;
        isInputBlocked.Value = true;
    }

    private void OnHealthChanged(float oldVal, float newVal)
    {
        if(isRunning && oldVal > 0 && newVal <= 0)
        {
            FinishedGameMode();
        }
    }

    private void FinishedGameMode()
    {
        StopGameMode();
        Instantiate(endGameMenu);
    }
}
