using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using RootMotion.FinalIK;


public class PlayerAnimationsController : MonoBehaviour
{
	newMovement playerTwinStickMovement;
	private Vector2 playerMovementVector;
	[SerializeField] Animator anim;
	bool dying = false;

    void Start()
	{
		playerTwinStickMovement = GetComponentInParent<newMovement>();
    }
 

    void Update()
	{

	// Convert movement vector for Animations Depending on direction facing
		Vector2 convertedMovementVector = ConvertMovementInput(transform, playerMovementVector);
	//Set Animations
		anim.SetFloat("Forwards", convertedMovementVector.y );
		anim.SetFloat("Sidewards", convertedMovementVector.x);

		playerMovementVector = playerTwinStickMovement.GetMovementVector();

		SetStates();
	}
    
    

	private Vector2 ConvertMovementInput(Transform playerTrans, Vector2 movementInput) 
	{
		Vector2 playerDir = new Vector2(playerTrans.forward.x, playerTrans.forward.z);
		float angleDiff = Vector2.SignedAngle(Vector2.up, playerDir);
		return Rotate(movementInput, -angleDiff);
	}
 
	private Vector2 Rotate(Vector2 v, float degrees) {
		float radians = degrees * Mathf.Deg2Rad;
		float sin = Mathf.Sin(radians);
		float cos = Mathf.Cos(radians);
 
		float tx = v.x;
		float ty = v.y;
 
		return new Vector2(cos * tx - sin * ty, sin * tx + cos * ty);
	}
	
	void SetStates ()
	{
		//Default State
		if (transform.position.y >=0 && dying == false)
		{
			anim.SetTrigger("Movement");
		}
		//Falling
		if (transform.position.y <= -0.5)
		{
			anim.SetTrigger("Falling");
		}
		if (GetComponentInParent<PlayerManager>().CheckHealth() == 0)
        {
			dying = true;
			anim.SetTrigger("Dying");
			GetComponentInChildren<FullBodyBipedIK>().enabled = false;
			anim.ResetTrigger("Movement");
        }
    
	}
}
