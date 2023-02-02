using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attouch : MonoBehaviour
{
    [SerializeField] hellowworld hellowworld;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("外部からアクセスです。"+hellowworld.person_type[1]);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
