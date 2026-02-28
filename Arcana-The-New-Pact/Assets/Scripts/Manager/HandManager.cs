using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class HandManager : MonoBehaviour
{
    public static HandManager Instance { get; private set; }

    public List<Warrior> warriorPrefabList;

    private Warrior currentWarrior;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        FollowCursor();
    }
    //public void AddPlant(PlantType plantType)
    //{
    //    Plant plantPrefab = GetPlantPrefab(plantType);
    //    if (plantPrefab == null)
    //    {
    //        print("对应植物不存在");
    //        return;
    //    }
    //    currentPlant = GameObject.Instantiate(plantPrefab);
    //}

    //private Warrior GetPlantPrefab(WarriorType warriorType)
    //{
    //    foreach (Warrior warrior in warriorPrefabList)
    //    {
    //        if (warrior.warriorType == warriorType)
    //        {
    //            return warrior;
    //        }
    //    }
    //    return null;
    //}

    void FollowCursor()
    {
        if (currentWarrior == null) return;

        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPosition.z = 0;
        currentWarrior.transform.position = mouseWorldPosition;


    }

    public void OnCellClick(Tile tile)
    {
        if (currentWarrior == null) return;

        bool isSuccess = tile.AddPlant(currentWarrior);

        if (isSuccess)
        {
            currentWarrior = null;
        }

    }
}
