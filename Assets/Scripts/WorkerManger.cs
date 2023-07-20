using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkerManger : MonoBehaviour
{
	//instance
	public static WorkerManger Instance { get; private set; }
	//inspector
	[SerializeField]
	Worker worker;

	//private

	public List<ProductionBuilding> productionBuildings = new();
	public List<ExtractionBuilding> extractionBuildings = new();
	public List<Building> buildingsB = new();


	//public methods
	public void OnBuildingPlaced(BuildingParent building)
	{
		var productionBuilding = building.GetComponent<ProductionBuilding>();
		if (productionBuilding != null)
		{
			productionBuildings.Add(productionBuilding);
			return;
		}

		var extractionBuilding = building.GetComponent<ExtractionBuilding>();
		if (extractionBuilding != null)
		{
			extractionBuildings.Add(extractionBuilding);
			return;
		}

		var buildingB = building.GetComponent<Building>();
		if (buildingB != null)
		{
			buildingsB.Add(buildingB);
			return;
		}
	}

	public BuildingParent GetNextDestination(BuildingParent sourceBuilding)
	{
		var productionBuilding = sourceBuilding.GetComponent<ProductionBuilding>();
		if (productionBuilding != null)
		{
			var warehouse = GetClosestWarehouse();
			if (warehouse == null)
			{
				return GetClosestProduction();
			}
			else
			{
				return warehouse;
			}
		}

		var extractionBuilding = sourceBuilding.GetComponent<ExtractionBuilding>();
		if (extractionBuilding != null)
		{
			return GetClosestProduction();
		}

		var buildingB = sourceBuilding.GetComponent<Building>();
		if (buildingB != null)
		{
			return GetClosestExtractor();
		}

		return null;
	}

	public BuildingParent GetClosestExtractor()
	{
		if (extractionBuildings.Count <= 0)
			return null;
		BuildingParent closest = extractionBuildings[0];
		float closestDist = Mathf.Infinity;
		foreach (var extra in extractionBuildings)
		{
			var extraDist = Vector3.Distance(extra.transform.position, worker.transform.position);
			if (closestDist > extraDist)
			{
				closest = extra;
				closestDist = extraDist;
			}
		}
		return closest;
	}
	public BuildingParent GetClosestProduction()
	{
		if (productionBuildings.Count <= 0)
			return null;
		BuildingParent closest = productionBuildings[0];
		float closestDist = Mathf.Infinity;
		foreach (var extra in productionBuildings)
		{
			var extraDist = Vector3.Distance(extra.transform.position, worker.transform.position);
			if (closestDist > extraDist)
			{
				closest = extra;
				closestDist = extraDist;
			}
		}
		return closest;
	}
	public BuildingParent GetClosestWarehouse()
	{
		if (buildingsB.Count <= 0)
			return null;
		BuildingParent closest = buildingsB[0];
		float closestDist = Mathf.Infinity;
		foreach (var extra in buildingsB)
		{
			var extraDist = Vector3.Distance(extra.transform.position, worker.transform.position);
			if (closestDist > extraDist)
			{
				closest = extra;
				closestDist = extraDist;
			}
		}
		return closest;
	}

	//unity methods
	private void Awake()
	{
		if (Instance != null && Instance != this)
		{
			Destroy(this);
		}
		else
		{
			Instance = this;
		}
	}
}
