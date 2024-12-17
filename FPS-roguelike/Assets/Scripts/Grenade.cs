using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine.Events;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    public float explosionRadius = 5f;
    public float explosionForce = 700f;
    public LayerMask enemyMask;
    public LayerMask obstacleLayer;
    public UnityEvent onExplode;
    public UnityEvent<GameObject> onEnemyHit;
    public float destroyDelay = 2f; // ��������� ��� �������� ����������� �������

    public void Explode()
    {
        onExplode.Invoke();
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, explosionRadius, enemyMask);
        List<GameObject> hitEnemies = new List<GameObject>(); // ��� ��������� ���������� ��������� �����

        foreach (var hitCollider in hitColliders)
        {
            if (hitEnemies.Contains(hitCollider.gameObject))
            {
                continue; // �������� �� ��, ����� �� ���� 2 ����
            }
            hitEnemies.Add(hitCollider.gameObject);

            Vector3 directionToEnemy = (hitCollider.transform.position - transform.position).normalized;
            RaycastHit hit;

            if (Physics.Raycast(transform.position, directionToEnemy, out hit, explosionRadius, obstacleLayer))
            {
                Debug.Log("����������� ������ :(");
                continue;
            }

            Debug.Log("Bpar �������: " + hitCollider.gameObject.name);
            onEnemyHit.Invoke(hitCollider.gameObject);

            Rigidbody enemyRigidbody = hitCollider.GetComponent<Rigidbody>();
            if (enemyRigidbody != null)
            {
                enemyRigidbody.AddExplosionForce(explosionForce, transform.position, explosionRadius);
                Destroy(hitCollider.gameObject);
            }
        }
        Destroy(gameObject, destroyDelay); // ����������� ������� ����� ��������� ���� �����
    }
}