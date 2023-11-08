using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QUESTION : MonoBehaviour
{
    GameObject paanel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    bool isOver = false;
    public void OnMouseEnter()
    {

        isOver = true;
    }

    public void OnMouseExit() 
    {

        isOver = false;
    }
}
