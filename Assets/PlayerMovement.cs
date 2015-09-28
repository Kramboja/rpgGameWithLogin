using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

	private float speed 			= 100.0f;
	private float rotationspeed 	= 100f;
	private float mouseSensivity 	= 10f;
	private float verticalRotation 	= 0;
	private float upDownRange 		= 60f;
	
	void Awake()
	{
		Screen.lockCursor = true;
	}
	// Update is called once per frame
	void Update () 
	{
		if (Input.anyKey)
			buttonPressed ();
		
		float rotHorizontal = Input.GetAxis ("Mouse X") * mouseSensivity;
		transform.Rotate (0, rotHorizontal, 0);
		
		verticalRotation -= Input.GetAxis ("Mouse Y") * mouseSensivity;
		verticalRotation = Mathf.Clamp (verticalRotation, -upDownRange, upDownRange);
		
		Camera.main.transform.localRotation = Quaternion.Euler (verticalRotation, 0, 0);
	}
	
	private void buttonPressed()
	{	
		if (Input.GetKey(KeyCode.Escape) && Screen.lockCursor)
			Screen.lockCursor = false;
		else
			Screen.lockCursor = true;
		
		float translation = Input.GetAxis ("Vertical") 	* speed;
		float rotation	= Input.GetAxis ("Horizontal") 	* rotationspeed;
		
		translation 	*= Time.deltaTime;
		rotation 		*= Time.deltaTime;
		
		transform.Translate (0, 0, translation);
		transform.Rotate (0, rotation, 0);
	}
}
