using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebafDataHolder : MonoBehaviour
{
    [SerializeField] private DebafPartData[] _debafs;

    public DebafPartData GetDebaf()
    {
        var debaf = _debafs[Random.Range(0, _debafs.Length)];
        return debaf;
    }
}
