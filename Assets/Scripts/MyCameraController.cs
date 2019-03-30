using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyCameraController : MonoBehaviour {

    //カメラにアタッチしZ座標を固定するスクリプト
    private GameObject unitychan;
    private float difference; //Z座標の差
    
	void Start () {
        this.unitychan = GameObject.Find("unitychan");
        this.difference = unitychan.transform.position.z - this.transform.position.z; //ユニティちゃんとカメラのZ座標の差を取得しておく
	}
	
	void Update () {
        //Xは常に0、Yはそのまま、ユニティちゃんからdifferenceの数値だけ後方に配置される(開始時から位置関係を固定)
        this.transform.position = new Vector3(0, transform.position.y, this.unitychan.transform.position.z - difference);
	}
}
