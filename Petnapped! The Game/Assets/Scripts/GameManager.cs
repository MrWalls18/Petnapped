using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject enemies, mazeExitDoor;
    [SerializeField] private List<Image> clueCards;
    [SerializeField] private List<GameObject> cluePickups;
    [SerializeField] private List<GameObject> boxOrder;
    [SerializeField] private Material boxMat;
    public Image interact, closeClue, readClue;
    public int clueCounter = 0;
    public int boxCounter = 0;
    private bool readingClue = false;
    public bool hasKey = false;
    public GameObject mazeDoor;
    public AudioSource paper, rightBox, wrongBox, doorOpen;




    private void Update()
    {
        if (readingClue && clueCounter >0)
        {
            closeClue.enabled = true;
            readClue.enabled = false;
        }
        else if (!readingClue && clueCounter > 0)
        {
            readClue.enabled = true;
            closeClue.enabled = false;
        }
    }

    public void ShowNextClue()
    {
        paper.Play();
        clueCards[clueCounter].enabled = true;
        clueCounter++;
        if (clueCounter < 2)
            cluePickups[clueCounter].SetActive(true);
        readingClue = true;
    }

    public void ShowAndHideClue()
    {
        int tempCounter = clueCounter;
        if (tempCounter > 0)
        {
            if (readingClue)
            {
                clueCards[--tempCounter].enabled = false;
                readingClue = false;
            }
            else
            {
                clueCards[--tempCounter].enabled = true;
                readingClue = true;
            }
        }
    }

    public void CheckBox(GameObject box)
    {
        if (clueCounter == 2 && boxCounter < boxOrder.Count)
        {
            if (box == boxOrder[boxCounter])
            {
                rightBox.Play();
                box.GetComponent<Renderer>().material.color = Color.green;

                boxCounter++;
            }

            else
            {
                foreach (GameObject boxes in boxOrder)
                {
                    boxes.GetComponent<Renderer>().material.color = boxMat.color;
                }

                wrongBox.Play();
                boxCounter = 0;
            }
        }

        if (boxCounter >= boxOrder.Count)
        {
            cluePickups[clueCounter].SetActive(true);
            mazeDoor.SetActive(false);
        }
    }



    public void CheckForKey()
    {
        if (hasKey)
        {
            cluePickups[clueCounter].SetActive(true);
            enemies.SetActive(false);
            mazeExitDoor.SetActive(true);
        }

    }

    public void WinScreen()
    {
        SceneManager.LoadScene("WinScene");
        Cursor.lockState = CursorLockMode.None;
    }
}
