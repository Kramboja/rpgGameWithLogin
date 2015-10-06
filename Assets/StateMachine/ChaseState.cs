using UnityEngine;
using System.Collections;

public class ChaseState : IEnemyState {

	private readonly StatePatternEnemy enemy;

	public ChaseState (StatePatternEnemy statePaternEnemy)
	{
		enemy = statePaternEnemy;
	}

	public void UpdateState()
	{
		look();
		chase();
	}
	
	public void OnTriggerEnter(Collider other)
	{
		
	}
	
	public void ToPatrolState()
	{
		
	}
	
	public void ToAlertState()
	{
		enemy.currentState = enemy.alertState;
	}
	
	public void ToChaseState()
	{
		
	}

	private void look()
	{
		RaycastHit hit;
		Vector3 enemyToTarget = (enemy.chaseTarget.position + enemy.offset) - enemy.eyes.transform.position;
		if(Physics.Raycast(enemy.eyes.transform.position,enemyToTarget,out hit, enemy.sightrange) && hit.collider.CompareTag("Player"))
		{
			enemy.chaseTarget = hit.transform;
		}
		else
		{
			ToAlertState();
		}
	}

	private void chase()
	{
		enemy.meshRendererFlag.material.color = Color.red;
		enemy.navMeshAgent.destination = enemy.chaseTarget.position;
		enemy.navMeshAgent.Resume();
	}
}
