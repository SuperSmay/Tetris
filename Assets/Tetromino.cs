using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Tetromino : MonoBehaviour
{
    public GameObject Block;
    public GameController GameController;

    private GameObject[,] Blocks;

    public Vector3 position;

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
        else if (Input.GetKeyDown(KeyCode.W))
        {
            Rotate(Rotation.CW);
        }
    }

    private void InitBlocks(TetrominoType type)
    {
        position = new Vector3(4, 19);
        if (type == TetrominoType.Line)
        {
            Blocks = new GameObject[4, 4];
            CreateNewBlock(new Vector3(0, 2), 1, TetrominoType.Line);
            CreateNewBlock(new Vector3(1, 2), 2, TetrominoType.Line);
            CreateNewBlock(new Vector3(2, 2), 3, TetrominoType.Line);
            CreateNewBlock(new Vector3(3, 2), 4, TetrominoType.Line);
        }
        if (type == TetrominoType.LeftZZ)
        {
            Blocks = new GameObject[3, 3];
            CreateNewBlock(new Vector3(0, 1), 1, TetrominoType.LeftZZ);
            CreateNewBlock(new Vector3(1, 1), 2, TetrominoType.LeftZZ);
            CreateNewBlock(new Vector3(1, 2), 3, TetrominoType.LeftZZ);
            CreateNewBlock(new Vector3(2, 2), 4, TetrominoType.LeftZZ);
        }
        if (type == TetrominoType.RightZZ)
        {
            Blocks = new GameObject[3, 3];
            CreateNewBlock(new Vector3(0, 2), 1, TetrominoType.RightZZ);
            CreateNewBlock(new Vector3(1, 2), 2, TetrominoType.RightZZ);
            CreateNewBlock(new Vector3(1, 1), 3, TetrominoType.RightZZ);
            CreateNewBlock(new Vector3(2, 1), 4, TetrominoType.RightZZ);
        }
        if (type == TetrominoType.L)
        {
            Blocks = new GameObject[3, 3];
            CreateNewBlock(new Vector3(0, 1), 1, TetrominoType.L);
            CreateNewBlock(new Vector3(1, 1), 2, TetrominoType.L);
            CreateNewBlock(new Vector3(2, 1), 3, TetrominoType.L);
            CreateNewBlock(new Vector3(2, 2), 4, TetrominoType.L);
        }
        if (type == TetrominoType.ReverseL)
        {
            Blocks = new GameObject[3, 3];
            CreateNewBlock(new Vector3(0, 2), 1, TetrominoType.ReverseL);
            CreateNewBlock(new Vector3(0, 1), 2, TetrominoType.ReverseL);
            CreateNewBlock(new Vector3(1, 1), 3, TetrominoType.ReverseL);
            CreateNewBlock(new Vector3(2, 1), 4, TetrominoType.ReverseL);
        }
        if (type == TetrominoType.Square)
        {
            Blocks = new GameObject[2, 2];
            CreateNewBlock(new Vector3(0, 0), 1, TetrominoType.Square);
            CreateNewBlock(new Vector3(0, 1), 2, TetrominoType.Square);
            CreateNewBlock(new Vector3(1, 0), 3, TetrominoType.Square);
            CreateNewBlock(new Vector3(1, 1), 4, TetrominoType.Square);
        }
        if (type == TetrominoType.T)
        {
            Blocks = new GameObject[3, 3];
            CreateNewBlock(new Vector3(0, 1), 1, TetrominoType.T);
            CreateNewBlock(new Vector3(1, 1), 2, TetrominoType.T);
            CreateNewBlock(new Vector3(1, 2), 3, TetrominoType.T);
            CreateNewBlock(new Vector3(2, 1), 4, TetrominoType.T);
        }
    }

    private void CreateNewBlock(Vector3 position, int number, TetrominoType type)
    {
        int x = Mathf.RoundToInt(position.x);
        int y = Mathf.RoundToInt(position.y);
        GameObject block = Instantiate(Block);
        block.GetComponent<Block>().Init(x, y, this);
        Blocks[x, y] = block;
        Debug.Log($"{Blocks[x, y]}"); 
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
        int NewX = Mathf.RoundToInt(position.x) + Mathf.RoundToInt(direction.x);
        int NewY = Mathf.RoundToInt(position.y) + Mathf.RoundToInt(direction.y);
        position = new Vector3(NewX, NewY);
    }

    private void Rotate(Rotation rotation)
    {
        //List<GameObject> conflictingBlocks = FindConflictsInRotate(rotation);
        //if (conflictingBlocks.Count != 0 && direction.y < 0)
        //{
        //    DropBlocks();
        //    return;
        //}
        //if (conflictingBlocks.Count != 0 && direction.x < 0)
        //{
        //    return;
        //}
        //if (conflictingBlocks.Count != 0 && direction.x > 0)
        //{
        //    return;
        //}
        //foreach (GameObject block in Blocks)
        //{
        //    GameController.instance.Blocks[block.GetComponent<Block>().X, block.GetComponent<Block>().Y] = null;
        //}
        //foreach (GameObject block in Blocks)
        //{
        //    int NewX = block.GetComponent<Block>().X + Mathf.RoundToInt(direction.x);
        //    int NewY = block.GetComponent<Block>().Y + Mathf.RoundToInt(direction.y);
        //    block.GetComponent<Block>().SetPosition(NewX, NewY);
        //}
        Blocks = ExtensionMethods.RotateMatrix(Blocks);
    }

    private List<GameObject> FindConflictsInMove(Vector2 direction)
    {
        List<GameObject> conflictingBlocks = new List<GameObject>();
        foreach (GameObject block in Blocks)
        {
            if (block == null) { continue; }
            int NewX = block.GetComponent<Block>().X + (int)direction.x;
            int NewY = block.GetComponent<Block>().Y + (int)direction.y;
            //Debug.Log($"New: {NewX}, {NewY} Currnet: {block.GetComponent<Block>().X}, {block.GetComponent<Block>().Y}, List Coords: {ExtensionMethods.CoordinatesOf(GameController.instance.Blocks, block)}");
            if (NewY < 0 || NewX < 0 || NewX > 9 || GameController.Blocks[NewX, NewY] != null)
            {
                conflictingBlocks.Add(block);
            }
        }
        return conflictingBlocks;
    }

    //private List<GameObject> FindConflictsInRotate(Rotation rotation)
    //{
    //    List<GameObject> conflictingBlocks = new List<GameObject>();
    //    foreach (GameObject block in Blocks)
    //    {
    //        int NewX = block.GetComponent<Block>().X + (int)direction.x;
    //        int NewY = block.GetComponent<Block>().Y + (int)direction.y;
    //        //Debug.Log($"New: {NewX}, {NewY} Currnet: {block.GetComponent<Block>().X}, {block.GetComponent<Block>().Y}, List Coords: {ExtensionMethods.CoordinatesOf(GameController.instance.Blocks, block)}");
    //        if (NewY < 0 || NewX < 0 || NewX > 9 || (GameController.Blocks[NewX, NewY] != null) && !Blocks.Contains(GameController.Blocks[NewX, NewY]))
    //        {
    //            conflictingBlocks.Add(block);
    //        }
    //    }
    //    return conflictingBlocks;
    //}

    //private List<GameObject> GetRotatedTetromino(Rotation rotation)
    //{

    //}

    private void DropBlocks()
    {
        foreach (GameObject block in Blocks)
        {
            if (block == null) { continue; }
            GameController.instance.Blocks[block.GetComponent<Block>().X, block.GetComponent<Block>().Y] = block;
            block.GetComponent<Block>().SetPosition(block.GetComponent<Block>().X, block.GetComponent<Block>().Y);
            block.GetComponent<Block>().landed = true;
            block.GetComponent<Block>().tetromino = null;
        }
        Blocks = new GameObject[0, 0];
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

public enum Rotation
{
    Left = 0,
    Right = 1,
    CCW = 0,
    CW = 1
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

    public static GameObject[,] RotateMatrix(GameObject[,] matrix)
    {
        int n = matrix.GetLength(0);
        GameObject[,] ret = new GameObject[n, n];

        for (int i = 0; i < n; ++i)
        {
            for (int j = 0; j < n; ++j)
            {
                ret[i, j] = matrix[n - j - 1, i];
                if (ret[i, j] != null) ret[i, j].GetComponent<Block>().SetPosition(i, j);
            }
        }

        return ret;
    }
}
