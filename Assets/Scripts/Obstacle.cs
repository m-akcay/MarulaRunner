using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : Interactable
{
    public override void particleEffect()
    {
        StartCoroutine(playParticlesForXSecs(1f));
    }
    private IEnumerator playParticlesForXSecs(float x)
    {
        ps.Play();
        yield return new WaitForSecondsRealtime(x);
        ps.Stop();
    }
    public Vector3 droppedCubePosition(Vector3 towerMovementDirection)
    {
        return transform.position - towerMovementDirection * transform.localScale.z;
    }
}
