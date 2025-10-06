using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WireController : MonoBehaviour
{
    [SerializeField] private SkinnedMeshRenderer _renderer;
    [SerializeField] private Transform _probe;

    private void Update()
    {
        Vector3 direction = _probe.position - transform.position;
        float segmentLength = direction.magnitude / (_renderer.bones.Length - 1);
        for (int i = 0; i < _renderer.bones.Length; i++)
        {
            _renderer.bones[i].transform.position = transform.position + direction.normalized * (segmentLength * i);
        }
    }
}
