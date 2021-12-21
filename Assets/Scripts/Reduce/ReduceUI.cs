using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReduceUI : MonoBehaviour
{
    public GameObject intro;
    public GameObject start;
    public GameObject resultsGood;
    public GameObject resultsBad;

    public AudioSource doneSound;
    // Start is called before the first frame update
    void Start()
    {
        intro.SetActive(true);
        start.SetActive(false);
        resultsGood.SetActive(false);
        resultsBad.SetActive(false);

        doneSound = GetComponent<AudioSource>();
    }

    public void SecondNext()
    {
        intro.SetActive(false);
        start.SetActive(true);
        resultsGood.SetActive(false);
        resultsBad.SetActive(false);
    }
}
