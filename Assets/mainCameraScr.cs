using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mainCameraScr : MonoBehaviour
{
    [SerializeField] private GameObject player;
    private Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
        offset =  transform.position - player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player.transform.position + offset;

        if (Input.GetKey(KeyCode.LeftShift))
        {
            
            transform.RotateAround(player.transform.position, Vector3.up, -5f);
        }
        
        else if (Input.GetKey(KeyCode.RightShift))
        {
            //ユニティちゃんを中心に5f度回転
            transform.RotateAround(player.transform.position, Vector3.up, 5f);
        }
    }
}
