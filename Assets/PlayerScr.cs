using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScr : MonoBehaviour
{
    private float speed;
    private bool speedFlag;


    [SerializeField] private float movespeed;
    private Rigidbody rb;

    public float jumpSpeed;

    public static PlayerScr player;
    public bool plateFlag;

    private bool jumpFlag;
    public bool reboundFlag;

    // Start is called before the first frame update
    void Start()
    {
        jumpFlag = false;
        player = this;
        rb = GetComponent<Rigidbody>();
        plateFlag = false;
        reboundFlag = false;

        speedFlag = false;
    }

    // Update is called once per frame
    void Update()
    {
        float moveH = Input.GetAxis("Horizontal");
        float moveV = Input.GetAxis("Vertical");


        //float moveJ = Input.GetAxis("Jump");
        if (Input.GetButtonDown("Jump") && jumpFlag == false)
        {
            rb.velocity = Vector3.up * jumpSpeed;
            jumpFlag = true;
        }

        //Vector3 movement = new Vector3(moveH/3, 0, moveV/3);



        transform.Translate(moveH/15,0,moveV/15);
        //rb.AddForce(0,0,0);
       // rigidbody.AddForce(movement * movespeed);

        transform.localRotation = Quaternion.identity;


        if(rb.velocity.y >= -5)
        {
            speedFlag = true;
            Debug.Log(rb.velocity.y);
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        jumpFlag = false;
        
        if (collision.gameObject.name == "plate")
        {
           
            reboundFlag = false;

            var playerutil = playerUtilScr.utils;

        
            //もし、playerの力がplateの耐久度より低かったら
            if (plateScr.plate.breakpoint > playerutil.breakpower)
            {
               // Debug.Log(plateScr.plate.breakpoint + " " + playerutil.breakpower);
                if(reboundFlag == false)
                {
                    rb.velocity = Vector3.up * playerutil.breakpower * 1.5f;
                    reboundFlag = true;
                }
                else if(reboundFlag == true)
                {
                    reboundFlag = false;
                }
            }

            //Debug.Log("poo");
            plateFlag = true;
            return;
        }
        
        plateFlag = false;
    }


}
