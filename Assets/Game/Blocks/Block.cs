using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public int X
    {
        get {return x;}
        set { 
            
            GameController.instance.Blocks[x, y] = null;
            x = Mathf.Clamp(value, 0, 9);
            GameController.instance.Blocks[x, y] = gameObject;
            transform.position = GetVector3();
        }
    }
    private int x;
    public int Y
    {
        get {return y;}
        set {
            
            GameController.instance.Blocks[x, y] = null;
            y = Mathf.Clamp(value, 0, 24);
            GameController.instance.Blocks[x, y] = gameObject;
            transform.position = GetVector3();
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
    public void Init(int x, int y)
    {
        this.x = x;
        this.y = y;
        GameController.instance.Blocks[x, y] = gameObject;
        gameObject.SetActive(true);
    }

    public Vector3 GetVector3()
    {
        float NewX = (x - 4.5f);
        float NewY = (y - 9.5f);
        return new Vector3(NewX, NewY);
    }
}
