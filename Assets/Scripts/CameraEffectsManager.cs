using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class CameraEffectsManager : MonoBehaviour
{

    void OnEnable()
    {
        ((Camera)gameObject.GetComponent(typeof(Camera))).depthTextureMode = DepthTextureMode.DepthNormals;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
