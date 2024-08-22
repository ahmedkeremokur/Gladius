using System.Collections;
using System.Collections.Generic;
using Unity.Jobs.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.UI;

public class Job : MonoBehaviour
{
    public Sprite[] images;
    public Image displayImage;

    public Character character;
    public int isWorking; //Comes from Character script. 0 = false, 1 = true

    private int jobTime, jobGold, jobExp, jobType; //for Working

    
    /*public enum JobType
    {
        Farmer = 0,
        Miner = 1,
        Blacksmith = 2,
        Merchant = 3,
        Guardian = 4

    }*/

    void Start()
    {
        character = FindObjectOfType<Character>();
        isWorking = character.isWorking;
    }


    public void ChangeImage(int imageIndex)
    {
        if (imageIndex >= 0 && imageIndex < images.Length)
        {
            displayImage.sprite = images[imageIndex];
        }
    }

    public void Work()
    {
        if (isWorking == 0)
        {
            character.isWorking = 1; isWorking = character.isWorking; 

            switch (jobType)
            {
                case 0: //Farmer
                    jobTime = 0;
                    jobGold = 0;
                    jobExp = 0;
                    break;
                case 1: //Miner
                    jobTime = 0;
                    jobGold = 0;
                    jobExp = 0;
                    break;
                case 2: //Blacksmith
                    jobTime = 0;
                    jobGold = 0;
                    jobExp = 0;
                    break;
                case 3: //Guardian
                    jobTime = 0;
                    jobGold = 0;
                    jobExp = 0;
                    break;
                case 4: //Merchant
                    jobTime = 0;
                    jobGold = 0;
                    jobExp = 0;
                    break;
            }

        }
        else if (isWorking == 1)
        {
            // Warning
        }
        else
        {
            Debug.LogError("Why the fuck isWorking is " + isWorking);   //Control the isWorking int
        }
    }

 
}
