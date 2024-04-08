using UnityEngine;
using System.Collections;

public class CreatingParticleSortingLayerFix : MonoBehaviour {
	//add
	//Renderer rend;
	void Start () {

		//remove
		//particleSystem.renderer.sortingLayerName = "Player";
	//	particleSystem.renderer.sortingOrder = -1;

		//add
		Renderer rend=GetComponent<Renderer>();
		rend.sortingLayerName = "Player";
		rend.sortingOrder = -1;
	}
	

	void Update () {
	
	}
}
