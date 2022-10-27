using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Photon.Realtime;
using Photon.Pun;
using Cinemachine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(PlayerInput))]
public class newMovement : MonoBehaviour
{

    public static bool shooting = false;


    [SerializeField] CinemachineVirtualCamera _camera;
    [SerializeField] private float playerSpeed;
    [SerializeField] private float gravityValue = -0.981f;
    [SerializeField] private float controllerDeadZone = 0.1f;
    [SerializeField] private float rotateSmoothing = 0.1f;

    CharacterController _characterController;

    private Vector2 movement;
    private Vector2 aim;

    private Vector3 playerVelocity;

    private PlayerControls _playerControls;

    PhotonView _photonView;

    


    private void Awake()
    {
        _photonView = GetComponent<PhotonView>();
        _characterController = GetComponent<CharacterController>();
        _playerControls = new PlayerControls();
        // turn off cameras that are not yours
        if (PhotonNetwork.InRoom && !_photonView.IsMine)
        {
            Destroy(_camera);
        }
    }

    private void OnEnable()
    {

        _playerControls.Enable();
    }

    private void OnDisable()
    {
        _playerControls.Disable();
    }


    // Update is called once per frame
    void Update()
    {

        if (PhotonNetwork.InRoom && !_photonView.IsMine)
        {
            Debug.Log("notmine");
            return;
        }
        HandleInput();
        HandleMovement();
        HandleRotation();
        SetShootingPosition();
    }

    private void HandleInput()
    {
     //set movement and aim vectors for movement
        movement = _playerControls.Controls.Movement.ReadValue<Vector2>();
        aim = _playerControls.Controls.AimMobile.ReadValue<Vector2>();

    }

    private void HandleMovement()
    {
        Vector3 move = new Vector3(movement.x, 0, movement.y);
        _characterController.Move(move * Time.deltaTime * movement.magnitude * playerSpeed);
     //adding gravity for falling
        playerVelocity.y += gravityValue * Time.deltaTime;
        _characterController.Move(playerVelocity * Time.deltaTime);
    }
    private void HandleRotation()
    {
        //rotate player 
        if (Mathf.Abs(aim.x) > controllerDeadZone || Mathf.Abs(aim.y) > controllerDeadZone)
        {
            Vector3 playerDirection = Vector3.right * aim.x + Vector3.forward * aim.y;
            if (playerDirection.sqrMagnitude > 0.0f)
            {
                Quaternion newRotation = Quaternion.LookRotation(playerDirection, Vector3.up);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, newRotation, rotateSmoothing * Time.deltaTime);
            }
        }
    }





    public Vector2 GetAimVector()
    {
        return aim;
    }
    public Vector2 GetMovementVector()
    {
        return movement;
    }

    private void SetShootingPosition()
    {
        if (aim.magnitude > 0.95f)
        {
            shooting = true;
        }
        else shooting = false;
    }

}
