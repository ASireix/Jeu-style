using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graze : MonoBehaviour
{
    [SerializeField]
    List<Collider> nearColliders = new List<Collider>();
    PlayerController playerController;
    // Start is called before the first frame update
    void Start()
    {
        playerController = GetComponentInParent<PlayerController>();
        gameObject.layer = playerController.gameObject.layer;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Bullet")
        {
            nearColliders.Add(other);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Bullet")
        {
            nearColliders.Remove(other);
            GameManager.instance.UpdateGauge(other.GetComponent<Bullet>().grazeAmount, playerController.playerNumber);
        }
    }
}
