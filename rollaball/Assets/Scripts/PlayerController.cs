using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{

    public float speed = 0;
    public TextMeshProUGUI countText;
    public GameObject winTextObject;
    public float gravityScale = 5;

    public LayerMask groundLayers;
    public float jumpForce = 30;
    public SphereCollider col;



    private Rigidbody rb;
    private int count;
    private int doublejump;
    private float movementX;
    private float movementY;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;

        SetCountText();
        winTextObject.SetActive(false);

        col = GetComponent<SphereCollider>();
    }



    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();



        movementX = movementVector.x;
        movementY = movementVector.y;



    }


    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
        if (count >= 8)
        {
            winTextObject.SetActive(true);
        }
    }


    void Update()
    {
        if ((doublejump > 1) && (Input.GetKeyDown(KeyCode.Space)))
        {
            if (doublejump == 1)
            {
                rb.AddForce(Vector3.up * jumpForce * 5, ForceMode.Impulse);
            }
            else 
            {
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            }
            doublejump--;
        }
    }

    
    private bool IsGround()
    {
        return Physics.CheckCapsule(col.bounds.center, new Vector3(col.bounds.center.x, 
            col.bounds.min.y, col.bounds.center.z), col.radius * .9f, groundLayers);
    }
    

    void FixedUpdate()
    {
        if (IsGround())
        {
            doublejump = 2;
        }
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);


        
        rb.AddForce(movement * speed);
        
        

        rb.AddForce(Physics.gravity * (gravityScale - 1) * rb.mass);




        



    }




    private void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            count += 1;

            SetCountText();
        }
    }
}

