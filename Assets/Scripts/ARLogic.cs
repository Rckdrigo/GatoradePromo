using UnityEngine;
using System.Collections;
using MyUnity.CommonUtilities;

public class ARLogic : MonoBehaviour {

	public GameObject model;

	// Update is called once per frame
	void Update () {
#if !UNITY_EDITOR
		model.SetActive(AREvent.onImageTarget);
		ScreenController.Instance.ARDetected(AREvent.onImageTarget);
#endif
	}
}
