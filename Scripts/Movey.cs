//Attach this script to a GameObject with a Rigidbody2D component. Use the left and right arrow keys to see the transform in action.
//Use the up and down keys to change the rotation, and see how using Transform.right differs from using Vector3.right

using UnityEngine;

public class Movey : MonoBehaviour
{
	Rigidbody2D m_Rigidbody;
	float m_Speed;

	void Start()
	{
		//Fetch the Rigidbody component you attach from your GameObject
		m_Rigidbody = GetComponent<Rigidbody2D>();
		//Set the speed of the GameObject
		m_Speed = 10.0f;
	}

	void Update()
	{
		if (Input.GetKey(KeyCode.RightArrow))
		{
			//Move the Rigidbody to the right constantly at speed you define (the red arrow axis in Scene view)
			m_Rigidbody.velocity = transform.right * m_Speed;
		}

		if (Input.GetKey(KeyCode.LeftArrow))
		{
			//Move the Rigidbody to the left constantly at the speed you define (the red arrow axis in Scene view)
			m_Rigidbody.velocity = -transform.right * m_Speed;
		}

		if (Input.GetKey(KeyCode.UpArrow))
		{
			//rotate the sprite about the Z axis in the positive direction
			transform.Rotate(new Vector3(0, 0, 1) * Time.deltaTime * m_Speed, Space.World);
		}

		if (Input.GetKey(KeyCode.DownArrow))
		{
			//rotate the sprite about the Z axis in the negative direction
			transform.Rotate(new Vector3(0, 0, -1) * Time.deltaTime * m_Speed, Space.World);
		}
	}
}