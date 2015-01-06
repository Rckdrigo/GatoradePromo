using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class WebCam : MonoBehaviour {
	WebCamTexture webCamTexture;

	// Use this for initialization
	void Start () {
		webCamTexture = new WebCamTexture();
		webCamTexture.Play();
		GetComponent<RawImage>().texture = webCamTexture;
		//GetComponent<RectTransform>().sizeDelta = new Vector2(,);
	}
}
