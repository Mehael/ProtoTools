using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class SortingLayerExposer : MonoBehaviour
{
    [SerializeField] private string SortingLayerName = "Default";
    [SerializeField] private int SortingOrder = 0;
 
    public void OnValidate() {
        Apply();
    }
 
    public void OnEnable() {
        Apply();
    }
 
    private void Apply() {
        var meshRenderer = gameObject.GetComponent<MeshRenderer>();
        meshRenderer.sortingLayerName = SortingLayerName;
        meshRenderer.sortingOrder = SortingOrder;
    }
}
