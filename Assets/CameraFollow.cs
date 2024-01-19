using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    public float smoothFactor;
    public Vector3 minValue, maxValue;
    public void FixedUpdate(){
       Follow();
    }
    
    void Follow(){
        Vector3 targetPosition = target.position+offset;


        Vector3 boundPosition = new Vector3(
            Mathf.Clamp(targetPosition.x, minValue.x,maxValue.x),
            Mathf.Clamp(targetPosition.y, minValue.y,maxValue.y),
            Mathf.Clamp(targetPosition.z, minValue.z,maxValue.z)
        );

        Vector3 smoootheedPosition = Vector3.Lerp(transform.position, boundPosition, smoothFactor*Time.fixedDeltaTime);
         transform.position= smoootheedPosition;
    }
}
