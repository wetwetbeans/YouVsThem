using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Photon.Pun;

[RequireComponent(typeof(CharacterController))]
public class Movement : MonoBehaviourPunCallbacks
{
	
	//Variables Start Here
	
	public static bool shooting = false;
	
	
	[SerializeField] float playerSpeed = 5f;
	[SerializeField] float gravityValue = -9.81f;
	[SerializeField] float controllerDeadzone = 0.14f;
	[SerializeField] float gamepadRotateSmoothing = 1000f;
	[SerializeField] GameObject mobileJoysticks;
	[SerializeField] bool isGamepad;
	[SerializeField] bool isMobile;
	
	private Vector2 movement;
	private Vector2 aim;
	
	private Vector3 playerVelocity;
	
	private CharacterController controller;
	private PlayerControls _playerControls;

	public PhotonView _photonView;
	
	
	//Functions Start Here
	
	protected void Awake()
	{
		_photonView = GetComponent<PhotonView>();
		isMobile = true;
		
 //Check Device Type
		if (SystemInfo.deviceType == DeviceType.Handheld)
		{
			isMobile = true;
		}
		
 // Turn on mobile functions if on handheld
		if (isMobile)
		{
			TurnOnMobileFunctions();
		}
		controller = GetComponent<CharacterController>();
		_playerControls = new PlayerControls();
	}
	
 // Do When Enabled
	protected void OnEnable()
	{
		_playerControls.Enable();
	}
 // Do When Disabled
	protected void OnDisable()
	{
		_playerControls.Disable();
	}
	
	
 // Do Every Frame
    void Update()
	{

		// Set Gamepad true if on mobile
		if (isMobile)
		{
			isGamepad = true;
		}

		if (!_photonView.IsMine)
		{
			Debug.Log("notmine");
			return;
		}
		Debug.Log("mine");
		HandleInput();
		HandleMovement();
		HandleRotation();
		SetShootingPosition();

	    
	}
    
	private void TurnOnMobileFunctions()
	{
		mobileJoysticks.SetActive(true);
		isGamepad = true;
	}
	
	
// Input  
	private void HandleInput()
	{
	//set movement and aim vectors for movement
		movement = _playerControls.Controls.Movement.ReadValue<Vector2>();
		if(isMobile)
		{
			aim = _playerControls.Controls.AimMobile.ReadValue<Vector2>();
		}
		else
		{
			aim = _playerControls.Controls.Aim.ReadValue<Vector2>();
		}
	}
	
	
//Movement
	private void HandleMovement()
	{
		Vector3 move = new Vector3(movement.x, 0, movement.y);	
		controller.Move(move * Time.deltaTime * movement.magnitude * playerSpeed);
		//adding gravity for falling
		playerVelocity.y = gravityValue;
		controller.Move(playerVelocity * Time.deltaTime);
	}
	
	
//Rotation
	private void HandleRotation()
	{
		if (isGamepad)
		{
			//rotate player 
			if (Mathf.Abs(aim.x) > controllerDeadzone || Mathf.Abs(aim.y) > controllerDeadzone)
			{
				Vector3 playerDirection = Vector3.right * aim.x + Vector3.forward * aim.y;
				if (playerDirection.sqrMagnitude > 0.0f)
				{
					Quaternion newRotation = Quaternion.LookRotation(playerDirection, Vector3.up);
					transform.rotation = Quaternion.RotateTowards(transform.rotation, newRotation, gamepadRotateSmoothing * Time.deltaTime);
				}
			}
		}
		else 
		{
			if (!isMobile) 
			{
				Ray ray = Camera.main.ScreenPointToRay(aim);
				Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
				float rayDistance;
			
				if(groundPlane.Raycast(ray, out rayDistance))
				{
					Vector3 point = ray.GetPoint(rayDistance);
					LookAt(point);
				}
			}		
		}		
	}
	
	
// Set Right Thumb Stick Shooting Magnitude (Position)
	private void SetShootingPosition()
	{
		if (aim.magnitude > 0.95f)
		{
			shooting = true;
		}
		else shooting = false;
	}
	
	private void LookAt (Vector3 lookPoint)
	{
		Vector3 heightCorrectedPoint = new Vector3(lookPoint.x, transform.position.y, lookPoint.z);
		transform.LookAt(heightCorrectedPoint);
	}
	
	public void OnDeviceChange(PlayerInput pi)
	{
		isGamepad = pi.currentControlScheme.Equals("Gamepad") ? true : false;
	}
	
	public Vector2 GetAimVector ()
	{
		return aim;
	}
	public Vector2 GetMovementVector()
	{
		return movement;
	}
}
