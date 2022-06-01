using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRangeAttack : MonoBehaviour
{
    public GameObject enemyArrow;
    public Transform FirePos;

    void EnemyFire()
    {
        Instantiate(enemyArrow, FirePos.transform.position, FirePos.transform.rotation);
    }
}
