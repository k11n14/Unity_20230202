using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

public class CannonManager : MonoBehaviour
{
    //2つのCannonオブジェクト格納する配列cannonsを用意
    [SerializeField] Cannon[] cannons;
    //発射力用の変数powerを用意
    [SerializeField] float power;

    [SerializeField] float shotMinInterval; // 追加
    [SerializeField] float shotMaxInterval; // 追加

    //isWaitingToShot;というbool値の変数
    //ブール値は真理値(TRUE または FALSE) を表します。
    bool isWaitingToShot;

    //OnTriggerStayは侵入している間、Updateのように呼び続けられる
    private void OnTriggerStay(Collider other)
    {
        //Debug.Log("OnTriggerStay:侵入を感知！！！");ß

        // isWaitingToShotがtrueの間はreturnで抜ける
        if (isWaitingToShot) return;
        //Debug.Log(isWaitingToShot + "if (isWaitingToShot) return;を抜けた");

        if (other.CompareTag("Player"))
        {
           

            // 侵入したら isWaitingToShotをtureにして、以降の処理を行わないようにする
            isWaitingToShot = true;
            //Debug.Log("isWaitingToShot="+isWaitingToShot);
            //コルーチンは非同期に処理されるので、3秒待っている間も次のReadyToShot()が呼び出され続けている
            StartCoroutine(ReadyToShot());
        }
    }


    IEnumerator ReadyToShot()
    {
        float waitingTime = Random.Range(shotMinInterval, shotMaxInterval); //追加
        int cannonIndex = Random.Range(0, cannons.Length); //追加

        //yield return new WaitForSeconds(3f);
        yield return new WaitForSeconds(waitingTime); // 変更


        //cannons[0].Shot(power);
        cannons[cannonIndex].Shot(power); // 変更

        // 発射し終わったらisWaitingToShotをfalseに戻す
        isWaitingToShot = false;
        //Debug.Log("isWaitingToShot=" + isWaitingToShot);


    }


}

//// Random.Rangeは引数の取る値がfloatの場合ならfloat型のランダム値を返す
//float waitingTime = Random.Range(shotMinInterval, shotMaxInterval);
//// float値の場合、最大値の値も含む

//// int値ならint型のランダム値を返す
//int cannonIndex = Random.Range(0, cannons.Length);
//// int値の場合、最大値の値は含まれない
//// cannons.Lengthは2になるが、この場合ランダムで出る値は0か1になる事に注意
///https://docs.unity3d.com/ja/current/ScriptReference/Random.Range.html






//落ちる床の時のようにコルーチンで3秒待って発射するように記述

//コルーチンの中でCannonクラスのShot()メソッドを呼び出して動作テストしてみる。