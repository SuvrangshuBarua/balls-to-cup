using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Rotator : MonoBehaviour
{
    #region Public Variable
    [HideInInspector]
    public float dragEffector = 0.25f;
    [Range(1f, 2f)]
    public float dragSensitivity = 1f;
    [Range(5, 20)]
    [Tooltip("rotational deceleration of the tube")]
    public int angularDrag = 10;
    //public Button resetButton;
    
    #endregion

    #region Private Variable
    private Rigidbody2D rigidBody;
    private Vector2 centerOfMass;
    private int dragMultiplier = 100;
    private bool dragged;
    private Vector2 dragAmount;
    private bool isCalledOnce;
    #endregion

    void Start()
    {
        centerOfMass = Vector2.zero;
        rigidBody = GetComponent<Rigidbody2D>();
        rigidBody.centerOfMass = centerOfMass;
        DragHandler.instance.onDragStart += DragStart;
        DragHandler.instance.onDrag_delta += DragDelta;
        DragHandler.instance.onDragEnd += DragEnd;
        isCalledOnce = false;
        //resetButton.onClick.AddListener(ReloadScene);
    }

    private void DragEnd(Vector2 obj)
    {
        dragged = false;
    }

    
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

    public void ApplySettings()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        rigidBody.angularDrag = angularDrag;
        dragEffector = rigidBody.mass * dragMultiplier * dragSensitivity;

    }
    private void ReloadScene()
    {
        SceneManager.LoadScene("Gameplay2D", LoadSceneMode.Single);
    }
}
