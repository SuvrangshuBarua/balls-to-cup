using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Random = UnityEngine.Random;

#if UNITY_EDITOR
[ExecuteInEditMode]
#endif
public class SpawnRandomizer : MonoBehaviour
{
    public Gradient gradient;
    [Header("Gizmo Sphere Properties")]
    [Tooltip("This transform is used to determine the center of UnitSphere")]
    public Transform sphereCenterTransform;
    [HideInInspector]
    public float sphereRadius = 3.5f;
    [Header("Instantiation Settings")]
    public GameObject ballPrefab;
    [Range(5, 50)]
    public int instanceNumber = 20;
    [Tooltip("Equal value as scale property of prefab")]
    public float minimumDistance = 1;
    public List<GameObject> spawnedObject = new List<GameObject>();


    private const int MAX_ATTEMPTS = 0;

    
    private void InstantiateSpheres(int number)
    {
        Vector2 center = transform.position + sphereCenterTransform.position;
        var minDistanceSqr = minimumDistance * minimumDistance;
        var usedPositions = new List<Vector3>(instanceNumber); ;
        Vector3 center3 = new Vector3(center.x, center.y, 0);
        for (int i = 0; i < number; i++)
        {
            var attempts = 0;
            Vector3 instancePosition = Vector3.zero;
            Vector3 instancePosZ;
            do
            {
                instancePosition = RandomOnSphere(center3, sphereRadius);
                instancePosZ = new Vector3(instancePosition.x, instancePosition.y, 0);
                attempts++;
                if (attempts >= MAX_ATTEMPTS)
                {
                    //"Unable to find free spot".Debug("CD0000");
                }

            }
            while (usedPositions.Any(p => (p - instancePosZ).sqrMagnitude <= minDistanceSqr));

            
            var instanceRotation = Quaternion.FromToRotation(Vector3.forward, center3 - instancePosZ);
            GameObject ball = Instantiate(ballPrefab, instancePosZ, instanceRotation, this.transform);
            AssignRandomColorFromGradient(ball);
            spawnedObject.Add(ball);
            usedPositions.Add(instancePosZ);
        }
    }
    private void AssignRandomColorFromGradient(GameObject instance)
    {
        var tempMaterial = new Material(instance.GetComponent<MeshRenderer>().sharedMaterial);
        tempMaterial.color = gradient.Evaluate(UnityEngine.Random.Range(0f, 1f));
        instance.GetComponent<MeshRenderer>().sharedMaterial = tempMaterial;
    }
    private Vector3 RandomOnSphere(Vector3 center, float radius)
    {
        return center + Random.onUnitSphere * radius;
    }
    private void ClearAllInstances()
    {
        if (spawnedObject.Count > 0)
        {
            foreach (GameObject item in spawnedObject)
            {
                DestroyImmediate(item);
            }
            spawnedObject.Clear();
        }
        else
            return;
        
    }
    public void SpawnRandomBalls()
    {
        ClearAllInstances();
        InstantiateSpheres(instanceNumber);
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(sphereCenterTransform.position, sphereRadius);
    }
#endif
}
