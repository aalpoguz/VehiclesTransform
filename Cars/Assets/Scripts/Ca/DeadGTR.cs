using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DeadGTR : MonoBehaviour
{
    public CarController carcontroller;
    public FlyController flyController;
    public CoinText cointext;
    private Rigidbody rb;
    public float speed;
    public int forwardSpeed;
    private Touch touch;
    float lastTouchedX;
    public GameObject Loosee;
    
    public TextMeshProUGUI scoreDeadGTR;




    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (flyController.isRideActive == true)
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



            Vector3 newPosition = new Vector3(newX, transform.position.y, transform.position.z + forwardSpeed / 4 * Time.deltaTime);
            transform.position = newPosition;
            // transform.position += new Vector3(0, 0, forwardSpeed * Time.deltaTime);

            Loosee.SetActive(true);

        }

    }


}
