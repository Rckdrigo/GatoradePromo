using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public sealed class DeactivateRenderer : MonoBehaviour {

	public GameObject[] objects;

	public void Deactivate(){
		foreach(GameObject o in objects)
			o.SetActive(false);
	}
	public void Activate(){
		foreach(GameObject o in objects)
			o.SetActive(true);
	}
}
