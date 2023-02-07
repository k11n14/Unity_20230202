using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

//リスポーンの実装
public class KillZone : MonoBehaviour
{
    [SerializeField] Transform spawnPoint; // リスポーンする場所


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Playerのポジションをスポーンポイントのポジションと同じにする 
            other.transform.position = spawnPoint.position;
        }
    }



}



//KillZoneという空のGemaObjectを作る
//Box Colliderを追加
//Is Triggerにチェックを入れる
//Is Triggerにチェックを入れると、そのコライダーは衝突判定のみを行うようになり、他のコライダーはすり抜けるようになる。


//OnTrigger〜系には下記の種類がある（OnCollider〜系と同じ）
//| OnTriggerEnter | 他のColliderが触れたとき true 
//| OnTriggerExit | 他のColliderが離れたとき true 
//| OnTriggerStay | 他のColliderと触れている間 true

//OnCollision〜とOnTrigger〜の違い

//              必要な物                      すり抜け
//OnCollision〜 Collider ＋ Rigidbody        すり抜けない
//OnTrigger〜   ColliderのIs Triggerにチェック すり抜ける 


//OnTrigger〜はすり抜ける事が出来るので「この領域に入ったら何かする」みたいな時によく使う。

//SpawnPointの設定
//KillZoneスクリプトをKillZoneオブジェクトにアタッチする
//SpawnPointという空のGameObjectを作り、落ちる床手前に配置
//SpawnPointをKillZoneのSpawnPointフィールドにアサイン




