using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScr : MonoBehaviour
{
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

    public GameObject floorPrefab;

    //X座標の最小値
    public float xMinPosition = -10f;
    //X座標の最大値
    public float xMaxPosition = 10f;
    //Y座標の最小値
    public float yMinPosition = 0f;
    //Y座標の最大値
    public float yMaxPosition = 10f;
    //Z座標の最小値
    public float zMinPosition = 10f;
    //Z座標の最大値
    public float zMaxPosition = 20f;

    // Start is called before the first frame update
    void Start()
    {
        jumpFlag = false;
        player = this;
        rb = GetComponent<Rigidbody>();
        plateFlag = false;
        reboundFlag = false;
    }

    // Update is called once per frame
    void Update()
    {
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

        if (Input.GetButtonDown("Jump") && jumpFlag == false)
        {
            rb.velocity = Vector3.up * jumpSpeed;
            jumpFlag = true;
        }

        //transform.localRotation = Quaternion.identity;
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        jumpFlag = false;
        Debug.Log(reboundFlag);
        //plateに当たったか？
        if (collision.transform.tag == "floortag")
        {
            reboundFlag = false;
        }

        if (collision.gameObject.name == "plate")
        {
            // var plate = plateScr.plate;
            
            var playerutil = playerUtilScr.utils;

            if (plateScr.plate.breakpoint < playerutil.breakpower)
            {
                reboundFlag = false;

                GameObject Floor = Instantiate(floorPrefab);

                Floor.transform.position = GetRandomPosition();

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

            //Debug.Log("poo");
            plateFlag = true;
            return;
        }
        
        plateFlag = false;
    }

    private Vector3 GetRandomPosition()
    {
        //それぞれの座標をランダムに生成する
        float x = Random.Range(xMinPosition, xMaxPosition);
        float y = Random.Range(yMinPosition, yMaxPosition);
        float z = Random.Range(zMinPosition, zMaxPosition);

        //Vector3型のPositionを返す
        return new Vector3(x, y, z);
    }

}
