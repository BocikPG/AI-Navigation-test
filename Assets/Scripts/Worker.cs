using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Worker : MonoBehaviour
{
	//inspector
	[SerializeField]
	NavMeshAgent agent;

	public Canvas workerCanvas;


	[SerializeField]
	GameResourcesList carriedResource;
	//private
	BuildingParent sourceBuilding;
	BuildingParent destinationBuilding;


	private bool isTraveling;

	//unity methods
	void Update()
	{
		if (!isTraveling)
		{
			if (WorkerManger.Instance.extractionBuildings.Count > 0)
			{
				sourceBuilding = WorkerManger.Instance.extractionBuildings[0];
				agent.destination = sourceBuilding.transform.position;
			}
			if (WorkerManger.Instance.productionBuildings.Count > 0)
			{
				if (carriedResource.resources.Count > 0)
				{
					destinationBuilding = WorkerManger.Instance.productionBuildings[0];
					agent.destination = destinationBuilding.transform.position;
				}

			}
		}

		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

		if (Physics.Raycast(ray, out RaycastHit hit))
		{
			//Debug.Log(hit.transform.gameObject.name);
			if (hit.transform.gameObject == gameObject)
			{
				workerCanvas.gameObject.SetActive(true);
			}
			else
			{
				workerCanvas.gameObject.SetActive(false);
			}
		}
		else
		{
			workerCanvas.gameObject.SetActive(false);
		}

	}

	void OnTriggerStay(Collider other)
	{
		if (sourceBuilding == null)
			return;
		if (other.gameObject == sourceBuilding.gameObject)
		{
			if (!isTraveling)
			{
				PickUpPackage(sourceBuilding);
				isTraveling = true;
			}
		}
		if (destinationBuilding == null)
			return;
		if (other.gameObject == destinationBuilding.gameObject)
		{
			DropPackage(destinationBuilding);
		}
	}

	//private methods

	private void DropPackage(BuildingParent destination)
	{
		foreach (var res in carriedResource.resources)
			destination.resourcesList.Add(res.resourceSO, res.amount);
	}

	private void PickUpPackage(BuildingParent source)
	{
		var productionBuilding = source.GetComponent<ProductionBuilding>();
		if (productionBuilding != null)
		{
			carriedResource.Add(productionBuilding.outputResourceSO, 1); //TODO: remove from source


			return;
		}

		var extractionBuilding = source.GetComponent<ExtractionBuilding>();
		if (extractionBuilding != null)
		{
			carriedResource.Add(extractionBuilding.resourceSO, 1);
			return;
		}
	}
}
