using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UnityChanController : MonoBehaviour
{
    //アニメ用コンポーネント
    private Animator myAnimator;
    private Rigidbody myRigidbody;
    //前進時にかかる力
    private float forwardForce = 800.0f;
    //左右移動時
    private float turnForce = 500.0f;
    //ジャンプ時
    private float upForce = 500.0f;
    //左右移動の範囲、X=-3.4～3.4の間で動かす
    private float movableRange = 3.4f;
    //減速時
    private float coefficient = 0.95f;
    //終了フラグ
    private bool isEnd = false;
    
    //UI用
    private GameObject stetaText;
    private GameObject scoreText;
    private int score=0;

    //左ボタン
    private bool isLButtonDown = false;
    //右ボタン
    private bool isRButtonDown = false;

    void Start()
    {
        this.myAnimator = GetComponent<Animator>();
        this.myAnimator.SetFloat("Speed", 1); //0.8以上で走りモーション
        this.myRigidbody = GetComponent<Rigidbody>();

        this.stetaText = GameObject.Find("GameResultText");
        this.scoreText = GameObject.Find("ScoreText");
    }

    void Update() {

        //終了処理
        if (this.isEnd)
        {
            //前進、移動、ジャンプ、アニメを減速
            this.forwardForce *= this.coefficient;
            this.turnForce *= this.coefficient;
            this.upForce *= this.coefficient;
            this.myAnimator.speed *= this.coefficient;

            //1秒後にリトライ
            Invoke("Retry", 1);

        }

        //毎フレームforwardForceの力をかけ前進
        this.myRigidbody.AddForce(this.transform.forward * this.forwardForce);

        //キー入力があり、かつX座標が範囲内であれば左右に力をかける
        if ((Input.GetKey(KeyCode.LeftArrow) || this.isLButtonDown )&& -this.movableRange < this.transform.position.x)
        {
            this.myRigidbody.AddForce(-this.turnForce, 0, 0);
        }
        else if ((Input.GetKey(KeyCode.RightArrow) || this.isRButtonDown) && this.transform.position.x < this.movableRange)
        {
            this.myRigidbody.AddForce(this.turnForce, 0, 0);
        }

        //Jump状態ならJumpをfalseにする
        if (this.myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Jump"))
        {
            this.myAnimator.SetBool("Jump", false);
        }

        //スペースが押され、かつY座標が0.5以下ならJumpに遷移し、上昇させる
        if (Input.GetKeyDown(KeyCode.Space) && this.transform.position.y < 0.5f)
        {
            this.myAnimator.SetBool("Jump", true);
            this.myRigidbody.AddForce(this.transform.up * this.upForce);
        }
              
    }

    //リトライ用
    void Retry()
    {
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(sceneIndex);
    }

    //障害物との接触判定
    public void OnTriggerEnter(Collider other) {
        //車、コーンの場合、終了フラグを立てゲームオーバーと表示する
        if (other.gameObject.tag == "CarTag" || other.gameObject.tag == "TrafficConeTag")
        {
            this.isEnd = true;
            this.stetaText.GetComponent<Text>().text = "GAME OVER";
        }
        //ゴールの場合
        if (other.gameObject.tag == "GoalTag")
        {
            this.isEnd = true;
            this.stetaText.GetComponent<Text>().text = "CLEAR!";
        }
        //コインの場合
        //スコアに10点加算し表示
        //パーティクルを再生、オブジェクトを破棄する
        if (other.gameObject.tag == "CoinTag")
        {
            this.score += 10;
            this.scoreText.GetComponent<Text>().text = "Score  " + this.score + "pt";
            GetComponent<ParticleSystem>().Play();
            Destroy(other.gameObject);
        }
        
    }
    

    //以下、ボタン用の関数
    //
    public void GetMyJumpButtonDown()
    {
        //ボタンが押された時、y座標が0.5以下(ジャンプしてない状態)ならジャンプする
        if (this.transform.position.y < 0.5f)
        {
            this.myAnimator.SetBool("Jump", true);
            this.myRigidbody.AddForce(this.transform.up * this.upForce);
        }
    }

    //押している状態ならフラグを立てる
    public void GetMyLeftButtonDown()
    {
        this.isLButtonDown = true;
    }
    public void GetMyLeftButtonUp()
    {
        this.isLButtonDown = false;
    }
    public void GetMyRightButtonDown()
    {
        this.isRButtonDown = true;
    }
    public void GetMyRightButtonUp()
    {
        this.isRButtonDown = false;
    }

}
