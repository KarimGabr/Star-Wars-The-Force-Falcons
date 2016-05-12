using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreBehavior : MonoBehaviour 
{
	public static int score;
	private Text myText;
	public static int enemyDestroyed;

	void Start () 
	{
		myText = GetComponent<Text>();
	}

	void Update () 
	{
		myText.text = enemyDestroyed.ToString();
	}

	public void incScore()
	{
	//	score++;
	//	myText.text = score.ToString();
	}

	public void Reset()
	{
	//	score = 0;
	//	myText.text = score.ToString();
	}
}
