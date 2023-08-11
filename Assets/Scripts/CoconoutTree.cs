using UnityEngine;

public class CoconoutTree : BiomeManager {
    



    protected override LayerMask GetBiomeLayer()
    {
        return coconutLayer = LayerMask.NameToLayer("Sand");    
    }
    private void OnDestroy()
    {
        RemoveOccupiedPosition(transform.position);
    }

}