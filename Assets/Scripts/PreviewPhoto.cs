using UnityEngine;
using UnityEngine.UI;
using MyUnity.CommonUtilities;
using System.Collections;
using System.IO;

public sealed class PreviewPhoto : Singleton<PreviewPhoto> {

	public UnityEngine.UI.Image image;

	public void ShowPreviewPhoto(){
		image.sprite = null;
		LookforFile();
	}

	bool SearchFile(string name, string format){
#if UNITY_EDITOR
		DirectoryInfo dir = new DirectoryInfo(Application.dataPath);	
#else
		DirectoryInfo dir = new DirectoryInfo(Application.persistentDataPath);	
#endif
		foreach(FileInfo file in dir.GetFiles(format))
			if(file.Name  == name)
				return true;
		return false;
	}

	void LookforFile(){
		if(SearchFile("Screenshot.jpg","*.jpg")){
			StartCoroutine(LookForPhoto());
		}
		else {		
			LookforFile();
		}
	}

	IEnumerator LookForPhoto(){
		WWW www;
#if UNITY_EDITOR
		string url = "file://"+Application.dataPath+"/"+ScreenCapture._FileName;
#else
		string url = "file://"+Application.persistentDataPath+"/"+ScreenCapture._FileName;
#endif
		yield return www = new WWW(url);
		if(www.error != null)
			StartCoroutine(LookForPhoto());
		//image.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width*2,Screen.height*2);
<<<<<<< HEAD
		image.sprite = Sprite.Create(www.texture,new Rect(0,0,www.texture.width,www.texture.height),new Vector2(0.5f,0.5f));
		image.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width,Screen.height);
=======
		image.sprite = Sprite.Create(www.texture,new Rect(0,0,Screen.width,Screen.height),new Vector2(0.5f,0.5f));
		//image.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width*2,Screen.height*2);
>>>>>>> Android
		//image.texture = www.texture;

	}
}
