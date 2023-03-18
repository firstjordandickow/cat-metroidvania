using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    [SerializeField] private Vector2 parallaxEffectMultipler;

    private Transform cameraTransform;
    private Vector3 lastCameraPosition;
    private void Start() {
        cameraTransform = Camera.main.transform;
        lastCameraPosition = cameraTransform.position;
    }

    private void LateUpdate() {
        Vector3 deltaMovement = cameraTransform.position - lastCameraPosition;
        
        transform.position += new Vector3(deltaMovement.x * parallaxEffectMultipler.x, deltaMovement.y * parallaxEffectMultipler.y);
        lastCameraPosition = cameraTransform.position;
    }
}
