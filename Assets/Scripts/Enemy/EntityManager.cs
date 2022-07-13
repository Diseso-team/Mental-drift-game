using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class EntityManager : MonoBehaviour
{
    // �������� ��� ������� � ����� ������ ��� ������������ � ����������
    #region Singleton
    private void Awake()
    {
        _instance = this; // ����������� ��������� ���� �����
    }
    private static EntityManager _instance;
    public static EntityManager Instance
    {
        get
        {
            return _instance;
        }
    }
    #endregion     

    public GameObject player;

    [SerializeField]
    private List<Transform> enemySpawnPoints;
    [SerializeField]
    private List<GameObject> enemyPrefabs;
    [SerializeField]
    private Transform enemyOrigin;

    [SerializeField]
    private float spawnDelay;
    [SerializeField]
    private int maxCount;

    //[HideInInspector]
    public Vector3 lastPlayerPos;
    [SerializeField]
    private float timeToSetPos;

    private void Start()
    {
        StartCoroutine(Spawner());
        StartCoroutine(SetLastPlayerPos());
    }

    IEnumerator SetLastPlayerPos()
    {
        while (true)
        {
            lastPlayerPos = player.transform.position;
            yield return new WaitForSeconds(timeToSetPos);
        }
    }

    IEnumerator Spawner() // ������� ������ ��� � spawnDelay �� enemyPrefabs �� ����������� enemySpawnPoints � ��������� �������
    {
        while (true)
        {
            if (transform.childCount < maxCount)
            {
                Instantiate(enemyPrefabs[Random.Range(0,enemyPrefabs.Count)], enemySpawnPoints[Random.Range(0, enemySpawnPoints.Count)].position, Quaternion.Euler(0, Random.Range(0,360),0), transform);
            }
            yield return new WaitForSeconds(spawnDelay);
        }
    }
}
