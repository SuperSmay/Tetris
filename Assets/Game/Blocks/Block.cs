using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public Tetromino Tetromino;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (tag == "FallingBlock")
        {
            Tetromino.BlockTouch(collision);
        }
        else
        {
           Tetromino = null;
        }
    }
}
