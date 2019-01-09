using UnityEngine;
using System.Collections;

public class ItemGenerator : MonoBehaviour
{
    //carPrefabを入れる
    public GameObject carPrefab;
    //coinPrefabを入れる
    public GameObject coinPrefab;
    //cornPrefabを入れる
    public GameObject conePrefab;
    //スタート地点
    private int startPos = -160;
    //ゴール地点
    private int goalPos = 120;
    //アイテムを出すx方向の範囲
    private float posRange = 3.4f;

    // Use this for initialization
    void Start()
    {

        //for (変数初期化; ループ条件式; 変数の更新)のおさらい
        //{
          //  繰り返したい処理;
        //}
        //一定の距離ごとにアイテムを生成
        for (int i = startPos; i < goalPos; i += 15)
        {
            //どのアイテムを出すのかをランダムに設定
            //numの値が１～１０の範囲
            //第１引数から第２引数の前まで（つまり１０）
            //１.0から1.5だと、第２引数を含んだ数、つまり1.5まで
            //小数点と整数と異なる。
           

                 // public class Random
                 //{
                 //public static int Range(int min, int max)
                 //{

                 //}
                 //}

                 // このように書かない
                 //Random rand = new Random();
                 //int num = Random.Range(1, 11)

            // 以下のように書く
            int num = Random.Range(1, 11);


            if (num <= 2)　//つまり、num=1と2なので、2/10＝20％の確率でコーンが出る。
            {
                //コーンをx軸方向に一直線に生成
                for (float j = -1; j <= 1; j += 0.4f)
                {
                    //Object.Instantiateはoriginal のオブジェクトをクローンする
                    //戻り値はObject The instantiated clone
                    //Instantiate で返されるオブジェクトは Object 型のためそのままでは操作できない。
                    //以下のように"as"でキャストをかける必要があり。
                    //親から子供、子供から親に型変換できる
                    GameObject cone = Instantiate(conePrefab) as GameObject;

                    //coneは変数、これはGameObject型、GameObject型の中にはTransform型のtransform変数があり、
                    //Transform型（クラス）には、positionという変数がある。
                    //コンストラクターは自分で作ることができるが、Vector３でUnityで提供されている
                    //だから書かなくてもよい。
                    //自分で書くときは以下のように使う。structはclassとは違うが、ほぼ一緒と考えてよい。
    　　　　　　　　　　　　　　　　　　//public struct Sample
    　　　　　　　　　　　　　　　　　　//{
    　　　　　　　　　　　　　　　　　　//public int _x;
　　　　　　　　　　　　　　　　　　　　//public int _y;

        　　　　　　　　　　　　　　　　// コンストラクタnewした時に値を設定できる、newする時に呼ぶ関数のようなもの
　　　　　　　　　　　　　　　　　　　　//public Sample(int x, int y)
　　　　　　　　　　　　　　　　　　　　//{
　　　　　　　　　　　　　　　　　　　　//_x = x;
　　　　　　　　　　　　　　　　　　　　//_y = y;
　　　　　　　　　　　　　　　　　　　　//}
　　　　　　　　　　　　　　　　　　　　//}

    　　　　　　　　　　　　　　　　　　// Boss lastBoss = new Boss();と以前のコマで書いたように

　　　　　　　　　　　　　　　　　　　　//Sample sample = new Sample(1, 2);
　　　　　　　　　　　　　　　　　　　　//Debug.Log(sample._x); // 1
　　　　　　　　　　　　　　　　　　　　//Debug.Log(sample._y); // 2
          
                    cone.transform.position = new Vector3(4 * j, cone.transform.position.y, i);
                }
            }
            else
            {

                //レーンごとにアイテムを生成
                //Lesson4の応用
                for (int j = -1; j <= 1; j++)
                {
                    //アイテムの種類を決める
                    //1～10の値がitemという変数（箱）に代入される、入ってくる
                    int item = Random.Range(1, 11);
                    //アイテムを置くZ座標のオフセットをランダムに設定
                    //-5から5の範囲
                    //-5～5の値がoffsetZという変数（箱）に代入される
                    int offsetZ = Random.Range(-5, 6);
                    //1~6の60 %コイン配置:30%車配置:10%何もなし
                  
                    if (1 <= item && item <= 6)
                    {
                        //コインを生成
                        //coinPrefabはプロジェクトにある。
                        //それは、元素材
                        //Instantiate関数は（）の引数の中にゲームの世界に生成するPrefabを持ってくる関数
                        GameObject coin = Instantiate(coinPrefab) as GameObject;//キャストする型を変換する
                                                                                //int型→1=1.0f←float型　「型変換」をasで行う。
                                                                                //Instantiate関数を呼び出して戻ってくる値の型がObject型になる。

                        coin.transform.position = new Vector3(posRange * j, coin.transform.position.y, i + offsetZ);
                    }
                    else if (7 <= item && item <= 9)
                    {
                        //車を生成
                        GameObject car = Instantiate(carPrefab) as GameObject;
                        car.transform.position = new Vector3(posRange * j, car.transform.position.y, i + offsetZ);
                    }
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}