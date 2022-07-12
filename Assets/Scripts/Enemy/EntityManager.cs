using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityManager : MonoBehaviour
{
    // Синглтон для доступа к этому классу без присваивания в инспекторе
    #region Singleton

    private void Awake()
    {
        _instance = this; // Присваиваем синглтону этот класс
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

    public List<Transform> enemySpawnPoints;


}
