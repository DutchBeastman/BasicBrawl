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

	private Vector2 footBase;

	void Start(){

		animator = GetComponent<Animator>();
	}


	void FixedUpdate() {
		PlayerMovement();
		footBase = new Vector2(transform.position.x,transform.position.y -0.15f);
		Debug.DrawRay(footBase, -Vector2.up, Color.green, 0.5f, false);
		RaycastHit2D hit = Physics2D.Raycast(footBase, -Vector2.up, 0.5f);
		if (hit.collider.gameObject.name == "ground") {
			Debug.Log(hit.collider.name + " Dit hit ik");
			jump = false;
		}
		//Determine the animation of the attack
		if(Input.GetKeyDown(KeyCode.Space)){
			if(Random.Range(0f, 1.0f) > 0.1f){
				animator.SetTrigger("attack");
				Throw((int)transform.localScale.x);
			}
			else{
				animator.SetTrigger("special");
				Throw((int)transform.localScale.x);
			}
		}
	}

	/// <summary cref="C &lt; T &gt;">
	/// Executes the player movement.
	/// </summary>
	public void PlayerMovement(){

		float translationY = Input.GetAxis ("Vertical") * 15;
		float translation = Input.GetAxis("Horizontal") * speed;
		translation *= Time.deltaTime;
		translationY *= Time.deltaTime;
		transform.Translate(translation,translationY,0);
		animator.SetFloat("speed", 1);
		if(!jump){
			jump = true;
		}

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
		gameObject.GetComponent<AxeManager>().ThrowAxe();


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
