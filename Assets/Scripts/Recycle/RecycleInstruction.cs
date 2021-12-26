/******************************************************************************
Author: Donavan, Eileen, Phil, Kelly, Elicia
Name of Class: RecycleInstruction
Description of Class: This script manages navigation of the instructions that
the users read before starting the game
Date Created: 10/12/21
******************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecycleInstruction : MonoBehaviour
{
    public GameObject start;
    public GameObject next;
    public GameObject back;

    public GameObject[] pageList;

    private int currentPage = 1;

    // Update is called once per frame
    void Update()
    {
        //If current page is 1
        if (currentPage == 1)
        {
            //The back button is deactivated
            back.SetActive(false);
        }
        //If current page is not 1
        else
        {
            //The back button is activated
            back.SetActive(true);
        }

        //If current page is the last page
        if (currentPage == pageList.Length)
        {
            //The next button is replaced with the start button that will start the game
            next.SetActive(false);
            start.SetActive(true);
        }
        //If current page is not the last page
        else
        {
            //The next button remains and the start button will not be activated
            next.SetActive(true);
            start.SetActive(false);
        }
    }

    public void NextPage()
    {
        //If current page is 1
        if (currentPage == 1)
        {
            //Deactivate item 0 (page 1) in the pageList list
            pageList[currentPage - 1].SetActive(false);

            //Activate item 1 (page 2) in the pageList list
            pageList[currentPage].SetActive(true);
        }
        //If current page is 2
        else if (currentPage == 2)
        {
            //Deactivate item 1 (page 2) in the pageList list
            pageList[currentPage - 1].SetActive(false);

            //Activate item 2 (page 3) in the pageList list
            pageList[currentPage].SetActive(true);
        }
        //If current page is 3
        else if (currentPage == 3)
        {
            //Deactivate item 2 (page 3) in the pageList list
            pageList[currentPage - 1].SetActive(false);

            //Activate item 3 (page 4) in the pageList list
            pageList[currentPage].SetActive(true);
        }
        //If current page is 4
        else if (currentPage == 4)
        {
            //Deactivate item 3 (page 4) in the pageList list
            pageList[currentPage - 1].SetActive(false);

            //Activate item 4 (page 5) in the pageList list
            pageList[currentPage].SetActive(true);
        }
        //If current page is 5
        else if (currentPage == 5)
        {
            //Deactivate item 4 (page 5) in the pageList list
            pageList[currentPage - 1].SetActive(false);

            //Activate item 5 (page 6) in the pageList list
            pageList[currentPage].SetActive(true);
        }

        //Current page plus 1
        currentPage += 1;
    }

    public void PreviousPage()
    {
        //If current page is 2
        if (currentPage == 2)
        {
            //Deactivate item 1 (page 2) in the pageList list
            pageList[currentPage - 2].SetActive(true);

            //Activate item 0 (page 1) in the pageList list
            pageList[currentPage - 1].SetActive(false);
        }
        //If current page is 3
        else if (currentPage == 3)
        {
            //Deactivate item 2 (page 3) in the pageList list
            pageList[currentPage - 2].SetActive(true);

            //Activate item 1 (page 2) in the pageList list
            pageList[currentPage - 1].SetActive(false);
        }
        //If current page is 4
        else if (currentPage == 4)
        {
            //Deactivate item 3 (page 4) in the pageList list
            pageList[currentPage - 2].SetActive(true);

            //Activate item 2 (page 3) in the pageList list
            pageList[currentPage - 1].SetActive(false);
        }
        //If current page is 5
        else if (currentPage == 5)
        {
            //Deactivate item 4 (page 5) in the pageList list
            pageList[currentPage - 2].SetActive(true);

            //Activate item 3 (page 4) in the pageList list
            pageList[currentPage - 1].SetActive(false);
        }
        //If current page is 6
        else if (currentPage == 6)
        {
            //Deactivate item 5 (page 6) in the pageList list
            pageList[currentPage - 2].SetActive(true);

            //Activate item 4 (page 5) in the pageList list
            pageList[currentPage - 1].SetActive(false);
        }

        //Current page minus 1
        currentPage -= 1;
    }
}
