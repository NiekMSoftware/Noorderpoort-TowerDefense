using UnityEngine;

public class EnemyLooking : MonoBehaviour
{
    //Self explanatory
    public Transform Base;
    public bool lookingAtBase = false;

    void Update()
    {
        if (lookingAtBase)
        {
            transform.LookAt(Base);
        }
    }
}
