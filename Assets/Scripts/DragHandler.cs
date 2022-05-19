using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragHandler : MonoBehaviour
{
    const float defWidth = 900;
    public float minimumPixelAmountToDrag = 15;
    public event System.Action<Vector2> onDragStart;
    public event System.Action<Vector2> onDrag_total;
    public event System.Action<Vector2> onDrag_delta;
    public event System.Action<Vector2> onDragEnd;
    public event System.Action<Vector3> onTap;
    float minimalPixelCount;

    public static DragHandler instance;
    public Drag currentDrag = null;
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        
        minimalPixelCount = (minimumPixelAmountToDrag / defWidth) * Screen.width;
    }

    void D_Start()
    {
        currentDrag = new Drag(Input.mousePosition, minimalPixelCount, onDragStart);
    }
    void D_Update()
    {
        if (currentDrag != null)
        {
            //bool dragWasPending = !currentDrag.dragConfrimed;
            //Vector2 v = currentDrag.GetDragAmount(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
            //if (!dragWasPending) onDrag?.Invoke(v);
            Vector2 totalTravel;
            Vector2 deltaTravel;
            if (currentDrag.GetDragAmount(new Vector2(Input.mousePosition.x, Input.mousePosition.y), out totalTravel, out deltaTravel))
            {
                onDrag_total?.Invoke(totalTravel);
                onDrag_delta?.Invoke(deltaTravel);
            }
        }
    }
    void D_End()
    {
        if (currentDrag != null)
        {
            if (currentDrag.dragConfrimed)
            {
                onDragEnd?.Invoke(Input.mousePosition);
            }
            else
            {
                onTap?.Invoke(Input.mousePosition);
            }
        }
        currentDrag = null;
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            D_Start();
        }
        else if (Input.GetMouseButton(0))
        {
            if (currentDrag == null)
            {
                D_Start();
            }
            else
            {
                D_Update();
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            D_End();
        }
        else
        {
            if (currentDrag != null)
            {
                D_End();
            }
        }
    }
}

public class Drag
{
    event System.Action<Vector2> onDragConfirmed;
    Vector2 startPos;
    Vector2 lastPos;
    public bool dragConfrimed { get; private set; }
    public Vector2 travelVec { get { return lastPos - startPos; } }
    float startTime;
    float minPixel = 100;

    public bool didntDragFarEnough
    {
        get
        {
            return travelVec.magnitude < minPixel;
        }
    }
    public Drag(Vector2 startPosition, float minPix, System.Action<Vector2> onDragConfirmed)
    {
        this.minPixel = minPix;
        startTime = Time.time;
        startPos = startPosition;
        lastPos = startPosition;
        if (minPix < 0)
        {
            dragConfrimed = true;
            onDragConfirmed?.Invoke(startPos);

        }
        else
        {
            this.onDragConfirmed = onDragConfirmed;
        }

    }
    //public Vector2 GetDragAmount(Vector2 currentPosition)
    //{
    //    lastPos = currentPosition;

    //    if (didntDragFarEnough)
    //    {
    //        return Vector2.zero;
    //    }
    //    else
    //    {
    //        if(!dragConfrimed)
    //        {
    //            dragConfrimed = true;
    //            onDragConfirmed?.Invoke(startPos);
    //        }
    //        return travelVec;
    //    }
    //}

    public bool GetDragAmount(Vector2 currentPosition, out Vector2 totalTravel, out Vector2 deltaTravel)
    {
        deltaTravel = currentPosition - lastPos;
        lastPos = currentPosition;
        totalTravel = travelVec;
        //Debug.Log(travelVec);

        if (didntDragFarEnough)
        {
            return false;
        }
        else
        {
            if (!dragConfrimed)
            {
                dragConfrimed = true;
                onDragConfirmed?.Invoke(startPos);
                return false;
            }
            return true;
        }
    }
}
