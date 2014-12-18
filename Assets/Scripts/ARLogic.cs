using UnityEngine;
using System.Collections;
using MyUnity.CommonUtilities;

public class ARLogic : MonoBehaviour {
	
	public GameObject button;
	public GameObject model;
	bool activateAR;

	// Update is called once per frame
	void Update () {
#if !UNITY_EDITOR		
		if(AREvent.onImageTarget)
			activateAR =false;

		button.SetActive(AREvent.onImageTarget || activateAR);
#else
		button.SetActive(activateAR);
#endif
		model.SetActive(activateAR && !AREvent.onImageTarget);
	
	}

	public void Deactivate(){
		model.SetActive(false);
		this.enabled = false;
	}

	public void ToogleAR(){
		activateAR = !activateAR;
	}
}
