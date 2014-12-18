using UnityEngine;
using System.Collections;


public class Axe2 : MonoBehaviour {

	public Player player;
	//Make sure that you hit the right collider so that the axe stops moving
	private void OnCollisionEnter2D(Collision2D collision){
		if(collision.collider.tag == "Ground" || collision.collider.tag == "Wall"){
			gameObject.collider2D.isTrigger = true;
			gameObject.rigidbody2D.isKinematic = true;
			gameObject.rigidbody2D.rotation = 0;
			
		}
		if(collision.collider.tag == "Player"){
			gameObject.collider2D.isTrigger = true;
			gameObject.rigidbody2D.isKinematic = true;
			gameObject.rigidbody2D.rotation = 0;
			collision.gameObject.SetActive(false);
			player.GetComponent<Player>().Respawn();

		}
		
	}
}
