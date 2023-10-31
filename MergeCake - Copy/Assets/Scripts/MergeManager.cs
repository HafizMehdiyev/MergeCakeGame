using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MergeManager : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (gameObject.tag == collision.gameObject.tag && gameObject.tag != "CompleteCake")
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);

           TagManager.Instance.TagReturn(gameObject.tag);
        }
    }

}
