using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionSystem : MonoBehaviour
{
[Header("Detection Parameters")] 
public Transform detectionPoint;
private const float detectionRadius = 0.2f;
public LayerMask detectionLayer;
public GameObject detectedObject;
public List<GameObject> pickedItem = new List<GameObject>();

    void Update()
    {
        if(DetectObject()){
            detectedObject.GetComponent<Item>().Interact();
        }
    }

    private void OnDrawGizmosSelected(){
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(detectionPoint.position,detectionRadius);
    }

    bool InteractInput(){
        return Input.GetKeyDown(KeyCode.E);
    }

    bool DetectObject(){

       Collider2D obj = Physics2D.OverlapCircle(detectionPoint.position, detectionRadius, detectionLayer);
       if(obj == null){
            detectedObject = null;
            return false;
       }
       else{
        detectedObject = obj.gameObject;
        return true;
       }
    }

    public void PickUpItem(GameObject item){
        pickedItem.Add(item);

    }
    
}
