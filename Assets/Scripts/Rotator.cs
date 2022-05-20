using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Rotator : MonoBehaviour
{
    #region Public Variable
    public float dragEffector = 0.25f;
    public Rigidbody2D rigidBody;
    public Button resetButton;
    public Vector2 centerOfMass;
    #endregion
    #region Private Variable
    #endregion

    void Start()
    {
        rigidBody.centerOfMass = centerOfMass;
        DragHandler.instance.onDragStart += DragStart;
        DragHandler.instance.onDrag_delta += DragDelta;
        DragHandler.instance.onDragEnd += DragEnd;
        resetButton.onClick.AddListener(ReloadScene);
    }

    private void ReloadScene()
    {
        SceneManager.LoadScene("Gameplay2D", LoadSceneMode.Single);
    }

    private void DragEnd(Vector2 obj)
    {
        dragged = false;
    }

    bool dragged;
    Vector2 dragAmount;
    private void FixedUpdate()
    {
        if (dragged)
        {
            rigidBody.AddTorque(dragEffector * dragAmount.x,ForceMode2D.Force);
        }
    }
    private void DragDelta(Vector2 dragAmount)
    {
        dragged = true;
        this.dragAmount = dragAmount;      
    }

    private void DragStart(Vector2 dragAmount)
    {
    }

    
}
