using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPos : MonoBehaviour
{
    public float minimumHeight = 0.5f;
    public Vector3 GetARandomPos()
    {
        
        Mesh planeMesh = gameObject.GetComponent<MeshFilter>().mesh;
        Bounds bounds = planeMesh.bounds;

        float minX = gameObject.transform.position.x - gameObject.transform.localScale.x * bounds.size.x * 0.5f;
        float maxY = gameObject.transform.position.y + gameObject.transform.localScale.z * bounds.size.z * 0.5f;

        Vector3 newVec = new Vector3(Random.Range(minX, -minX),
                                     Random.Range(minimumHeight, maxY),
                                     -10
                                     );
        return newVec;
    }
}
