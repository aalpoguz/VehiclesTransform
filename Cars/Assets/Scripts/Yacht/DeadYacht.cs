using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadYacht : MonoBehaviour
{
    public CarController carController;
    private Rigidbody rb;
    public float speed;
    public int forwardSpeed;
    private Touch touch;
    float lastTouchedX;
    public GameObject LoseScreen;



    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (carController.isSwimActive == true)
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
        if (other.gameObject.CompareTag("DeadZone"))
        {
            LoseScreen.SetActive(true);
        }
    }
}
