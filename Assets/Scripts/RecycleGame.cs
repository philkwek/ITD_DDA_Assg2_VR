using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecycleGame : MonoBehaviour
{
    public List<GameObject> throwables;
    public bool isGameActive;
    public static bool isOne = false;

    // Start is called before the first frame update
    void Start()
    {
        isGameActive = true;
    }

    // Update is called once per frame
    void Update()
    {
        SpawnThrowables();
    }

    private void SpawnThrowables()
    {
        while (isGameActive && !isOne)
        {
            int index = Random.Range(0, throwables.Count);
            Instantiate(throwables[index]);
            isOne = true;
        }
    }
}
