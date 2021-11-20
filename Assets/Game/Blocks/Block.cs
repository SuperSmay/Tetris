using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block
{
    public int X
    {
        get {return x;}
        set { x = Mathf.Clamp(value, 0, 9); }
    }
    private int x;
    public int Y
    {
        get { return y; }
        set { y = Mathf.Clamp(value, 0, 24); }
    }
    private int y;
    public Block(int x, int y)
    {
        X = x;
        Debug.Log(this.x);

        Y = y;
    }

    public Vector3 GetVector3()
    {
        float NewX = (x - 4.5f);
        float NewY = (y - 9.5f);
        Debug.Log(NewX);
        return new Vector3(NewX, NewY);
    }
}
