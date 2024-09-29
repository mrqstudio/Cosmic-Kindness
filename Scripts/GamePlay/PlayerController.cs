using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    // Input variables
    private float horizontalInput;
    private float verticalInput;

    // Player movement and rotation speeds
    public float playerSpeed;
    public float playerRotationSpeed;

    // Rigidbody component
    private Rigidbody playerRb;

    public Animator anim;

    public AudioSource audioSource;
    public AudioClip audioClip;

    public bool namazDone = false;
    public GameObject NamazDoneMessage;
    public GameObject MosqueAudio;

    public TextMeshProUGUI TrashCounter;
    private int countT = 0;
    public TextMeshProUGUI StoneCounter;
    private int countS = 0;

    public GameObject ThanksMSG;
    public GameObject TrashBinMessage;

    public TextMeshProUGUI DeedsCount;
    private int deeds = 0;

    public AudioSource CoinAudio;

    private TimeDead TimeDeadScipt;

    // Start is called before the first frame update
    void Start()
    {
        TimeDeadScipt = GameObject.Find("DeadScriptObject").GetComponent<TimeDead>();
        audioSource = GetComponent<AudioSource>();
        playerRb = GetComponent<Rigidbody>();
        playerRb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        anim.SetBool("Run", false);
        audioSource.clip = audioClip;
        ThanksMSG.SetActive(false);
        TrashBinMessage.SetActive(false);
    }

    // FixedUpdate is called for consistent physics updates
    void FixedUpdate()
    {
        horizontalInput = Input.GetAxis("PlayerHorizontal");
        verticalInput = Input.GetAxis("PlayerVertical");

        // Rotate and move the player
        transform.Rotate(Vector3.up * horizontalInput * playerRotationSpeed * Time.fixedDeltaTime);
        Vector3 moveDirection = transform.forward * verticalInput * playerSpeed;
        playerRb.velocity = new Vector3(moveDirection.x, playerRb.velocity.y, moveDirection.z);

        // Update UI
        UpdateCounters();

        // Handle animation and audio
        HandleAnimationAndAudio();
    }

    void UpdateCounters()
    {
        TrashCounter.text = countT.ToString();
        StoneCounter.text = countS.ToString();
    }

    void HandleAnimationAndAudio()
    {
        if (verticalInput != 0)
        {
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
            anim.SetBool("Run", true);
        }
        else
        {
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
            }
            anim.SetBool("Run", false);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "TrashCan":
                HandleTrashCanCollision();
                break;
            case "NamazDone":
                HandleNamazDoneCollision();
                break;
            case "Trash":
                HandleTrashCollision(collision);
                break;
            case "Stones":
                HandleStoneCollision(collision);
                break;
        }
    }

    void HandleTrashCanCollision()
    {
        if (countT > 0 || countS > 0)
        {
            CoinAudio.Play();
            deeds += countT*100 + countS*200;
        }
        countT = 0;
        countS = 0;
        DeedsCount.text = deeds.ToString();
    }

    void HandleNamazDoneCollision()
    {
        TimeDeadScipt.DeadCount += 60;
        namazDone = true;
        StartCoroutine(ShowMessage(NamazDoneMessage, 5, () => MosqueAudio.SetActive(false)));
    }

    void HandleTrashCollision(Collision collision)
    {
        if (countT < 5)
        {
            TimeDeadScipt.DeadCount += 60;
            StartCoroutine(ShowMessage(ThanksMSG, 5));
            countT++;
            Destroy(collision.gameObject);
        }
        else
        {
            StartCoroutine(ShowMessage(TrashBinMessage, 5));
        }
    }

    void HandleStoneCollision(Collision collision)
    {
        TimeDeadScipt.DeadCount += 60;
        StartCoroutine(ShowMessage(ThanksMSG, 5));
        countS++;
        Destroy(collision.gameObject);
    }

    IEnumerator ShowMessage(GameObject message, float duration, System.Action onComplete = null)
    {
        message.SetActive(true);
        yield return new WaitForSeconds(duration);
        message.SetActive(false);
        onComplete?.Invoke();
    }
}
