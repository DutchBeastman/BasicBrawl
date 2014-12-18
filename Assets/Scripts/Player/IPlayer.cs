using UnityEngine;

public interface IPlayer {
	/** <summary>Executes the player movement</summary>*/
	 void PlayerMovement();

	/** <summary>Throw the object, always moves in the forward direction</summary>
	 * <param name="dir">The direction of the throw</param> */
	void Throw(int direction);
}