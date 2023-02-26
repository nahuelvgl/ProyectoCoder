using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Bullet : MonoBehaviour
{   [SerializeField] private Vector3 scale;
    [SerializeField] private float selfDestruct;
    [SerializeField] private float speed;
    private KeyCode scaleTransform;
    private Vector3 direction;
    

    void Start()
    {
        scaleTransform = KeyCode.Space;
        scale = transform.localScale;
        speed = 0.05f;
        direction = new Vector3(0,0.1f,1);
    }

    private void ScaleTransform(Vector3 scale)
    {
        transform.localScale += scale;
    }

    void Update()
    {
        transform.position += direction * speed;
        SelfDestruct();
        if (Input.GetKeyDown(scaleTransform))
        {
            ScaleTransform(scale);
        }
    }
  
    private void SelfDestruct()
    {
        selfDestruct -= Time.deltaTime;
        if (selfDestruct <= 0) 
        {
            DestroyBullet();
        }
    }

    private void DestroyBullet()
    {
        Destroy(gameObject);
    }
}
