using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class FlyController : MonoBehaviour
{
    public CarController carcontroller;
    public CoinText coinText;
    private Rigidbody rb;
    public float speed;
    public int forwardSpeed;
    public int deadSpeed;
    private Touch touch;
    public bool isSwimActive = false;
    public bool isRideActive = false;
    public GameObject deadGTR;
    public GameObject Yacht;
    float lastTouchedX;
    public GameObject LoseScreen;
    
    public TextMeshProUGUI scoreDeadGTR;
    public GameObject deadGTRSetActive;

    void Start()
    {
        isRideActive = false;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (carcontroller.isFlyActive == true || carcontroller.isRideActive == true)
        {

            float newX = 0;
            float touchXDelta = 0;

            if (Input.touchCount > 0)
            {


                if (Input.GetTouch(0).phase == TouchPhase.Began)
                {

                    lastTouchedX = Input.GetTouch(0).position.x;
                }


                else if (Input.GetTouch(0).phase == TouchPhase.Moved)
                {
                    touchXDelta = 5 * (Input.GetTouch(0).position.x - lastTouchedX) / Screen.width;
                    lastTouchedX = Input.GetTouch(0).position.x;

                }

            }

            newX = transform.position.x + speed * touchXDelta * Time.deltaTime;
            newX = Mathf.Clamp(newX, -7, 7);



            Vector3 newPosition = new Vector3(newX, transform.position.y, transform.position.z + forwardSpeed * Time.deltaTime);
            transform.position = newPosition;
            // transform.position += new Vector3(0, 0, forwardSpeed * Time.deltaTime);




        }


    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Swim"))
        {
            Yacht.SetActive(true);
            isSwimActive = true;
            isRideActive = false;
            carcontroller.isFlyActive = false;
            this.gameObject.SetActive(false);
            other.gameObject.SetActive(false);
        }

        if (other.gameObject.CompareTag("Ride"))
        {
            deadGTRSetActive.SetActive(true);
            forwardSpeed = 0;
            scoreDeadGTR.text = coinText.score.ToString();
            isRideActive = true;
            this.gameObject.SetActive(false);
            other.gameObject.SetActive(false);

        }

        if (other.gameObject.CompareTag("Coin"))
        {
            Destroy(other.gameObject);
            coinText.AddScore();

        }

        if (other.gameObject.CompareTag("Trap"))
        {
            Destroy(other.gameObject);
            StartCoroutine(Trapped());
        }
    }

    IEnumerator Trapped()
    {
        carcontroller.slowedSpeed = forwardSpeed / 2;
        forwardSpeed = carcontroller.slowedSpeed;
        yield return new WaitForSeconds(1f);
        forwardSpeed = forwardSpeed * 2;
    }

}


