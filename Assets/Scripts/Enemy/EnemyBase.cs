using System;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
	[SerializeField] public EnemySO enemySO;
	private float currentHealth;
    private Transform playerLocation;
    
	public void Start()
	{
		currentHealth = enemySO.health;
		playerLocation = GameObject.Find("Player").transform;
	}

	private void OnEnable()
	{
		transform.rotation = Quaternion.identity;
	}

	void Update()
	{
		EnemyMove();
	}
	
	public void SetPlayerLocation(Transform playerLocation)
	{
		this.playerLocation = playerLocation;
	}

	public void EnemyMove()
	{
		if(!playerLocation) return;
		transform.position = Vector3.MoveTowards(transform.position, playerLocation.position, enemySO.moveSpeed * Time.deltaTime);
	}
	
	public void OnCollisionEnter(Collision collision)
	{
		if(collision.gameObject.tag == "Player")
		{
			SelfDestruct();
			//todo: create event for player that will decrease player health
		}

		if (collision.gameObject.tag == "Bubble") {
			SelfDestruct();
		}
	}
	
	public void SelfDestruct()
	{
		//todo: chance for droping upgrade for player to pick up
		gameObject.SetActive(false);
		// Destroy(gameObject);
	}
	
	//todo
	private void DropUpgrade()
	{
	}
}
