using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinControllor : MonoBehaviour {

        // Use this for initialization
        void Start()
        {
        //回転を開始する角度を設定
        //Random.Range (float min, float max);について
        //min(この値を含む) と max(この値を含む) の範囲のランダムな float 型の数を返します
        //Random,Range関数をRotate関数の第二引数で直接使っている。
            this.transform.Rotate(0, Random.Range(0, 360), 0);
        }

        // Update is called once per frame
        void Update()
        {
            //回転の速さをこれが50とかにすると高速になる
            this.transform.Rotate(0, 3, 0);
        }
    }
