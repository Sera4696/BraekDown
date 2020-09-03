using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerUtilScr : MonoBehaviour
{
    public float breakpower;
    public static playerUtilScr utils;

    public float beforeplayerpos;

    // Start is called before the first frame update
    void Start()
    {
        utils = this;
        breakpower = 0;
    }

    // Update is called once per frame
    void Update()
    {
        var player = PlayerScr.player;

        ////力がplayerのY軸よりも小さかったらY軸を代入
        //if(player.transform.position.y > breakpower)
        //{
        //    breakpower = player.transform.position.y;
        //    //Debug.Log(breakpower);
        //}


        //今のポジションが1フレーム前のポジションよりデカかったら
        if (player.transform.position.y > beforeplayerpos)
        {
            //今のポジション　ー　1フレーム前のポジション
            float power = player.transform.position.y - beforeplayerpos;
            //過去のポジションを更新
            beforeplayerpos = player.transform.position.y;
            //誤差を＋
            breakpower += power;

        }

        //plateに当たったら力を0にする
        if (player.plateFlag == true)
        {
            beforeplayerpos = player.transform.position.y;
            breakpower = 0;
            //Debug.Log(player.plateFlag);
            player.plateFlag = false;
        }

        ////plateに当たったら力を0にする
        //if (player.plateFlag == true)
        //{
        //    breakpower = 0;
        //    //Debug.Log(player.plateFlag);
        //    player.plateFlag = false;
        //}

    }

   
}
