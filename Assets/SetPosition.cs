using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SetPosition : MonoBehaviour {
	#if UNITY_IOS
	public Vector4 iphone, iphone5, ipad;
	
	// Use this for initialization
	void Start () {
		switch(iPhone.generation){
			case iPhoneGeneration.iPhone4:
			GetComponent<RectTransform>().position= new Vector3(iphone.x,iphone.y,0);
			GetComponent<RectTransform>().sizeDelta= new Vector3(iphone.z,iphone.w);
			break;
			 
			case iPhoneGeneration.iPhone5:
			GetComponent<RectTransform>().position= new Vector3(iphone5.x,iphone5.y,0);
			GetComponent<RectTransform>().sizeDelta= new Vector3(iphone5.z,iphone5.w);
			break;
			
			default:
			GetComponent<RectTransform>().position= new Vector3(ipad.x,ipad.y,0);
			GetComponent<RectTransform>().sizeDelta= new Vector3(ipad.z,ipad.w);
			break;
		}
	}
    #endif
    
    void Update(){
		print("DPI: " +Screen.dpi);
    }
}
