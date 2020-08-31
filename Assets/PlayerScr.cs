using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScr : MonoBehaviour
{

    [SerializeField] private float movespeed;
    private Rigidbody rb;

    public float jumpSpeed;

    public static PlayerScr player;
    public bool plateFlag;

    private bool jumpFlag;
    // Start is called before the first frame update
    void Start()
    {
        jumpFlag = false;
        player = this;
        rb = GetComponent<Rigidbody>();
        plateFlag = false;
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
        

    }


    private void OnCollisionEnter(Collision collision)
    {
        jumpFlag = false;

        //Debug.Log(plate1.breakpoint);
        //Debug.Log(plate2.breakpoint);
        //Debug.Log(plate3.breakpoint);
        //Debug.Log(plate4.breakpoint);

        //plateに当たったか？
        if (collision.gameObject.name == "plate")
        {
            // var plate = plateScr.plate;
           
            var playerutil = playerUtilScr.utils;

            if (10 < playerutil.breakpower)
            {
                Destroy(collision.gameObject);
            }


        //Debug.Log("poo");
        plateFlag = true;
            return;
        }
        
        plateFlag = false;
    }


}
