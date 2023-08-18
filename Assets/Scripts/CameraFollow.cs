using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class CameraFollow : MonoBehaviour {

	public Transform target;            // The position that that camera will be following.
	public float smoothing = 3f;        // The speed with which the camera will be following.

	public Vector3 offset;                     // The initial offset from the target.

	Vector3 tem;                       //store the temporary position of camera

	public Camera cam;

	Ray r1;
	Ray r2;
	Ray r3;

	void Start()
	{
		// Calculate the initial offset.
		offset = transform.position - target.position;
		


	}

	void FixedUpdate()
	{
		// Create a postion the camera is aiming for based on the offset from the target.
		Vector3 targetCamPos = target.position + offset;

		// Smoothly interpolate between the camera's current position and it's target position.
		transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);
		
		




	}


	public void cameraTo(Vector3 pos)
    {
        if (tem == null)
        {
			tem = transform.position;

		}
		if (offset!=null)
        {
			transform.position = pos +offset;
        }
    }

	public void cameraBack()
	{
		transform.position = tem;
	}
	
	
   
}
