using UnityEngine;
using MyUnity.CommonUtilities;
using System.Collections;

public sealed class ScreenController : Singleton<ScreenController> {

	Animator animator;

	void Start () {
		animator = GetComponent<Animator>();
	}

	public void Continue () {
		animator.SetTrigger("Continue");
	}

	public void Fail () {
		animator.SetTrigger("Fail");
	}

	public void Return () {
		animator.SetTrigger("Return");
	}

	public void Disconnected () {
		animator.SetTrigger("NoConnection");
	}

	public void Quit(){
		Application.Quit();
	}
}
