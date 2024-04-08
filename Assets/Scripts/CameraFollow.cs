using UnityEngine;
using System.Collections;

//make sure scripting runtime version set to .Net 4.x equivalent
//edit -> project settings-> player -> other

public class CameraFollow : MonoBehaviour {

	public GameObject targetObject;
	private float distanceToTarget;
	// Use this for initialization
	void Start () {
		distanceToTarget = transform.position.x - targetObject.transform.position.x;
	}
	
	// Update is called once per frame
	void Update () {
		float targetObjectX = targetObject.transform.position.x;
		
		Vector3 newCameraPosition = transform.position;
		newCameraPosition.x = targetObjectX +distanceToTarget ;
		transform.position = newCameraPosition;
	}
}
