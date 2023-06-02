using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class Bullet : MonoBehaviour
{
    Rigidbody rb;
    public float startForce;
    public int rotationSpeed;
    public float dmg;

    public float maxSpeed;
    public GameObject bulletToRotate;
    Team team;
    public float currentSpeed;
    public float grazeAmount;

    public int maxArmor;
    int armor;
    [SerializeField] GameObject destroyVFX;
    public UnityEvent<GameObject> OnBulletDestroyed = new UnityEvent<GameObject>();
    
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * startForce);
        currentSpeed = maxSpeed;
        armor = maxArmor;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        bulletToRotate.transform.Rotate(new Vector3(rotationSpeed, 0, 0));
        Vector3 vel = rb.velocity;
        if (vel.magnitude > currentSpeed)
        {
            rb.velocity = rb.velocity = vel.normalized * currentSpeed;
        }
        else
        {
            rb.AddForce(transform.forward * 10f);
        }
    }

    public void SetSpeed(float speed)
    {
        maxSpeed = speed;
        currentSpeed = speed;
        rb.AddForce(transform.forward * 5000);
        Vector3 vel = rb.velocity;
        if (vel.magnitude > currentSpeed)
        {
            rb.velocity = rb.velocity = vel.normalized * currentSpeed;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        transform.rotation = Quaternion.LookRotation(rb.velocity, transform.up);

        string leTag = collision.gameObject.tag;

        if (leTag == "Player")
        {
            collision.gameObject.GetComponent<PlayerController>().DecreaseHealth(dmg);
            HandleDestruction();
        }
        else if (leTag == "Bullet")
        {
            if (armor <= 0)
            {
                HandleDestruction();
            }
            else
            {
                armor--;
            }
        }
            /*
        else
        {
            var speed = lastVelocity.magnitude;
           
            var direction = Vector3.Reflect(lastVelocity.normalized, collision.contacts[0].normal);
           
            rb.velocity = direction * Mathf.Max(speed, 10f);

            
        }*/
    }

    protected virtual void HandleDestruction()
    {

        GameObject vfx = Instantiate(destroyVFX, transform);
        vfx.transform.SetParent(null);
        gameObject.SetActive(false);
        OnBulletDestroyed.Invoke(gameObject);
        Destroy(gameObject, 1f);
        Destroy(vfx, 10f);
    }

    public void SetMaxSpeed(float s)
    {
        maxSpeed = s;
        currentSpeed = s;
    }

    public bool CompareTeam(Team t)
    {
        return t == team;
    }
}
