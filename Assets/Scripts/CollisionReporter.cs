using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CollisionReporter : MonoBehaviour
{
    //[System.Serializable]
    //public class ColliderEvent : UnityEvent<Collider2D> { }

    //public LayerMask collisionLayer;
    //public bool useOneTimeOnlyCollision;
    //public ColliderEvent OnTriggerEnterEvent;
    //public ColliderEvent OnTriggerExitEvent;

    //private bool isCollidedOnce;

    //private void Awake()
    //{
    //    isCollidedOnce = false;
    //}
    //private void OnTriggerEnter2D(Collider2D c)
    //{
    //    if((collisionLayer.value & (1 << c.gameObject.layer)) > 0)
    //    {
    //        if(useOneTimeOnlyCollision && !isCollidedOnce)
    //        {
    //            isCollidedOnce = true;
    //            OnTriggerEnterEvent?.Invoke(c);
    //        }

    //    }

    //}
    //private void OnTriggerExit2D(Collider2D c)
    //{
    //    if ((collisionLayer.value & (1 << c.gameObject.layer)) > 0)
    //    {
    //        OnTriggerExitEvent?.Invoke(c);
    //    }

    //}
    public UnityEvent onCollisonEvent;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PointProvider ball = collision.gameObject.GetComponent<PointProvider>();
        if (ball != null && !ball.isPointProvidedOnce)
        {
            ball.isPointProvidedOnce = true;
            onCollisonEvent.Invoke();
        }

    }

}
