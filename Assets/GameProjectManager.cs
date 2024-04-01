using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameProjectManager : MonoBehaviour
{
   
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
                waitingToStartTimer -= Time.deltaTime;
                if (waitingToStartTimer < 0f)
                {
                    state = State.CountdownToStart;
                    
                } 
                break;
            case State.CountdownToStart:
                countdownToSartTimer -= Time.deltaTime;
                if (countdownToSartTimer < 0f)
                {
                    state = State.GamePlaying;

                }
                break;
            case State.GamePlaying:
                gamePlayingTimer -= Time.deltaTime;
                if (gamePlayingTimer < 0f)
                {
                    state = State.GameOver;

                }
                break;
            case State.GameOver:
                break;
        }
        Debug.Log(state);
    }

    public bool IsGamePlaying()
    {
        return state == State.GamePlaying;
    }

}
