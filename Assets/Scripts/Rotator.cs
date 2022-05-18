using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    #region Public Variable
    public float dragEffector = 5;
    #endregion
    #region Private Variable
    private Transform _transform;

    #endregion

    void Start()
    {
        _transform = transform;
        DragHandler.instance.onDragStart += DragStart;
        DragHandler.instance.onDrag_delta += DragDelta;
        DragHandler.instance.onDragEnd += DragEnd;

    }

    private void DragEnd(Vector2 obj)
    {
    }

    private void DragDelta(Vector2 dragAmount)
    {
        Vector3 rotationAmount = new Vector3(_transform.rotation.x, _transform.rotation.y, dragAmount.x/dragEffector);
        _transform.Rotate(rotationAmount);

        Debug.Log($"Drag Direction {dragAmount}");
    }

    private void DragStart(Vector2 dragAmount)
    {
    }

    
}
