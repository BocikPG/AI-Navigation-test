using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.AI.Navigation;

public class WalkableGround : MonoBehaviour
{
	//instance
	public static WalkableGround Instance { get; private set; }
	//inspector
	[SerializeField]
	NavMeshSurface surface;

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

	//public methods
	public void RebuildWalkableSurface(BuildingParent building)
	{
		surface.BuildNavMesh();
	}


}
