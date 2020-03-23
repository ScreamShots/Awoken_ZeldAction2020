using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Chest : MonoBehaviour
{
	private Animation _creditText;
	private Animator _animator;
	private bool _isOpened = false;
	private bool _isPlayerColliding = false;

	private void Start()
	{
		_animator = GetComponent<Animator>();
		_creditText = GetComponentInChildren<Animation>();
	}

	private void Update()
	{
		if (_isPlayerColliding && !_isOpened && Input.GetKeyDown(KeyCode.E))
		{
			OpenChest();
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("Player"))
		{
			_isPlayerColliding = true;
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("Player"))
			{
				_isPlayerColliding = false;
			}
	}

	private void OpenChest()
	{
		_isOpened = true;
		_animator.SetTrigger("Opened");
		_creditText.Play();
	}
}
