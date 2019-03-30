using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinController : MonoBehaviour {

    //開始時にコインのRotateYを0~360に設定する
	void Start () {
        this.transform.Rotate(0, Random.Range(0, 360), 0);		
	}
	
	//毎フレームYを軸に3ずつ回転
	void Update () {
        this.transform.Rotate(0, 3, 0);
    }
}
