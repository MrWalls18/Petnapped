using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private List<Image> clueCards;
    [SerializeField] private List<GameObject> cluePickups;
    [SerializeField] private List<GameObject> boxOrder;
    [SerializeField] private Material boxMat;
    public int clueCounter = 0;
    public int boxCounter = 0;
    private bool readingClue = false;
    public bool hasKey = false;



    public void ShowNextClue()
    {

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

                box.GetComponent<Renderer>().material.color = Color.green;

                boxCounter++;
            }

            else
            {
                foreach (GameObject boxes in boxOrder)
                {
                    boxes.GetComponent<Renderer>().material.color = boxMat.color;
                }

                boxCounter = 0;
            }
        }

        if (boxCounter >= boxOrder.Count)
        {
            cluePickups[clueCounter].SetActive(true);
        }
    }



    public void CheckForKey()
    {
        if (hasKey)
            cluePickups[clueCounter].SetActive(true);

    }
}
