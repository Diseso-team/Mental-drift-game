using System.Collections;
using System.Collections.Generic;
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

    public List<Transform> enemySpawnPoints;


}
