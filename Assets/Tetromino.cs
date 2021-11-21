using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Tetromino : MonoBehaviour
{
    public GameObject Block;
    public GameController GameController;

    private List<GameObject> Blocks = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        InitBlocks(TetrominoType.T);
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
            CreateNewBlock(new Vector3(3, 20));
            CreateNewBlock(new Vector3(4, 20));
            CreateNewBlock(new Vector3(5, 20));
            CreateNewBlock(new Vector3(6, 20));
        }
        if (type == TetrominoType.LeftZZ)
        {
            CreateNewBlock(new Vector3(4, 20));
            CreateNewBlock(new Vector3(5, 20));
            CreateNewBlock(new Vector3(5, 21));
            CreateNewBlock(new Vector3(6, 21));
        }
        if (type == TetrominoType.RightZZ)
        {
            CreateNewBlock(new Vector3(6, 20));
            CreateNewBlock(new Vector3(5, 20));
            CreateNewBlock(new Vector3(5, 21));
            CreateNewBlock(new Vector3(4, 21));
        }
        if (type == TetrominoType.L)
        {
            CreateNewBlock(new Vector3(4, 22));
            CreateNewBlock(new Vector3(4, 21));
            CreateNewBlock(new Vector3(4, 20));
            CreateNewBlock(new Vector3(5, 20));
        }
        if (type == TetrominoType.ReverseL)
        {
            CreateNewBlock(new Vector3(5, 22));
            CreateNewBlock(new Vector3(5, 21));
            CreateNewBlock(new Vector3(5, 20));
            CreateNewBlock(new Vector3(4, 20));
        }
        if (type == TetrominoType.Square)
        {
            CreateNewBlock(new Vector3(4, 20));
            CreateNewBlock(new Vector3(4, 21));
            CreateNewBlock(new Vector3(5, 20));
            CreateNewBlock(new Vector3(5, 21f));
        }
        if (type == TetrominoType.T)
        {
            CreateNewBlock(new Vector3(6, 20));
            //CreateNewBlock(new Vector3(5, 20));
            //CreateNewBlock(new Vector3(4, 20));
            //CreateNewBlock(new Vector3(5, 21));
        }
    }

    private void CreateNewBlock(Vector3 position)
    {
        int x = Mathf.RoundToInt(position.x);
        int y = Mathf.RoundToInt(position.y);
        GameObject block = Instantiate(Block);
        block.GetComponent<Block>().Init(x, y);
        Blocks.Add(block);
    }

    private void FixedUpdate()
    {
        Move(Vector2.down);
    }

    private void Move(Vector2 direction)
    {
        List<GameObject> conflictingBlocks = FindConflictsInMove(direction);
        if (conflictingBlocks.Count != 0 && direction.y < 0)
        {
            DropBlocks();
            return;
        }
        if (conflictingBlocks.Count != 0 && direction.x < 0)
        {
            return;
        }
        if (conflictingBlocks.Count != 0 && direction.x > 0)
        {
            return;
        }
        foreach (GameObject block in Blocks)
        {
            int NewX = block.GetComponent<Block>().X + Mathf.RoundToInt(direction.x);
            int NewY = block.GetComponent<Block>().Y + Mathf.RoundToInt(direction.y);
            block.GetComponent<Block>().X = NewX;
            block.GetComponent<Block>().Y = NewY;
        }
    }

    private List<GameObject> FindConflictsInMove(Vector2 direction)
    {
        List<GameObject> conflictingBlocks = new List<GameObject>();
        foreach (GameObject block in Blocks)
        {
            int NewX = block.GetComponent<Block>().X + (int)direction.x;
            int NewY = block.GetComponent<Block>().Y + (int)direction.y;
            Debug.Log($"New: {NewX}, {NewY} Currnet: {block.GetComponent<Block>().X}, {block.GetComponent<Block>().Y}, List Coords: {ExtensionMethods.CoordinatesOf(GameController.instance.Blocks, block)}");
            if (NewY < 0 || NewX < 0 || NewX > 9 || (GameController.Blocks[NewX, NewY] != null) && !Blocks.Contains(GameController.Blocks[NewX, NewY]))
            {
                conflictingBlocks.Add(block);
            }
        }
        return conflictingBlocks;
    }

    private void DropBlocks()
    {
        Blocks.Clear();
        InitBlocks((TetrominoType)Mathf.RoundToInt(UnityEngine.Random.Range(0, 6)));
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


public static class ExtensionMethods
{
    public static Tuple<int, int> CoordinatesOf(GameObject[,] matrix, GameObject value)
    {
        int w = matrix.GetLength(0); // width
        int h = matrix.GetLength(1); // height

        for (int x = 0; x < w; ++x)
        {
            for (int y = 0; y < h; ++y)
            {
                if (matrix[x, y] == value)
                    return Tuple.Create(x, y);
            }
        }

        return Tuple.Create(-1, -1);
    }
}
