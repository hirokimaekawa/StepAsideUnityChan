using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyCameraController : MonoBehaviour {

    //Unityちゃんのオブジェクト
    //①GameObjectクラスのunitycyanという変数を定義？宣言？
    private GameObject unitychan;
    //float型のdifference変数
    //Unityちゃんとカメラの距離
    private float difference;

    // Use this for initialization
    void Start()
    {
        //Unityちゃんのオブジェクトを取得
        //Find関数でUnityちゃんの座標（x,y,z）を取得する
        //Find関数は string name で GameObject を検索し返します。とリファレンスに書いてある。
        //①でunitychan変数を宣言してなくてもできる。
        //this.defference = GameObject.Find("unitychan").transform.position.z - this.transform.position.z;と書くことができる。
        //ただし、UnitychanのZ座標にアクセスするたびに検索する作業が無駄になる。
        this.unitychan = GameObject.Find("unitychan");
        //Unityちゃんとカメラの位置（z座標）の差を求める
        //このメンバ変数のdifference変数は次の式に代入される→unitychan(変数)のpositionのzから（this.○○.transform.position.z）を引く。
        //○○はカメラの位置。○○はいらないのか？
        //Anwser:いらない。thisはMyCameraControllorを指している。
        this.difference = unitychan.transform.position.z - this.transform.position.z;

    }

    // Update is called once per frame
    void Update()
    {
        //Unityちゃんの位置に合わせてカメラの位置を移動
        this.transform.position = new Vector3(0, this.transform.position.y, this.unitychan.transform.position.z - difference);
    }
}