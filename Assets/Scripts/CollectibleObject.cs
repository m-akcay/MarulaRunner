using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleObject : Interactable
{
    public void destroy()
    {
        StartCoroutine(destroyInXSecs(3f));
    }

    private IEnumerator destroyInXSecs(float x)
    {
        GetComponent<Renderer>().enabled = false;
        yield return new WaitForSecondsRealtime(x);
        this.gameObject.SetActive(false);
    }
}
