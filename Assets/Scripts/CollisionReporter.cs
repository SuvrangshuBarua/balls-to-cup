using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D))]
public class CollisionReporter : MonoBehaviour
{
    [System.Serializable]
    public class ColliderEvent : UnityEvent<Collider2D> { }


    public ColliderEvent OnTriggerEnterEvent;
    public ColliderEvent OnTriggerExitEvent;

    private void OnTriggerEnter2D(Collider2D c)
    {
        OnTriggerEnterEvent?.Invoke(c);
    }
    private void OnTriggerExit2D(Collider2D c)
    {
        OnTriggerExitEvent?.Invoke(c);
    }

}
