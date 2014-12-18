using UnityEngine;
using System.Collections;

public class Player2 : MonoBehaviour {

	
	public const string AXE_PREFAB = "Prefabs/Axe2";
	// speed can be edited in the Editor for easy customization
	[SerializeField]private float speed = 10.0f;
	[SerializeField]private int numAxes = 3;
	[SerializeField]private int jumpHeight = 500;
	[SerializeField]private Transform respawnPoint;
	
	
	private bool lookRight = true;
	private Animator animator;
	private Player player;
	private bool jump;
	private bool canPickUp;
	private Vector2 footBase;
	private GameObject axe;
	
	
	
	
	void Start(){
		
		animator = GetComponent<Animator>();
	}
	
	
	void Update() {
		//Determine the animation of the attack and the direction
		if(Input.GetKeyDown(KeyCode.Keypad0)){
			if(Random.Range(0f, 1.0f) > 0.1f){
				Throw((int)transform.localScale.x);
				animator.SetTrigger("attack");
			}
			else{
				Throw((int)transform.localScale.x);
				animator.SetTrigger("special");
				
			}
		}
		//Execute the player movement and everything inside it.
		PlayerMovement();
		
		
		
		
	}
	
	/// <summary cref="C &lt; T &gt;">
	/// Executes the player movement.
	/// </summary>
	public void PlayerMovement(){
		//get the input and make the player move
		float translation = Input.GetAxis("HorizontalArrow") * speed;
		translation *= Time.deltaTime;
		transform.Translate(translation,0,0);
		
		//Determine if you can jump by casting a raycast to the ground.
		footBase = new Vector2(transform.position.x,transform.position.y -0.15f);
		RaycastHit2D hit = Physics2D.Raycast(footBase, -Vector2.up, 0.5f);
		if (hit.collider.tag == "Ground" && Input.GetAxis ("VerticalArrow") > 0) {
			rigidbody2D.AddForce(new Vector2(0f, jumpHeight));
			
		}		
		//executes the walk animation 
		if(Input.GetAxis("HorizontalArrow") > 0 || Input.GetAxis("HorizontalArrow") < 0){
			animator.SetFloat("speed", 1);
		}else{
			animator.SetFloat("speed", 0);
		}
		//to determine where the player should look
		if(Input.GetAxis("HorizontalArrow") > 0 && !lookRight){
			Flip();
		}
		if(Input.GetAxis("HorizontalArrow") < 0 && lookRight){
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
	/// Throw the object, always moves in the forward direction
	/// </summary>
	/// <param name="dir">The direction of the throw</param>
	public void Throw(int dir) {
		//Here I instantiate the axe if I have any left then use the same localscale rotation to determen where to rotate its image to.
		if(numAxes > 0){
			axe = GameObject.Instantiate(Resources.Load(AXE_PREFAB),new Vector3(transform.position.x, transform.position.y + 1,0), Quaternion.identity) as GameObject;
			var s = transform.localScale;
			s.x *= 1;
			axe.transform.localScale = s;
			axe.rigidbody2D.AddForce(new Vector2(dir * 1250 , 0));
			canPickUp = false;
			numAxes -= 1;
			StartCoroutine(PickUp());
		}
		
	}
	
	/// <summary>
	/// The axe will turn into an pickup after 2 seconds
	/// </summary>
	/// <returns>Wheter you can pick up the axe</returns>
	IEnumerator PickUp(){
		yield return new WaitForSeconds(2f);
		canPickUp = true;
	}
	public void Respawn(){
		StartCoroutine(CoRespawn());
	}
	IEnumerator CoRespawn(){
		yield return new WaitForSeconds(3f);
		transform.position = respawnPoint.transform.position;
		gameObject.SetActive(true);
	}
	
	public InteractableType GetInteractableType() {
		return InteractableType.PickUp;
	}
	
	void  OnTriggerEnter2D(Collider2D collision){
		if(collision.collider2D.name == "Axe2" && canPickUp){
			Destroy(collision.gameObject);
			numAxes =+ 1;
		}
	}
}
