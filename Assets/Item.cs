using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
[RequireComponent(typeof(BoxCollider2D))]

public class Item : MonoBehaviour
{
    //Collider Trigger
    //Interaction Type
    public enum InteracsionType{
        NONE, 
        PickUp, 
        Examine
    }

    public InteracsionType type;

    //Customer Event
    public UnityEvent customEvent;


    private void Reset(){
        GetComponent<Collider2D>().isTrigger = true;
        gameObject.layer = 10;
    }
  
    public void Interact(){
        switch(type)
        {
            case InteracsionType.PickUp:
                GameObject item = gameObject;
                FindObjectOfType<InteractionSystem>().PickUpItem(item);
                gameObject.SetActive(false);
                break;
            case InteracsionType.Examine:
                break;
            default:
            break;
        }

        customEvent.Invoke();
    }

    
}
