using Group8.TrashDash.TrashBin;
using System;
using UnityEngine;

[CreateAssetMenu(fileName = "New Trash Info", menuName = "Item/Trash")]
public class TrashContentInfo : ScriptableObject
{
    public string Name;
    public Sprite Sprite = default;
    public Group8.TrashDash.TrashBin.TrashBinTypes TrashBinType = default;
}