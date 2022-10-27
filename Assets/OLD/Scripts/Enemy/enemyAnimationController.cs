using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemyAnimationController : MonoBehaviour
{
    Animator _anim;
    GameObject player;
    NavMeshAgent _zombieNavmesh;


    // Start is called before the first frame update
    void Start()
    {
        _zombieNavmesh = GetComponentInParent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");
        _anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player.GetComponent<PlayerManager>().isPlayerDead)
        {
            _anim.SetBool("isWalking", false);
            _anim.SetBool("isAttacking", false);
        }

        //set animations based on speed
        if (_zombieNavmesh.velocity.magnitude > 0.3f && _zombieNavmesh.isActiveAndEnabled)
        {
            _anim.SetBool("isWalking", true);
            _anim.SetBool("isAttacking", false);
        }
        else _anim.SetBool("isWalking", false);
    }

    public void StationaryAttack()
    {
        _anim.SetBool("isWalking", false);
        _anim.SetBool("isAttacking", true);
    }

    public void NotAttacking ()
    {
        _anim.SetBool("isAttacking", false);
    }

    public void Death()
    {
        _anim.SetBool("Death", true);
        _anim.SetBool("isWalking", false);
        _anim.SetBool("isAttacking", false);
    }


}
