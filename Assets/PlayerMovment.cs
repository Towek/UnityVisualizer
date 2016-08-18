using UnityEngine;
using System.Collections;

[RequireComponent (typeof(CharacterController))]
public class PlayerMovment : MonoBehaviour
{
    //public
    public Rigidbody rb;
    public float movementSpeed = 5.0f;
    public float mouseSpeed = 2.0f;
    public float mouseRange = 60.0f;
    public float jumpHeight = 5.0f;
    //private
    private CharacterController cc;
    private float mouseVertical = 0;
    private float verticalVelocity = 0.0f;
    private GameObject Hand;
    private Rigidbody Hrb;
    // Use this for initialization
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Hand = GameObject.Find("Hand");
        cc = GetComponent<CharacterController>();
        Hrb = Hand.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //mouse rotate
        float mouseHorizontal = Input.GetAxis("Mouse X") * mouseSpeed;
        transform.Rotate(0, mouseHorizontal, 0);

        mouseVertical -= Input.GetAxis("Mouse Y") * mouseSpeed;
        mouseVertical = Mathf.Clamp(mouseVertical, -mouseRange, mouseRange);
        Camera.main.transform.localRotation = Quaternion.Euler(mouseVertical, 0, 0);

        //movement
        verticalVelocity += Physics.gravity.y * Time.deltaTime;
        float forwardSpeed = Input.GetAxis("Vertical") * movementSpeed;
        float sideSpeed = Input.GetAxis("Horizontal") * movementSpeed;

        if (Input.GetButton("Jump") && cc.isGrounded){
            verticalVelocity = jumpHeight;
        }

        if (Input.GetButton("Fire1")){
            Debug.Log("FIRE1 clicked");
        }

        Vector3 speed = new Vector3(sideSpeed, verticalVelocity, forwardSpeed);
        speed = transform.rotation * speed;
        cc.Move(speed * Time.deltaTime);
    }
}