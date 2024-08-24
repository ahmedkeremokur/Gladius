using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Controller : MonoBehaviour
{
    public GameObject character, charmarkt, market, trainingfield, arena, job, isWorkingWarning;

    public Button btnChar, btnMarket, btnTraining, btnArena, btnJob;

    public GameObject armorSmith, weaponSmith, potionSeller;    //Shop
    public Button btnArmor, btnWeapon, btnPot;                  //Shop

    public TMP_Text goldText, jobLvlWarning;
    public int gold;

    public int whereAreYou;  /* 0: Character
                              * 1: Market
                              * 2: Training Field
                              * 3: Arena
                              * 4: Job */


    public Character charr;



    private void Start()
    {
        charr = FindObjectOfType<Character>();

        GoCharacter();
        gold = PlayerPrefs.GetInt("gold");

        if (isWorkingWarning == null )
        {
            GameObject isWorkingWarning = GameObject.Find("isWorking Warning");
            Debug.LogWarning("isWorkingWarning was null.");
        }

        isWorkingWarning.SetActive(false);       
    }

    private void Update()
    {
        goldText.text = gold.ToString();

        //-----Gold Keycodes +20.000 or set 0-----//
        if (Input.GetKeyDown(KeyCode.G))
        {
            gold += 20000;
        }
        if (Input.GetKeyDown(KeyCode.J))
        {
            gold = 0;
            PlayerPrefs.SetInt("gold", gold);
        }
    }
    public void Quit()
    {
        Application.Quit();
    }

    public void IsWorkingWarningOpenClose()
    {
        if (charr.isWorking == 0)
        {
            charr.isWorking = 1;
            isWorkingWarning.SetActive(true);
        }
        else
        {
            charr.isWorking = 0;
            isWorkingWarning.SetActive(false);
        }
    }
     
    //----- 4 Menu -----//

    public void GoCharacter()
    {
        whereAreYou = 0;
        charmarkt.SetActive(true);
        character.SetActive(true);
        market.SetActive(false);
        trainingfield.SetActive(false);
        arena.SetActive(false);
        job.SetActive(false);

        btnChar.interactable = false;
        btnMarket.interactable = true;
        btnTraining.interactable = true;
        btnArena.interactable = true;
        if(charr.lvl >= 5)
        {
            btnJob.interactable = true;
            jobLvlWarning.text = "";
        }
            
        else
        {
            btnJob.interactable = false;
            jobLvlWarning.text = "After Lvl 5";
        }
            
    }
    public void GoMarket()
    {
        whereAreYou = 1;
        charmarkt.SetActive(true);
        character.SetActive(false);
        market.SetActive(true);
        trainingfield.SetActive(false);
        arena.SetActive(false);
        job.SetActive(false);

        btnChar.interactable = true;
        btnMarket.interactable = false;
        btnTraining.interactable = true;
        btnArena.interactable = true;
        if (charr.lvl >= 5)
        {
            btnJob.interactable = true;
            jobLvlWarning.text = "";
        }

        else
        {
            btnJob.interactable = false;
            jobLvlWarning.text = "After Lvl 5";
        }

        WeaponSmith();

    }

    public void GoTrainingField()
    {
        whereAreYou = 2;
        charmarkt.SetActive(false);
        trainingfield.SetActive(true);
        arena.SetActive(false);
        job.SetActive(false);

        btnChar.interactable = true;
        btnMarket.interactable = true;
        btnTraining.interactable = false;
        btnArena.interactable = true;
        if (charr.lvl >= 5)
        {
            btnJob.interactable = true;
            jobLvlWarning.text = "";
        }

        else
        {
            btnJob.interactable = false;
            jobLvlWarning.text = "After Lvl 5";
        }
    }

    public void GoArena()
    {
        whereAreYou = 3;
        charmarkt.SetActive(false);
        trainingfield.SetActive(false);
        arena.SetActive(true);
        job.SetActive(false);

        btnChar.interactable = true;
        btnMarket.interactable = true;
        btnTraining.interactable = true;
        btnArena.interactable = false;
        if (charr.lvl >= 5)
        {
            btnJob.interactable = true;
            jobLvlWarning.text = "";
        }

        else
        {
            btnJob.interactable = false;
            jobLvlWarning.text = "After Lvl 5";
        }
    }

    public void GoJob()
    {
        whereAreYou= 4;
        charmarkt.SetActive(false);
        trainingfield.SetActive(false);
        arena.SetActive(false);
        job.SetActive(true);

        btnChar.interactable = true;
        btnMarket.interactable = true;
        btnTraining.interactable = true;
        btnArena.interactable = true;
        btnJob.interactable = false;

        jobLvlWarning.text = "";
    }

    
    //-----Market-----//
    public void ArmorSmith()
    {
        armorSmith.SetActive(true);
        weaponSmith.SetActive(false);
        potionSeller.SetActive(false);

        btnArmor.interactable = false;
        btnWeapon.interactable = true;
        btnPot.interactable = true;
    }

    public void WeaponSmith()
    {
        armorSmith.SetActive(false);
        weaponSmith.SetActive(true);
        potionSeller.SetActive(false);

        btnArmor.interactable = true;
        btnWeapon.interactable = false;
        btnPot.interactable = true;
    }

    public void PotionSeller()
    {
        armorSmith.SetActive(false);
        weaponSmith.SetActive(false);
        potionSeller.SetActive(true);

        btnArmor.interactable = true;
        btnWeapon.interactable = true;
        btnPot.interactable = false;

    }

    public void OnApplicationQuit()
    {
        PlayerPrefs.SetInt("gold", gold);
    }

}
