using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallPhysics : MonoBehaviour
{
    [Tooltip("Detemines how friction effects")]
    public int drag  = 0;
    [Tooltip("Check true to apply mass according to scale or false to override")]

    public bool applyAutoMass;
    [ConditionalPropertyAttribuite(ActionOnConditionTrue.Disable, ConditionOperator.And, nameof(applyAutoMass))]
    public float mass = 0.5f;

    private Rigidbody2D[] rigidbodyArray;
    
    
    private void Start()
    {
        
    }
    private bool PopulatePropertyList()
    {
        rigidbodyArray = transform.GetComponentsInChildren<Rigidbody2D>();
        return rigidbodyArray.Length > 0 ? true : false;
    }
    private void AssignProperties()
    {
        if(PopulatePropertyList())
        {
            foreach (Rigidbody2D rb in rigidbodyArray)
            {
                rb.drag = this.drag;
                rb.useAutoMass = applyAutoMass;
                if(!applyAutoMass)
                {
                    rb.mass = this.mass;
                }               
            }
        }
        else
        {
            $"Instantiate ball at first using SpawnRandomizer".Debug("FF00FF");
        }
        
    }

    public void ApplySettings()
    {
        AssignProperties();
    }

}
