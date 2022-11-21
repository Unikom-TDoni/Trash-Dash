using Group8.TrashDash.TrashBin;
using System;
using UnityEngine;

[CreateAssetMenu(fileName = "New Trash Info", menuName = "Item/Trash Info")]
public class TrashContentInfo : ScriptableObject
{
    public string Name;
    public Sprite Sprite = default;
    public TrashBinTypes TrashBinType = default;
    public Mesh Mesh;
    public Material[] Materials;
}