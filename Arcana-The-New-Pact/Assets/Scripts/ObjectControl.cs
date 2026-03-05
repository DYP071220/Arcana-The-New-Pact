using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectControl : BaseObj
{
    // Start is called before the first frame update
    private Vector3 primitivePos;
    private Vector3 primitiveRot;
    private Vector3 tempPos;
    private bool canMove;

    public Transform t;
    private float distance;
    void Start()
    {
        distance = 1;
        canMove=true;
        primitivePos = transform.position;
        primitiveRot = transform.eulerAngles;
        tempPos=Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            MoveTo(t.position,distance);
        }
    }
    protected override void MoveTo(Vector3 go,float dis)
    {
        if (Vector3.Distance(transform.position, go) > dis)
        {
            tempPos = go;
            tempPos.y=transform.position.y;
            transform.LookAt(tempPos);
            transform.Translate(Vector3.forward*Time.deltaTime*5);
        }
        else
        {
            if (dis > 0.1f)
            {
                //攻击
            }
            else
            {
                //返回到原始位置
                GoBack();
            }
        }
    }
    private void GoBack()
    {
        transform.position=primitivePos;
        transform.eulerAngles = primitiveRot;
        canMove = false;
    }
}
