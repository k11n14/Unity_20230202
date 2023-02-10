using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBall : MonoBehaviour
{
    [SerializeField] float destroyTime;

    // Start is called before the first frame update
    void Start()
    {
        // this.gameObject は自分自身（インスタンス化されたCannonBall）の事
        Destroy(this.gameObject, destroyTime);
        //Destroy(Object obj, float t = 0.0F); でオブジェクトの消去が出来る
        //第２引数 tには消去までの秒数を入れる。省略すると即座に消去される。
        //this.gameObjectはgameObjectだけでもOKだが、分かりやすいように明示的にthis.をつけた
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
