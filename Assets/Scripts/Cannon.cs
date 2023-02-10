using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Cannon : MonoBehaviour
{

    [SerializeField] GameObject cannonBallPrefab;
    [SerializeField] Transform muzzle;
    [SerializeField] float rotationSpeed = 1f; // 追加
    [SerializeField] float angleRange = 60f; // 追加
    Transform body;
    float angleValue;

    // Start is called before the first frame update
    void Start()
    {
        //Shot(20f); // テスト用
        body = transform.GetChild(0);
    }

    // Update is called once per frame
    void Update()
    {
        // Time.timeは再生されてからの時間経過 これをSin関数の値として利用する
        // それに rotationSpeed を掛けて回転速度を調整出来るようにしている
        float cycle = Mathf.Sin(Time.time * rotationSpeed);
        // UpdateのたびにangleValueが加算されていく
        //angleValue += 3f * Time.deltaTime;
        // cycleには 1から-1の間の値が入っているので、それにangleRangeを掛けて狙った角度にする
        angleValue = cycle * angleRange;
        // Quaternion.AngleAxisでY軸(Vector3.up)のみ操作する
        // rotationではなく、localRotationに代入している事に注意
        body.localRotation = Quaternion.AngleAxis(angleValue, Vector3.up);
        //https://docs.unity3d.com/ja/2022.1/Manual/class-Quaternion.html
    }

    public void Shot(float power)
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

//プレイヤーの侵入判定
//KillZoneを作った時のように空のGameObjectを用意する。
//名前はCannonShotAreaとした。
//BoxColliderを追加して坂を囲むようにColliderを調節する
//IsTriggerにチェックを入れる
//コライダーの調節は動画のようにEdit Colliderボタンを押して、シーン上で調整出来る。
//3D空間は奥行きや高さが分かりづらい事があるので、シーンギズモをアイソメトリックに切り替えて調節・配置する
//CannonManagerスクリプトを作成する


//プレイヤーの回転を実装した時にも触れたように、transform.rotationはQuaternionという型で定義されていて、
//単純にVector3を代入して操作する事は出来ない。
//Unityには回転を制御するメソッドがいくつか用意されているのでそれらを使用する。
//今回はY軸のみを操作するのでQuaternion AngleAxis(float angle, Vector3 axis); を使用する。
//https://docs.unity3d.com/ja/2022.1/ScriptReference/Quaternion.AngleAxis.html