using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

public class BulletCoder : MonoBehaviour
{
   

    
    public Vector3 direction;
    public float speed;
    public float damage;
    public float lifeTime;
    


    void Start()
    {
        
       
    }


    void Update()
    {
        transform.position += transform.forward * speed;
        Countdown();

    }

    private void Countdown() 
    {
        lifeTime -= 1 * Time.deltaTime;
        if (lifeTime  <= 0)
        {
            killBullet();
            lifeTime = 3; 
        }

    }
    private void Countdown2()
    {
        
        if (lifeTime <= Time.time)
        {
            killBullet();
        }

    }

    private void killBullet() 
    {
     
        Destroy(gameObject);    
     
    }

}
