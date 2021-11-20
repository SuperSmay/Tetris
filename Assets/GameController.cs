using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject BlockObject;
    public Block[,] Blocks = new Block[10, 25];
    private List<GameObject> BlockGameObjects = new List<GameObject>();

    public Block Block;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateScreen();
    }

    private void UpdateScreen()
    {
        foreach (GameObject block in BlockGameObjects)
        {
            Destroy(block);
        }
        BlockGameObjects.Clear();
        foreach (Block block in Blocks)
        {
            if (block != null)
            {
                GameObject NewObject = Instantiate(BlockObject);
                NewObject.transform.position = block.GetVector3();
                BlockGameObjects.Add(NewObject);
            }
            
        }
    }
}
