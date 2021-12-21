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

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(currentPage);
        if (currentPage == 1)
        {
            back.SetActive(false);
        }
        else
        {
            back.SetActive(true);
        }

        if (currentPage == pageList.Length)
        {
            next.SetActive(false);
            start.SetActive(true);
        }
        else
        {
            next.SetActive(true);
            start.SetActive(false);
        }
    }

    public void NextPage()
    {
        if (currentPage == 1)
        {
            pageList[currentPage - 1].SetActive(false);
            pageList[currentPage].SetActive(true);
        }
        else if (currentPage == 2)
        {
            pageList[currentPage - 1].SetActive(false);
            pageList[currentPage].SetActive(true);
        }
        else if (currentPage == 3)
        {
            pageList[currentPage - 1].SetActive(false);
            pageList[currentPage].SetActive(true);
        }
        else if (currentPage == 4)
        {
            pageList[currentPage - 1].SetActive(false);
            pageList[currentPage].SetActive(true);
        }
        else if (currentPage == 5)
        {
            pageList[currentPage - 1].SetActive(false);
            pageList[currentPage].SetActive(true);
        }

        currentPage += 1;
    }

    public void PreviousPage()
    {
        if (currentPage == 2)
        {
            pageList[currentPage - 2].SetActive(true);
            pageList[currentPage - 1].SetActive(false);
        }
        else if (currentPage == 3)
        {
            pageList[currentPage - 2].SetActive(true);
            pageList[currentPage - 1].SetActive(false);
        }
        else if (currentPage == 4)
        {
            pageList[currentPage - 2].SetActive(true);
            pageList[currentPage - 1].SetActive(false);
        }
        else if (currentPage == 5)
        {
            pageList[currentPage - 2].SetActive(true);
            pageList[currentPage - 1].SetActive(false);
        }

        currentPage -= 1;
    }
}
