using UnityEngine;

public class Enemy52 : MonoBehaviour
{
    [SerializeField] protected int HP=10;
    [SerializeField] protected int Damage=1;

    void Start()
    {
        // ”станавливаем здоровье из глобального класса EnemyStats
        HP = 10;
    }

    public void TakeDamage(int damageAmount)
    {
        HP -= damageAmount;
        Debug.Log("Pop");

        if (HP <= 0)
            Destroy(gameObject);
        else
            Debug.Log("Ranil");
    }

    public int GetDamage()
    {
        return Damage;
    }
}
