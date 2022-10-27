using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Photon.Pun;

public class EnemyManager : MonoBehaviour
{
    public GameObject closestPlayer;
    NavMeshAgent _navMeshAgent;


    bool canHit = true;
    bool isNotInCollision = true;
    bool isInPlayerCollider = false;
    bool searchForPlayer = true;
    enemyAnimationController _enemyAnimationController;
    public GameObject bulletPlayerGameObject;

    public GameObject[] players;
    bool isDead = false;

    [Header("Zombie info")]
    [SerializeField] float speed;
    [SerializeField] float secondsBetweenHit;
    [SerializeField] float health;
    [SerializeField] int damagePerHit;
    [SerializeField] int KillPointsToAdd;
    [SerializeField] int killsToAdd = 1;


    
    // Start is called before the first frame update
    void Start()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
        closestPlayer = players[0];  
        
        // initialise references
        _enemyAnimationController = GetComponent<enemyAnimationController>();
        _navMeshAgent = GetComponent<NavMeshAgent>();

        //set follow speed
        _navMeshAgent.speed = speed;
    }


    // Done Every Frame
    void Update()
    {
        if (isDead == false)
        {
            if (PhotonNetwork.InRoom && !PhotonNetwork.IsMasterClient)
            {
                return;
            }
            if (searchForPlayer)
            {
                // searchForPlayer = false;
                // StartCoroutine("searchForPlayerTimer");
                findClosestPlayers(); ;
            }
            if (health <= 0)
                Death();
        }

    }

    public void findClosestPlayers()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
        Transform closestPlayer = null;
        float closestPlayerToDistance = float.MaxValue;
        NavMeshPath path = new NavMeshPath();

        for (int i = 0; i < players.Length; i++)
        {
            if (NavMesh.CalculatePath(transform.position, players[i].transform.position, _navMeshAgent.areaMask, path))
            {
                float distance = Vector3.Distance(transform.position, path.corners[0]);
                for (int j = 1; j < path.corners.Length; j++)
                {
                    distance += Vector3.Distance(path.corners[j - 1], path.corners[j]);
                }
                if (distance < closestPlayerToDistance)
                {
                    closestPlayerToDistance = distance;
                    closestPlayer = players[i].transform;
                }
            }
        }

        if (closestPlayer != null)
        {
            _navMeshAgent.SetDestination(closestPlayer.position);
        }
    }

    IEnumerator searchForPlayerTimer()
    {
        yield return new WaitForSeconds(3);
        searchForPlayer = true;
    }

    private void OnCollisionEnter(Collision collision)
    {

        if (isNotInCollision)
        {
            canHit = true;
            if (collision.gameObject == closestPlayer)
            {
                isInPlayerCollider = true;
                //set attacking animation
                _enemyAnimationController.StationaryAttack();
                //stop zombie movement
                _navMeshAgent.enabled = false;
                //stop OnCollisionEnter from repeating whilst still in collider
                isNotInCollision = false;
                //Give damage to player
                StartCoroutine(CanHitPlayerMethod());
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject == closestPlayer)
        {
            isInPlayerCollider = false;
            canHit = false;
            _enemyAnimationController.NotAttacking();
            isNotInCollision = true;
            //start zombie movement
            _navMeshAgent.enabled = true;
            findClosestPlayers();
        }

    }

    IEnumerator CanHitPlayerMethod()
    {
        if (canHit)
        {
            yield return new WaitForSeconds(secondsBetweenHit);
            if (isInPlayerCollider)
            {
                closestPlayer.GetComponent<PlayerManager>().Hit(damagePerHit);
            }

            StartCoroutine(CanHitPlayerMethod());
        }
        else yield break;
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
    }

    void Death()
    {
        isDead = true;
    //Add to player kill count in player manager and UI;;
        bulletPlayerGameObject.GetComponent<PlayerManager>().AddToKills(killsToAdd);
    //Add Points
        bulletPlayerGameObject.GetComponent<PlayerManager>().AddToScore(KillPointsToAdd);
        //Stop movement and change to death animation
        _navMeshAgent.enabled = false;
        _enemyAnimationController.Death();
        GetComponent<BoxCollider>().enabled = false;
        GetComponent<EnemyManager>().enabled = false;
    }



}
