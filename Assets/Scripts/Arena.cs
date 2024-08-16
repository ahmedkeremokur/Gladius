using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEditor.Playables;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Arena : MonoBehaviour
{
    public Character character;
    public Controller controller;
    public Button enemy1Attack, enemy2Attack, enemy3Attack, enemy4Attack;

    public GameObject arenaResultObject, winResult, loseResult;

    public bool arenaResultBool;

    public int enemyAttack, enemyDef, enemyHp, enemyStr, enemyVit, enemyDex, enemyAgi, enemyChar, enemyInt, enemyNo,
        characterDamage;

    public int characterCritChance, enemyCritChance,    //Dexterity - Agility (Dexterity for crit chance. Agility for reduce get crit damage)
        characterDoubleAttackChance, enemyDoubleAttackChance;   //Charisma - Intelligence (Charisma for double attack chance. Intelligence for avoid)

    public TextMeshProUGUI round1Text, round2Text, round3Text;
    
    private StringBuilder round1Log = new StringBuilder(),  //Round information
        round2Log = new StringBuilder(),
        round3Log = new StringBuilder();


    void Start()
    {
        character = FindObjectOfType<Character>();
        controller = FindObjectOfType<Controller>();
    }

    public void Attack1()
    {
        enemyNo = 1;
        Enemy();
        Fight();
        Debug.LogWarning("Arena Result: " + arenaResultBool + 
            " Character Attack: " + character.attack);
    }

    public void Attack2()
    {
        enemyNo = 2;
        Enemy();
        Fight();
        Debug.LogWarning("Arena Result: " + arenaResultBool +
            " Character Attack: " + character.attack);
    }

    public void Attack3()
    {
        enemyNo = 3;
        Enemy();
        Fight();
        Debug.LogWarning("Arena Result: " + arenaResultBool +
            " Character Attack: " + character.attack);
    }

    public void Attack4()
    {
        enemyNo = 4;
        Enemy();
        Fight();
        Debug.LogWarning("Arena Result: " + arenaResultBool +
            " Character Attack: " + character.attack);
    }

    public void CharacterStats()
    {

    }
    public void Enemy()
    {
        if (enemyNo == 1)
        {
            enemyAttack = Random.Range(2, 5);
            enemyDef = Random.Range(2, 5);
            enemyHp = Random.Range(50, 80);
            enemyStr = Random.Range(1, 3);
            enemyDex = Random.Range(1, 3);
            enemyAgi = Random.Range(1, 3);
            enemyChar = Random.Range(1, 3);
            enemyInt = Random.Range(1, 3);

        }
        else if (enemyNo == 2)
        {
            enemyAttack = Random.Range(8, 12);
            enemyDef = Random.Range(8, 12);
            enemyHp = Random.Range(150, 200);
            enemyStr = Random.Range(5, 7);
            enemyVit = Random.Range(5, 7);
            enemyDex = Random.Range(5, 7);
            enemyAgi = Random.Range(5, 7);
            enemyChar = Random.Range(5, 7);
            enemyInt = Random.Range(5, 7);
        }
        else if (enemyNo == 3)
        {
            enemyAttack = Random.Range(30, 40);
            enemyDef = Random.Range(30, 40);
            enemyHp = Random.Range(380, 420);
            enemyStr = Random.Range(15, 20);
            enemyVit = Random.Range(15, 20);
            enemyDex = Random.Range(15, 20);
            enemyAgi = Random.Range(15, 20);
            enemyChar = Random.Range(15, 20);
            enemyInt = Random.Range(15, 20);
        }
        else if (enemyNo == 4)
        {
            enemyAttack = Random.Range(70, 90);
            enemyDef = Random.Range(70, 90);
            enemyHp = Random.Range(540, 780);
            enemyStr = Random.Range(30, 40);
            enemyVit = Random.Range(30, 40);
            enemyDex = Random.Range(30, 40);
            enemyAgi = Random.Range(30, 40);
            enemyChar = Random.Range(30, 40);
            enemyInt = Random.Range(30, 40);
        }
    }

    public void Fight()
    {
        int characterDamage = CalculateDamage(character.attack, enemyDef, character.str);
        int enemyDamage = CalculateDamage(enemyAttack, character.def, enemyStr);
        float characterCritChance = CalculateCritChance(character.dex, enemyAgi);
        float enemyCritChance = CalculateCritChance(enemyDex, character.agi);
        float characterDoubleChance = CalculateDoubleChance(character.charisma, enemyInt);
        float enemyDoubleChance = CalculateDoubleChance(enemyChar, character.intelligence);

        Debug.Log($"Starting Fight: Character HP = {character.hp}, Enemy HP = {enemyHp}, Character Damage = {characterDamage}, Enemy Damage = {enemyDamage}");


            for (int i = 0; i < 3; i++)     //Round 1-2-3
            {
            StringBuilder currentRoundLog = i == 0 ? round1Log : i == 1 ? round2Log : round3Log;

            currentRoundLog.AppendLine($"Round {i + 1}:");
            currentRoundLog.AppendLine($"Character HP: {character.hp}");
            currentRoundLog.AppendLine($"Enemy HP: {enemyHp}");

            //Character attacks
            int critRandom = Random.Range(0, 100);
            int doubleRandom = Random.Range(0, 100);
            Debug.Log($"critRandom: {critRandom}, character crit chance: {characterCritChance}");
            
            if (characterDoubleChance * 100 >= doubleRandom)    //if double attack
            {
                currentRoundLog.AppendLine("Double Attacks: Character");
                for (int j = 0; j < 2; j++)
                {
                    if (characterCritChance * 100 >= critRandom)    //if critical hit in double attack
                    {
                        enemyHp -= characterDamage * 2;
                        Debug.Log("You made a Crit Damage!!");
                    }
                    else                                            //no critical in double
                    {
                        enemyHp -= characterDamage;
                        Debug.Log("You couldn't made a Crit Damage ");
                    }
                    Debug.LogError("Double Attack turn:" + j);
                }
            }

            else if(characterDoubleChance * 100 < doubleRandom) //No double attack
            {
                if (characterCritChance * 100 >= critRandom)    //if critical in no double
                {
                    enemyHp -= characterDamage * 2;
                    Debug.Log("You made a Crit Damage!!");
                }
                else                                            //no critical in no double
                {
                    enemyHp -= characterDamage;
                    Debug.Log("You couldn't made a Crit Damage ");
                }
            }
                                
            if (enemyHp <= 0)
                {
                    arenaResultBool = true;
                    ArenaResult();
                    Debug.Log("Enemy defeated, Character wins");
                    return;
                }



            //Enemy attacks

            int critRandomEnemy = Random.Range(0, 100);
            int doubleRandomEnemy = Random.Range(0, 100);
            Debug.Log($"critRandomEnemy: {critRandomEnemy}, enemy crit chance: {enemyCritChance}");

            if (enemyDoubleChance * 100 >= doubleRandomEnemy)                            //if double attack
            {
                for (int j = 0; j < 2; j++)
                {
                    if (enemyCritChance * 100 >= critRandomEnemy)                       //if critical hit
                    {
                        enemyHp -= enemyDamage * 2;
                        Debug.Log("You made a Crit Damage!!");
                    }
                    else                                                                //No critical in double     
                    {
                        enemyHp -= enemyDamage;
                        Debug.Log("You couldn't made a Crit Damage ");
                    }
                    Debug.LogError("Double Attack turn:" + j);
                }
            }

            else if (enemyDoubleChance * 100 < doubleRandomEnemy)                       //No double attack
            {
                if (enemyCritChance * 100 >= critRandomEnemy)                           //if critical in no double
                {
                    enemyHp -= enemyDamage * 2;
                    Debug.Log("You made a Crit Damage!!");
                }
                else                                                                    //no critical in no double
                {
                    enemyHp -= enemyDamage;
                    Debug.Log("You couldn't made a Crit Damage ");
                }
            }


            if (character.hp <= 0)
                {
                    arenaResultBool = false;
                    ArenaResult();
                    Debug.Log("Character defeated, Enemy wins");
                    return;
                }

                Debug.Log("Character Hp:" + character.hp + " / Enemy Hp: " + enemyHp);

            }

        if (character.hp >= enemyHp) arenaResultBool = true;
        else arenaResultBool = false;
        ArenaResult();

        character.UpdateStatUI();
    }

    private int CalculateDamage(int attack, int defence, int strength)
    {
        int baseDamage = attack - defence;
        if (baseDamage < 0) baseDamage = 0;
        int damage = baseDamage + (strength / 10);
        Debug.Log($"CalculateDamage: Attack = {attack}, Defence = {defence}, Strength = {strength}, Damage = {damage}");
        return damage;

    }

    private float CalculateCritChance(int dex, int agility)     // 0 <= critChance < 1
    {
        int critCode = dex - agility;
        float critChance = 1f - Mathf.Exp(-critCode/50f);
        Debug.Log($"Calculate Dexterity = {dex}, Agility = {agility}, CritChance = {critChance}");
        return critChance;
    }

    private float CalculateDoubleChance(int charisma, int intelligence) // 0 <= doubleChance <      Actually this is same as the crit.
    {
        int doubleCode = charisma - intelligence;
        float doubleChance = 1f - Mathf.Exp(-doubleCode / 50f);
        Debug.Log($"Calculate Charisma = {charisma}, Intelligence = {intelligence}, CritChance = {doubleChance}");
        return doubleChance;
    }

    public void ArenaResult()
    {
        if (!arenaResultBool)
            controller.gold += Random.Range(5 * enemyNo * enemyNo, 10 * enemyNo * enemyNo);
        else if (arenaResultBool)
            controller.gold += Random.Range(50 * enemyNo * enemyNo, 100 * enemyNo * enemyNo);
    }

    private void UpdateFightLog()
    {
        round1Text.text = round1Log.ToString();
        round2Text.text = round2Log.ToString();
        round3Text.text = round3Log.ToString();
    }
}
