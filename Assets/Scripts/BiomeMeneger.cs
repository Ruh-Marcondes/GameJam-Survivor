using UnityEngine;
using System.Collections.Generic;

public class BiomeManager : MonoBehaviour
{
    private const int MAX_COCONUTTREE = 3;
    private const int MAX_BUSH = 50;

    public GameObject coconutPrefab;
    public GameObject bushPrefab;

    private Camera mainCamera;
    private GameObject player;
    private float spawnBuffer = 1.0f;
    private float minDistanceFromPlayer = 2.0f;
    private float minDistanceFromAnother = 2.0f;

    private float count;

    private List<Vector3> occupiedPositions = new List<Vector3>();

    private void Start(){
        mainCamera = Camera.main;
        player = GameObject.FindWithTag("Player");
        count = 0;
    }

    private void FixedUpdate()
    {
        timer();
        
        Vector2 cameraMin = mainCamera.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 cameraMax = mainCamera.ViewportToWorldPoint(new Vector2(1, 1));

        if(count >= 5 ){
            CheckForSpawn(cameraMin, cameraMax, coconutPrefab);
            CheckForSpawn(cameraMin, cameraMax, bushPrefab);

            count = 0;
        }
    }

    private void CheckForSpawn(Vector2 min, Vector2 max, GameObject prefab){
       
        Vector3 spawnPosition = CalculateSpawnPosition(min, max, prefab);
        if (spawnPosition != Vector3.zero)
        {
            SpawnObject(prefab, spawnPosition);
        }
    }

    private Vector3 CalculateSpawnPosition(Vector2 min, Vector2 max, GameObject prefab){
        Vector3 spawnPosition = Vector3.zero;

        for (int i = 0; i < 100; i++){
            spawnPosition = new Vector3(
                Random.Range(min.x - spawnBuffer, max.x + spawnBuffer),
                Random.Range(min.y - spawnBuffer, max.y + spawnBuffer),
                0f
            );

            if (!IsPositionOccupied(spawnPosition) &&
                IsPositionFarEnough(spawnPosition, minDistanceFromPlayer)){
                return spawnPosition;
            }
        }

        return Vector3.zero;
    }

    private bool IsPositionOccupied(Vector3 position){
        foreach (Vector3 occupiedPosition in occupiedPositions){
            if (Vector3.Distance(position, occupiedPosition) < minDistanceFromPlayer)
            {
                return true;
            }
        }
        return false;
    }

    private bool IsPositionFarEnough(Vector3 position, float minDistance){
        if (Vector3.Distance(position, player.transform.position) < minDistance){
            return false;
        }

        foreach (Vector3 occupiedPosition in occupiedPositions){
            if (Vector3.Distance(position, occupiedPosition) < minDistance){
                return false;
            }
        }
        return true;
    }

    private void SpawnObject(GameObject prefab, Vector3 position){
        Instantiate(prefab, position, Quaternion.identity);
        occupiedPositions.Add(position);
    }

    private void timer(){
        count += Time.deltaTime;
        //Debug.Log(count);
    }
}
