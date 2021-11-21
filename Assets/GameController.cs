using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController instance;

    public GameObject BlockObject;
    public GameObject[,] Blocks = new GameObject[10, 25];
    private List<GameObject> BlockGameObjects = new List<GameObject>();


    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
