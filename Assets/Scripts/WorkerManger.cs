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

	List<BuildingParent> buildings;


    //public methods
    public void OnBuildingPlaced(BuildingParent building)
    {
        Debug.Log(building.ToString());
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
