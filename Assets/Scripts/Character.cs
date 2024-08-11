using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
//using Microsoft.Unity.VisualStudio.Editor;

public class Character : MonoBehaviour
{
    public Controller controller;
    public Charslot charslot;

    public List<Charslot> charslots;

    public int hp, maxHp, attack, def, exp, maxExp;
    public int str = 1, vit = 1, dex = 1, agi = 1, charisma = 1, intelligence = 1, lvl = 1;
    private int costStr, costVit, costDex, costAgi, costCharisma, costIntelligence;
    

    public TextMeshProUGUI hpText, attackText, defText, expText, lvlText;
    public TextMeshProUGUI strText, vitText, dexText, agiText, charismaText, intelligenceText;
    public TextMeshProUGUI costStrText, costVitText, costDexText, costAgiText, costCharismaText, costIntelligenceText;

    public Image hpBar, expBar;
    


    void Start()
    {      
        controller = GameObject.FindGameObjectWithTag("Controller").GetComponent<Controller>();
        charslot = FindObjectOfType<Charslot>();
        LoadCharacter();
        UpdateCharacter();
        UpdateCosts();
        UpdateStatUI();
        //Debug.LogError("Character Attack:" + attack + "Character Def: " + def);
    }


    public Character()
    {
        lvl = 1;
        hp = 1; maxHp = 1;
        attack = 1;
        def = 1; 
        exp = 0; maxExp = 100*(lvl*lvl);

        str = 1;
        vit = 1;
        dex = 1;
        agi = 1;
        charisma = 1;
        intelligence = 1;
    }

    /*
    public void TrainStr()
    {
        str++;
        costStr = str * str;
        attack += str;       
        controller.gold -= costStr;

        UpdateCharacter();
        UpdateCosts();
        UpdateStatUI();              
    }
    public void TrainVit()
    {
        vit++;
        costVit = vit * vit;
        hp += 2;
        maxHp += 2;
        controller.gold += costVit;

        UpdateCharacter();
        UpdateCosts();
        UpdateStatUI();
    }
    public void TrainDex()
    {
        dex++;
        costDex = dex * dex;
        UpdateCosts();
        UpdateStatUI(); 
        controller.gold -= costDex;

    }
    public void TrainAgi()
    {
        agi++;
        costAgi = agi * agi;
        UpdateCosts();
        UpdateStatUI();
        controller.gold -= costAgi;
    }
    public void TrainCharisma()
    {
        charisma++;
        costCharisma = charisma * charisma;
        UpdateCosts();
        UpdateStatUI();
        controller.gold -= costCharisma;
    }
    public void TrainIntelligence()
    {
        intelligence++;
        costIntelligence = intelligence * intelligence;
        UpdateCosts();
        UpdateStatUI();
        controller.gold -= costIntelligence;
    }
    */

    public void TrainStat(ref int stat, ref int cost, string statName)
    {

        if (controller.gold >= cost)
        {
            controller.gold -= cost;
            stat++;
            cost = stat * stat;
            
            if (statName == "Vit")
            {
                hp += 2;
                maxHp += 2;
            }

            UpdateCharacter();
            UpdateCosts();
            UpdateStatUI();
        }
        else Debug.LogError("Not enough funds");
        
    }

    public void TrainStr()
    {
        TrainStat(ref str, ref costStr, "Str");
    }

    public void TrainVit()
    {
        TrainStat(ref vit, ref costVit, "Vit");
    }

    public void TrainDex()
    {
        TrainStat(ref dex, ref costDex, "Dex");
    }

    public void TrainAgi()
    {
        TrainStat(ref agi, ref costAgi, "Agi");
    }

    public void TrainCharisma()
    {
        TrainStat(ref charisma, ref costCharisma, "Charisma");
    }

    public void TrainIntelligence()
    {
        TrainStat(ref intelligence, ref costIntelligence, "Intelligence");
    }

    public void CheckExp()
    {
        if (exp >= maxExp)
        {
            lvl++;
            maxExp = 100*(lvl*lvl);
            UpdateStatUI();
        }
    }

    private void UpdateCosts()
    {
        costStr = str * str;
        costVit = vit * vit;
        costDex = dex * dex;
        costAgi = agi * agi;
        costCharisma = charisma * charisma;
        costIntelligence = intelligence * intelligence;

        costStrText.text = costStr.ToString();
        costVitText.text = costVit.ToString();
        costDexText.text = costDex.ToString();
        costAgiText.text = costAgi.ToString();
        costCharismaText.text = costCharisma.ToString();
        costIntelligenceText.text = costIntelligence.ToString();
    }

    private void UpdateCharacter()
    {
        attack = str;
        foreach (var charslot in charslots)
        {
            attack += charslot.GetWeaponDamage();
        }

        def = vit;
        foreach (var charslot in charslots)
        {
            def += charslot.GetArmorDefence();
        }
    }

    public void UpdateStatUI()
    {
        strText.text = ("Strength = " + str);
        vitText.text = ("Vitality = " + vit);
        dexText.text = ("Dexterity = " + dex);
        agiText.text = ("Agility = " + agi);
        charismaText.text = ("Charisma = " + charisma);
        intelligenceText.text = ("Intelligence = " + intelligence);

        hpText.text = ("HP: " + hp + "/" + maxHp);
        attackText.text = ("Attack: " + attack);
        defText.text = ("Defence: " + def);
        expText.text = ("Exp: " + exp + "/" + maxExp);
        lvlText.text = ("Lvl: " + lvl);

        hpBar.fillAmount = (float)hp/maxHp;
        hpBar.color = Color.Lerp(Color.red, Color.green, (float)hp/maxHp);

        expBar.fillAmount = (float)exp/maxExp;
        hpBar.color = Color.Lerp(Color.cyan, Color.magenta, (float)exp/maxExp);
    }

    
    public void SaveCharacter()
    {
        maxExp = 100 * (lvl*lvl);
                     
        PlayerPrefs.SetInt("exp", def);
        PlayerPrefs.SetInt("maxExp", maxExp);
        PlayerPrefs.SetInt("lvl", lvl);

        PlayerPrefs.SetInt("strength", str);
        PlayerPrefs.SetInt("vitality", vit);
        PlayerPrefs.SetInt("dexterity", dex);
        PlayerPrefs.SetInt("agility", agi);
        PlayerPrefs.SetInt("charisma", charisma);
        PlayerPrefs.SetInt("intelligence", intelligence);

        PlayerPrefs.SetInt("hp", hp);
        PlayerPrefs.SetInt("maxHp", maxHp);

        UpdateCharacter();
        PlayerPrefs.SetInt("attack", attack);
        PlayerPrefs.SetInt("def", def);
    }

    public void LoadCharacter()
    {        
        hp = PlayerPrefs.GetInt("hp", hp);
        maxHp = PlayerPrefs.GetInt("maxHp", maxHp);
        attack = PlayerPrefs.GetInt("attack", attack);
        def = PlayerPrefs.GetInt("def", def);
        lvl = PlayerPrefs.GetInt("lvl", lvl);
        exp = PlayerPrefs.GetInt("exp", def);
        maxExp = PlayerPrefs.GetInt("maxExp", maxExp);

        str = PlayerPrefs.GetInt("strength", str);
        vit = PlayerPrefs.GetInt("vitality", vit);
        dex = PlayerPrefs.GetInt("dexterity", dex);
        agi = PlayerPrefs.GetInt("agility", agi);
        charisma = PlayerPrefs.GetInt("charisma", charisma);
        intelligence = PlayerPrefs.GetInt("intelligence", intelligence);

        maxExp = 100 * (lvl * lvl);

        maxHp = 100 + vit * 2;
        if (hp > maxHp)
            hp = maxHp;
        Debug.Log("hp / maxhp: " + hp + "/" + maxHp);
    }

    public void ResetStats()
    {
        str = 1; vit = 1; dex = 1; agi = 1; charisma = 1; intelligence = 1;
        UpdateCosts();
        UpdateStatUI();
    }
    public void OnApplicationQuit()
    {
        SaveCharacter();
    }

}
