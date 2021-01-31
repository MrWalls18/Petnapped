using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour
{
    [SerializeField]private Camera playerCamera;
    [SerializeField]private float interactDistance;
    [SerializeField]private LayerMask layerMask;

    private GameManager gameManager;

    private void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

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

        if (Input.GetKeyDown(KeyCode.Z))
        {
            gameManager.ShowAndHideClue();
        }
    }

    void InteractWith(RaycastHit interactableObject)
    {
        switch (interactableObject.transform.tag)
        {
            case "Clue":
                interactableObject.transform.gameObject.SetActive(false);
                gameManager.ShowNextClue();
                break;
            case "Box":
                gameManager.CheckBox(interactableObject.transform.gameObject);
                break;
            case "Key":
                Destroy(interactableObject.transform.gameObject);
                gameManager.hasKey = true;
                break;
            default:
                Debug.LogError("Error: Incorrect tag: " + interactableObject.transform.tag);
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "KeyCheck")
        {
            gameManager.CheckForKey();
        }
    }
}
