using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Script responsible of differentiate the objects that should wrap
 */
public class WrapTrigger : MonoBehaviour
{
    public void WrapToPosition(Vector2 position)
    {
        transform.position = position;
    }
}
