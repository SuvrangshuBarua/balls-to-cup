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
    //public List<Rigidbody> ballRigidbodies = new List<Rigidbody>();
    public Button resetButton;
    public float minimumVelocity = 1;
    #endregion
    #region Private Variable
    private Transform _transform;
    private float defaultFixedDeltaTime;
    #endregion

    void Start()
    {
        _transform = transform;
        DragHandler.instance.onDragStart += DragStart;
        DragHandler.instance.onDrag_delta += DragDelta;
        DragHandler.instance.onDragEnd += DragEnd;
        defaultFixedDeltaTime = Time.fixedDeltaTime;
        resetButton.onClick.AddListener(ReloadScene);
    }

    private void ReloadScene()
    {
        SceneManager.LoadScene("Gameplay", LoadSceneMode.Single);
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
            //Vector3 rotationAmount = Vector3.zero;
            //rotationAmount += new Vector3(_transform.rotation.x, _transform.rotation.y, dragEffector * dragAmount.x);
            ////rigidBody.rotation = Quaternion.Euler(rotationAmount);
            ////rigidBody.AddTorque(rotationAmount);

            //Quaternion deltaRotation = Quaternion.Euler(rotationAmount * Time.fixedDeltaTime);

            rigidBody.AddTorque(dragEffector * dragAmount.x,ForceMode2D.Force);

            //rigidBody.MoveRotation(rigidBody.rotation * deltaRotation);
        }
    }
    private void DragDelta(Vector2 dragAmount)
    {
        dragged = true;
        this.dragAmount = dragAmount;



        //if(rigidBody.angularVelocity.z >= minimumVelocity)
        //{
        //    foreach (Rigidbody item in ballRigidbodies)
        //    {
        //        //item.isKinematic = true;
        //        //item.useGravity = false;
        //        //Time.fixedDeltaTime = 0.01f;
        //    }

        //}
        //else
        //{
        //    foreach (Rigidbody item in ballRigidbodies)
        //    {
        //        //item.isKinematic = false;
        //        //item.useGravity = true;
        //        //Time.fixedDeltaTime = defaultFixedDeltaTime;
        //    }
        //}
        //Debug.Log($"Drag Direction {dragAmount}");
    }

    private void DragStart(Vector2 dragAmount)
    {
    }

    
}
