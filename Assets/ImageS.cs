using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageS : MonoBehaviour
{
    public static Dictionary<int,Sprite> spritesLibrary=new Dictionary<int, Sprite>();
    // Start is called before the first frame update
    void Start()
    {
        //存sprites入字典
        Sprite[] sprites= new Sprite[14];
        sprites= Resources.LoadAll<Sprite>("2048Atlas");
        for (int i = 0; i < sprites.Length; i++)
        {
            spritesLibrary.Add(int.Parse(sprites[i].name), sprites[i]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
