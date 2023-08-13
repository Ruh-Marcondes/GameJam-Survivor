using UnityEngine;
using System.Collections.Generic;

public class BiomeManager : MonoBehaviour
{
    // Contantes de qtd de cada coisa
    private const int MAX_COCONUTTREE = 3;
    private const int MAX_BUSH = 50;

    //Objetos que serão spawnados
    public GameObject thisPrefeb;

    //Camera e distancias
    private Camera mainCamera;
    private GameObject player;
    private float spawnBuffer = 1.0f;
    /*
    O spawnBuffer é usado para garantir que os objetos não 
    sejam gerados muito próximos uns dos outros ou dos limites da área de jogo visível.
    */
    private float minDistanceFromPlayer = 2.0f;
    private float minDistanceFromAnother = 2.0f;

    //Layers
    public LayerMask layerMaskthis;

    //Outros
    private float count;

    private List<Vector3> occupiedPositions = new List<Vector3>();

    private void Start()
    {
        mainCamera = Camera.main;
        player = GameObject.FindWithTag("Player");
        count = 0;
        /*
        O metodo Start atribui a val main camera a camera
        acha o Player
        define o valor de count;
        */
    }

    private void FixedUpdate()
    {
        /*
        De tempos em tempos será chamado  a função timer e os vetores atualizão para a posição da camera. Além do if auto esplixativo ali
        */
        timer();

        Vector2 cameraMin = mainCamera.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 cameraMax = mainCamera.ViewportToWorldPoint(new Vector2(1, 1));

        CheckForSpawn(cameraMin, cameraMax, thisPrefeb);
  
    }

    private void CheckForSpawn(Vector2 min, Vector2 max, GameObject prefab)
    {
        Vector3 spawnPosition = CalculateSpawnPosition(min, max, prefab); // varivel vector3 que recebe o calculo so
        if (spawnPosition != Vector3.zero
        && IsObjectInBiomeLayer(prefab)
        && count >= 5)
        { //If verifica se vector3 não é zero se está no bioma correto e se count é maior que o tempinho que precisa
            SpawnObject(prefab, spawnPosition); // chama o spawnObject passa 2 parametros.
        }
    }


    private Vector3 CalculateSpawnPosition(Vector2 min, Vector2 max, GameObject prefab)
    {
        Vector3 calculatedSpawnPosition = Vector3.zero; // zera a possição.

        for (int i = 0; i < 100; i++)
        {
            calculatedSpawnPosition = new Vector3(
                Random.Range(min.x - spawnBuffer, max.x + spawnBuffer), //1. `Random.Range(min.x - spawnBuffer, max.x + spawnBuffer)`: Isso gera um valor aleatório entre `min.x - spawnBuffer` (o limite esquerdo do retângulo com uma margem) e `max.x + spawnBuffer` (o limite direito do retângulo com uma margem). Isso determina a coordenada X da posição de spawn.
                Random.Range(min.y - spawnBuffer, max.y + spawnBuffer), //2. `Random.Range(min.y - spawnBuffer, max.y + spawnBuffer)`: Isso gera um valor aleatório entre `min.y - spawnBuffer` (o limite inferior do retângulo com uma margem) e `max.y + spawnBuffer` (o limite superior do retângulo com uma margem). Isso determina a coordenada Y da posição de spawn.
                                                                        //3. `0f`: Essa é a coordenada Z da posição de spawn. No Unity, o eixo Z é usado para determinar a profundidade em um espaço tridimensional. Definir como 0f significa que o objeto será colocado no plano XY, sem profundidade.
                0f
            );

            if (!IsPositionOccupied(calculatedSpawnPosition)  // Verifica   se não está ocupado passa a posição sorteada
                && IsPositionFarEnough(calculatedSpawnPosition, minDistanceFromPlayer) &&//Verifica se está longe o suficiente do Player
                IsPositionFarEnoughFromOther(calculatedSpawnPosition, minDistanceFromAnother))//Verifica se está longe o sulficiente de outros objetos
            {
                return calculatedSpawnPosition;
            }
        }

        return Vector3.zero;
    }


    private bool IsPositionFarEnoughFromOther(Vector3 position, float minDistance)
    {
        foreach (Vector3 occupiedPosition in occupiedPositions)
        {
            if (Vector3.Distance(position, occupiedPosition) < minDistance)
            {
                return false;
            }
        }
        return true;
    }
    protected virtual LayerMask GetBiomeLayer()
    {
        return LayerMask.NameToLayer("Default"); // Ou o valor que fizer sentido para a classe base
    }


    private bool IsPositionFarEnough(Vector3 position, float minDistance)
    {
        if (Vector3.Distance(position, player.transform.position) < minDistance)
        {
            return false;
        }

        foreach (Vector3 occupiedPosition in occupiedPositions)
        {
            if (Vector3.Distance(position, occupiedPosition) < minDistance)
            {
                return false;
            }
        }
        return true;
    }

    private void SpawnObject(GameObject prefab, Vector3 position)
    {
        Instantiate(prefab, position, Quaternion.identity);
        occupiedPositions.Add(position);
    }

    private void timer()
    {
        count += Time.deltaTime;
        //Debug.Log(count);
    }

    protected bool IsObjectInBiomeLayer(GameObject obj)
    {
        if (obj.layer == layerMaskthis.value)
        {
            return true;
        }
        return false;
    }
    protected void RemoveOccupiedPosition(Vector3 position)
    {
        if (occupiedPositions.Contains(position))
        {
            occupiedPositions.Remove(position);
        }
    }
    private bool IsPositionOccupied(Vector3 position) // Verifica se a posição sorteada está nessa posição
    {
        foreach (Vector3 occupiedPosition in occupiedPositions)
        {
            if (Vector3.Distance(position, occupiedPosition) < minDistanceFromAnother)
            {
                return true;
            }
        }
        return false;
    }


}
