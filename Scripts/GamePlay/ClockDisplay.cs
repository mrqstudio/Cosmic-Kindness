using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System.Diagnostics;
using Debug = UnityEngine.Debug;

public class ClockDisplay : MonoBehaviour
{
    public TextMeshProUGUI clockText;
    private float timer = 0f;

    public GameObject MosqueAudio;
    public bool MosqueActive;

    public GameObject NamazMessage;

    private PlayerController player;

    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerController>();
        MosqueAudio.SetActive(false);
        NamazMessage.SetActive(false);
    }

    void FixedUpdate()
    {
        timer += Time.deltaTime;
        if (timer >= 1f) // Update every second
        {
            DateTime currentTime = DateTime.Now;
            string timeString = currentTime.ToString("hh:mm:ss tt");
            clockText.text = timeString;
            timer = 0f; // Reset the timer

            if (timeString == "04:30:00 AM" || timeString == "01:30:00 PM" || timeString == "04:30:00 PM" || timeString == "06:05:00 PM" || timeString == "07:30:00 PM")
            {
                

                StartCoroutine(AzanEnd());

            }
        }


    }


    IEnumerator AzanEnd()
    {
        MosqueAudio.SetActive(true);
        MosqueActive = true;
        yield return new WaitForSeconds(4);
        NamazMessage.SetActive(true);
        yield return new WaitForSeconds(5);
        NamazMessage.SetActive(false);
        yield return new WaitForSeconds(155);
        MosqueAudio.SetActive(false);
        MosqueActive = false;
    }
}