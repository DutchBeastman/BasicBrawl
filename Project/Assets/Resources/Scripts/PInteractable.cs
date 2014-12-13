using UnityEngine;

public enum InteractableType {
	PickUp,
	Loot
}

public interface PInteractable {
	/** <summary>Executes the player movement</summary>
	 * <param name="playerVelocity">Dictates the player velocity</param> */
	void PlayerMovement(Vector2 playerVelocity);
	
	/** <summary>Throws the Object</summary> */
	void Throw();
	
	/** <summary>Throw the object, always moves in the forward direction</summary>
	 * <param name="forward">The forward direction</param>
	 * <param name="up">The up direction</param> */
	void Throw(Vector3 forward);
	
	/** <summary>Lock or unlock the object, if it's locked it can't be picked up or move</summary>
	 * <param name="locked">Whether or not the object is locked</param> */
	void PickUp(bool locked);
	
	/** <summary>Get the type of the object, should never change</summary>
	 * <returns>The type of the object</returns> */
	InteractableType GetInteractableType();
	
	/** <returns>Whether or not the object is currently locked</returns> */
	bool PickUp();
}