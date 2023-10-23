using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FaceShader : MonoBehaviour
{
    [SerializeField] Renderer head;
    Transform headTransform;
    Material faceShader;
    // Start is called before the first frame update
    void Start()
    {
        headTransform = head.transform;
        faceShader = head.material;
    }

    // Update is called once per frame
    void Update()
    {
        faceShader.SetVector("_HeadForward", headTransform.forward);
        faceShader.SetVector("_HeadRight", headTransform.right);
    }
}
