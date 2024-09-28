using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Loading : MonoBehaviour
{
    public TextMeshProUGUI textLoading; // Rename to match standard conventions
    private float progress; // Renamed for clarity

    public GameObject first;
    public GameObject second;
    public GameObject third;

    // Start is called before the first frame update
    void Start()
    {
        // Initially hide all UI elements
        first.SetActive(false);
        second.SetActive(false);
        third.SetActive(false);

        // Start loading sequence
        StartCoroutine(LoadingSequence());
    }

    // Update is called once per frame
    void Update()
    {
        // Check if loading is complete
        if (progress >= 100)
        {
            SceneManager.LoadScene(1);
        }
    }

    IEnumerator LoadingSequence()
    {
        // Show first element
        first.SetActive(true);
        yield return new WaitForSeconds(3); // Display for 3 seconds
        first.SetActive(false);

        // Show second element
        second.SetActive(true);
        yield return new WaitForSeconds(3); // Display for 3 seconds
        second.SetActive(false);

        // Show third element
        third.SetActive(true);
        yield return StartCoroutine(TextLoading());
    }

    IEnumerator TextLoading()
    {
        // Load progress text
        while (progress < 100)
        {
            progress++;
            textLoading.text = "Loading..... " + progress + "%";
            yield return new WaitForSeconds(0.1f);
        }
    }
}
