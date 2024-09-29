using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public GameObject BackPackMenu;

    public GameObject player;
    public GameObject Bike;
    public Vector3 Playerposition;

    public GameObject BikeWorkOFF;
    public GameObject BikeCamera;
    public GameObject NoBikeWorkOFF;
    public GameObject PlayerCamera;


    public GameObject StartControl;
    // Start is called before the first frame update
    void Start()
    {
        StartControl.SetActive(false);

        BackPackMenu.SetActive(false);
        player.SetActive(true);    
        Bike.SetActive(false);
        NoBikeWorkOFF.SetActive(true);
        BikeWorkOFF.SetActive(false);
        BikeCamera.SetActive(false);
        PlayerCamera.SetActive(true);
        StartCoroutine(StartController());
    }

    IEnumerator StartController()
    {
        StartControl.SetActive(true);
        yield return new WaitForSeconds(3);
        StartControl.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void BackPackButton()
    {
        BackPackMenu.SetActive(true);
    }

    public void CrossBackPackButton()
    {
        BackPackMenu.SetActive(false);
    }

    public void BikeButton()
    {
         Playerposition = player.transform.position;
        Playerposition.y = 0.05f;
        Bike.transform.position = Playerposition;
        Bike.SetActive(true);
        player.SetActive(false);

        NoBikeWorkOFF.SetActive(false);
        BikeCamera.SetActive(true);
        BikeWorkOFF.SetActive(true);
        PlayerCamera.SetActive(false);
    }

    public void PlayerButton()
    {
        Vector3 Bikeposition = Bike.transform.position;
        player.transform.position = Bikeposition;
        Bike.SetActive(false);
        player.SetActive(true);


        NoBikeWorkOFF.SetActive(true);
        BikeWorkOFF.SetActive(false);
        PlayerCamera.SetActive(true);
        BikeCamera.SetActive(false);
    }
}
