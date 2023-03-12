using UnityEngine;

public class FPSInputControllerMobile : MonoBehaviour {

	private GunHanddle gunHanddle;
	private FPSController FPSmotor;
	
	public TouchScreenVal touchMove;
	public TouchScreenVal touchAim;
	public TouchScreenVal touchShoot;
	public TouchScreenVal touchZoom;
	public TouchScreenVal touchJump;

	public Joystick Joystick;
	public Joystick Joystick2;
	
	public Texture2D ImgButton;
	public float TouchSensMult = 0.05f;

	public float VerticalMoveSpeed;
	public float HorizontalMoveSpeed;
	public float AimSpeed;

	private bool isRun;

	public RigthTouchMovement rt;
	bool check;
	bool canSound = true;

	void Start(){
		check = false;
		Application.targetFrameRate = 60;
	}
	void Awake ()
	{
		FPSmotor = GetComponent<FPSController> ();		
		gunHanddle = GetComponent<GunHanddle> (); 
	}

	void Update ()
	{
		if (Joystick.Vertical > 0.7f)
        {
			isRun = true;
		//	canSound = true;
		}
			
		else
        {
			isRun = false;
			GameManager.Instance.footSound.Stop();
		}


		if (Joystick.Vertical > 0.7f && canSound)
		{

			GameManager.Instance.footSound.Play();
			canSound = false;
		}


			


		if (isRun)
        {
			FPSmotor.Boost(1.2f);
			
		}
		FPSmotor.Move( new Vector3(Joystick.Horizontal * HorizontalMoveSpeed * Time.deltaTime, 0, Joystick.Vertical * VerticalMoveSpeed * Time.deltaTime) );
		FPSmotor.Aim(new Vector2(Joystick2.Horizontal*Time.deltaTime* AimSpeed, Joystick2.Vertical * Time.deltaTime * AimSpeed));
		//FPSmotor.Aim(new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y")));
	//	FPSmotor.Aim(new Vector2(rt.GetHorizontalAxis()*Time.deltaTime* AimSpeed, rt.GetVerticalAxix() * Time.deltaTime * AimSpeed));

		Vector2 aimdir = touchAim.OnDragDirection(true);
		//FPSmotor.Aim(new Vector2(aimdir.x,-aimdir.y)*TouchSensMult);
		Vector2 touchdir = touchMove.OnTouchDirection (false);
		//FPSmotor.Move (new Vector3 (touchdir.x, 0, touchdir.y));


		
		FPSmotor.Jump (Input.GetButton ("Jump"));
		
		if(touchShoot.OnTouchPress()){
			gunHanddle.Shoot();	
		}
		if(touchZoom.OnTouchRelease()){
			gunHanddle.ZoomToggle();
		}
	}

	
	
	
	void OnGUI(){
		
	/*	touchMove.Draw();
		touchAim.Draw();
		touchShoot.Draw();
		touchZoom.Draw();*/
		
	}

	public void Zoom()
    {
		check = !check;

		GameManager.Instance.ZoomButton1.SetActive(check);
		GameManager.Instance.ZoomButton2.SetActive(check);
        
		gunHanddle.Zoom();

	}

	public void Shoot()
    {
		
			gunHanddle.Shoot();
			
		
		
		
	}

	public void Reload()
    {
		check = false;
		GameManager.Instance.ZoomButton1.SetActive(check);
		GameManager.Instance.ZoomButton2.SetActive(check);
		gunHanddle.Reload();

		
	}

	public void ZoomIn()
    {
		gunHanddle.ZoomAdjust(+1);
	}

	public void ZoomOut()
    {
		gunHanddle.ZoomAdjust(-1);
	}

	public void JoystickDown()
    {
		canSound = true;
    }






}
