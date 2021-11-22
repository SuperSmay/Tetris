using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public int number;
    public Tetromino tetromino;
    public bool landed = false;
    public int X
    {
        get {
            if (!landed) return Mathf.RoundToInt(tetromino.position.x) + x;
            else return x;
        }
    }
    private int x;
    public int Y
    {
        get {
            if (!landed) return Mathf.RoundToInt(tetromino.position.y) + y;
            else return y;
        }
    }
    private int y;

    public static bool ObjectInArray(GameObject[,] array, GameObject value)
    {
        int w = array.GetLength(0); // width
        int h = array.GetLength(1); // height

        for (int x = 0; x < w; ++x)
        {
            for (int y = 0; y < h; ++y)
            {
                if (array[x, y] == value)
                    return true;
            }
        }

        return false;
    }
    public void Init(int x, int y, Tetromino tetromino)
    {
        this.x = x;
        this.y = y;
        this.tetromino = tetromino;
    }

    public void SetPosition(int x, int y)
    {
        //if (GameController.instance.Blocks[this.x, this.y] != null && GameController.instance.Blocks[this.x, this.y] == this)
        //{
        //    GameController.instance.Blocks[x, y] = null;
        //}
        this.x = x;
        this.y = y;
        //GameController.instance.Blocks[x, y] = gameObject;
    }

    public Vector3 GetVector3()
    {
        float ObjectX = (X - 4.5f);
        float ObjectY = (Y - 9.5f);
        return new Vector3(ObjectX, ObjectY);
    }

    void Update()
    {
        transform.position = GetVector3();
        if (!ObjectInArray(GameController.instance.Blocks, gameObject))
        {
            gameObject.GetComponent<SpriteRenderer>().color = Color.blue;
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().color = Color.red; 
        }
    }
}
