using UnityEngine;

public enum InteractableType {
	PickUp
}

public interface IInteractable {
	/** <summary>Executes the player movement</summary>*/
	 void PlayerMovement();

	/** <summary>Throw the object, always moves in the forward direction</summary>
	 * <param name="dir">The direction of the throw</param> */
	void Throw(int dir);
	
	/** <summary>Get the type of the object, should never change</summary>
	 * <returns>The type of the object</returns> */
	InteractableType GetInteractableType();

}