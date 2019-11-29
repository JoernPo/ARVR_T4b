using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ideAAL_Guide : MonoBehaviour {

    Animator _anim;
    NavMeshAgent _agent;

    [SerializeField] Transform[] positions;

	// Use this for initialization
	void Start () {
        _anim = GetComponent<Animator> ();
        _agent = GetComponent<NavMeshAgent> ();

      //  prep ();
	}

    void prep ()
    {
        _agent.destination = positions[0].position;
      
    }

    // Update is called once per frame
    void Update ()
    {
        if (Input.GetKeyDown (KeyCode.Space))
        {
            _agent.destination = positions[0].position;
            _anim.SetTrigger ("isWalking");
        }

        // Check if we've reached the destination
        if (!(_agent.remainingDistance >= _agent.stoppingDistance))
        {
            _anim.SetTrigger ("isIdle");
            Debug.Log ("Hello");
        }
    }
}
