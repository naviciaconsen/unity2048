using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class GameCore : MonoBehaviour
{
    int[,] cellsPoses = new int[4, 4];
    public Transform cellsParents;
    public ImageText[] Pics = new ImageText[16];

    int[] values = new int[13];//values为2048数值组
    List<Location> Emptyloactions = new List<Location>();

    public Text t1;
    public Text t2;

    struct Location
    {
        public int r, c;
        public Location(int r, int c)
        {
            this.r = r;
            this.c = c;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("Pic").OrderBy(g=>g.transform.GetSiblingIndex()).ToArray();
        //for (int i = 0; i < gameObjects.Length; i++)
        //{
        //    Pics[i] = gameObjects[i].GetComponent<ImageText>();
        //}

        Mapping();
        GameStart();
    }

    // Update is called once per frame
    void Update()
    {
        MoveUp();
        MoveDown();
        MoveLeft();
        MoveRight();
    }

    void Mapping()
    {
        int k = 0;
        for (int i = 0; i < 4; i++)
            for (int j = 0; j < 4; j++)
            {
                Pics[k].NumToSprites(cellsPoses[i, j]);
                k++;
            }
    }//更新数组

    public void GameStart()
    {
        for (int i = 0; i < 2; i++)
            NewNumber();
    }//开始时出两个数字

    void NewNumber()
    {
        if(DidntChange==false)
        {
            Emptyloactions.Clear();//Clear List

            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                {
                    if (cellsPoses[i, j] == 0)
                    {
                        Emptyloactions.Add(new Location(i, j));
                    }
                }//Add empty cell

            int Rnum = UnityEngine.Random.Range(0, Emptyloactions.Count);
            Location EmtLoc = Emptyloactions[Rnum];
            cellsPoses[EmtLoc.r, EmtLoc.c] = UnityEngine.Random.Range(1, 11) >= 3 ? 2 : 4;
            //Emptyloactions.RemoveAt(Rnum);//将值为0的cellposes下标存于list中随即后删除
            Mapping();

            Pics[EmtLoc.c + EmtLoc.r * 4].GetComponent<RectTransform>().localScale = Vector3.zero;//出数动画先归零        
        }
    }//空位置出数



    public int[] OrderArray = new int[4];
    public int[] MergeArray = new int[4];

    int[,] oldArray = new int[4, 4];

    void CompareArray(int[,] newArray)
    {
        int c = 0 ;
        for (int a = 0; a < 4; a++)
        {
            for(int b = 0; b < 4; b++)
            {
                if(oldArray[a,b] == newArray[a, b])
                {
                    c++;
                }
            }
        }
        if (c >= newArray.Length - 1)
            DidntChange = true;
        else DidntChange = false;
    }
    void Copy2DArray()
    {
        for(int a = 0;a < 4;a++)
            for(int b = 0;b < 4; b++)
            {
                oldArray[a, b] = cellsPoses[a,b];
            }
    }
    bool DidntChange = false;

    void Ordering()//给MergeArray排0至后
    {
        Array.Clear(OrderArray, 0, 4);
        int index = 0;
        for (int i = 0; i < 4; i++)
        {
            if (MergeArray[i] != 0)
            {
                OrderArray[index++] = MergeArray[i];
            }
        }
        OrderArray.CopyTo(MergeArray, 0);
    }

    void Merge()//合并
    {
        Ordering();
        for (int i = 0; i < 3; i++)
        {
            if (MergeArray[i] == MergeArray[i + 1])
            {
                if (MergeArray[i] + MergeArray[i + 1] != 0)
                {
                    MergeArray[i] += MergeArray[i + 1];
                    MergeArray[i + 1] = 0;

                    Debug.Log("merged");
                }
            }
        }
        Ordering();
    }

    void MoveUp()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Copy2DArray();

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    MergeArray[j] = cellsPoses[j, i];
                }

                Merge();
                for (int j = 0; j < 4; j++)
                {
                    cellsPoses[j, i] = MergeArray[j];
                }                             
            }
            CompareArray(cellsPoses);
            Mapping();
            NewNumber();
            scoreCounting();
            Debug.Log("MoveUp");

        }
    }

    void MoveDown()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            Copy2DArray();

            for (int i = 0; i < 4; i++)
            {
                int index = 0;
                for (int j = 3; j >= 0; j--)
                {
                    MergeArray[index++] = cellsPoses[j, i];
                }
                Merge();
                index = 0;
                for (int j = 3; j >= 0; j--)
                {
                    cellsPoses[j, i] = MergeArray[index++];
                }
            }
            CompareArray(cellsPoses);
            Mapping();
            NewNumber();
            Debug.Log("MoveDown");
            scoreCounting();
        }
    }

    void MoveLeft()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Copy2DArray();

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    MergeArray[j] = cellsPoses[i, j];
                }
                Merge();

                for (int j = 0; j < 4; j++)
                {
                    cellsPoses[i, j] = MergeArray[j];
                }
            }
            CompareArray(cellsPoses);
            Mapping();
            Debug.Log("MoveLeft");
            NewNumber();
            scoreCounting();
        }
    }

    void MoveRight()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            Copy2DArray();

            for (int i = 0; i < 4; i++)
            {
                int index = 0;
                for (int j = 3; j >= 0; j--)
                {
                    MergeArray[index++] = cellsPoses[i, j];
                }
                Merge();
                index = 0;

                for (int j = 3; j >= 0; j--)
                {
                    cellsPoses[i, j] = MergeArray[index++];
                }
            }
            CompareArray(cellsPoses);
            Mapping();
            Debug.Log("MoveRight");
            NewNumber();
            scoreCounting();
        }
    }

    //bool fullnum;
    //void FULLnum()
    //{
    //    int s = 0;
    //    for(int i = 0;i < 4;i++)
    //    {
    //        for(int j = 0;j < 4; j++)
    //        {
    //            if(cellsPoses[i, j] != 0)
    //            {
    //                s++;
    //            }
    //        }
    //    }
    //    if(s==cellsPoses.Length-1)
    //        fullnum= true;
    //    else fullnum= false;
    //}

    public void Refresh()
    {
        for (int i = 0; i < 4; i++)
            for (int j = 0; j < 4; j++)
            {
                cellsPoses[i, j] = 0;
            }
        GameStart();
    }

    public void scoreCounting()
    {
        int[] cellnums = new int[16];

        int index = 0;
        for (int i = 0; i < 4; i++)
            for (int j = 0; j < 4; j++)
            {
                cellnums[index++] = cellsPoses[i, j];
            }
        Array.Sort(cellnums);
        Array.Reverse(cellnums);

        GameManager.score = cellnums[0];
        //Debug.Log(cellnums[0]);

        int cellindex = 0;
        t1.text = "";
        t2.text = "";
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                t1.text += cellsPoses[i, j].ToString();
                t2.text += cellsParents.GetChild(cellindex).name + ": " + cellsParents.GetChild(cellindex).transform.GetChild(0).name + "\n";
                cellindex++;
            }
            t1.text += " ";
        }
    }//计分

    public void Restart()
    {
        GameManager. score = 0;
        Refresh();
    }
}