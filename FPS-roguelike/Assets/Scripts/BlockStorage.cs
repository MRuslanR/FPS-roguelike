
using System.Collections.Generic;
using UnityEngine;

public class BlockStorage : MonoBehaviour
{
    private List<GameObject> blocks = new List<GameObject>();
    public GameObject[] currentBlocks; // Массив блоков
    private GameObject currentBlock;
    private Transform exit; // Ссылка на компонент Exit
    private int lastInd = -1;

    private void Start()
    {
        GenerateBlock();
    }

    public void GenerateBlock()
    {
        int randomIndex;
        do {
            randomIndex = Random.Range(0, currentBlocks.Length);
        }
        while (randomIndex == lastInd);
        lastInd = randomIndex;

        if (blocks.Count != 0){
            exit = GameObject.FindWithTag("Exit").transform; 
            currentBlock = Instantiate(currentBlocks[randomIndex], exit.position, Quaternion.identity); // Обратите внимание на сохранение позиции
            // Отключение старого exit, чтобы сохранить его функциональность
            exit.gameObject.SetActive(false);
        }
        else{        
            currentBlock = Instantiate(currentBlocks[randomIndex], transform.position, Quaternion.identity);
        }
        blocks.Add(currentBlock);
    }

    // Метод для удаления самого старого блока
    public void RemoveOldestBlock()
    {
        if (blocks.Count > 2)
        {
            GameObject oldestBlock = blocks[0];
            blocks.RemoveAt(0);

            // Удаление блока из сцены
            Destroy(oldestBlock);
        }
    }
}

