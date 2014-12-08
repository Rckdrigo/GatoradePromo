using UnityEngine;
using UnityEngine.UI;
using MyUnity.CommonUtilities;
using System.Collections;

public sealed class PreviewPhoto : Singleton<PreviewPhoto> {

	public RawImage image;

	public void ShowPreviewPhoto(){
		StartCoroutine(LookForPhoto());
	}

	IEnumerator LookForPhoto(){
		WWW www;
		string url = "file://"+ScreenCapture.GetLastScreenshotPath();
		yield return www = new WWW(url);
		image.GetComponent<RectTransform>().sizeDelta = new Vector2(www.texture.width,www.texture.height);
		image.texture = www.texture;
	}
}
