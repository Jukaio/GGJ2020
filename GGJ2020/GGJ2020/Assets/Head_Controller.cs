using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Head_Controller : MonoBehaviour
{
    Collider2D collider_;

    private void Start()
    {
        collider_ = (Collider2D)GetComponent(typeof(Collider2D));
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {

        collision.transform.parent.GetComponent<Collider2D>().enabled = false;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        StartCoroutine(Wait_For(collision, 0));
    }

    int i;
    IEnumerator Wait_For(Collider2D collision, int skip_Frames)
    {
        for (i = 0; i < skip_Frames; i++)
            yield return new WaitForEndOfFrame();
        collision.transform.parent.GetComponent<Collider2D>().enabled = true;
    }
}

