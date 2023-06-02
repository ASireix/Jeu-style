using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitbox : MonoBehaviour
{
    [SerializeField] bool continuous;
    [SerializeField] float ticks;
    [SerializeField] float totalDamage;
    float duration;
    [SerializeField] List<PlayerController> playerControllers;

    // Start is called before the first frame update
    void Start()
    {
        playerControllers = new List<PlayerController>();
    }
    
    // Update is called once per frame
    private void FixedUpdate()
    {
        if (continuous)
        {
            foreach (var item in playerControllers)
            {
                item.DecreaseHealth(ticks);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (continuous)
            {
                playerControllers.Add(other.GetComponent<PlayerController>());
            }
            else
            {
                other.GetComponent<PlayerController>().DecreaseHealth(totalDamage);
            }
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (continuous)
        {
            playerControllers.Remove(other.GetComponent<PlayerController>());
        }
    }

    public void SetDamage(float dmg, float d, LayerMask layer)
    {
        totalDamage = dmg;
        duration = d * 50f;
        ticks = totalDamage / duration;
        gameObject.layer = layer; 
    }

}
