using UnityEngine;
using UnityEngine.UI;
using MyUnity.CommonUtilities;
using System.Collections;
using System.IO;

public sealed class PreviewPhoto : Singleton<PreviewPhoto> {

	public UnityEngine.UI.Image image;

	public void ShowPreviewPhoto(){
        image.sprite = null;
		image.sprite = Sprite.Create(ScreenCapture.tex,new Rect(0,0,Screen.width,Screen.height),new Vector2(0.5f,0.5f));
	
	}
}
