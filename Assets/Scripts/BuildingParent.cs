using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingParent : MonoBehaviour
{
	[SerializeField]
	Collider PickUpZone;

	public GameResourcesList resourcesList;

	public Canvas buildingCanvas;
	public virtual void Update()
	{
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

		if (Physics.Raycast(ray, out RaycastHit hit))
		{
			//Debug.Log(hit.transform.gameObject.name);
			if (hit.transform.gameObject == gameObject)
			{
				buildingCanvas.gameObject.SetActive(true);
			}
			else
			{
				buildingCanvas.gameObject.SetActive(false);
			}
		}
		else
		{
			buildingCanvas.gameObject.SetActive(false);
		}
	}
}
