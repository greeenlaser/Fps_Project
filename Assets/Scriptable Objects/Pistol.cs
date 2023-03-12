using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(filename = "New Gun", menuName = "Gun")]
public class Gun : ScriptableObject
{
    public int ammo;

    public float shootDelay;

    public int damage;
}
