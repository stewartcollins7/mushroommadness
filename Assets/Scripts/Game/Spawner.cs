using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {

    public GameObject pantherCapMushroom;
    public GameObject buttonMushroom;
    public GameObject deathCapMushroom;
    public GameObject leaf;
    public GameObject grass;
    public Transform spawnPoint1;
    public Transform spawnPoint2;

    private float spawnWidth = 30;
    private float spawnLength = 30;
    private int spawnFrequency = 30;

    void Awake()
    {
        CurrentLevelInfo levelInfo = FindObjectOfType<CurrentLevelInfo>();
        Vector3 mushroomRatio = levelInfo.mushroomRatio;
        SpawnMushrooms(spawnPoint1.position, spawnFrequency, mushroomRatio);
        SpawnLeaves(spawnPoint1.position, spawnFrequency);
        SpawnGrass(spawnPoint1.position, spawnFrequency*2);
        SpawnMushrooms(spawnPoint2.position, spawnFrequency, mushroomRatio);
        SpawnLeaves(spawnPoint2.position, spawnFrequency);
        SpawnGrass(spawnPoint2.position, spawnFrequency * 2);
        
    }
    public void SpawnMushrooms(Vector3 spawnPosition, int spawnFrequency, Vector3 mushroomRatio)
    {
        
        float ratioTotal = mushroomRatio.x + mushroomRatio.y + mushroomRatio.z;
        float buttonSpawn = (mushroomRatio.x/ratioTotal)*spawnFrequency;
        float deathCapSpawn = (mushroomRatio.y / ratioTotal) * spawnFrequency;
        float pantherCapSpawn = (mushroomRatio.z / ratioTotal) * spawnFrequency;
        SpawnMushroomType(spawnPosition, buttonSpawn, buttonMushroom);
        SpawnMushroomType(spawnPosition, deathCapSpawn, deathCapMushroom);
        SpawnMushroomType(spawnPosition, pantherCapSpawn, pantherCapMushroom);

    }

    private void SpawnMushroomType(Vector3 spawnPosition, float spawnFrequency, GameObject mushroomPrefab)
    {
        Vector3 position;
        for (int i = 0; i < spawnFrequency; i++)
        {
            position = spawnPosition;
            position.x += Random.Range(-spawnWidth, spawnWidth);
            position.z += Random.Range(-spawnLength, spawnLength);
            position.y += 2;
            if (!Physics.CheckSphere(position, 1))
            {
                position.y -= 2;
                Instantiate(mushroomPrefab, position, Quaternion.AngleAxis(90, Vector3.left));
                if (Random.Range(0, 9) < 5f)
                {
                    position.y += 1.0f;
                    position.x += 0.25f;
                    Instantiate(leaf, position, Quaternion.Euler(-66, -1031, 900));
                }
                else
                {
                    position.x += 0.5f;
                    position.z -= 0.5f;
                    Instantiate(grass, position, Quaternion.AngleAxis(-75, Vector3.up));
                    position.x -= 1f;
                    Instantiate(grass, position, Quaternion.AngleAxis(-75, Vector3.up));
                }
            }
        }
    }
    public void SpawnLeaves(Vector3 spawnPosition, int spawnFrequency)
    {
        Vector3 position;
        for (int i = 0; i < spawnFrequency * 2; i++)
        {
            position = spawnPosition;
            position.x += Random.Range(-spawnWidth, spawnWidth);
            position.z += Random.Range(-spawnLength, spawnLength);
            position.y += 2;
            if (!Physics.CheckSphere(position, 1))
            {
                position.y -= 1.0f;
                Instantiate(leaf, position, Quaternion.Euler(-66, -1031, 900));

            }
            //Instantiate(mushroom, position, Quaternion.AngleAxis(90, Vector3.left));
        }
    }

    public void SpawnGrass(Vector3 spawnPosition, int spawnFrequency)
    {
        Vector3 position;
        for (int i = 0; i < spawnFrequency * 2; i++)
        {
            position = spawnPosition;
            position.x += Random.Range(-spawnWidth, spawnWidth);
            position.z += Random.Range(-spawnLength, spawnLength);
            position.y += 2;
            if (!Physics.CheckSphere(position, 1))
            {
                position.y -= 2;
                position.x += 0.5f;
                position.z -= 0.5f;
                Instantiate(grass, position, Quaternion.AngleAxis(-75, Vector3.up));
                position.x -= 1f;
                Instantiate(grass, position, Quaternion.AngleAxis(-75, Vector3.up));

            }

        }
    }
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
