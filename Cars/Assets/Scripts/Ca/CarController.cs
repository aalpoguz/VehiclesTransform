using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class CarController : MonoBehaviour
{
    public FlyController flyController;
    public CoinText coinText;
    private Rigidbody rb;
    public float speed;
    public int forwardSpeed;
    private Touch touch;
    private int firstTouch = 0;
    public TextMeshProUGUI scoreText;
    public GameObject jetSetActive;
    public GameObject spaceGround;
    public GameObject deadYachtSetActive;
    public int slowedSpeed;
    public GameObject splashParticle;

    float lastTouchedX;


    public bool isFlyActive = false;
    public bool isSwimActive = false;
    public bool isRideActive = false;






    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {

        float newX = 0;
        float touchXDelta = 0;




        if (Input.touchCount > 0)
        {


            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                firstTouch = 1;
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

        if (firstTouch == 1)
        {
            Vector3 newPosition = new Vector3(newX, transform.position.y, transform.position.z + forwardSpeed * Time.deltaTime);
            transform.position = newPosition;
            // transform.position += new Vector3(0, 0, forwardSpeed * Time.deltaTime);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Fly"))
        {
            splashParticle.SetActive(true);
            spaceGround.SetActive(true);
            isFlyActive = true;
            jetSetActive.SetActive(true);
            this.gameObject.SetActive(false);
            other.gameObject.SetActive(false);

        }

        if (other.gameObject.CompareTag("Swim"))
        {
            deadYachtSetActive.SetActive(true);
            isSwimActive = true;

            scoreText.text = coinText.score.ToString();
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
        slowedSpeed = forwardSpeed / 2;
        forwardSpeed = slowedSpeed;
        yield return new WaitForSeconds(1f);
        forwardSpeed = forwardSpeed * 2;
    }


}
