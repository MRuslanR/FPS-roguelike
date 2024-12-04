
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
        GenerateBlock(true);
        GenerateBlock();
    }

    public void GenerateBlock(bool start = false)
    {
        int randomIndex;
        if (start){
            randomIndex = 4;
        }
        else{
            do {
                randomIndex = Random.Range(0, 4);
            }
            while (randomIndex == lastInd);
            lastInd = randomIndex;
        }
        exit = GameObject.FindWithTag("Exit").transform; 
        currentBlock = Instantiate(currentBlocks[randomIndex], exit.position, Quaternion.identity);
        exit.gameObject.SetActive(false);
        blocks.Add(currentBlock);
    }

    // Метод для удаления самого старого блока
    public void RemoveOldestBlock()
    {
        GameObject oldestBlock = blocks[0];
        blocks.RemoveAt(0);
        Destroy(oldestBlock);
    }
}
