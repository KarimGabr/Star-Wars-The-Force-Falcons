using UnityEngine;
using System.Collections;

public class Player1Controller : MonoBehaviour 
{
	private float speed = 10f;
	private float leftBorder;
	private float rightBorder;
	private float playerBulletSpeed = 5f;
	private float firingRate = 0.6f;
	public AudioClip fireSound;
	private LevelManager levelManager;

	void Start () 
	{
		levelManager = GameObject.FindObjectOfType<LevelManager>();
		//restricting player to gamespace using camera view port
		float distance = this.transform.position.z - Camera.main.transform.position.z;
		Vector3 cameraLeftBorder = 	Camera.main.ViewportToWorldPoint(new Vector3(0f, 0, distance));
		Vector3 cameraRightBorder = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0, distance));
		leftBorder = cameraLeftBorder.x + 1.3f;
		rightBorder = cameraRightBorder.x - 1.3f;
	}
	
	void Fire()
	{
		GameObject playerBullet = Instantiate(Resources.Load("cyan_bullet"), this.transform.position, Quaternion.identity) as GameObject;
		playerBullet.layer = LayerMask.NameToLayer("LightSide");
		playerBullet.rigidbody2D.velocity = new Vector2(0, playerBulletSpeed);
		AudioSource.PlayClipAtPoint(fireSound, this.transform.position);
	}

	void Update () 
	{
		if(Input.GetKeyDown(KeyCode.Space))
		{
			InvokeRepeating("Fire",0.000001f, firingRate);
		}
		
		if(Input.GetKeyUp(KeyCode.Space))
		{
			CancelInvoke("Fire");
		}

		if(Input.GetKey(KeyCode.A))
		{
			// or -->> transform.position +=  new Vector3 (-speed * Time.deltaTime, 0, 0);
			this.transform.position += Vector3.left * speed * Time.deltaTime;
		}
		else if(Input.GetKey(KeyCode.D)) 
		{
			// or -->> transform.position +=  new Vector3 (speed * Time.deltaTime, 0, 0);
			this.transform.position += Vector3.right * speed * Time.deltaTime;
		}

		//restricting player to gamespace using given values
		float newX = Mathf.Clamp(this.transform.position.x, -12f, 12f);
		//restricting player to gamespace using camera view port
		newX = Mathf.Clamp(this.transform.position.x, leftBorder, rightBorder);

		this.transform.position = new Vector3(newX, this.transform.position.y, this.transform.position.z);
	}

	void OnCollisionEnter2D()
	{
		levelManager.LoadWinScreen();
	}
}
