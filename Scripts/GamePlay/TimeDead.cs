using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeDead : MonoBehaviour
{

    public TextMeshProUGUI TimeDeader;
    public int DeadCount;

    public GameObject GameOver;
    // Start is called before the first frame update
    void Start()
    {
        GameOver.SetActive(false);
        DeadCount = 60;
        StartCoroutine(GameOverScreen());
    }

    // Update is called once per frame
    void Update()
    {
        TimeDeader.text = "" + DeadCount;
        if(DeadCount == 0)
        {
            GameOver.SetActive(true);
        }
    }

    IEnumerator GameOverScreen()
    {
        while (DeadCount!=0)
        {
            DeadCount--;
            yield return new WaitForSeconds(1);
        }
    }
}
