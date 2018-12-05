using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallTriggerLeft : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D _other)
    {
        if (_other.GetComponent<IPlayer>() != null)
        {
            GetComponentInParent<CameraActor>().mCanMoveLeft = false;
        }
    }

    void OnTriggerExit2D(Collider2D _other)
    {
        if (_other.GetComponent<IPlayer>() != null)
        {
            GetComponentInParent<CameraActor>().mCanMoveLeft = true;
        }
    }
}
