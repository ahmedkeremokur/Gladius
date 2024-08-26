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
    public Controller controller;
    public int isWorking; //Comes from Character script. 0 = false, 1 = true

    private int jobTime, jobGold, jobExp, jobType; //for Working

    public GameObject workBtn;

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

        if(workBtn = null)
        {
            workBtn = GameObject.Find("Work Button");
        }
        if(workBtn != null)
        {
            if (character.isWorking == 1)
                workBtn.SetActive(false);
            else
                workBtn.SetActive(true);
        }       
    }


    public void ChangeImage(int imageIndex)
    {
        if (imageIndex >= 0 && imageIndex < images.Length)
        {
            displayImage.sprite = images[imageIndex];
        }
    }

    public void SetJobType(int type)
    {
        jobType = type;
    }
    public void Work()
    {
        if (isWorking == 0)
        {
            character.isWorking = 1; isWorking = character.isWorking; workBtn.SetActive(false);

            switch (jobType)
            {
                case 0: //Farmer
                    jobTime = 300;      //5 minute
                    jobGold = 20;
                    jobExp = 5;
                    break;
                case 1: //Miner
                    jobTime = 1800;     //30 minute
                    jobGold = 150;
                    jobExp = 40;
                    break;
                case 2: //Blacksmith
                    jobTime = 7200;     //2 hour
                    jobGold = 750;
                    jobExp = 200;
                    break;
                case 3: //Guardian
                    jobTime = 21600;        //6 hour
                    jobGold = 2500;
                    jobExp = 750;
                    break;
                case 4: //Merchant
                    jobTime = 43200;        //12 hour
                    jobGold = 8000;
                    jobExp = 2000;
                    break;
            }

            StartCoroutine(WorkCoroutine(jobTime, jobGold, jobExp));

            character.UpdateStatUI();

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

    private IEnumerator WorkCoroutine(int time, int gold, int exp)
    {
        yield return new WaitForSeconds(time);      //Waiting...

        //Add gold and exp
        controller.gold += gold;
        character.exp += exp;

        character.UpdateStatUI();

        //Reset working status
        isWorking = 0;
        character.isWorking = 0;
        workBtn.SetActive(true);


        Debug.Log("Job completed!");
    }

 
}
