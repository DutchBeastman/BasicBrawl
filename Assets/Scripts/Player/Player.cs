using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour, IInteractable {
	// speed can be edited in the Editor for easy customization
	[SerializeField]
	private float speed = 10.0f;

	private bool lookRight = true;

	private Animator animator;

	private Player player;

	private bool jump;

	void Start(){
		animator = GetComponent<Animator>();
	}


	void FixedUpdate() {
		PlayerMovement();
		Debug.DrawRay(transform.position, -Vector2.up, Color.green, 1, false);
		RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up, 4,9);
		if (hit.collider != null) {
			Debug.Log(hit.collider.name + " Dit hit ik");
		}
		//Determine the animation of the attack
		if(Input.GetKeyDown(KeyCode.Space)){
			if(Random.Range(0f, 1.0f) > 0.1f)
				animator.SetTrigger("attack");
			else
				animator.SetTrigger("special");
		}
	}

	/// <summary cref="C &lt; T &gt;">
	/// Executes the player movement.
	/// </summary>
	public void PlayerMovement(){
		float translation = Input.GetAxis("Horizontal") * speed;
		translation *= Time.deltaTime;
		transform.Translate(translation,0,0);

		//to determine where the player should look
		if(Input.GetAxis("Horizontal") > 0 && !lookRight){
			Flip();
		}
		if(Input.GetAxis("Horizontal") < 0 && lookRight){
			Flip();
		}

	}
	/// <summary>
	/// Flip this instance.
	/// </summary>
	public void Flip(){
		var s = transform.localScale;
		s.x *= -1;
		transform.localScale = s;
		lookRight = !lookRight;

	}

	/// <summary>
	/// Owner of the axe that is thrown.
	/// </summary>
	/// <param name="player">Defines the player that is the owner</param>
	public void OwnerAxe(Player player){

	}
	/// <summary>
	/// Throw the object, always moves in the forward direction
	/// </summary>
	/// <param name="dir">The direction of the throw</param>
	public void Throw(int dir) {
		dir = (int)transform.localScale.x;


	}
	
	public void PickUp(Transform position){

	}

	public bool PickUp() {
		return false;
	}

	public InteractableType GetInteractableType() {
		return InteractableType.PickUp;
	}
}
