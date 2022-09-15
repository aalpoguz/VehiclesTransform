using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowController : MonoBehaviour
{
    public CarController carcontroller;
    public FlyController flycontroller;
    [SerializeField] private Transform carTransform;
    [SerializeField] private Transform jetTransform;
    [SerializeField] private Transform yachtransform;

    private Vector3 offset;
    private Vector3 newPosition;

    [SerializeField][Range(0, 3)] private float lerpValue;
    void Start()
    {
        offset = transform.position - carTransform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        SmoothCameraFollow();
    }

    private void SmoothCameraFollow()
    {
        if (carcontroller.isFlyActive == false && flycontroller.isSwimActive ==false)
        {
            newPosition = Vector3.Lerp(transform.position, carTransform.position + offset, lerpValue * Time.deltaTime);
            transform.position = newPosition;
        }

        if (carcontroller.isFlyActive == true )
        {
            newPosition = Vector3.Lerp(transform.position, jetTransform.position + offset, lerpValue * Time.deltaTime);
            transform.position = newPosition;
        }
        
        
        
        if (flycontroller.isSwimActive == true && carcontroller.isFlyActive == false)
        {
            newPosition = Vector3.Lerp(transform.position, yachtransform.position + offset, lerpValue * Time.deltaTime);
            transform.position = newPosition;
        }


    }
}
