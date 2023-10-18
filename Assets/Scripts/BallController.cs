using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour {

    public Rigidbody rb;
    public float impulseForce = 5f;

    private Vector3 startPos;
    public int perfectPass = 0;
    private bool ignoreNextCollision;
    public bool isSuperSpeedActive;
    public ParticleSystem bounceFx;
    public AudioClip bounce;
    public AudioClip fall;
    public AudioClip powerup;
    public AudioSource soundPlayer;

    private void Awake()
    {
        startPos = transform.position;
    }



    private void OnCollisionEnter(Collision other)
    {
        if (ignoreNextCollision)
            return;
        if (isSuperSpeedActive)
        {
            if (!other.transform.GetComponent<Goal>())
            {
                /*foreach (Transform t in other.transform.parent)
                {
                    gameObject.AddComponent<TriangleExplosion>();

                    StartCoroutine(gameObject.GetComponent<TriangleExplosion>().SplitMesh(true));
                    //Destroy(other.gameObject);
                    Debug.Log("exploding - exploding - exploding - exploding");
                }*/
                Destroy(other.transform.parent.gameObject);

            }

        }
        // If super speed is not active and a death part git hit -> restart game
        else
        {
            DeathPart deathPart = other.transform.GetComponent<DeathPart>();
            if (deathPart)
                deathPart.HittedDeathPart();
        }

        rb.velocity = Vector3.zero; // Remove velocity to not make the ball jump higher after falling done a greater distance
        rb.AddForce(Vector3.up * impulseForce, ForceMode.Impulse);
        bounceFx.Play();
        soundPlayer.PlayOneShot(bounce, 1.0f);



        // Safety check
        ignoreNextCollision = true;
        Invoke("AllowCollision", .2f);

        // Handlig super speed
        perfectPass = 0;
        isSuperSpeedActive = false;
    }

    private void Update()
    {
        // activate super speed
        if (perfectPass >= 3 && !isSuperSpeedActive)
        {
            isSuperSpeedActive = true;
            rb.AddForce(Vector3.down * 10, ForceMode.Impulse);
        }
    }

    public void ResetBall()
    {
        transform.position = startPos;
        
    }

    private void AllowCollision()
    {
        ignoreNextCollision = false;
    }

    public void PlayFall()
    {
        soundPlayer.PlayOneShot(fall, 1.0f);
    }

    public void PlayPowerup()
    {
        soundPlayer.PlayOneShot(powerup, 1.0f);
    }

}
