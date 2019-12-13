using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Despawner : MonoBehaviour
{

    public float fadeTime = 1f;
 
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Cuttable")
        {
            StartCoroutine(Despawn(collider.gameObject));
        }
    }

    IEnumerator Despawn(GameObject cuttable)
    {

        
        Color alphaColor = cuttable.GetComponent<MeshRenderer>().material.color;
        alphaColor.a = 0;
        float time = fadeTime;
        while (cuttable.GetComponent<MeshRenderer>().material.color.a>0.1)
        {
            if (cuttable == null) { break; }
            cuttable.GetComponent<MeshRenderer>().material.color = Color.Lerp(cuttable.GetComponent<MeshRenderer>().material.color, alphaColor, time * Time.deltaTime);
            yield return null;
        }
        
        Destroy(cuttable, fadeTime);
    }
}
