using UnityEngine;
using System.Collections.Generic;

public class Axe : MonoBehaviour {
	public const string AXE_PREFAB = "Prefabs/Axe";

	[SerializeField]
	private int numAxes;
	[SerializeField]
	private List<GameObject> axes;



	// Use this for initialization
	void Start () {
		axes = new List<GameObject>();

		for(int i = 0; i < numAxes; i++){

			axes.Add(GameObject.Instantiate(Resources.Load(AXE_PREFAB),transform.position, Quaternion.identity) as GameObject);

		}
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		
	}
}
