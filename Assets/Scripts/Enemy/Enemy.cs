using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Enemy : MonoBehaviour
{
    private GameObject player;
    private NavMeshAgent agent; // �������� �������� � ������� ������������� � NavMeshAgent

    public float detectionDistance; // ���������� �� �����, �� ������� �� �������� ������, ���������� �� ��� ���� ������
    public float viewDistance; // ���������� ��� ������� ���� �������� ������, ���� ����� ����� � ���� ������
    public float viewAngle; // ��� ������ �����

    public bool drawGizmos = false; // ����� �� ���������� Gizmos

    private Coroutine GLPP;

    private void Start()
    {
        player = EntityManager.Instance.player; // ����� ������ ������ �� ��������� ������
        agent = GetComponent<NavMeshAgent>();
        
        StartCoroutine(CheckOnPlayer());
        GLPP =  StartCoroutine(GotoLastPlayerPos());
    }

    #region NavMesh

    public IEnumerator GotoLastPlayerPos()
    {
        while (true)
        {
            agent.SetDestination(transform.parent.GetComponent<EntityManager>().lastPlayerPos); // ��� � �����, ��� ����� ��� lastPlayerPos 
            yield return new WaitForSeconds(10);
        }
    }

    public IEnumerator CheckOnPlayer() // ���������, ����� �� ����� � ���� ���� �����
    {
        while (true)
        {
            float distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);

            if (distanceToPlayer <= detectionDistance || distanceToPlayer <= viewDistance && IsInView())
            {
                StartCoroutine(MoveToPlayer());
                StopCoroutine(GLPP);
                yield break; // ��������� ��������
            }
            yield return new WaitForSeconds(1f);
        }
    }
    private bool IsInView() // ��������� ���������� �� ����� � ���� ������ �����
    {
        float realAngle = Vector3.Angle(transform.forward, player.transform.position - transform.position); // forward - ��� ����� �������, ��� Z
        RaycastHit hit;
        if (Physics.Raycast(transform.position, player.transform.position - transform.position, out hit, viewDistance))
        {
            if (realAngle < viewAngle / 2f && Vector3.Distance(transform.position, player.transform.position) <= viewDistance && hit.transform == player.transform.transform)
            {
                return true;
            }
        }
        return false;
    }

    private void OnDrawGizmosSelected() //������ ����, ��� ���� �������� ������
    {
        if (!drawGizmos) return;
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, detectionDistance);

        Vector3 left = transform.position + Quaternion.Euler(new Vector3(0, viewAngle / 2, 0)) * transform.forward * viewDistance;
        Vector3 right = transform.position + Quaternion.Euler(-new Vector3(0, viewAngle / 2, 0)) * transform.forward * viewDistance;
        Debug.DrawLine(transform.position, left, Color.yellow);
        Debug.DrawLine(transform.position, right, Color.yellow);
    }

    public IEnumerator MoveToPlayer()
    {
        while (true)
        {
            agent.SetDestination(player.transform.position);
            yield return new WaitForSeconds(0.5f); // ����� ����� ��������
        }
    }
    #endregion


}
