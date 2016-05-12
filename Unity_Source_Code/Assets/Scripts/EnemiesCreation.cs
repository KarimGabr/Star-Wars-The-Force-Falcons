using UnityEngine;
using System.Collections;

public class EnemiesCreation : MonoBehaviour 
{
	private float enemyShipsBorderWidth = 26f;
	private float enemyShipsBorderHeight = 8f;
	private bool movingRight = true;
	private float speed = 1f;
	private float cameraleftBorder;
	private float camerarightBorder;
	private float enemyBulletSpeed = 5f;
	private float firingRate = 0.3f;
	private float spawnDelay = 0.5f;

	public AudioClip fireSound;
	
	void Start () 
	{
		ScoreBehavior.enemyDestroyed = 0;
		
		float distance = this.transform.position.z - Camera.main.transform.position.z;
		Vector3 cameraLeftBorder = 	Camera.main.ViewportToWorldPoint(new Vector3(0f, 0, distance));
		Vector3 cameraRightBorder = Camera.main.ViewportToWorldPoint(new Vector3(1f, 0, distance));
		camerarightBorder = cameraRightBorder.x;
		cameraleftBorder = cameraLeftBorder.x;

		EnemiesCreationProcess();
	}

	void Update () 
	{
		if(movingRight)
		{
			transform.position += Vector3.right * speed * Time.deltaTime;
		}
		else if(!movingRight)
		{
			transform.position += Vector3.left * speed * Time.deltaTime;
		}

		float enemyShipsBorderRight = this.transform.position.x + 13f;
		float enemyShipsBorderLeft = this.transform.position.x - 13f;

		if(enemyShipsBorderLeft < cameraleftBorder)
		{
			movingRight = true;
		}

		else if(enemyShipsBorderRight > camerarightBorder)
		{
			movingRight = false;
		}

		foreach (Transform child in this.transform)
		{
			float probability = Time.deltaTime * firingRate;
			if(Random.value < probability)
			{
				if(child.childCount == 1)
				{
					GameObject enemyBullet = Instantiate(Resources.Load("red_bullet"), child.transform.position, Quaternion.identity) as GameObject;
					enemyBullet.layer = LayerMask.NameToLayer("DarkSide");
					enemyBullet.rigidbody2D.velocity = new Vector2(0, -enemyBulletSpeed);
					AudioSource.PlayClipAtPoint(fireSound, this.transform.position);
				}

				if(child.childCount == 0)
				{
					SpawnUntilFull();
				}
			}
		}

		//if(AllShipsDestroyed())	SpawnUntilFull();

	}

	private void EnemiesCreationProcess()
	{
		for (int i = 4; i <= 8; i += 2) 
		{
			for (int j = 12; j >= -12; j -= 3) 
			{
				GameObject gizmo = Instantiate(Resources.Load("EnemiesPositions"), new Vector3(j, i , -5f), Quaternion.identity) as GameObject;
				gizmo.transform.parent = this.transform;
			}	
		}

		foreach (Transform child in transform) 
		{
			GameObject tie_fighter = Instantiate(Resources.Load("tie-fighter"), child.transform.position, Quaternion.identity) as GameObject;	
			tie_fighter.layer = LayerMask.NameToLayer("DarkSide");
			tie_fighter.transform.parent = child;
		}
	}

	void Fire()
	{

	}
	
	void OnDrawGizmos()
	{
		Gizmos.DrawWireCube(this.transform.position, new Vector3(enemyShipsBorderWidth, enemyShipsBorderHeight));
	}

	void SpawnEnemies()
	{
		foreach (Transform child in transform) 
		{
			GameObject tie_fighter = Instantiate(Resources.Load("tie-fighter"), child.transform.position, Quaternion.identity) as GameObject;	
			tie_fighter.layer = LayerMask.NameToLayer("DarkSide");
			tie_fighter.transform.parent = child;
		}
	}


	#region Respawn all dark ships after they are all destroyed

	bool AllShipsDestroyed()
	{
		foreach (Transform child in this.transform)
		{
			if(child.childCount == 1)	return false;
		}
		return true;
	}

	Transform NextFreePosition()
	{
		foreach (Transform child in this.transform)
		{
			if(child.childCount == 0)	return child;
		}
		return null;
	}

	void SpawnUntilFull()
	{
		Transform freePosition = NextFreePosition();
		if(freePosition)
		{
			GameObject tie_fighter = Instantiate(Resources.Load("tie-fighter"), freePosition.position, Quaternion.identity) as GameObject;	
			tie_fighter.layer = LayerMask.NameToLayer("DarkSide");
			tie_fighter.transform.parent = freePosition;
		}
		if(NextFreePosition())	Invoke("SpawnUntilFull", spawnDelay);
	}

	#endregion

}
