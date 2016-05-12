using UnityEngine;
using System.Collections;

public class EnemiesPositions : MonoBehaviour 
{
	void OnDrawGizmos()
	{
		Gizmos.DrawWireSphere(this.transform.position, 1);
	}
}
