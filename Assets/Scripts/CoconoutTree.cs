using UnityEngine;

public class CoconoutTree : BiomeMeneger {
    


protected override int GetMaxSpawns()
    {
        return MAX_COCONUTTREE;
    }

    protected override LayerMask GetBiomeLayer()
    {
        return coconutLayer;
    }
    private void OnDestroy()
    {
        RemoveOccupiedPosition(transform.position);
    }

}