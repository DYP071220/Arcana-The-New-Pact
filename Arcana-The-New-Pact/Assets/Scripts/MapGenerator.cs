using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

[Serializable]
public class ItemSpawnData
{
    public TileBase Tile;
    public int weight;
}
public class MapGenerator : MonoBehaviour
{
    public Tilemap groundTileMap;
    public Tilemap itemTileMap;
    public int width;
    public int height;

    public int seed;
    public bool useRandomSeed;

    public float lacunarity;

    [Range(0,1f)]
    public float waterProbality;
    public List<ItemSpawnData> itemspawnDatas;

    //移除孤岛Tile的次数
    public int removeSeperateTileNumberOfTimes;

    public TileBase groundTile;
    public TileBase waterTile;

    private float[,] mapData;//true是陆地，false是水
   public void GenerateMap()
    {
        itemspawnDatas.Sort((data1, data2) =>
        {
            return data1.weight.CompareTo(data2.weight);
        });
        GenerateMapData();
        //地图处理
        
        for (int i = 0; i < removeSeperateTileNumberOfTimes; i++) 
        {if (!RemoveSeperateTile())//如果本次操作什么都没有处理,则不进行循环
            {
                break;
            }
            RemoveSeperateTile();
        }

        GenerateTileMap();
    }
    private void GenerateMapData()
    {
        //对于种子的应用
        if (!useRandomSeed)
        {
            seed=Time.time.GetHashCode();
        }
        UnityEngine.Random.InitState(seed);
        mapData=new float[width,height];

        float randomOffset=UnityEngine.Random.Range(-10000f,10000f);

        float minValue=float.MaxValue;
        float maxValue=float.MinValue;
        ;
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                float noiseValue=Mathf.PerlinNoise(x* lacunarity + randomOffset, y* lacunarity + randomOffset);
                mapData[x,y]=noiseValue;  
                if (noiseValue < minValue)
                {
                    minValue=noiseValue;
                }
                if (noiseValue > maxValue)
                {
                    maxValue = noiseValue;
                }

            }
        }
        //平滑到0~1
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {

                mapData[x, y] = Mathf.InverseLerp(minValue,maxValue,mapData[x,y]);

            }
        }
    }
    private bool RemoveSeperateTile()
    {
        bool res=false;//是否是有效操作
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                //是地面且只有一个邻居也是地面
                if (IsGround(x, y) && GetFourNeighborsGroundCount(x,y) <=1)
                {
                    mapData[x, y] = 0;//设置为水
                    res = true;
                }
                
            }
          
        }
        return res;
    }
    private int GetFourNeighborsGroundCount(int x,int y)
    {
        int count = 0;
        //top
        if (IsInMapRange(x, y + 1) && IsGround(x, y + 1))
            count += 1;
        //button
        if (IsInMapRange(x, y - 1) && IsGround(x, y - 1)){
            count += 1; }

        //left
        if (IsInMapRange(x - 1, y  ) && IsGround(x - 1, y )) {
            count += 1; }

        //right
        if (IsInMapRange(x + 1, y  ) && IsGround(x + 1, y )) {
            count += 1; }

        return count;
    }
    private int GetEidhtNeighborsGroundCount(int x, int y)
    {
        int count = 0;
        //top
        if (IsInMapRange(x, y + 1) && IsGround(x, y + 1))
            count += 1;
        //button
        if (IsInMapRange(x, y - 1) && IsGround(x, y - 1))
        {
            count += 1;
        }

        //left
        if (IsInMapRange(x - 1, y) && IsGround(x - 1, y))
        {
            count += 1;
        }

        //right
        if (IsInMapRange(x + 1, y) && IsGround(x + 1, y))
        {
            count += 1;
        }

        //left top
        if (IsInMapRange(x-1, y + 1) && IsGround(x-1, y + 1))
            count += 1;
        //left button
        if (IsInMapRange(x-1, y - 1) && IsGround(x-1, y - 1))
        {
            count += 1;
        }

        //right top
        if (IsInMapRange(x + 1, y+1) && IsGround(x + 1, y+1))
        {
            count += 1;
        }

        //right button
        if (IsInMapRange(x + 1, y-1) && IsGround(x + 1, y-1))
        {
            count += 1;
        }
        return count;
    }
    private void GenerateTileMap()
    {
        CleanTileMap();

        //地面
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                TileBase tile = IsGround(x,y)?groundTile:waterTile;
                groundTileMap.SetTile(new Vector3Int(x,y), tile);
            }
        }
        //物品
        int weightTotal = 0;
        for (int i = 0; i < itemspawnDatas.Count; i++)
        {
            weightTotal += itemspawnDatas[i].weight;
        }
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (IsGround(x, y)&&GetEidhtNeighborsGroundCount(x,y)==8)//只有地面可以生成物品
                {
                    float randValue=UnityEngine.Random.Range(1,weightTotal+1);
                    float temp = 0;
                    for (int i = 0; i < itemspawnDatas.Count; i++)
                    {
                        temp+= itemspawnDatas[i].weight;
                        if (randValue<temp)
                        {
                            if (itemspawnDatas[i].Tile)
                            { 
                                //命中
                                itemTileMap.SetTile(new Vector3Int(x,y),itemspawnDatas[i].Tile);
                            }
                            break;
                        }
                    }
                }
            }
        }
    }
    public bool IsInMapRange(int x,int y)
    {
        return x>=0 && y>=0 && x<width && y<height;
    }
    public bool IsGround(int x,int y)
    {
        return mapData[x, y] > waterProbality;
    }
    public void CleanTileMap()
    {
        groundTileMap.ClearAllTiles();
        itemTileMap.ClearAllTiles();
    }

}
