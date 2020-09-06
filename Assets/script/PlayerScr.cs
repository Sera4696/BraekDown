using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScr : MonoBehaviour
{
    GameObject clickGameobj;
    private Vector3 clickwarpPos;
    private bool clickwarpFlag;

    [SerializeField] private Vector3 velocity;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float applySpeed = 0.2f;

    [SerializeField] private PlayerFollowCamera refCamera;
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
        clickwarpFlag = false;
    }

    // Update is called once per frame
    void Update()
    {
        //float moveH = Input.GetAxis("Horizontal");
        //float moveV = Input.GetAxis("Vertical");

        velocity = Vector3.zero;
        if (Input.GetKey(KeyCode.W))
            velocity.z += 1;
        if (Input.GetKey(KeyCode.A))
            velocity.x -= 1;
        if (Input.GetKey(KeyCode.S))
            velocity.z -= 1;
        if (Input.GetKey(KeyCode.D))
            velocity.x += 1;

        velocity = velocity.normalized * moveSpeed * Time.deltaTime;

        if (velocity.magnitude > 0)
        {
            // プレイヤーの回転(transform.rotation)の更新
            // 無回転状態のプレイヤーのZ+方向(後頭部)を、移動の反対方向(-velocity)に回す回転とします

            transform.rotation = Quaternion.Slerp(transform.rotation,
                                                  Quaternion.LookRotation(refCamera.hRotation * -velocity),
                                                  applySpeed);

            // プレイヤーの位置(transform.position)の更新
            // 移動方向ベクトル(velocity)を足し込みます
            transform.position += refCamera.hRotation * velocity;         
        }


        //float moveJ = Input.GetAxis("Jump");
        if (Input.GetButtonDown("Jump") && jumpFlag == false)
        {
            rb.velocity = Vector3.up * jumpSpeed;
            jumpFlag = true;
        }

        if (Input.GetMouseButtonDown(0))
        {
            clickGameobj = null;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit = new RaycastHit();

            if (Physics.Raycast(ray, out hit))
            {
                clickGameobj = hit.collider.gameObject;

                clickwarpPos = clickGameobj.transform.position;


                if(clickwarpFlag == true)
                {
                    Debug.Log(clickGameobj);
                    transform.position = clickwarpPos;

                    transform.localPosition += new Vector3(0, 1.3f, 0);
                }
            }

            
        }

    }


    private void OnCollisionEnter(Collision collision)
    {
        jumpFlag = false;




        //足場に当たったか？
        if (collision.transform.tag == "floortag")
        {
            //できる状態
            reboundFlag = false;
            clickwarpFlag = true;
        }
        else
        {
            clickwarpFlag = false;
        }

        if (collision.gameObject.name == "plate")
        {
            // var plate = plateScr.plate;
            

            var playerutil = playerUtilScr.utils;

            if (plateScr.plate.breakpoint < playerutil.breakpower)
            {
                Destroy(collision.gameObject);
            }

            if (plateScr.plate.breakpoint > playerutil.breakpower)
            {

                if (reboundFlag == false)
                {
                    rb.velocity = Vector3.up * playerutil.breakpower * 1.5f;
                    reboundFlag = true;
                }

                else if (reboundFlag == true)
                {
                    reboundFlag = false;
                }


            }

            
            plateFlag = true;
            return;
        }
        
        plateFlag = false;
    }


    private void OnTriggerStay(Collider other)
    {
        Material mat = this.GetComponent<Renderer>().material;
        mat.color = new Color(1.0f, 1, 1, 1.0f);

        //範囲内判定
        if (other.transform.tag == "floortag")
        {
            
            mat.color = new Color(1.0f,0,0,1.0f);
            clickwarpFlag = true;
        }
        else
        {
            clickwarpFlag = false;
        }
    }
}
