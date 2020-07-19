using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : LivingEntity
{
    Vector2 playerInput;
    Vector3 velocity;

    AudioSource audioSource;

    public float maxAcceleration;
    public float maxSpeed;

    public Turret turret;

    float shotCooldown = .5f;
    float currentShot = .5f;


    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.Play();
    }
    private void Update()
    {
        PlayerMovement();
        Shoot();
        currentShot += Time.deltaTime;

        audioSource.volume = Mathf.Abs(velocity.x);
    }

    void PlayerMovement()
    {
        playerInput.x = Input.GetAxis("Horizontal");

        //playerInput = Vector2.ClampMagnitude(playerInput, 1f); //not need bec

        var desiredVelocity = new Vector3(playerInput.x, 0f, 0f) * maxSpeed;

        float maxSpeedChange = maxAcceleration * Time.deltaTime;

        velocity.x = Mathf.MoveTowards(velocity.x, desiredVelocity.x, maxSpeedChange);

        Vector3 displacement = velocity * Time.deltaTime;

        Vector3 newPosition = transform.localPosition + displacement;

        if (newPosition.x < -28)
        {
            transform.localPosition = new Vector3(-28, transform.localPosition.y, 0f);
            velocity.x = 0;
        }
        else if (newPosition.x > 28)
        {
            transform.localPosition = new Vector3(28, transform.localPosition.y, 0f);
            velocity.x = 0;
        }
        else
        {
            transform.localPosition = newPosition;
            
        }
    }

    void Shoot()
    {
        if (Input.GetKeyDown("space") && currentShot >= shotCooldown)
        {
            turret.ShootProjectile(this.velocity);
            currentShot = 0;
            
        }
    }
}
