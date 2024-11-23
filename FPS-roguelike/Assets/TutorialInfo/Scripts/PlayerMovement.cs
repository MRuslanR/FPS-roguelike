using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public float speed = 12f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;



    public float groundDistance = 0.4f;
    public Transform groundCheck;
    public LayerMask groundMask;


    Vector3 velocity;

    bool isGrounded;
    //bool isMoving;

    private Vector3 lastPosition = new Vector3(0f,0f,0f);

    private CharacterController controller;

    void Start()
    {
     controller = GetComponent<CharacterController>();

    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if (isGrounded && velocity.y < 0){
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward*z;

        controller.Move(move * speed * Time.deltaTime);
        if (Input.GetButtonDown("Jump") && isGrounded){
            velocity.y = Mathf.Sqrt(jumpHeight * -1f * gravity); 
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        if (lastPosition != gameObject.transform.position && isGrounded == true){
            //isMoving = true;
        }
        else{
            //isMoving = false;
        }
        lastPosition = gameObject.transform.position; 
    }
}
