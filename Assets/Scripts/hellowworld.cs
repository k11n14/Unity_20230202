using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hellowworld : MonoBehaviour
{
    //型　変数宣言　＝　”文字列”；
    string const_message = "hello const";
    //型　変数宣言　＝　”文字列”；
    string func_message = "hello func";
    //型[] 配列名宣言　＝　new 型[]{”文字列”、”文字列”、”文字列”}；
    string[] languages = new string[] { "PHP", "JAVA", "CSS" };
    //アクセスなんとか　リスト宣言＜型＞　名宣言　＝　new <型>{”文字列”、”文字列”、”文字列”}；
    public List<string> person_type = new List<string> { "white", "black", "yellow" };
    // Start is called before the first frame update
    //関数　名宣言（）
    void Start()
    {
        Debug.Log(const_message);
        //関数の呼び出し

        //戻り値、引数なし
        void hello_func()
        {
            Debug.Log(func_message);
        }
        hello_func();

        //引数あり
        void hello_argument(string text)
        {
            Debug.Log(text);
        }
        hello_argument("hello argument");

        Debug.Log( hello_return_value("!!!!"));

        for(int i = 0; i < languages.Length; i++)
        {
            Debug.Log(languages[i]);
        }
        //languages[3] = "HTML";これはエラー。配列は追加できない。
        languages[2] = "C#";//変更はできる

        //リストは追加できる。
        person_type.Add("bule");
        for(int i =0; i < person_type.Count; i++)
        {
            Debug.Log(person_type[i]);
        }

    }

        //引数、戻り値あり
        public string hello_return_value(string text)
        {
            return text + "hello_return_value";
        }

    // Update is called once per frame
    void Update()
    {
        
    }
}
