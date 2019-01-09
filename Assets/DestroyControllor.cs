using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyControllor : MonoBehaviour {
    private GameObject unitychan;

    // Use this for initialization
    //プレイボタンを押したときに発生する処理内容
    //後から生成したものは、生成した後に処理する
    //=ゲームの世界に登場して最初のフレームで呼ばれる関数がStart関数
    void Start () {
        this.unitychan = GameObject.Find("unitychan");
        Debug.Log("A");

	}
	
	// Update is called once per frame
	void Update () {
        
        if (unitychan.transform.position.z > this.transform.position.z + 10) {
            Destroy(this.gameObject);
        }
	}
}
