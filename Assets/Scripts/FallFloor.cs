using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class FallFloor : MonoBehaviour
{
    [SerializeField] float waitingTime = 3f; // 追加
    [SerializeField] float initializeTime = 5f; //追加
    [SerializeField] Color warningColor = Color.red; // 追加


    Rigidbody rb; // Rigidbody格納用の変数を用意
    Vector3 initalPosition; //追加
    Quaternion initialRotation; //追加
    Color initialColor; // 追加
    MeshRenderer floorMeshRenderer; // 追加



    // Start is called before the first frame update
    void Start()
    {
        // GetComponent<コンポーネント名>();でRigidbodyコンポーネントを取得
        rb = GetComponent<Rigidbody>();
        initalPosition = transform.position; //追加
        initialRotation = transform.rotation; //追加
        floorMeshRenderer = transform.GetChild(0).GetComponent<MeshRenderer>(); // 追加
        initialColor = floorMeshRenderer.material.color; // 追加
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //床にPlayerが乗った（衝突した）ことを判定するにはOnCollisionEnter イベントを使用する
    // https://docs.unity3d.com/ja/current/ScriptReference/Collider.OnCollisionEnter.html
    //OnCollisionEnterで衝突判定をするにはオブジェクトのColliderとRigidbodyが必要
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("衝突した！");
        if (collision.gameObject.CompareTag("Player"))
        {
            // isKinematicをfales（チェックを外した状態）にして落下させる
            //rb.isKinematic = false;
            StartCoroutine(Fall()); // 追加
            floorMeshRenderer.material.color = warningColor; // 追加
        }


    }

    // 追加 IEnumerableというよく似た型があるので間違わないように
    IEnumerator Fall()
    {
        //yield return new WaitForSeconds(待つ秒数); を記述する事で、その後の処理をn秒後に行う
        yield return new WaitForSeconds(waitingTime);
        rb.isKinematic = false;

        yield return new WaitForSeconds(initializeTime); //追加
        Init(); //追加
    }

    //追加
    void Init()
    {
        rb.isKinematic = true;
        transform.position = initalPosition;
        transform.rotation = initialRotation;
        floorMeshRenderer.material.color = initialColor; // 追加

    }

}




//落ちる床の実装
//衝突判定の確認
//FallFloorのプレハブを開く。（ヒエラルキーにあるFallFloorではない）
//Rigidbodyを追加。
//そのままでは重力で落下してしまうので、IsKinematicにチェックを入れる。
//IsKinematicにチェックを入れるとRigidbodyが物理的な影響を受けなくなり、落下しなくなる。
//これをスクリプトからON / OFFする事で床を落下させる。
//作成したFallFloorスクリプトをアタッチ


//| OnCollisionEnter | 他のColliderが触れたとき true 
//| OnCollisionExit | 他のColliderが離れたとき true 
//| OnCollisionStay | 他のColliderと触れている間 true 


//数秒後に落ちる
//数秒後に実行するような処理にはコルーチン(Coroutine)という機能を使う
//https://docs.unity3d.com/ja/current/ScriptReference/Coroutine.html

//このような非同期処理をもっとスマートに行うUniTaskというライブラリもある。
//より複雑な非同期処理を行う際はUniTaskを使った方が書きやすい。
//https://github.com/Cysharp/UniTask


//床を元に戻す
//initalPosition、initialRotationで床の元の位置と回転を保存しておく
//yield return new
//WaitForSeconds(initializeTime); を追加してInit()を実行
//isKinematicをtrue戻し、位置と回転に保存していた値を代入して元の位置に戻す


//色の変更
//位置を戻す処理と同じように元の色を保存しておき、後で戻す。
//色はMeshRenderer.material.colorに色成分を代入することで変更出来る
//色を変更するオブジェクトはスクリプトがアタッチされてるFallFloorオブジェクトではなく。
//その子にあるFloorオブジェクトなのでFloorオブジェクトを取得する必要がある。
//[SerializeField] を使ってインスペクタからFloorオブジェクトを取得してもいい
//transform.GetChild(index)で子のオブジェクトを取得する事が出来る
//indexに入る数字はヒエラルキーの上からになるので、transform.GetChild(0)で取得出来る。


//プレイヤーだけに反応
//Tag機能を使ってプレイヤーを判定する。
//Playerを選択してTagのドロップダウンボタンをクリック
//リストの中からPlayerタグを選択//予め何個かタグが用意されている
//独自のタグを追加したい場合は「 Add Tag…  」から追加
//衝突したオブジェクトの情報はOnCollisionEnterの引数Collisionに格納されている
//collision.gameObject.CompareTag("Player")でタグがPlayerの場合trueを返す

