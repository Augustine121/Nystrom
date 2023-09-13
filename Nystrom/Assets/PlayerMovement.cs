using UnityEngine;
enum State
{
    STATE_STANDING,
    STATE_JUMPING,
    STATE_DUCKING,
    STATE_DUPLICATE,
    STATE_MOVE_FORWARD,
    STATE_SPRINTING,
    STATE_DIVING
}
public class PlayerMovement : MonoBehaviour
{
    public Rigidbody rb;
    public float forwardForce;
    public float upForce;
    public float duplicationCooldown;
    float nextDuplication = 0.0f;
    State state_ = State.STATE_DUPLICATE;

    // Update is called once per frame
    void FixedUpdate()
    {
        switch (state_)
        {
            case State.STATE_STANDING:
                if (Input.GetKey("s"))
                {
                    //duck
                    state_ = State.STATE_DUCKING;
                    rb.transform.localScale = new Vector3(1, .5f, 1);
                }
                else if (Input.GetKey("e") && (Time.time > nextDuplication))
                {
                    //duplicate 3 times
                    state_ = State.STATE_DUPLICATE;
                    nextDuplication = Time.time + duplicationCooldown;
                    for (int i = 0; i < 4; i++)
                    {
                        Instantiate(rb, rb.position, new Quaternion(0, 0, 0, 0));
                    }
                }
                else if (Input.GetKey("w"))
                {
                    //move forward
                    state_ = State.STATE_MOVE_FORWARD;
                    rb.AddForce(0, 0, forwardForce * Time.deltaTime);
                }
                else if (Input.GetKey("space"))
                {
                    //jump
                    state_ = State.STATE_JUMPING;
                    rb.AddForce(0, upForce * Time.deltaTime, 0, ForceMode.VelocityChange);
                }
                break;
            case State.STATE_DUCKING:
                if (!Input.GetKey("s"))
                {
                    //stand
                    state_ = State.STATE_STANDING;
                    rb.transform.localScale = new Vector3(1, 1, 1);
                    rb.transform.localPosition = new Vector3(rb.position.x, rb.position.y + .5f, rb.position.z);
                }
                break;
            case State.STATE_DUPLICATE:
                if ((rb.position.y < 1.005) && (rb.position.y > 0.995))
                {
                    state_ = State.STATE_STANDING;
                }
                break;
            case State.STATE_MOVE_FORWARD:
                //move forward
                rb.AddForce(0, 0, forwardForce * Time.deltaTime);
                if (!Input.GetKey("w"))
                {
                    //stand
                    state_ = State.STATE_STANDING;
                }
                else if (Input.GetKey(KeyCode.LeftShift))
                {
                    //sprinting
                    state_ = State.STATE_SPRINTING;
                    rb.AddForce(0, 0, 5 * forwardForce * Time.deltaTime);
                }
                else if (Input.GetKey("space"))
                {
                    //jumping
                    state_ = State.STATE_JUMPING;
                    rb.AddForce(0, upForce * Time.deltaTime, 0, ForceMode.VelocityChange);
                }
                break;
            case State.STATE_SPRINTING:
                //move forward faster
                rb.AddForce(0, 0, 5 * forwardForce * Time.deltaTime);
                if (!(Input.GetKey("w")))
                {
                    //stand
                    state_ = State.STATE_STANDING;
                }
                else if (!Input.GetKey(KeyCode.LeftShift))
                {
                    //move forward regularly
                    state_ = State.STATE_MOVE_FORWARD;
                    rb.AddForce(0, 0, forwardForce * Time.deltaTime);
                }
                break;
            case State.STATE_JUMPING:
                if ((rb.position.y < 1.005) && (rb.position.y > 0.995))
                {
                    //stand
                    state_ = State.STATE_STANDING;
                }
                else if ((Input.GetKey("s")))
                {
                    //diving
                    state_ = State.STATE_DIVING;
                    rb.AddForce(0, 10 * -upForce * Time.deltaTime, 0);
                }
                break;
            case State.STATE_DIVING:
                rb.AddForce(0, 10 * -upForce * Time.deltaTime, 0);
                if ((rb.position.y < 1.005) && (rb.position.y > 0.995))
                {
                    //stand
                    state_ = State.STATE_STANDING;
                }
                break;
            default:
                break;
        }
    }
}