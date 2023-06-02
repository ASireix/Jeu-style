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

    

    public IEnumerator Grow()
    {
        while(gameObject.transform.lossyScale.x  < maxSize)
        {
            gameObject.transform.localScale += new Vector3(growSpeed, growSpeed, growSpeed)*10;
            yield return null;
        }
    }

    public void Shrinkage()
    {
        StartCoroutine("Shrink");
    }

    public IEnumerator Shrink()
    {
        while (gameObject.transform.lossyScale.x > 0)
        {
            gameObject.transform.localScale -= new Vector3(shrinkSpeed, shrinkSpeed, shrinkSpeed)*10;
            yield return null;
        }
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            //other.GetComponent<Bullet>().InvertColor();
        }
    }
}
