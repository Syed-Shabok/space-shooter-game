using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyOne : MonoBehaviour
{   
    private int enemySlayPoints = 1; 
    public GameObject explosionVfx;
    public GameManager manager;
    

    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.FindGameObjectWithTag("Manager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Messile")
        {   
            //Spawns Explosion VFX at position where collision with messile is detected.
            Vector3 currentPosition = this.transform.position;
            GameObject explosionInstance = Instantiate(explosionVfx, currentPosition, explosionVfx.transform.rotation);
            Destroy(explosionInstance, 1.0f);
            
            //Destroys the emeny and the messile instances. 
            Destroy(this.gameObject);
            Destroy(collision.gameObject);
            
            //Updates player score based on enemy teir.
            manager.UpdatePlayerScore(enemySlayPoints);
        }
    }
}
