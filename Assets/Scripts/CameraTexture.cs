using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CameraTexture : MonoBehaviour
{
    public RawImage camera_texture;
    private RawImage rawImage;
    // Get static sprite
    void Start()
    {
        rawImage = GetComponent<RawImage>();
    }

    // Apply sprite to the camera 
    void Update()
    {
        rawImage.texture = camera_texture.texture;
    }
}