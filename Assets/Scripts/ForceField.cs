using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceField : MonoBehaviour
{
    public float maxSize;

    public float shrinkSpeed;
    public float growSpeed;

    private void Start()
    {
        gameObject.transform.localScale = Vector3.zero;
        StartCoroutine("Grow");
    }

    private void Update()
    {
        
    }

    public IEnumerator Grow()
    {
        while(gameObject.transform.localScale.x  < maxSize)
        {
            gameObject.transform.localScale += new Vector3(growSpeed, growSpeed, growSpeed);
            yield return null;
        }
    }

    public IEnumerator Shrink()
    {
        while (gameObject.transform.localScale.x > 0)
        {
            gameObject.transform.localScale -= new Vector3(shrinkSpeed, shrinkSpeed, shrinkSpeed);
            yield return null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            other.GetComponent<Bullet>().InvertColor();
        }
    }
}
