using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalPrefabReference : MonoBehaviour
{
    public static GlobalPrefabReference instance = null;

    public Transform basicAttackPrefab;
    public Transform fireBreathPrefab;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }
}
