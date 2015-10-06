using UnityEngine;
using System.Collections;

public class AlertState : IEnemyState {

	private readonly StatePatternEnemy enemy;
	private float searchTimer;

	public AlertState (StatePatternEnemy statePaternEnemy)
	{
		enemy = statePaternEnemy;
	}

	public void UpdateState()
	{
		look();
		search();
	}
	
	public void OnTriggerEnter(Collider other)
	{
		
	}
	
	public void ToPatrolState()
	{
		enemy.currentState = enemy.patrolState;
		searchTimer = 0;
	}
	
	public void ToAlertState()
	{
		Debug.Log("Can't transision to same state");
	}
	
	public void ToChaseState()
	{
		enemy.currentState = enemy.chaseState;
		searchTimer = 0;
	}

	private void look()
	{
		RaycastHit hit;
		
		if(Physics.Raycast(enemy.eyes.transform.position,enemy.eyes.forward,out hit, enemy.sightrange) && hit.collider.CompareTag("Player"))
		{
			enemy.chaseTarget = hit.transform;
			ToChaseState();
		}
	}

	private void search()
	{
		enemy.meshRendererFlag.material.color = Color.yellow;

		enemy.navMeshAgent.Stop();
		enemy.transform.Rotate(0f,enemy.searchingTurnSpeed * Time.deltaTime,0);
		searchTimer += Time.deltaTime;

		if(searchTimer >= enemy.searchingDuration)
			ToPatrolState();
	}
}
