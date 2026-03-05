using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseObj : MonoBehaviour
{
    // Start is called before the first frame update
    protected abstract void MoveTo(Vector3 go,float dis);
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
