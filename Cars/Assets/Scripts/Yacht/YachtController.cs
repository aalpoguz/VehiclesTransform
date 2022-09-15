using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class YachtController : MonoBehaviour
{

    public UIManager uimanager;
    public FlyController flyController;
    private Rigidbody rb;
    public CoinText coinText;
    public float speed;
    public int forwardSpeed;
    public int slowedSpeed;
    private Touch touch;
    private float newPosition;
    public bool isFlyActive = false;
    public bool isRideActive = false;
    public bool isSwimActive = false;
    float lastTouchedX;
    public GameObject finishScreen;
    public GameObject LoseScreen;
    public TextMeshProUGUI score;
    public int scoreHolder = 0;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {


        if (flyController.isSwimActive == true)
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

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("DeadZone"))
        {
            Destroy(gameObject);
            Debug.Log("Kaybettin Loser pic");
        }


    }

    private void OnTriggerEnter(Collider other)
    {
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

        if (other.gameObject.CompareTag("finish"))
        {

            forwardSpeed = 0;
            finishScreen.SetActive(true);
            score.text = coinText.score.ToString();
            score.gameObject.SetActive(true);
            Debug.Log("game over ");
        }
        if (other.gameObject.CompareTag("DeadZone"))
        {
            LoseScreen.SetActive(true);
        

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
