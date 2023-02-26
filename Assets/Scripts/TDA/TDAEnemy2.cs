using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TDAEnemy2 : MonoBehaviour
{
    [SerializeField] private Transform[] waypoints;
    private int m_currentWaypointIndex;
    [SerializeField] Transform characterPosition;
    [SerializeField] float speed = 1f;
    [SerializeField] private float e_raycastDistance = 5f;
    [SerializeField] private LayerMask m_layerToCollideWith;
    [SerializeField] float rotationVelocity = 3f;
    private void Awake()
    {
        m_currentWaypointIndex = Random.Range(0, waypoints.Length);
    }
    void Update()
    {
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
        Transform l_currentLocation = waypoints[m_currentWaypointIndex];
        if (Vector3.Distance(transform.position, l_currentLocation.position) < 0.01f)
        {
            m_currentWaypointIndex = (m_currentWaypointIndex + 1) % waypoints.Length;
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, l_currentLocation.position, speed * Time.deltaTime);
        }
        LookAtCharacter();
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
    public void Init()
    {
        m_currentWaypointIndex = Random.Range(0, waypoints.Length);
    }
    public void ReceiveWaypoints(Transform[] p_waypoints)
    {
        waypoints = p_waypoints.ToArray();
    }

}