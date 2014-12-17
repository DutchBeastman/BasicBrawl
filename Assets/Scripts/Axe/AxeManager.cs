using UnityEngine;
using System.Collections.Generic;

public class AxeManager : MonoBehaviour {
	public const string AXE_PREFAB = "Prefabs/Axe";
	
	[SerializeField]
	private int numAxes;
	private int throwableAxes;
	private List<GameObject> axes;
	private GameObject axe;

	// Use this for initialization
	void Start () {
		axes = new List<GameObject>();

		throwableAxes = numAxes;
		for(int i = 0; i < numAxes; i++){
			//Instantiate Axes
			axes.Add(axe = GameObject.Instantiate(Resources.Load(AXE_PREFAB),transform.position, Quaternion.identity) as GameObject);
			axe.name = "axe";
			axe.gameObject.SetActive(false);


			
		}
	}
	public void ThrowAxe(){
		throwableAxes -= 1;
	}

}
