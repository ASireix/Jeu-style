using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaMatManager : MonoBehaviour
{
    MeshRenderer meshRenderer;
    // Start is called before the first frame update
    void Start()
    {
        meshRenderer = gameObject.GetComponent<MeshRenderer>();
    }

    public void SetColor(Material wall, Material ground)
    {


        meshRenderer.materials[0] = wall;
        meshRenderer.materials[1] = ground;
    }
}
