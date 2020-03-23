using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
	private Animator _animator;
	private BoxCollider2D _collider;

	private void Start()
	{
		_animator = GetComponent<Animator>();
		_collider = GetComponent<BoxCollider2D>();
	}

	public void OpenDoor()
	{
		_animator.SetTrigger("Opened");
		_collider.enabled = false;
	}
}
