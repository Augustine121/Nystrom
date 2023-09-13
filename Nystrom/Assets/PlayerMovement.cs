using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody rb;
    public float forwardForce = 2000f;
    public float sidewaysForce = 100f;
    public float upForce = 1000f;
    public bool checkLeft = false;
    public bool checkRight = false;
    public bool checkUp = false;

    void Update()
    {
        if (Input.GetKey("a"))
        {
            checkLeft = true;
        }
        if (Input.GetKey("d"))
        {
            checkRight = true;
        }
        if (Input.GetKey("space"))
        {
            checkUp = true;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.AddForce(0, 0, forwardForce * Time.deltaTime);
        if (checkLeft)
        {
            rb.AddForce(-sidewaysForce * Time.deltaTime, 0, 0, ForceMode.VelocityChange);
            checkLeft = false;
        }
        if (checkRight)
        {
            rb.AddForce(sidewaysForce * Time.deltaTime, 0, 0, ForceMode.VelocityChange);
            checkRight = false;
        }
        if (checkUp && (rb.position.y < 1.1 && rb.position.y > 0.9))
        {
            rb.AddForce(0, upForce * Time.deltaTime, 0, ForceMode.VelocityChange);
            checkUp = false;
        }
        if (rb.position.y < -1f)
        {
            FindObjectOfType<GameManager>().EndGame();
        }
    }
}