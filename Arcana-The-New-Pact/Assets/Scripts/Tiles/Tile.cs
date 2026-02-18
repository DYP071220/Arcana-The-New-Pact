using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class Tile : MonoBehaviour
{
    [SerializeField] private Color color;
    [SerializeField] private SpriteRenderer arenderer;
    [SerializeField] private GameObject highlight;

    //TODO 
    /// <summary>
    /// 地图差分颜色
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    public void Init(int x,int y)
    {
            arenderer.color = color;
    }


    #region 鼠标高亮显示
    private void OnMouseEnter()
    {
        highlight.SetActive(true);
    }
    private void OnMouseExit() 
    { 
        highlight.SetActive(false);
    }
    #endregion
}
