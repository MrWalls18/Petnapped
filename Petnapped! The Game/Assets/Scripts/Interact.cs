using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
            if (interactableObject.transform.gameObject.layer == LayerMask.NameToLayer("interactable"))
            {
                gameManager.interact.enabled = true;
                if (Input.GetKeyDown(KeyCode.E))
                {
                    InteractWith(interactableObject);
                }
            }
            else
                gameManager.interact.enabled = false;


            
        }
        else
        {
            gameManager.interact.enabled = false;
        }


        if (Input.GetKeyDown(KeyCode.R))
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
            case "Fence":
                if (gameManager.hasKey)
                {
                    Destroy(interactableObject.transform.gameObject);
                    gameManager.doorOpen.Play();
                }
                break;
            case "Dog":
                gameManager.WinScreen();
                break;
            default:
                Debug.LogError("Error: Incorrect tag: " + interactableObject.transform.tag);
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Car")
        {
            SceneManager.LoadScene("GameOver");
            Cursor.lockState = CursorLockMode.None;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "KeyCheck")
        {
            gameManager.CheckForKey();
        }
    }

}
