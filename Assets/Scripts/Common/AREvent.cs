/*==============================================================================
*	Created by Rodrigo Medina 
*				Sept / 2014	
*	Github: Rckdrigo
==============================================================================*/

using UnityEngine;
using System.Collections;
using MyUnity.CommonUtilities;

public class AREvent : MonoBehaviour, ITrackableEventHandler {
	
	private TrackableBehaviour mTrackableBehaviour;
	public static bool onImageTarget;
	
	void Start(){
		mTrackableBehaviour = GetComponent<TrackableBehaviour>();
		if (mTrackableBehaviour)
			mTrackableBehaviour.RegisterTrackableEventHandler(this);
	}
	
	public void OnTrackingLost()
	{
		onImageTarget = false;
	}
	
	public void OnTrackingFound()
	{
		onImageTarget = true;
	}
	
	public void OnTrackableStateChanged(
		TrackableBehaviour.Status previousStatus,
		TrackableBehaviour.Status newStatus)
	{
		if (newStatus == TrackableBehaviour.Status.DETECTED ||
		    newStatus == TrackableBehaviour.Status.TRACKED)
		{
			OnTrackingFound();
		}
		else
		{
			OnTrackingLost();
		}
	}
}