using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Worker : MonoBehaviour
{
	[SerializeField]
	NavMeshAgent agent;

	private bool isWorking;

	void Update()
	{
		if (!isWorking)
		{
			if (WorkerManger.Instance.extractionBuildings.Count > 0)
			{
				agent.destination = WorkerManger.Instance.extractionBuildings[0].transform.position;
			}

		}

	}
}
