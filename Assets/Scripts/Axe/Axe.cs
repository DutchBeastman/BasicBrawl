using UnityEngine;
using System.Collections.Generic;

public class Axe : MonoBehaviour {
	private Player owner;
	
	// Make sure that you hit the right collider so that the axe stops moving
	private void OnCollisionEnter2D(Collision2D collision) {
		if(collision.collider.CompareTag("Ground") || collision.collider.CompareTag("Wall")) {
			gameObject.collider2D.isTrigger = true;
			gameObject.rigidbody2D.isKinematic = true;
			gameObject.rigidbody2D.rotation = 0;
		} else if(collision.collider.tag == "Player") {
			if(collision.gameObject != owner.gameObject) {
				gameObject.collider2D.isTrigger = true;
				gameObject.rigidbody2D.isKinematic = true;
				gameObject.rigidbody2D.rotation = 0;
				collision.gameObject.GetComponent<Player>().Respawn();
			}
		}
	}

	/** <returns>Whether or not the given player is the owner of this axe.</returns>
	 * <param name="player">The player.</param> */
	public bool AmIOwner(Player player) {
		if(player == owner)
			return true;

		return false;
	}

	public Player Owner {
		/** <summary>Set the owner to the given value.</summary> */
		set { owner = value; }
	}
}
