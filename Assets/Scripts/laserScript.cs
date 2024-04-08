using UnityEngine;
using System.Collections;

public class laserScript : MonoBehaviour {

	//1
	public Sprite laserOnSprite;    
	public Sprite laserOffSprite;
	
	//2    
	public float interval = 0.5f;    
	public float rotationSpeed = 0.0f;
	
	//3
	private bool isLaserOn = true;    
	private float timeUntilNextToggle;


	//add
	BoxCollider2D bc2d;
	void Start () {
		timeUntilNextToggle = interval;
		bc2d=GetComponent<BoxCollider2D>();
	}
	
	void FixedUpdate () {
		//1
		timeUntilNextToggle -= Time.fixedDeltaTime;
		
		//2
		if (timeUntilNextToggle <= 0) {
			
			//3
			isLaserOn = !isLaserOn;
			
			//4
			//change
			//collider2D.enabled = isLaserOn;
			bc2d.enabled=isLaserOn;

			//5
			//change
			//SpriteRenderer spriteRenderer = ((SpriteRenderer)this.renderer);
			SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

			if (isLaserOn)
				spriteRenderer.sprite = laserOnSprite;
			else
				spriteRenderer.sprite = laserOffSprite;
			
			//6
			timeUntilNextToggle = interval;
		}
		
		//7
		transform.RotateAround(transform.position, Vector3.forward, rotationSpeed * Time. fixedDeltaTime);
	}
}
