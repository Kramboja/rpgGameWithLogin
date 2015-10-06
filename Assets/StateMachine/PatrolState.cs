using UnityEngine;
using System.Collections;

public class PatrolState : IEnemyState {

	private readonly StatePatternEnemy enemy;
	private int nextWayPoint;

	public PatrolState (StatePatternEnemy statePaternEnemy)
	{
		enemy = statePaternEnemy;
	}

	public void UpdateState()
	{
		look();
		Patrol();
	}
	
	public void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.CompareTag("Player"))
			ToAlertState();
	}
	
	public void ToPatrolState()
	{
		Debug.Log("Can't transision to same state");
	}
	
	public void ToAlertState()
	{
		enemy.currentState = enemy.alertState;
	}
	
	public void ToChaseState()
	{
		enemy.currentState = enemy.chaseState;
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

	void Patrol()
	{
		enemy.meshRendererFlag.material.color = Color.green;
		enemy.navMeshAgent.destination = enemy.waypoints[nextWayPoint].position;
		enemy.navMeshAgent.Resume();

		if(enemy.navMeshAgent.remainingDistance <= enemy.navMeshAgent.stoppingDistance && !enemy.navMeshAgent.pathPending)
		{
			nextWayPoint = (nextWayPoint+1) % enemy.waypoints.Length;
		}
	}
}
