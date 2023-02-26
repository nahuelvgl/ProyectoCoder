using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField] private int enemiesLeft = 2;
    private RayCastCapsuleCharacter m_RayCastCapsuleCharacter;


    public RayCastCapsuleCharacter GetRayCastCapsuleCharacter()
    {
        return m_RayCastCapsuleCharacter;
    }

    public void SetRayCastCapsuleCharacter(RayCastCapsuleCharacter p_RayCastCapsuleCharacter)
    {
        m_RayCastCapsuleCharacter = p_RayCastCapsuleCharacter;
    }

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(this);
            instance = this;
        }
    }

    public void SubstractEnemy()
    {
        enemiesLeft -= 1;
    }

    public int GetTotalEnemiesLeft()
    {
        return enemiesLeft;
    }

}

