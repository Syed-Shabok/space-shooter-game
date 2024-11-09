using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileScript : MonoBehaviour
{   
    public float missileSpeed = 20.0f;
    public float deadZone = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y > deadZone)
        {
            Destroy(gameObject);
        }

        transform.Translate(Vector3.up * Time.deltaTime * missileSpeed);
    }
    

    //Destroys the messile when it hits an Asteroid. 
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Asteroid")
        {       
            Destroy(this.gameObject);
        }
    }

}
