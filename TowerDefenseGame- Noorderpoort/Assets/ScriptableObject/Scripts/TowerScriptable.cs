using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Tower", menuName = "Tower")]
public class TowerScriptable : ScriptableObject
{
    [Header("General")]
    public bool canUpgrade;
    public bool isOffensive;
    public TowerScriptable upgradeScriptable;

    [Header("General Stats")]
    public int cost;
    public int sellValue;

    [Header("Offensive Stats")]
    public float damage;
    public float firerate;
    public float range;
    public float projectileSpeed;

    [Header("Defensive stats")]
    public int moneyPerWave;
}
