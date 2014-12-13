using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	/// <summary cref="C &lt; T &gt;">
	/// Executes the player movement.
	/// </summary>
	void PlayerMovement()
	{

		
		rigidbody2D.rotation = 0;
		playerVector = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Jump"));
		rigidbody2D.AddRelativeForce(playerVector * 1000 *  Time.deltaTime);
		
		if(Input.anyKey == false){
			rigidbody2D.velocity = new Vector2(Mathf.SmoothDamp(rigidbody2D.velocity.x,0, ref tempVel, smooth),Mathf.SmoothDamp(rigidbody2D.velocity.y,-1, ref tempVel, smooth));
			
		}
		

	}

}
