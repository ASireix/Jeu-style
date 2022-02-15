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

    public Material white;
    public Material black;

    public MeshRenderer meshRenderer;
    public GameObject bulletToRotate;


    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * force);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        bulletToRotate.transform.Rotate(new Vector3(rotationSpeed, 0, 0));

    }

    public void SetBullet(string color)
    {
        
        switch (color)
        {
            case "black":
                meshRenderer.material = black;
                gameObject.layer = LayerMask.NameToLayer("Black");
                bulletColor = "black";
                break;
            case "white":
                meshRenderer.material = white;
                gameObject.layer = LayerMask.NameToLayer("White");
                bulletColor = "white";
                break;
            default:
                break;
        }
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

        if (collision.gameObject.tag == "Player")
        {
            Destroy(collision.gameObject);
        }/*
        else
        {
            var speed = lastVelocity.magnitude;
           
            var direction = Vector3.Reflect(lastVelocity.normalized, collision.contacts[0].normal);
           
            rb.velocity = direction * Mathf.Max(speed, 10f);

            
        }*/
    }
}
