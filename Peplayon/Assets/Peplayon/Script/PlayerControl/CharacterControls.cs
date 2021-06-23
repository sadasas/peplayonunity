using UnityEngine;
using System.Collections;
using Mirror;

[RequireComponent(typeof(Rigidbody))]
public class CharacterControls : NetworkBehaviour
{
    public float speed;
    public float airVelocity;
    public float gravity;
    public float maxVelocityChange;
    public float jumpHeight;
    public float maxFallSpeed;
    public float rotateSpeed; //Speed the player rotate
    public float animSpeed;
    public bool isGround = true;
    private Vector3 moveDir;

    public static bool cutsceneawal = true;
    public static bool isLobbyScene = false;

    [SerializeField]
    private GameObject cam = null;

    public Rigidbody rb;

    private float distToGround;

    public bool canMove = true; //If player is not hitted
    private bool isStuned = false;
    private bool wasStuned = false; //If player was stunned before get stunned another time
    private float pushForce;
    private Vector3 pushDir;

    public Vector3 checkPoint;
    private bool slide = false;

    private int blendtohash;
    public float blend = 0.0f;

    [SerializeField]
    private Animator anim;

    public bool Animation;

    public LayerMask ground;

    public void lolos()
    {
        cutsceneawal = true;
        blend = 0f;
    }

    [Client]
    private void Awake()
    {
        /*        rb.freezeRotation = true;
                rb.useGravity = false;*/

        /*  checkPoint = transform.position;*/
        Cursor.visible = false;
        // get the distance to ground
        distToGround = GetComponent<Collider>().bounds.extents.y;
    }

    public void SetDefaultValue()
    {
        canMove = true;
        speed = 10.0f;
        airVelocity = 8f;
        gravity = 16f;
        maxVelocityChange = 10.0f;
        jumpHeight = 6f;
        maxFallSpeed = 20.0f;
        rotateSpeed = 25f; //Speed the player rotate
    }

    public bool IsGrounded()
    {
        isGround = Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.3f, ground);
        return isGround;
    }

    [Client]
    private void FixedUpdate()
    {
        if (cutsceneawal == false)
        {
            MusicPlayer musicplayer = GameObject.FindGameObjectWithTag("MusicPlayer").GetComponent<MusicPlayer>();
            musicplayer.BGMINGAME = true;
            if (!hasAuthority) return;
            Debug.Log("cutsceneawal false");
            //Gw extract buat lobby
            ControllerPlayerExt();
        }
        if (isLobbyScene)
        {
            if (!hasAuthority) return;
            ControllerPlayerExt();
        }
    }

    /**
     * Intinya ini gw extract
     */
    [Client]
    private void ControllerPlayerExt()
    {
        //cam = GameObject.FindGameObjectWithTag("PlayerCamera").gameObject;
        cam = FindObjectOfType<CameraManager>().gameObject;
        rb = GetComponent<Rigidbody>();
        Debug.Log("Done");
        blendtohash = Animator.StringToHash("Blend");
        anim.SetFloat(blendtohash, blend);

        if (Animation && blend <= 1f)
        {
            blend += animSpeed;
        }
        else if (!Animation && blend > 0f)
        {
            blend -= animSpeed;
        }
        else if (!Animation && blend < 0f)
        {
            blend = 0f;
        }

        if (canMove)
        {
            //Debug.Log("canMove");
            if (moveDir.x != 0 || moveDir.z != 0)
            {
                /*anim.SetBool("isRun", true);*/
                Animation = true;
                Vector3 targetDir = moveDir; //Direction of the character

                targetDir.y = 0;
                if (targetDir == Vector3.zero)
                    targetDir = transform.forward;
                Quaternion tr = Quaternion.LookRotation(targetDir); //Rotation of the character to where it moves
                Quaternion targetRotation = Quaternion.Slerp(transform.rotation, tr, Time.deltaTime * rotateSpeed); //Rotate the character little by little
                transform.rotation = targetRotation;
            }
            else
            {
                Animation = false;
            }

            if (IsGrounded())
            {
                // Calculate how fast we should be moving
                Vector3 targetVelocity = moveDir;
                targetVelocity *= speed;

                // Apply a force that attempts to reach our target velocity
                Vector3 velocity = rb.velocity;
                if (targetVelocity.magnitude < velocity.magnitude) //If I'm slowing down the character
                {
                    targetVelocity = velocity;
                    rb.velocity /= 1.1f;
                }
                Vector3 velocityChange = (targetVelocity - velocity);
                velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
                velocityChange.z = Mathf.Clamp(velocityChange.z, -maxVelocityChange, maxVelocityChange);
                velocityChange.y = 0;
                if (!slide)
                {
                    if (Mathf.Abs(rb.velocity.magnitude) < speed * 1.0f)
                        rb.MovePosition(transform.position + moveDir * speed * Time.deltaTime);
                }
                else if (Mathf.Abs(rb.velocity.magnitude) < speed * 1.0f)
                {
                    //Debug.Log(rb.velocity.magnitude);
                }

                // Jump
                if (Input.GetKey(KeyCode.Space))
                {
                    anim.SetBool("isJump", true);
                    rb.velocity = new Vector3(velocity.x, CalculateJumpVerticalSpeed(), velocity.z);
                }
                else
                {
                    anim.SetBool("isJump", false);
                }
            }
            else
            {
                anim.SetBool("isJump", false);
                if (!slide)
                {
                    Vector3 targetVelocity = new Vector3(moveDir.x * airVelocity, rb.velocity.y, moveDir.z * airVelocity);
                    Vector3 velocity = rb.velocity;
                    Vector3 velocityChange = (targetVelocity - velocity);
                    velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
                    velocityChange.z = Mathf.Clamp(velocityChange.z, -maxVelocityChange, maxVelocityChange);
                    rb.AddForce(velocityChange, ForceMode.VelocityChange);
                    if (velocity.y < -maxFallSpeed)
                        rb.velocity = new Vector3(velocity.x, -maxFallSpeed, velocity.z);
                }
                else if (Mathf.Abs(rb.velocity.magnitude) < speed * 1.0f)
                {
                    rb.AddForce(moveDir * 0.15f, ForceMode.VelocityChange);
                }
            }
        }
        else
        {
            rb.velocity = pushDir * pushForce;
        }
        // We apply gravity manually for more tuning control
        rb.AddForce(new Vector3(0, -gravity * GetComponent<Rigidbody>().mass, 0));
    }

    [Client]
    private void Update()
    {
        if (cutsceneawal == false)
        {
            if (!hasAuthority) return;
            cam = GameObject.FindGameObjectWithTag("PlayerCamera").gameObject;
            rb = GetComponent<Rigidbody>();

            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");

            Vector3 v2 = v * cam.transform.forward; //Vertical axis to which I want to move with respect to the camera
            Vector3 h2 = h * cam.transform.right; //Horizontal axis to which I want to move with respect to the camera
            moveDir = (v2 + h2).normalized; //Global position to which I want to move in magnitude 1

            /*  RaycastHit hit;
              if (Physics.Raycast(transform.position, -Vector3.up, out hit, distToGround + 0.1f))
              {
                  if (hit.transform.tag == "Slide")
                  {
                      slide = true;
                  }
                  else
                  {
                      slide = false;
                  }
              }*/
        }
    }

    private float CalculateJumpVerticalSpeed()
    {
        // From the jump height and gravity we deduce the upwards speed
        // for the character to reach at the apex.
        return Mathf.Sqrt(2 * jumpHeight * gravity);
    }

    public void HitPlayer(Vector3 velocityF, float time)
    {
        rb.velocity = velocityF;

        pushForce = velocityF.magnitude;
        pushDir = Vector3.Normalize(velocityF);
        StartCoroutine(Decrease(velocityF.magnitude, time));
    }

    public void LoadCheckPoint()
    {
        transform.position = checkPoint;
    }

    private IEnumerator Decrease(float value, float duration)
    {
        if (isStuned)
            wasStuned = true;
        isStuned = true;
        canMove = false;

        float delta = 0;
        delta = value / duration;

        for (float t = 0; t < duration; t += Time.deltaTime)
        {
            yield return null;
            if (!slide) //Reduce the force if the ground isnt slide
            {
                pushForce = pushForce - Time.deltaTime * delta;
                pushForce = pushForce < 0 ? 0 : pushForce;
                //Debug.Log(pushForce);
            }
            rb.AddForce(new Vector3(0, -gravity * GetComponent<Rigidbody>().mass, 0)); //Add gravity
        }

        if (wasStuned)
        {
            wasStuned = false;
        }
        else
        {
            isStuned = false;
            canMove = true;
        }
    }
}