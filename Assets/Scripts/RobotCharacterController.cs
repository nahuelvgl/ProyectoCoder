using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class RobotCharacterController : MonoBehaviour
{
    [SerializeField] private float speed;
    private Vector3 direction;
    public float health = 100;
    [SerializeField] private Animator robotCharacterAnimationController;
    bool characterMovement;
    bool rotation;
    // Start is called before the first frame update
    void Start()
    { 
        speed = 2f;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (health > 0)
        {
        var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");
        var direction = new Vector3(horizontal, 0, vertical);
        if (direction != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(direction);
        }
       
            Move(direction);
            CharacterMovement();
            JAttack();
            KAttack();
            LAttack();
        }
        Death();
       
    }
    private void Move(Vector3 moveDirection)
    {
        if (health <= 0) return;
        transform.position += moveDirection * (speed * Time.deltaTime);
      
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S))
        {
            characterMovement = true;
            rotation = true;
        }
        else
        {
            characterMovement = false;
            rotation = false;
        }
    }
    public void RecieveDamage()
    {
        health -= 50f;
    }
    private void CharacterMovement()
    {
        if (health <= 0) return;
        if (characterMovement == true || rotation == true)
        {
            robotCharacterAnimationController.SetFloat("Speed", 0.15f);
            

        }
     else if (characterMovement == false)
        {
            robotCharacterAnimationController.SetFloat("Speed", 0f);
           
        }
    }
   
    private void JAttack()
    {
        if (health <= 0) return;
        if (Input.GetKeyDown(KeyCode.J))
        {

            robotCharacterAnimationController.SetTrigger("JAttack");

        }
    }
    private void KAttack()
    {
        if (health <= 0) return;
        if (Input.GetKeyDown(KeyCode.K))
        {

            robotCharacterAnimationController.SetTrigger("KAttack");

        }
    }
    private void LAttack()
    {
        if (health <= 0) return;
        if (Input.GetKeyDown(KeyCode.L))
        {

            robotCharacterAnimationController.SetTrigger("LAttack");

        }
    }
    private void Death()
    {
        if (health <= 0)
        {
            AnimatorStateInfo stateInfo = robotCharacterAnimationController.GetCurrentAnimatorStateInfo(0);
            if (!stateInfo.IsName("Death"))
            {
                robotCharacterAnimationController.SetTrigger("Death");
            }
        }
    }
}
