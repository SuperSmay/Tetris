using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tetromino : MonoBehaviour
{
    public GameObject Block;
    public GameController GameController;

    private List<Block> Blocks = new List<Block>();

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
            CreateNewBlock(new Vector3(5, 20));
            CreateNewBlock(new Vector3(4, 20));
            CreateNewBlock(new Vector3(5, 21));
        }
    }

    private void CreateNewBlock(Vector3 position)
    {
        int x = Mathf.RoundToInt(position.x);
        int y = Mathf.RoundToInt(position.y);
        Block block = new Block(x, y);
        Blocks.Add(block);
        GameController.Blocks[block.X, block.Y] = block;
    }

    private void FixedUpdate()
    {
        Move(Vector2.down);
    }

    private void Move(Vector2 direction)
    {
        List<Block> conflictingBlocks = FindConflictsInMove(direction);
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
        foreach (Block block in Blocks)
        {
            int NewX = block.X + Mathf.RoundToInt(direction.x);
            int NewY = block.Y + Mathf.RoundToInt(direction.y);
            GameController.Blocks[block.X, block.Y] = null;
            GameController.Blocks[NewX, NewY] = block;
            block.X = NewX;
            block.Y = NewY;
        }
    }

    private List<Block> FindConflictsInMove(Vector2 direction)
    {
        List<Block> conflictingBlocks = new List<Block>();
        foreach (Block block in Blocks)
        {
            int NewX = block.X + (int)direction.x;
            int NewY = block.Y + (int)direction.y;
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
        InitBlocks((TetrominoType)Mathf.RoundToInt(Random.Range(0, 6)));
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
