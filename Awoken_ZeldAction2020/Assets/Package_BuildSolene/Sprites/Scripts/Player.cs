using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	private Animator _animator;
	private float _speed = 5f;

	private Vector2 _lastMainDirection;

	private void Start()
	{
		_animator = GetComponent<Animator>();
	}

	private void FixedUpdate()
	{
		Vector2 inputsVector = Vector2.right * Input.GetAxis("Horizontal") + Vector2.up * Input.GetAxis("Vertical");
		Vector2 movement = inputsVector * Time.fixedDeltaTime * _speed;

		Vector2 mainDirection;
		mainDirection = Mathf.Abs(inputsVector.y) > Mathf.Abs(inputsVector.x) &&
			Mathf.Abs(inputsVector.x) < .25f ?
				new Vector2(0, 1 * Mathf.Sign(inputsVector.y)) : 
				new Vector2(1 * Mathf.Sign(inputsVector.x), 0) ;

		if (inputsVector == Vector2.zero)
		{
			mainDirection = Vector2.zero;
		}

		if (mainDirection != _lastMainDirection)
		{
			if (mainDirection == Vector2.zero)
			{
				_animator.SetTrigger("IDLE");
			}
			else
			{
				switch (mainDirection.y)
				{
					case 1:
						_animator.SetTrigger("Up");
						break;
					case -1:
						_animator.SetTrigger("Down");
						break;
				}

				switch (mainDirection.x)
				{
					case 1:
						_animator.SetTrigger("Right");
						break;
					case -1:
						_animator.SetTrigger("Left");
						break;
				}
			}

			_lastMainDirection = mainDirection;
		}

		transform.Translate(movement);
	}
}
