using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tetromino : MonoBehaviour
{
    public GameObject Block;
    public GameController GameController;

    private List<Transform> Blocks = new List<Transform>();

    // Start is called before the first frame update
    void Start()
    {
        InitBlocks(TetrominoType.Line);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Move(Vector2.left);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            Move(Vector2.right);
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            Move(Vector2.down);
        }
    }

    private void InitBlocks(TetrominoType type)
    {
        if (type == TetrominoType.Line)
        {
            CreateNewBlock(new Vector3(-1.5f, 9.5f));
            CreateNewBlock(new Vector3(-.5f, 9.5f));
            CreateNewBlock(new Vector3(.5f, 9.5f));
            CreateNewBlock(new Vector3(1.5f, 9.5f));
        }
        if (type == TetrominoType.LeftZZ)
        {
            CreateNewBlock(new Vector3(-.5f, 9.5f));
            CreateNewBlock(new Vector3(.5f, 9.5f));
            CreateNewBlock(new Vector3(.5f, 10.5f));
            CreateNewBlock(new Vector3(1.5f, 10.5f));
        }
        if (type == TetrominoType.RightZZ)
        {
            CreateNewBlock(new Vector3(1.5f, 9.5f));
            CreateNewBlock(new Vector3(.5f, 9.5f));
            CreateNewBlock(new Vector3(.5f, 10.5f));
            CreateNewBlock(new Vector3(-.5f, 10.5f));
        }
        if (type == TetrominoType.L)
        {
            CreateNewBlock(new Vector3(-.5f, 11.5f));
            CreateNewBlock(new Vector3(-.5f, 10.5f));
            CreateNewBlock(new Vector3(-.5f, 9.5f));
            CreateNewBlock(new Vector3(.5f, 9.5f));
        }
        if (type == TetrominoType.ReverseL)
        {
            CreateNewBlock(new Vector3(.5f, 11.5f));
            CreateNewBlock(new Vector3(.5f, 10.5f));
            CreateNewBlock(new Vector3(.5f, 9.5f));
            CreateNewBlock(new Vector3(-.5f, 9.5f));
        }
        if (type == TetrominoType.Square)
        {
            CreateNewBlock(new Vector3(-.5f, 9.5f));
            CreateNewBlock(new Vector3(-.5f, 10.5f));
            CreateNewBlock(new Vector3(.5f, 9.5f));
            CreateNewBlock(new Vector3(.5f, 10.5f));
        }
        if (type == TetrominoType.T)
        {
            CreateNewBlock(new Vector3(1.5f, 9.5f));
            CreateNewBlock(new Vector3(.5f, 9.5f));
            CreateNewBlock(new Vector3(-.5f, 9.5f));
            CreateNewBlock(new Vector3(.5f, 10.5f));
        }
    }

    private void CreateNewBlock(Vector3 position)
    {
        GameObject NewBlock = Instantiate(Block);
        NewBlock.transform.position = position;
        NewBlock.GetComponent<Block>().Tetromino = this;
        Blocks.Add(NewBlock.transform);
    }

    private void FixedUpdate()
    {
        Move(Vector2.down);
    }

    private void Move(Vector2 direction)
    {
        foreach (Transform transform in Blocks)
        {
            transform.position = new Vector3(transform.position.x + direction.x, transform.position.y + direction.y);
        }
    }

    private void DropBlocks()
    {
        foreach (Transform transform in Blocks)
        {
            foreach (Transform t in transform)
            {
                t.gameObject.tag = "Block";
                t.gameObject.SetActive(true);
                if (t.gameObject.name == "Whole")
                {
                    t.gameObject.SetActive(false);
                }
            }
            transform.gameObject.tag = "Block";
            GameController.Blocks.Add(transform);
            
        }
        Blocks.Clear();
    }

    public void BlockTouch(Collider2D collision)
    {
        if (collision.tag == "Bottom")
        {
            DropBlocks();
            InitBlocks((TetrominoType)Mathf.RoundToInt(Random.Range(0, 6)));
        }
        else if (collision.tag == "Block" && collision.name == "Top")
        {
            DropBlocks();
            InitBlocks((TetrominoType)Mathf.RoundToInt(Random.Range(0, 6)));
        }
    }
}
public enum TetrominoType {
    Line = 0,
    LeftZZ = 1,
    RightZZ = 2,
    T = 3,
    Square = 4,
    L = 5,
    ReverseL = 6
}
