using System;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
	[SerializeField] private EnemySO enemySO;
	private int currentHealth;
    private Transform playerLocation;
    
    void Start()
	{
		currentHealth = enemySO.health;
		playerLocation = GameObject.Find("Player").transform;
	}

	void Update()
	{
		EnemyMove();
	}

	public void EnemyMove()
	{
		if(!playerLocation) return;
		transform.position = Vector3.MoveTowards(transform.position, playerLocation.position, 10f * Time.deltaTime);
	}
	
	public void OnCollisionEnter(Collision collision)
	{
		if(collision.gameObject.tag == "Player")
		{
			TakeDamage(1);
		}
	}
	
	public void TakeDamage(int damage)
	{
		currentHealth -= damage;
		if (currentHealth <= 0)
		{
			//todo: chance for droping upgrade for player to pick up
			Destroy(gameObject);
		}
	}
	
	//todo
	public void DropUpgrade()
	{
	}
}
