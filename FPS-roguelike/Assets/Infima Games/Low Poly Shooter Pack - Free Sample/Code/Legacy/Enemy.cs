using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private GameObject player;
    private NavMeshAgent agent;
    private int hp = 5;

    public GameObject bulletPrefab; // Префаб пули
    public Transform firePoint;    // Точка выстрела
    public float fireRate = 2f;    // Частота выстрелов (каждые 2 секунды)

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        agent = GetComponent<NavMeshAgent>();

        // Запускаем корутину для стрельбы
        StartCoroutine(ShootAtPlayer());
    }

    // Update is called once per frame
    void Update()
    {
        // Следуем за игроком
        agent.SetDestination(player.transform.position);
    }

    public void Boom()
    {
        Destroy(gameObject);
    }
    public void Damage()
    {
        hp--;
        if (hp <= 0)
        {
            Destroy(gameObject);
        }
    }

    // Корутина для стрельбы
    private IEnumerator ShootAtPlayer()
    {
        while (true)
        {
            yield return new WaitForSeconds(fireRate);

            // Вызываем метод выстрела
            Shoot();
        }
    }

    private void Shoot()
    {
        if (bulletPrefab == null || firePoint == null || player == null)
        {
            Debug.LogWarning("Не настроен префаб пули или точка выстрела!");
            return;
        }

        // Создаём пулю
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);

        // Направляем пулю в сторону игрока
        Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
        if (bulletRb != null)
        {
            Vector3 direction = (player.transform.position - firePoint.position).normalized;
            bulletRb.linearVelocity = direction * 20f; // Скорость пули
        }
    }
}