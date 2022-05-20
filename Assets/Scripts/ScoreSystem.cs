using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ScoreSystem : MonoBehaviour
{
    public bool shouldResetScore;
    public IntVariable startingScore;
    public IntVariable Score;
    public UnityEvent OnPointCollectedEvent;

    private void Start()
    {
        if (shouldResetScore) Score.SetValue(startingScore);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PointProvider ball = collision.gameObject.GetComponent<PointProvider>();
        if (ball != null && !ball.isPointProvidedOnce)
        {
            ball.isPointProvidedOnce = true;
            Score.ApplyChange(ball.point);
            OnPointCollectedEvent.Invoke();
        }

    }

}
