using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCastEnemy1 : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Rigidbody m_rigidbody;
    [SerializeField] Transform characterPosition;
    [SerializeField] float persuitDistance = 0.5f;
    [SerializeField] float speed = 1f;
    [SerializeField] private float e_raycastDistance = 1f;
    [SerializeField] private LayerMask m_layerToCollideWith;
    [SerializeField] Vector3 initialRotation;
    [SerializeField] float rotationVelocity = 1f;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        RayCastEnemyBehaviour();
        LookAtCharacter();
        EnemyAttackRange();
    }
    public void Destruction()
    {
        Destroy(gameObject);
        GameManager.instance.SubstractEnemy();
    }
    private void RayCastEnemyBehaviour()
    {
        var vectorToCharacter = characterPosition.position - transform.position;
        var distance = vectorToCharacter.magnitude;
        if (distance > persuitDistance)
        {
            transform.position += vectorToCharacter.normalized * (speed * Time.deltaTime); 
        }  

    }
    private void EnemyAttackRange()
    {
        var l_hasCollided =
            Physics.Raycast(transform.position, transform.forward, out RaycastHit p_raycastHitInfo, e_raycastDistance,
                m_layerToCollideWith);

        if (l_hasCollided)
        {
            var character = p_raycastHitInfo.collider.GetComponent<RayCastCapsuleCharacter>();
            if (character != null)
            {
                character.CharacterDestruction();
            }

        }
    }
    private void LookAtCharacter()
    {
        var vectorToCharacter = characterPosition.position - transform.position;
        var newRotation = Quaternion.LookRotation(vectorToCharacter);
        transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, Time.deltaTime * rotationVelocity);
    }
}
