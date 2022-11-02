using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
	[SerializeField] GameObject deathFX;
	[SerializeField] GameObject hitVFX;
	
	
	[SerializeField] int scoreValue = 10;
	[SerializeField] int health = 3;

	GameObject parentGameobject;
	ScoreBoard scoreBoard;


	private void Start()
	{
		
		scoreBoard = FindObjectOfType<ScoreBoard>();
		parentGameobject = GameObject.FindWithTag("SpawnAtRuntime");
		AddRigidBody();
	}

	private void AddRigidBody()
	{
		Rigidbody rb = gameObject.AddComponent<Rigidbody>();
		rb.useGravity = false;
	}

	private void OnParticleCollision(GameObject other)
	{
		ProcessHit();
		if (health < 1)
		{
			KillEnemy();
		}
		
	}

	private void ProcessHit()
	{
		 GameObject vfx = Instantiate(hitVFX,transform.position,Quaternion.identity);

		 vfx.transform.parent = parentGameobject.transform;

		 health--;
		 
	}

	private void KillEnemy()
	{
		scoreBoard.UpdateScore(scoreValue);
		
		GameObject fx = Instantiate(deathFX, transform.position, Quaternion.identity);
		fx.transform.parent = parentGameobject.transform;
		Destroy(gameObject);
	}
}
