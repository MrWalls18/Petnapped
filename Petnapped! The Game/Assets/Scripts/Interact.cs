using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour
{
    [SerializeField]private Camera playerCamera;
    [SerializeField]private float interactDistance;
    [SerializeField]private LayerMask layerMask;


    // Update is called once per frame
    void Update()
    {
        RaycastHit interactableObject;

        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out interactableObject, interactDistance, layerMask))
        {
            Debug.Log("Press E to interact");


            if (Input.GetKeyDown(KeyCode.E))
            {
                InteractWith(interactableObject);
            }
        }
    }

    void InteractWith(RaycastHit interactableObject)
    {
        switch (interactableObject.transform.tag)
        {
            case "Key":
                Destroy(interactableObject.transform.gameObject);
                break;
            case "Door":
                    Debug.LogWarning("Key is needed!");
                break;
            case "Tutorial Door":
                Destroy(interactableObject.transform.gameObject);
                break;
            default:
                Debug.LogError("Error: Incorrect tag: " + interactableObject.transform.tag);
                break;
        }
    }
}
