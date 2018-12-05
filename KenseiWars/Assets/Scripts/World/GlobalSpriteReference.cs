using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalSpriteReference : MonoBehaviour
{
    public static GlobalSpriteReference instance = null;

    public Sprite Attack1;
    public Sprite Attack2;
    public Sprite Attack3;
    public Sprite EnemyAttack1;

    public Sprite EnemyNormal;
    public Sprite EnemyHit;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }
}
