using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : MonoBehaviour {

    public GameObject objectToMove;

    public Transform startpoint;
    public Transform endpoint;

    public float moveSpeed;

    private Vector3 currentTarget;

    // Use this for initialization
    void Start () {
        currentTarget = endpoint.position;
	}
	
	// Update is called once per frame
	void Update () {
        objectToMove.transform.position = Vector3.MoveTowards(objectToMove.transform.position, currentTarget, moveSpeed * Time.deltaTime);

        if (objectToMove.transform.position == endpoint.position) {
            currentTarget = startpoint.position;
        }

        if (objectToMove.transform.position == startpoint.position) {
            currentTarget = endpoint.position;
        }
    }
}
