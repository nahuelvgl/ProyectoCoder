using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;
public enum PurpleZombieStates
{
    StationaryBeahaviour,
    ChaseBehaviour
}

public class PURPLEZombyEnemy : MonoBehaviour
{
    [SerializeField] private float actionTime = 1f;
    [SerializeField] Transform characterPosition;
    [SerializeField] float persuitDistance = 15f;
    [SerializeField] float speed = 5f;
    [SerializeField] private PurpleZombieStates currentState;
    [SerializeField] float rotationVelocity = 3f;
    private float timer = 1.5f;
    private bool attackdistance;
    [SerializeField] private Animator zombyEnemyAnimationController;
    bool enemyMovement;
    private Collider enemyCollider;
    private bool isDefeated = false;

    private void Start()
    {
        actionTime = 1f;

    }

    void Update()
    {
        if (!isDefeated)
        {
            SetCurrentState();
            LookAtCharacter();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        RobotCharacterController character = other.GetComponent<RobotCharacterController>();
        if (character != null)
        {
            attackdistance = true;
            AttackTimer();
        }
        else if (character = null)
        {
            attackdistance = false;
        }
    }

    private void HitDistance()
    {
        if (attackdistance == true && Input.GetKey(KeyCode.L))
        {
            isDefeated = true;
            zombyEnemyAnimationController.SetTrigger("Death");
        }
    }

    private void AttackTimer()
    {
        if (!isDefeated)
        {
            actionTime -= Time.deltaTime;
            if (actionTime <= 0)
            {
                GameObject character = GameObject.Find("RobotCharacter");
                RobotCharacterController robotCharacterControllerScript = character.GetComponent<RobotCharacterController>();
                robotCharacterControllerScript.RecieveDamage();
                zombyEnemyAnimationController.SetTrigger("Attack");
                ResetTimer();
            }
        }
    }

    private void ResetTimer()
    {
        actionTime = timer;
    }

    private void ChaseBehaviour()
    {
        if (!isDefeated)
        {
            var vectorToCharacter = characterPosition.position - transform.position;
            var distance = vectorToCharacter.magnitude;
            if (distance > persuitDistance)
            {
                transform.position += vectorToCharacter.normalized * (speed * Time.deltaTime);
                enemyMovement = true;
            }
            else
            {
                enemyMovement = false;
            }
            EnemyMovement();
            HitDistance();
        }
    }

    private void EnemyMovement()
    {
        if (enemyMovement == true && !isDefeated)
        {
            zombyEnemyAnimationController.SetFloat("Speed", 0.15f);
        }
        else if (enemyMovement == false || isDefeated)
        {
            zombyEnemyAnimationController.SetFloat("Speed", 0f);
        }
    }
    private void LookAtCharacter()
    {
        var vectorToCharacter = characterPosition.position - transform.position;
        var newRotation = Quaternion.LookRotation(vectorToCharacter);
        transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, Time.deltaTime * rotationVelocity);
    }
    public void SetCurrentState()
    {
        switch (currentState)
        {
            case PurpleZombieStates.StationaryBeahaviour:   
                break;
            case PurpleZombieStates.ChaseBehaviour:
                ChaseBehaviour();
                break;
            default:
                break;
        }
    }
}