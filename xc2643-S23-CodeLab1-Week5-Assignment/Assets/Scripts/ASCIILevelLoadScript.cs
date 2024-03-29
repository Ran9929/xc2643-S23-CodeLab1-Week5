using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ASCIILevelLoadScript : MonoBehaviour
{
    public GameObject Fruit_0;
    public GameObject Fruit_1;
    public GameObject Fruit_2;
    public GameObject Fruit_3;
    public GameObject Wall;
    
    GameObject currentPlayer;
    public GameObject level;
    
    int currentLevel = 0;

    public int CurrentLevel
    {
        get { return currentLevel; }
        set
        {
            currentLevel = value;
            LoadLevel();
        }
    }

    const string FILE_NAME = "LevelNum.txt";
    const string FILE_DIR = "/Levels/";
    string FILE_PATH;

    public float xOffset;
    public float yOffset;

    public Vector2 playerStartPos;
    
    // Start is called before the first frame update
    void Start()
    {
        FILE_PATH = Application.dataPath + FILE_DIR + FILE_NAME;

        LoadLevel();
    }

    bool LoadLevel()
    {
        /*Destroy(level);
        
        level = new GameObject("Level");*/
        
        string newPath = FILE_PATH.Replace("Num", currentLevel + "");
        
        //load all the lines of the file into an array of strings
        string[] fileLines = File.ReadAllLines(newPath);

        //for loop to go through each line
        for (int yPos = 0; yPos < fileLines.Length; yPos++)
        {
            //get each line out of the array
            string lineText = fileLines[yPos];

            //turn the current line into an array of chars
            char[] lineChars = lineText.ToCharArray();

            //loop through each char
            for (int xPos = 0; xPos < lineChars.Length; xPos++)
            {
                //get the current char
                char c = lineChars[xPos];

                //make a variable for a new GameObject
                GameObject newObj;

                switch (c)
                {
                    case '0':  //is the char a p?
                        /*playerStartPos = new Vector2(xOffset + xPos, yOffset - yPos);*/
                        newObj = Instantiate<GameObject>(Fruit_0); //make a new player
                        /*currentPlayer = newObj;*/
                        break;
                    case '1': //is the char a w?
                        newObj = Instantiate<GameObject>(Fruit_1); //make a new wall
                        break;
                    case '2': //is there a carrot?
                        newObj = Instantiate<GameObject>(Fruit_2); //make a spike
                        break;
                    case '3':
                        newObj = Instantiate<GameObject>(Fruit_3);
                        break;
                    case 'w':
                        newObj = Instantiate<GameObject>(Wall);
                        break;
                    default: //otherwise
                        newObj = null; //null
                        break;
                }

                //if we made a new GameObject
                if (newObj != null)
                {
                    //position it based on where it was
                    //in the file, using the variables
                    //we used to loop through our arrays
                    newObj.transform.position =
                        new Vector2(
                            xOffset + xPos, 
                            yOffset - yPos);

                    newObj.transform.parent = level.transform;
                }
            }
        }

        return false;
    }

    public void ResetPlayer()
    {
        currentPlayer.transform.position = playerStartPos;
    }

    public void HitDoor()
    {
        Debug.Log("Triggered a door!");
        CurrentLevel++;
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            HitDoor();
        }
    }
}
