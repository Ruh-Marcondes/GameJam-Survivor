using UnityEngine;

public class CoconoutTree : BiomeManager {
    

    

    protected override LayerMask GetBiomeLayer()
    {
        return layerMaskthis = LayerMask.NameToLayer("Sand");    
    }
    private void OnDestroy()
    {
        RemoveOccupiedPosition(transform.position);
    }

}