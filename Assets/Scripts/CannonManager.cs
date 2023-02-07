using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonManager : MonoBehaviour
{
    //2つのCannonオブジェクト格納する配列cannonsを用意
    [SerializeField] Cannon[] cannons;
    //発射力用の変数powerを用意
    [SerializeField] float power;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("侵入した");
            StartCoroutine(ReadyToShot());
        }
    }

    IEnumerator ReadyToShot()
    {
        yield return new WaitForSeconds(3f);

        cannons[0].Shot(power);
    }

// Start is called before the first frame update
void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}







//落ちる床の時のようにコルーチンで3秒待って発射するように記述

//コルーチンの中でCannonクラスのShot()メソッドを呼び出して動作テストしてみる。