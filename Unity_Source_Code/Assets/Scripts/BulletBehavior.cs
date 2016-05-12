using UnityEngine;
using System.Collections;

public class BulletBehavior : MonoBehaviour 
{
	private float cameratopBorder;
	private float cameradownBorder;

	void Start () 
	{
		float distance = this.transform.position.z - Camera.main.transform.position.z;
		Vector3 cameraTopBorder = 	Camera.main.ViewportToWorldPoint(new Vector3(0f,1f, distance));
		Vector3 cameraDownBorder = Camera.main.ViewportToWorldPoint(new Vector3(0f, 0, distance));
		cameratopBorder = cameraTopBorder.y;
		cameradownBorder = cameraDownBorder.y;
	}

	void Update () 
	{
		if (this.transform.position.y > cameratopBorder) 
		{
			Destroy(gameObject);
		}

		if (this.transform.position.y < cameradownBorder) 
		{
			Destroy(gameObject);
		}
	}

	void OnCollisionEnter2D()
	{
		Destroy(gameObject);
	}
}
