using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameProjectManager : MonoBehaviour
{
    public static GameProjectManager Instance {  get; private set; }

    public event EventHandler OnStateChanged;  //An event that can be called (if implemented the right way) to see when the game changes states.


    private enum State //All the gamestates of the game
    {
        WaitingToStart,
        CountdownToStart,
        GamePlaying,
        GameOver,
    }

    private State state;
    private float waitingToStartTimer = 1f;
    private float countdownToSartTimer = 3f;
    private float gamePlayingTimer = 10f;

    private void Awake() //When the game starts, choose which state to start in
    {
        state = State.WaitingToStart;
    }


    private void Update()
    {
        switch (state) //Switch between gamestates & set the conditions for swapping to other gameStates.
        {
            case State.WaitingToStart:
                waitingToStartTimer -= Time.deltaTime;   //Start the countdown for this state, in the future this will be removed
                if (waitingToStartTimer < 0f)    //The conditions to change the state, for now each state is timed to test them.
                {
                    state = State.CountdownToStart;
                    OnStateChanged?.Invoke(this, EventArgs.Empty); //A notification/event that the state has changed.
                } 
                break;
            case State.CountdownToStart:
                countdownToSartTimer -= Time.deltaTime;
                if (countdownToSartTimer < 0f)
                {
                    state = State.GamePlaying;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);

                }
                break;
            case State.GamePlaying:
                gamePlayingTimer -= Time.deltaTime;
                if (gamePlayingTimer < 0f)
                {
                    state = State.GameOver;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);

                }
                break;
            case State.GameOver:
                break;
        }
        //Debug.Log(state);
    }

    public bool IsGamePlaying()
    {
        return state == State.GamePlaying;
    }


    public bool IsCountdownToStartActive()
    {
        return state == State.CountdownToStart;
    }

    public float GetCountdownToStartTimer()
    {
        //return countdownToSartTimer;
        return 5;
    }

    public bool IsGameOver()
    {
        return state == State.GameOver;
    }
}
