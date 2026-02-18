using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[CreateAssetMenu(fileName="NewCard",menuName="Card")]
public class CardMessage : ScriptableObject
{
    public int ActionPoint;
    public string Title;
    public string Description;
    public Sprite Card_Art;
}
