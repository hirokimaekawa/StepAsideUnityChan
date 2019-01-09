using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnityChanController : MonoBehaviour {
    //アニメーションするためのコンポーネントを入れる
    private Animator myAnimator;
    //Unityちゃんを移動させるコンポーネントを入れる（追加）
    private Rigidbody myRigidbody;
    //前進するための力（追加）
    private float forwardForce = 800.0f;
    //左右に移動するための力（追加）
    private float turnForce = 500.0f;
    //ジャンプするための力（追加）
    private float upForce = 500.0f;
    //左右の移動できる範囲（追加）
    private float movableRange = 3.4f;

    //動きを減速させる係数（追加）
    private float coefficient = 0.95f;

    //ゲーム終了の判定（追加）
    private bool isEnd = false;

    //ゲーム終了時に表示するテキスト（追加）
    private GameObject stateText;

    //スコアを表示するテキスト（追加）
    private GameObject scoreText;
    //得点（追加）
    private int score = 0;

    //左ボタン押下の判定（追加）
    private bool isLButtonDown = false;
    //右ボタン押下の判定（追加）
    private bool isRButtonDown = false;

    // Use this for initialization
    void Start()
    {

        //Animatorコンポーネントを取得
        //"this"は「このクラスのメンバ変数を使う」ことを明示的に示している。
        //メンバ変数とはオブジェクトが持つパラメータのこと。
        this.myAnimator = GetComponent<Animator>();

        //走るアニメーションを開始
        //Animatorクラスの「SetFloat」関数は、第一引数に「与えられた」パラメータ(=Speed)に、第二引数の値を代入する関数です。
        //また、第一引数のバラメータがアニメーション再生の条件に使われています。
        this.myAnimator.SetFloat("Speed", 1);

        //Rigidbodyコンポーネントを取得（追加）
        this.myRigidbody = GetComponent<Rigidbody>();

        //シーン中のstateTextオブジェクトを取得（追加）
        //Find関数は、Unityの中のGameObjectを探す。しかもstring型、つまり文字を探す。GameResultTextを取得しています。
        this.stateText = GameObject.Find("GameResultText");

        //シーン中のscoreTextオブジェクトを取得（追加）
        this.scoreText = GameObject.Find("ScoreText");
    }
    void Update()
    {
        //ゲーム終了ならUnityちゃんの動きを減衰する（追加）
        if (this.isEnd)
        {
            this.forwardForce *= this.coefficient;
            this.turnForce *= this.coefficient;
            this.upForce *= this.coefficient;
            this.myAnimator.speed *= this.coefficient;
        }

        //Unityちゃんに前方向の力を加える（追加）
        //Vector3型でしている
        this.myRigidbody.AddForce(this.transform.forward * this.forwardForce);
        //Unityちゃんを矢印キーまたはボタンに応じて左右に移動させる（追加）
        if (Input.GetKey(KeyCode.LeftArrow) && -this.movableRange < this.transform.position.x)
        {
            //左に移動（追加）
            //
            this.myRigidbody.AddForce(-this.turnForce, 0, 0);
        }
        else if (Input.GetKey(KeyCode.RightArrow) && this.transform.position.x < this.movableRange)
        {
            //右に移動（追加）
            this.myRigidbody.AddForce(this.turnForce, 0, 0);
        }
        //Jumpステートの場合はJumpにfalseをセットする（追加）
        //GetCurrentAnimatorStateInfo関数の引数（）には、レイヤー番号０を入れる。
        //ジャンプするなどはBaseLayerに含まれている。引数には０を入れる。
        if (this.myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Jump"))
        {
            this.myAnimator.SetBool("Jump", false);
        }

        //ジャンプしていない時にスペースが押されたらジャンプする（追加）
        if (Input.GetKeyDown(KeyCode.Space) && this.transform.position.y < 0.5f)
        {
            //ジャンプアニメを再生（追加）
            this.myAnimator.SetBool("Jump", true);
            //Unityちゃんに上方向の力を加える（追加）
            //ちなみに、transform.upは緑軸（ｙ軸）方向、transform.rightは赤軸（x軸）方向、transform.forwardは青軸（z軸）方向である。
            this.myRigidbody.AddForce(this.transform.up * this.upForce);
        }

        //Unityちゃんを矢印キーまたはボタンに応じて左右に移動させる（追加）
        if ((Input.GetKey(KeyCode.LeftArrow) || this.isLButtonDown) && -this.movableRange < this.transform.position.x)
        {
            //左に移動
            this.myRigidbody.AddForce(-this.turnForce, 0, 0);
        }
        else if ((Input.GetKey(KeyCode.RightArrow) || this.isRButtonDown) && this.transform.position.x < this.movableRange)
        {
            //右に移動
            this.myRigidbody.AddForce(this.turnForce, 0, 0);
        }
         //private GameObject unitychan;
    //private GameObject carPrefab;
    //private GameObject coinPrefab;
    //private GameObject conePrefab;
         //this.unitychan = GameObject.Find("unitychan");
         //if(unitychan.transform.position.z>carPrefab.trasform.position.z||coinPrefab.trasform.position.z||conePrefab.trasform.position.z){ 
        //Destroy()
        //}

        //if(this.transform.position.z)
    }
    //トリガーモードで他のオブジェクトと接触した場合の処理（追加）
    void OnTriggerEnter(Collider other)
    {

        //障害物に衝突した場合（追加）
        if (other.gameObject.tag == "CarTag" || other.gameObject.tag == "TrafficConeTag")
        {
            this.isEnd = true;
            //stateTextにGAME OVERを表示（追加）
            this.stateText.GetComponent<Text>().text = "GAME OVER";
        }

        //ゴール地点に到達した場合（追加）
        if (other.gameObject.tag == "GoalTag")
        {
            this.isEnd = true;
            //stateTextにGAME CLEARを表示（追加）
            this.stateText.GetComponent<Text>().text = "CLEAR";
        }
        //コインに衝突した場合（追加）
        if (other.gameObject.tag == "CoinTag")
        {
            // スコアを加算(追加)
            this.score += 10;

            //ScoreText獲得した点数を表示(追加)
            //このメンバ変数のscoreText変数の
            this.scoreText.GetComponent<Text>().text = "Score" + this.score + "pt";

            //パーティクルを再生
            GetComponent<ParticleSystem>().Play();

           
            //接触したコインのオブジェクトを破棄（追加）
            Destroy(other.gameObject);
        }
    }

    //アクセス演算子 返り値 メソッド名(引数){まとまって命令を書く}  メソッド名（）｛｝＝関数？
    //ジャンプボタンを押した場合の処理（追加）
    public void GetMyJumpButtonDown()
    {
        if (this.transform.position.y < 0.5f)
        {
            this.myAnimator.SetBool("Jump", true);
            this.myRigidbody.AddForce(this.transform.up * this.upForce);
        }
    }

    //左ボタンを押し続けた場合の処理（追加）
    public void GetMyLeftButtonDown()
    {
        this.isLButtonDown = true;
    }
    //左ボタンを離した場合の処理（追加）
    public void GetMyLeftButtonUp()
    {
        this.isLButtonDown = false;
    }

    //右ボタンを押し続けた場合の処理（追加）
    public void GetMyRightButtonDown()
    {
        this.isRButtonDown = true;
    }
    //右ボタンを離した場合の処理（追加）
    public void GetMyRightButtonUp()
    {
        this.isRButtonDown = false;
    }

}

