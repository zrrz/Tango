﻿using UnityEngine;
using System.Collections;

[RequireComponent (typeof (PlayerColor))]
public class GameButton : MonoBehaviour {

	public bool isPressed = false;
	public bool requiresPressure = false;

	private Animator animator;
	private PlayerColor.pColor myColor = PlayerColor.pColor.Red;

	private void Start() {
		animator = GetComponent<Animator>();
	}

	private void OnTriggerEnter( Collider col ) {
		if( col.transform.tag == "Player" || col.transform.tag == "Box" ) {
			PlayerColor.pColor  colColor = col.GetComponent<PlayerColor>().color;
			if( colColor == myColor ) {
				isPressed = true;
				animator.SetTrigger( "Depress" );
			}
		}
	}

	private void OnTriggerExit( Collider col ) {
		if( isPressed && requiresPressure ) {
			isPressed = false;
			animator.SetTrigger( "Raise" );
		}
	}
}
