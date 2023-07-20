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
        if(productionBuilding!=null)
        {
            productionBuildings.Add(productionBuilding);
            return;
        }

        var extractionBuilding = building.GetComponent<ExtractionBuilding>();
        if(extractionBuilding!=null)
        {
            extractionBuildings.Add(extractionBuilding);
            return;
        }

        var buildingB = building.GetComponent<Building>();
        if(buildingB!=null)
        {
            buildingsB.Add(buildingB);
            return;
        }
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
