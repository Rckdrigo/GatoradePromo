using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public enum IosDevice{IPad,IPhone4,IPhone5}

public class AspectAdapt : MonoBehaviour {

	[System.Serializable]
	public struct AspectRect{
		public Vector3 position;
		public Vector2 deltaSize;
	}
	
	public AspectRect[] rect;

	IosDevice device;

	void Start(){
	#if UNITY_IOS
		switch(iPhone.generation){
		case iPhoneGeneration.iPhone4:
		device = IosDevice.IPhone4;
			break;
			
			
		case iPhoneGeneration.iPhone4S:	
		device = IosDevice.IPhone4;
			break;
			
			
		case iPhoneGeneration.iPhone5:
		device = IosDevice.IPhone5;
			break;
			
			
		case iPhoneGeneration.iPhone5C:
		device = IosDevice.IPhone5;
			break;
			
			
		case iPhoneGeneration.iPhone5S:
			device = IosDevice.IPhone5;
			break;
			
		case iPhoneGeneration.iPad1Gen:
			device = IosDevice.IPad;
			break;
			
		case iPhoneGeneration.iPad2Gen:
			device = IosDevice.IPad;
			break;
			
		case iPhoneGeneration.iPad3Gen:
			device = IosDevice.IPad;
			break;
			
		case iPhoneGeneration.iPad4Gen:
			device = IosDevice.IPad;
			break;
			
		case iPhoneGeneration.iPad5Gen:
			device = IosDevice.IPad;
			break;
			
		case iPhoneGeneration.iPadAir2:
			device = IosDevice.IPad;
			break;
			
		case iPhoneGeneration.iPadMini1Gen:
			device = IosDevice.IPad;
			break;
			
		case iPhoneGeneration.iPadMini2Gen:
			device = IosDevice.IPad;
			break;
		
		default:
			device = IosDevice.IPhone4;
			break;
		
		}
		
		switch(device){
		case IosDevice.IPhone4:
			rect[2].position.x = Screen.width/2;
			GetComponent<RectTransform>().position = rect[2].position;
			GetComponent<RectTransform>().sizeDelta = rect[2].deltaSize;
			break;
			
		case IosDevice.IPhone5:
			rect[1].position.x = Screen.width/2;
			GetComponent<RectTransform>().position = rect[1].position;
			GetComponent<RectTransform>().sizeDelta = rect[1].deltaSize;
			break;
			
		case IosDevice.IPad:
			rect[0].position.x = Screen.width/2;
			GetComponent<RectTransform>().position = rect[0].position;
			GetComponent<RectTransform>().sizeDelta = rect[0].deltaSize;
			break;
		}
    #endif
	}

}
