using UnityEngine;
using UnityEngine.AI;

[RequireComponent (typeof(DamageManager))]
[RequireComponent (typeof(CharacterController))]
public class EnemyAdvanced : MonoBehaviour
{
	public NavMeshAgent NavAgent;
	public float GravityMult = 1;
	public float Slip = 10;
	private float fallvelocity = 0;
	private Vector3 moveDirection;
	private Vector3 movePosition;

	public Animator animator;
	public float Speed = 3;
	public float PatolDistance = 200;
	public float AiDelayMax = 10;
	public float AiDelayMin = 1;
	public AudioClip[] footstepSound;
	public Vector3 targetPosition;
	public string[] TargetTag = { "Player" };

	public bool Rush = false;
	public float DetectDistance = 30;
	public float MoveToDistance = 20;
	public float AttackDistance = 10;

	public Transform MuzzlePoint;
	public GameObject Projectile;
	public float ShootDelay = 0.5f;
	public float ShootRate = 1;
	public float ShootForce = 3000;
	public float ShootSpread = 10;
	public GameObject ShootFX;
	private int state = 0;
	private float timethink = 0;
	private CharacterController characterController;
	private float shoottemp = 0;
	public GameObject Target;
	public AudioClip SoundShoot;
	public AudioSource Audio;

	void Start ()
	{
		NavAgent = GetComponent<NavMeshAgent> ();
		Audio = this.GetComponent<AudioSource> ();
		characterController = this.GetComponent<CharacterController> ();

		if (animator == null)
			animator = this.GetComponent<Animator> ();
	}

	void OnHit(){
		if (animator) {
			animator.SetTrigger ("Hit");
		}
	}

	void Update ()
	{
		isGrounded = GroundChecking ();
		targetPosition.y = transform.position.y;
		Quaternion rotationTarget = Quaternion.LookRotation ((targetPosition - this.transform.position).normalized);
		transform.rotation = Quaternion.Lerp (this.transform.rotation, rotationTarget, Time.deltaTime * 5);


		if (Target == null) {
			if (timethink <= 0) {
				targetPosition = new Vector3 (Random.Range (-PatolDistance, PatolDistance), 0, Random.Range (-PatolDistance, PatolDistance));
				timethink = Random.Range (AiDelayMin, AiDelayMax);
				state = Random.Range (0, 2);
			} else {
				timethink -= 1 * Time.deltaTime;
			}

			switch (state) {
			case 0:
				movePosition = targetPosition;
				Vector3 direction = (targetPosition - transform.position).normalized;
				moveDirection = Vector3.Lerp (moveDirection, direction, Time.deltaTime * Slip);
				break;
			case 1:
				movePosition = this.transform.position;
				moveDirection = Vector3.zero;
				break;
			}

			animator.SetInteger ("State", 0);
			findTarget ();
		} else {
			float distancetotarget = Vector3.Distance (Target.transform.position, this.transform.position);

			animator.SetInteger ("State", 1);
			Vector3 posShot = this.transform.position + Vector3.up;
			if (MuzzlePoint)
				posShot = MuzzlePoint.transform.position;
			
			if (distancetotarget <= AttackDistance) {
				targetPosition = Target.transform.position;

				Vector3 pretargetdir = (Target.transform.position - Vector3.up * 1f) - this.transform.position;
				RaycastHit precheck;
				if (Physics.Raycast (posShot, pretargetdir.normalized, out precheck, 10000)) {
					if (precheck.collider && precheck.collider.CompareTag ("Player")) {
						if (Random.Range (0, 99) < 100 * ShootRate) {
							if (Time.time > shoottemp + ShootDelay) {
								animator.SetTrigger ("Shoot");
								Vector3 spread = new Vector3 (Random.Range (-ShootSpread, ShootSpread), Random.Range (-ShootSpread, ShootSpread), Random.Range (-ShootSpread, ShootSpread)) / 100;
								Vector3 dir = ((Target.transform.position + spread - (Vector3.up * 1f)) - this.transform.position);
						
								shootTarget (posShot, dir.normalized);
								movePosition = this.transform.position;
								moveDirection = Vector3.zero;
								shoottemp = Time.time;
							}
						}
					}
				}

			} else {
				if (distancetotarget <= MoveToDistance || Rush) {
					targetPosition = Target.transform.position;
					Vector3 direction = (Target.transform.position - transform.position).normalized;
					movePosition = Target.transform.position;
					moveDirection = Vector3.Lerp (moveDirection, direction, Time.deltaTime * Slip);
				}
			}

			if (distancetotarget > DetectDistance && !Rush) {
				Target = null;
			}
		}

		if (!isGrounded) {
			fallvelocity -= 90 * GravityMult * Time.deltaTime;
		}

		moveDirection.y = fallvelocity;
		if (NavAgent && NavAgent.isOnNavMesh) {
			NavAgent.SetDestination (movePosition);
			animator.SetFloat ("MoveVelocity", NavAgent.velocity.magnitude);
		} else {
			if (characterController)
				characterController.Move (moveDirection * Speed * Time.deltaTime);
			animator.SetFloat ("MoveVelocity", moveDirection.magnitude);
		}
	}

	void findTarget ()
	{
        if (AIManager.TargetFinder == null)
            return;
		for (int i = 0; i < TargetTag.Length; i++) {
            TargetCollector targetList = AIManager.TargetFinder.FindTargetTag(TargetTag[i]);
            if (targetList == null)
                return;
            GameObject[] targetGet = targetList.Targets;
            for (int v = 0; v < targetGet.Length; v++) {
				if (targetGet [v] != null) {
					float distance = Vector3.Distance (targetGet [v].transform.position, this.transform.position);
					if (distance < DetectDistance || Rush) {
						Target = targetGet [v];
					}
				}
			}
		}
	}

	void shootTarget (Vector3 position, Vector3 direction)
	{
		if (Projectile) {
			GameObject bullet = (GameObject)GameObject.Instantiate (Projectile.gameObject, position, this.transform.rotation);
			bullet.transform.forward = direction;
			Rigidbody rig = bullet.GetComponent<Rigidbody> ();
			if (rig) {
				rig.AddForce (direction * ShootForce, ForceMode.Impulse);
			}

			GameObject.Destroy (bullet, 3);

			if (Audio && SoundShoot) {
				Audio.PlayOneShot (SoundShoot);
			}
		}
		if (ShootFX) {
			GameObject shootfx = (GameObject)GameObject.Instantiate (ShootFX, position, Quaternion.identity);
			shootfx.transform.forward = direction;
			GameObject.Destroy (shootfx, 2);
		}	
	}

	public float DistanceToGround = 0.1f;
	private bool isGrounded = false;
	public bool GroundChecking ()
	{
		if (GetComponent<Collider> ()) {
			RaycastHit hit;
			if (characterController.isGrounded)
				return true;
			if (Physics.Raycast (GetComponent<Collider> ().bounds.center, -Vector3.up, out hit, DistanceToGround + 0.1f)) {
				return true;
			}
		}
		return false;
		
	}
}
