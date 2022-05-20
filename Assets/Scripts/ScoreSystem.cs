using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ScoreSystem : MonoBehaviour
{
    public bool shouldResetData;

    public IntVariable initialScore;
    public IntVariable score;
    public IntVariable targetScore;
    public IntVariable initialBallNumber;
    public IntVariable availabeBallNumber;

    public CollisionReporter lostBallReporter;

    public UnityEvent OnPointCollectedEvent;
    public UnityEvent OnLevelWinEvent;
    public UnityEvent OnLevelLoseEvent;
    

    private CollisionReporter collisionReporter;

    private void Start()
    {
        if (shouldResetData)
        {
            score.SetValue(initialScore);
            availabeBallNumber.SetValue(initialBallNumber);

        }
        collisionReporter = GetComponent<CollisionReporter>();
        collisionReporter?.OnTriggerEnterEvent.AddListener(TriggerEnterCallback);
        if (lostBallReporter != null) lostBallReporter?.OnTriggerEnterEvent.AddListener(CheckFailCondition);
    }

    
    private void OnDestroy()
    {
        collisionReporter?.OnTriggerEnterEvent.RemoveListener(TriggerEnterCallback);
        if (lostBallReporter != null) lostBallReporter?.OnTriggerEnterEvent.RemoveListener(CheckFailCondition);
    }
    private void TriggerEnterCallback(Collider2D collider)
    {
        BallProperty ball = collider.gameObject.GetComponent<BallProperty>();
        if (ball != null && !ball.isPointProvidedOnce)
        {
            ball.isPointProvidedOnce = true;
            score.ApplyChange(ball.point);
            OnPointCollectedEvent.Invoke();
        }
        if (score.Value >= targetScore.Value)
        {
            OnLevelWinEvent?.Invoke();
        }
    }
    private void CheckFailCondition(Collider2D collider)
    {
        BallProperty ball = collider.gameObject.GetComponent<BallProperty>();
        if (ball != null && !ball.isLost)
        {
            ball.isLost = true;
            availabeBallNumber.ApplyChange(-1);
        }
        if(availabeBallNumber.Value < targetScore.Value)
        {
            OnLevelLoseEvent?.Invoke();
        }
    }


}
