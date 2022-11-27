using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PowerUpSO : ScriptableObject
{
    public new string name;
    public Sprite Sprite;
    public Mesh Mesh;
    public Color Color;
    public Material[] Materials;
    public float duration;

    public virtual void Initialize(GameObject go) { }
    public abstract void Use();
}
