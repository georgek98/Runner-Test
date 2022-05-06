using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeCollider : MonoBehaviour
{
    void OnTriggerEnter(Collider hit)
    {
        if (hit.gameObject.tag == Constants.PlayerTag)
        {
            Debug.Log("collider IN");
            GameManager.Instance.CanSwipe = true;
        }
    }

    void OnTriggerExit(Collider hit)
    {
        if (hit.gameObject.tag == Constants.PlayerTag)
        {
            Debug.Log("collider out");
            GameManager.Instance.CanSwipe = false;
        }
    }
}
