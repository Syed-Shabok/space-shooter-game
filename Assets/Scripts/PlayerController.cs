using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{   
    public float horizontalInput;
    public float verticalInput;
    public Joystick joystick;
    public float playerSpeed = 10.0f;
    public float xBound = 7.80f;
    public float yBound = 3.90f;

    public GameObject messile;
    public Transform messileShootPosition;
    public GameObject muzzleFlash;
    public Transform muzzleFlashPosition;
    public GameObject explosionVfx;
    public AudioClip messileFireSound;
    public AudioClip playerExplosionSound;
    public AudioSource audioSource;
    public float sfxVolume = 1.0f;
    
    public GameManager manager;

    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovement();
    }

    //Function used to control player movement.
    public void PlayerMovement()
    {   
        //If statements used to prevent the player from going off-screen.
        if (transform.position.x < -xBound)
        {
            transform.position = new Vector3(-xBound, transform.position.y, transform.position.z);
        }
        if (transform.position.x > xBound)
        {
            transform.position = new Vector3(xBound, transform.position.y, transform.position.z);
        }
        if (transform.position.y < -yBound)
        {
            transform.position = new Vector3(transform.position.x, -yBound, transform.position.z);
        }
        if (transform.position.y > yBound)
        {
            transform.position = new Vector3(transform.position.x, yBound, transform.position.z);
        }
        
        // if (joystick.Horizontal >= 0.2f)
        // {
        //     horizontalInput = playerSpeed;
        // } 
        // else if (joystick.Horizontal <= -0.2f)
        // {
        //     horizontalInput = -playerSpeed;
        // }
        // else
        // {
        //     horizontalInput = 0.0f;
        // }

        // if (joystick.Vertical >= 0.2f)
        // {
        //     verticalInput = playerSpeed;
        // } 
        // else if (joystick.Vertical <= -0.2f)
        // {
        //     verticalInput = -playerSpeed;
        // }
        // else
        // {
        //     verticalInput = 0.0f;
        // }

        //Code used to take user input and translate it into player movement.
        horizontalInput = joystick.Horizontal; 
        verticalInput = joystick.Vertical;

        transform.Translate(Vector3.right * horizontalInput * Time.deltaTime * playerSpeed);
        transform.Translate(Vector3.up * verticalInput * Time.deltaTime * playerSpeed);
    }

    public void PlayerShoot()
    {
        GameObject messileInstance = Instantiate(messile, messileShootPosition.position, messile.transform.rotation);
        SpawnMuzzleFlash();
        audioSource.PlayOneShot(messileFireSound, sfxVolume);
        Destroy(messileInstance, 2.0f);
    }

    public void SpawnMuzzleFlash()
    {
        GameObject messileVfxInstance = Instantiate(muzzleFlash, muzzleFlashPosition.position, muzzleFlash.transform.rotation);
        Destroy(messileVfxInstance, 1.0f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Asteroid")
        {   
            //Spawns Explosion VFX at position where collision with enemy is detected.
            Vector3 currentPosition = this.transform.position;
            GameObject explosionInstance = Instantiate(explosionVfx, currentPosition, explosionVfx.transform.rotation);
            Destroy(explosionInstance, 1.0f);
            
            //Destroys the player and enemy instances. 
            Destroy(this.gameObject);
            Destroy(collision.gameObject);

            //Playes an explosion sound on player's death.
            audioSource.PlayOneShot(playerExplosionSound, sfxVolume);

            //Makes the Game Over Menu appear.
            manager.GameOver();
        }
    }

}
