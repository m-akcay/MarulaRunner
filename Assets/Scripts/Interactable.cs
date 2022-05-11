using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    [SerializeField] protected ParticleSystem ps = null;
    [SerializeField] protected float _effect = 0.25f;
    public float effect => _effect;

    protected void OnEnable()
    {
        ps = GetComponent<ParticleSystem>();
        ps.Stop();
    }

    public virtual void particleEffect()
    {
        ps.Play();
    }

}
