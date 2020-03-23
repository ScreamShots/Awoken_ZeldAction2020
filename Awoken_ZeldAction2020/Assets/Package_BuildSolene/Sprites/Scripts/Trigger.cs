using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{
	private Animator _animator;
	private Door _parentDoor;

	private void Start()
	{
		_animator = GetComponent<Animator>();
		_parentDoor = GetComponentInParent<Door>();
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		_animator.SetTrigger("Pushed");
		collision.gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
		_parentDoor.OpenDoor();
	}
}
