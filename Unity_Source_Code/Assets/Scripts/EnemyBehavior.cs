using UnityEngine;
using System.Collections;

public class EnemyBehavior : MonoBehaviour 
{
	public AudioClip deathSound;
	//private ScoreBehavior scoreHandler;
	
	void Start () 
	{
	//	scoreHandler = GameObject.Find("Score").GetComponent<ScoreBehavior>();
	}

	void Update () 
	{
		
	}

	void OnCollisionEnter2D()
	{
		Destroy(gameObject);
		AudioSource.PlayClipAtPoint(deathSound, this.transform.position);
		ScoreBehavior.enemyDestroyed++;
	//	scoreHandler.incScore();
	}
}
