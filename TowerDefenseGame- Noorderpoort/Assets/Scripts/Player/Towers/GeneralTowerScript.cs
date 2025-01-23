using UnityEngine;

public class GeneralTowerScript : MonoBehaviour
{
    public bool isBeingPlaced = false;
    public TowerScriptable towerStats;

    /// <summary>
    /// Set the stats of the tower, changes based on tower type
    /// </summary>
    /// <param name="stats"></param>
    public virtual void SetStats(TowerScriptable stats)
    {

    }
}
