using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour, IPlayer {
	public const string AXE_PREFAB = "Prefabs/Axe";
	
	[SerializeField] private Transform respawnPoint;

	[SerializeField] private KeyCode attackKey;

	[SerializeField] private string horizontal;
	[SerializeField] private string vertical;

	[SerializeField] private float speed = 10.0f;

	[SerializeField] private int numAxes = 3;
	[SerializeField] private int jumpHeight = 500;
	
	private Animator animator;

	private Vector2 footBase;

	private bool canPickUp;
	private bool lookRight = true;

	private void Start() {
		animator = GetComponent<Animator>();
	}

	private void Update() {
		// Determine the animation of the attack and the direction
		if(Input.GetKeyDown(attackKey)) {
			if(Random.Range(0f, 1.0f) > 0.1f) {
				Throw((int)transform.localScale.x);
				animator.SetTrigger("attack");
			} else {
				Throw((int)transform.localScale.x);
				animator.SetTrigger("special");
			}
		}

		// Execute the player movement and everything inside it.
		PlayerMovement();
	}

	private void OnTriggerEnter2D(Collider2D collision){
		if(collision.collider2D.CompareTag("Axe") && canPickUp){
			Axe axe = collision.GetComponent<Axe>();
			
			if(axe.AmIOwner(this)) {
				Destroy(collision.collider2D.gameObject);
				numAxes++;
			}
		}
	}

	/** <summary> Executes the player movement.</summary> */
	public void PlayerMovement() {
		// Get the input and make the player move
		float hor = Input.GetAxis(horizontal);
		float translation = (hor * speed) * Time.deltaTime;

		transform.Translate(translation, 0, 0);

		// Determine if you can jump by casting a raycast to the ground.
		footBase = new Vector2(transform.position.x,transform.position.y -0.15f);

		RaycastHit2D hit = Physics2D.Raycast(footBase, -Vector2.up, 0.5f);
		if(hit.collider != null) {
			if(hit.collider.CompareTag("Ground") && Input.GetAxis(vertical) > 0) {
				rigidbody2D.AddForce(new Vector2(0, jumpHeight));
			}
		}

		// Executes the walk animation 
		if(hor > 0 || hor < 0){
			animator.SetFloat("speed", 1);
		} else {
			animator.SetFloat("speed", 0);
		}

		// To determine where the player should look
		if((hor > 0 && !lookRight) || (hor < 0 && lookRight)) {
			Flip();
		}
	}

	/** <summary>Throw an axe.</summary>
	 * <param name="direction">The direction to throw the axe in.</param> */
	public void Throw(int direction) {
		// Here I instantiate the axe if I have any left then use the same localscale rotation to determine where to rotate its image to.
		if(numAxes > 0) {
			GameObject axe = GameObject.Instantiate(Resources.Load(AXE_PREFAB),new Vector3(transform.position.x, transform.position.y + 1,0), Quaternion.identity) as GameObject;
			
			Axe axeComponent = axe.GetComponent<Axe>();
			axeComponent.Owner = this;
			
			Vector3 s = transform.localScale;

			axe.transform.localScale = s;
			axe.rigidbody2D.AddForce(new Vector2(direction * 1250 , 0));

			canPickUp = false;
			numAxes--;
			StartCoroutine(PickUp());
		}
	}

	public void Respawn(){
		StartCoroutine(CoRespawn());
	}

	/** <summary>Flip the player.</summary> */
	private void Flip(){
		var s = transform.localScale;
		s.x *= -1;

		transform.localScale = s;
		lookRight = !lookRight;
	}
	
	/** <summary>Pickup delay after throwing an axe.</summary> */
	private IEnumerator PickUp() {
		yield return new WaitForSeconds(2);

		canPickUp = true;
	}

	/** <summary>Respawn delay.</summary> */
	private IEnumerator CoRespawn() {
		yield return new WaitForSeconds(3);

		transform.position = respawnPoint.transform.position;

		gameObject.SetActive(true);
	}
}
