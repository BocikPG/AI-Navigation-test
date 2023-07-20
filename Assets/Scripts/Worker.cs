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


	private bool isCarrying => !carriedResource.isEmpty();

	//unity methods
	void Update()
	{
		if (sourceBuilding == null)
		{
			sourceBuilding = WorkerManger.Instance.GetClosestExtractor();
			if (sourceBuilding != null)
				agent.destination = sourceBuilding.transform.position;
		}
		if (destinationBuilding == null)
		{
			destinationBuilding = WorkerManger.Instance.GetClosestProduction();
			if (isCarrying && destinationBuilding != null)
				agent.destination = destinationBuilding.transform.position;
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
			if (!isCarrying)
			{
				if (!PickUpPackage(sourceBuilding))
				{
					sourceBuilding = WorkerManger.Instance.GetClosestExtractor();
					destinationBuilding = WorkerManger.Instance.GetClosestProduction();
					agent.destination = sourceBuilding.transform.position;
				}
			}
			else
			{
				if (destinationBuilding != null)
				{
					agent.destination = destinationBuilding.transform.position;
				}
			}


		}
		if (destinationBuilding == null)
			return;
		if (other.gameObject == destinationBuilding.gameObject)
		{
			if (isCarrying)
			{
				DropPackage(destinationBuilding);
				sourceBuilding = destinationBuilding;
				destinationBuilding = WorkerManger.Instance.GetNextDestination(sourceBuilding);
				agent.destination = sourceBuilding.transform.position;
			}
			else
			{
				sourceBuilding = WorkerManger.Instance.GetClosestExtractor();
				destinationBuilding = WorkerManger.Instance.GetClosestProduction();
				agent.destination = sourceBuilding.transform.position;

			}

		}
	}

	//private methods

	private void DropPackage(BuildingParent destination)
	{
		foreach (var res in carriedResource.resources)
		{
			destination.resourcesList.Add(res.resourceSO, res.amount);
			carriedResource.TryUse(res.resourceSO, res.amount);
		}

	}

	private bool PickUpPackage(BuildingParent source)
	{
		var productionBuilding = source.GetComponent<ProductionBuilding>();
		if (productionBuilding != null)
		{
			if (productionBuilding.resourcesList.TryUse(productionBuilding.outputResourceSO, 1))
			{
				carriedResource.Add(productionBuilding.outputResourceSO, 1);
				return true;
			}
		}

		var extractionBuilding = source.GetComponent<ExtractionBuilding>();
		if (extractionBuilding != null)
		{
			if (extractionBuilding.resourcesList.TryUse(extractionBuilding.resourceSO, 1))
			{
				carriedResource.Add(extractionBuilding.resourceSO, 1);
				return true;
			}
		}
		return false;
	}


}
