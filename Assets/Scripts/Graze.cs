using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graze : MonoBehaviour
{

    public List<SphereCollider> nearColliders = new List<SphereCollider>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Bullet")
        {
            nearColliders.Add(other.gameObject.GetComponent<SphereCollider>());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Bullet")
        {
            nearColliders.Remove(other.gameObject.GetComponent<SphereCollider>());
        }
    }
}
