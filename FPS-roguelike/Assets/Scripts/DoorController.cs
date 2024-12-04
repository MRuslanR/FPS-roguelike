using UnityEngine;

public class DoorController : MonoBehaviour
{
    public float rotationSpeed = 2.0f; // Скорость вращения
    public float viewAngle = 60.0f; 
    public bool isOpen = false; // Состояние двери
    private bool playerInside = false; // Игрок внутри двери
    private Quaternion closedRotation; // Закрытое положение
    private Quaternion openRotation; // Открытое положение

    private AudioSource audioSource; // Компонент AudioSource
    public AudioClip openSound; // Звук открытия двери
    public AudioClip closeSound; // Звук закрытия двери

    public AudioClip closedSound; // Звук запертой двери


    private BlockStorage Manager;
    private bool isCreated = false;

    private void Start()
    {
        closedRotation = transform.rotation; // Сохраняем начальное положение двери
        openRotation = Quaternion.Euler(transform.eulerAngles + new Vector3(0, -90, 0)); // Определяем открытое положение
        audioSource = GetComponent<AudioSource>(); // Получаем компонент AudioSource
        Manager = FindObjectOfType<BlockStorage>();
        isOpen = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) // Проверяем нажатие клавиши E и что игрок не внутри двери
        {
            GameObject player = GameObject.FindWithTag("Player");
            float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
            if (IsPlayerLookingAtDoor(player) && distanceToPlayer <= 3.0f ){
                if (!playerInside) // Проверяем расстояние и состояние двери
                {
                    ToggleDoor();
                }
                else
                {
                    audioSource.PlayOneShot(closedSound); // Звук запертой двери
                }
            }
        }

        // Плавно поворачиваем дверь
        if (isOpen)
        {
            if (!isCreated){
                isCreated = true;
                Manager.GenerateBlock();
            }
            transform.rotation = Quaternion.Slerp(transform.rotation, openRotation, Time.deltaTime * rotationSpeed);
        }
        else
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, closedRotation, Time.deltaTime * rotationSpeed);
        }
    }

    private bool IsPlayerLookingAtDoor(GameObject player)
    {
        // Получаем направление взгляда игрока
        Vector3 playerDirection = player.transform.forward;
        Vector3 directionToDoor = (transform.position - player.transform.position).normalized;

        // Рассчитываем угол между направлениями
        float angle = Vector3.Angle(playerDirection, directionToDoor);

        // Проверяем, что угол меньше указанного
        return angle < viewAngle;
    }



    private void ToggleDoor()
    {
        isOpen = !isOpen; // Меняем состояние двери

        if (isOpen)
        {
            audioSource.PlayOneShot(openSound); // Воспроизводим звук открытия двери
        }
        else
        {
            audioSource.PlayOneShot(closeSound); // Воспроизводим звук закрытия двери
        }
    }

    public void Close()
    {
        Manager.RemoveOldestBlock();
        playerInside = true; // Игрок зашёл за дверь
        isOpen = false; // Закрыть дверь при входе
        audioSource.PlayOneShot(closeSound);
    }
}
