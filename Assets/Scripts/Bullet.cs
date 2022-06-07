using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    string bulletColor;
    Rigidbody rb;
    public float force;
    public int rotationSpeed;
    public float dmg;

    public Outline outline;

    public Material white;
    public Material black;

    public MeshRenderer meshRenderer;
    public GameObject bulletToRotate;

    [SerializeField]
    float currentBlinkTime;
    public float blinkTime;

    // Start is called before the first frame update
    void Start()
    {
        currentBlinkTime = blinkTime;
        rb = gameObject.GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * force);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        bulletToRotate.transform.Rotate(new Vector3(rotationSpeed, 0, 0));

        if (currentBlinkTime > 0)
        {
            currentBlinkTime -= Time.deltaTime;
            outline.enabled = true;
        }
        else
        {
            outline.enabled = false;
        }
        
    }

    public void SetBullet(string color)
    {
        
        switch (color)
        {
            case "black":
                meshRenderer.material = black;
                gameObject.layer = LayerMask.NameToLayer("Black");
                bulletColor = "black";
                outline.OutlineColor = Color.white;
                break;
            case "white":
                meshRenderer.material = white;
                gameObject.layer = LayerMask.NameToLayer("White");
                bulletColor = "white";
                outline.OutlineColor = Color.black;
                break;
            default:
                break;
        }
    }

    public void InvertColor()
    {
        if (bulletColor == "white")
        {
            SetBullet("black");
        }
        else
        {
            SetBullet("white");
        }
    }

    public void BlinkOutline()
    {
        currentBlinkTime = blinkTime;
    }

    public void Switch()
    {
        if (bulletColor == "white")
        {
            SetBullet("black");
        }
        else
        {
            SetBullet("white");
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        transform.rotation = Quaternion.LookRotation(rb.velocity, transform.up);

        currentBlinkTime = blinkTime;

        string leTag = collision.gameObject.tag;

        if (leTag == "Player")
        {
            collision.gameObject.GetComponent<PlayerController>().DecreaseHealth(dmg);
            Destroy(gameObject);
        }
        else if (leTag == "Bullet")
        {
            Destroy(collision.gameObject);
        }
            /*
        else
        {
            var speed = lastVelocity.magnitude;
           
            var direction = Vector3.Reflect(lastVelocity.normalized, collision.contacts[0].normal);
           
            rb.velocity = direction * Mathf.Max(speed, 10f);

            
        }*/
    }
}
