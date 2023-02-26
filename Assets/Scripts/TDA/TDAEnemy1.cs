using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TDAenemy1 : MonoBehaviour
{
    [SerializeField] private Transform[] waypoints;
    private int m_currentLocationIndex;
    [SerializeField] private Rigidbody m_rigidbody;
    [SerializeField] Transform characterPosition;
    [SerializeField] float speed = 1f;
    [SerializeField] private float e_raycastDistance = 1f;
    [SerializeField] private LayerMask m_layerToCollideWith;
    [SerializeField] float rotationVelocity = 1f;
 
    void Update()
    {
        LookAtCharacter();
        EnemyAttackRange();
        TDAEnemyBehaviour();
    }
    public void Destruction()
    {
        Destroy(gameObject);
        GameManager.instance.SubstractEnemy();
    }
    private void TDAEnemyBehaviour()
    {
        Transform l_currentLocation = waypoints[m_currentLocationIndex];
        if (Vector3.Distance(transform.position, l_currentLocation.position) < 0.01f)
        {
            m_currentLocationIndex = ( m_currentLocationIndex+ 1 ) % waypoints.Length;
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, l_currentLocation.position, speed * Time.deltaTime);
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
    private void Move(Vector3 direction)
    {
        transform.position += direction * (speed * Time.deltaTime);
    }  
}

