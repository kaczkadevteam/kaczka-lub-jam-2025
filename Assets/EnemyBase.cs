using System;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
	public event EventHandler<OnEnemyDeathEventArgs> OnEnemyDeath;
	public event EventHandler<OnEnemyDamagePlayerEventArgs> OnEnemyDamagePlayer;

	public class OnEnemyDeathEventArgs : EventArgs
	{
		public EnemyBase enemyBase;
	}

	public class OnEnemyDamagePlayerEventArgs : EventArgs
	{
		public EnemyBase enemyBase;
	}

	[SerializeField] private EnemySO enemySO;
    private Transform playerLocation;
    
    void Start()
	{
		playerLocation = GameObject.Find("Player").transform;
	}

	void Update()
	{
		EnemyMove();
	}

	public void EnemyMove()
	{
		transform.position = Vector3.MoveTowards(transform.position, playerLocation.position, 10f * Time.deltaTime);
	}
	
	void OnCollisionEnter(Collision collision)
	{
		Debug.Log("Collision");
		if(collision.gameObject.tag == "Player")
		{
			TakeDamage();
		}
	}
	
	public void TakeDamage()
	{
		OnEnemyDeath?.Invoke(this, new OnEnemyDeathEventArgs { enemyBase = this });
		Destroy(gameObject);
	}

}
