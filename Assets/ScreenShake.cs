using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ScreenShake : MonoBehaviour
{
    public float shakeMagnitude = 0.1f;
    public float shakeDuration = 0.2f;
    
    private float shakeTimer = 0f;
    private Vector3 initialPosition;
    
    private void Start()
    {
        initialPosition = transform.position;
    }
    
    private void Update()
    {
        if (shakeTimer > 0f)
        {
            // Generate random offset based on Perlin noise
            Vector2 shakeOffset = new Vector2(
                Mathf.PerlinNoise(Time.time * 50f, 0f),
                Mathf.PerlinNoise(0f, Time.time * 50f)
            ) * 2f - Vector2.one;
            
            // Apply offset to camera position
            transform.position = initialPosition + (Vector3)shakeOffset * shakeMagnitude;
            
            shakeTimer -= Time.deltaTime;
        }
        else
        {
            transform.position = initialPosition;
        }
        
     
    }
    
    public void Shake()
    {
        shakeTimer = shakeDuration;
    }

    
}