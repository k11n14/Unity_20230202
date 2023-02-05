using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{

    [SerializeField] GameObject cannonBallPrefab;
    [SerializeField] Transform muzzle;

    // Start is called before the first frame update
    void Start()
    {
        Shot(20f); // テスト用
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Shot(float power)
    {
        //CannonBallプレハブをInstantiateでインスタンス化
        //Instantiate(インスタンス元のオブジェクト, 生成時のPosition, 生成時のRotation);
        //Quaternion.で回転していない状態を設定
        //https://docs.unity3d.com/ja/current/ScriptReference/Quaternion-identity.html
        var cannonBall = Instantiate(cannonBallPrefab, muzzle.position, Quaternion.identity);
        //インスタンス化したCannonBallのRigidBodyを取得
        var cannonBallRb = cannonBall.GetComponent<Rigidbody>();
        //AddForceで発射させる
        //muzzle.forwardでmuzzleの前方向の単位ベクトル（長さ1のベクトル）を取得。それにpowerを乗算して発射される強さとする。質量は無視したいのでForceMode.VelocityChangeとした。
        cannonBallRb.AddForce(muzzle.forward * power, ForceMode.VelocityChange);
    }
}



//大砲エリアの実装
//大砲エリアの仕様
//この坂にプレイヤーが侵入したら砲弾を発射させる
//エリアにプレイヤーがいる間はどちらかの大砲がランダムなタイミングで砲弾を発射し続ける。
//大砲は一定間隔で首振り運動をしている
//発射された砲弾は一定時間後に消滅する

//砲弾の発射
//まず発射する砲弾の準備をする
//インポートしたGsSampleStage > Prefabsの中にあるCannonBallをダブルクリックして編集モードに入る
//RigidbodyをアタッチしてMassを1000にしておく（プレイヤーにぶつかった時にプレイヤーを飛ばしたいため）
//次は大砲本体の準備をする
//同じくCannonのプレハブをダブルクリックして開く
//Bodyの中に空のGameObjectを作成して名前をMuzzle（銃口）とする。
//Muzzleを大砲の先端中央に配置する。この位置から砲弾を発射させる。

//Instantiateは引数の取り方によって、それぞれの引数の意味が変わってくるので注意
// Unityマニュアルより
//public static Object Instantiate(Object original);
//public static Object Instantiate(Object original, Transform parent);
//public static Object Instantiate(Object original, Transform parent, bool instantiateInWorldSpace);
//// ↓ 今回使用したのはこちら　　↓ 
//public static Object Instantiate(Object original, Vector3 position, Quaternion rotation);
//public static Object Instantiate(Object original, Vector3 position, Quaternion rotation, Transform parent);
//https://docs.unity3d.com/ja/current/ScriptReference/Object.Instantiate.html

//CannonオブジェクトにCannonにスクリプトをアタッチして、Muzzle、CannonBallプレハブをそれぞれアサインする