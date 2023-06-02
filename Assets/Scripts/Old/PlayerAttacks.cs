using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttacks : MonoBehaviour
{

    public GameObject bullet;
    public Transform shootPosition;

    public string color;

    public Graze areoOfGraze;

    public float shootCD;
    public float invertCD;

    float currentShootCD;
    float currentInvertCD;

    public CDHelper cDHelper;

    private void Start()
    {
        
    }

    private void Update()
    {
        if (currentShootCD > 0)
        {
            currentShootCD -= Time.deltaTime;
        }

        if (currentInvertCD > 0)
        {
            currentInvertCD -= Time.deltaTime;
        }

        cDHelper.abilityOneCD.text = (int)currentShootCD + "";
        cDHelper.abilityTwoCD.text = (int)currentInvertCD + "";
    }

    public void OnShoot(InputAction.CallbackContext context)
    {
        if (context.performed && currentShootCD<=0)
        {
            GameObject tempBullet = Instantiate(bullet, shootPosition.position, transform.rotation);
            //tempBullet.GetComponent<Bullet>().SetBullet(color);
            currentShootCD = shootCD;
        }
        
    }

    public void OnInvert(InputAction.CallbackContext context)
    {
        if (context.performed && currentInvertCD <= 0)
        {
            /*foreach (var item in areoOfGraze.nearColliders)
            {
                //item.gameObject.GetComponent<Bullet>().Switch();
            }*/
            currentInvertCD = invertCD;
        }

    }



}
