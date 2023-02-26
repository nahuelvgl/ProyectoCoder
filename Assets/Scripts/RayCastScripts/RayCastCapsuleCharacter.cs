using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static RayCastEnemy1;

public class RayCastCapsuleCharacter : MonoBehaviour
{
    [SerializeField] private float speed = 2f;
    private float m_raycastDistance;
    [SerializeField] private List<float> weapons;
    private int currentWeaponIndex = 0;
    [SerializeField] private LayerMask m_layerToCollideWith;
    void Start()
    {
        speed = 2f;
        m_raycastDistance = weapons[currentWeaponIndex];
    }

    // Update is called once per frame
    void Update()
    {
        var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");
        var direction = new Vector3(horizontal, 0, vertical);
        if (direction != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(direction);
        }
        Move(direction);
        if (Input.GetKeyDown(KeyCode.J))
        {
            CharacterAttackRange();
        }
        if(Input.GetKeyDown(KeyCode.E))
        {
            NextWeapon();
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            PreviousWeapon();
        }
    }

    private void Move(Vector3 moveDirection)
    {
        transform.position += moveDirection * (speed * Time.deltaTime);


    }
    private void CharacterAttackRange()
    {
        var l_hasCollided =
            Physics.Raycast(transform.position, transform.forward, out RaycastHit p_raycastHitInfo, m_raycastDistance,
                m_layerToCollideWith);

        if (l_hasCollided)
        {
            var enemy = p_raycastHitInfo.collider.GetComponent<RayCastEnemy1>();
            if (enemy != null)
            {
                enemy.Destruction();
            }
        
        }
    }
    public void CharacterDestruction()
    {
        Destroy(gameObject);
    }
    private void NextWeapon()
    {
        Debug.Log("Next Weapon");
        currentWeaponIndex = (currentWeaponIndex + 1) % weapons.Count;
        m_raycastDistance = weapons[currentWeaponIndex];
    }
    private void PreviousWeapon()
    {
        Debug.Log("Previous Weapon");
        currentWeaponIndex = (currentWeaponIndex - 1) % weapons.Count;
        m_raycastDistance = weapons[currentWeaponIndex];
    }
}
