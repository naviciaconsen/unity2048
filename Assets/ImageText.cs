using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageText : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (this.GetComponent<RectTransform>().localScale != Vector3.one)
        {
            /*this.GetComponent<RectTransform>().localScale = Vector3.Lerp(this.GetComponent<RectTransform>().localScale, Vector3.one, 0.1f);*///出数动画
            this.GetComponent<RectTransform>().localScale = Vector3.MoveTowards(this.GetComponent<RectTransform>().localScale, Vector3.one, 0.4f);
        }
    }

    public void NumToSprites(int num)
    {              
        this.GetComponent<Image>().sprite = ImageS.spritesLibrary[num];
        this.gameObject.name = "" + num;
    }
}
