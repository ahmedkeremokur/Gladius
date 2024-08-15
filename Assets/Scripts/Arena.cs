using System.Collections;
using System.Collections.Generic;
using UnityEditor.Playables;
using UnityEngine;
using UnityEngine.UI;

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

    }

    public void Attack3()
    {

    }

    public void Attack4()
    {

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
            //enemyVit = Random.Range(1, 3);
            /*enemyDex = Random.Range(1, 3);
            enemyAgi = Random.Range(1, 3);
            enemyChar = Random.Range(1, 3);
            enemyInt = Random.Range(1, 3);*/
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


            for (int i = 0; i < 3; i++)
            {
            //Character attacks
            int critRandom = Random.Range(0, 100);
            int doubleRandom = Random.Range(0, 100);
            Debug.Log($"critRandom: {critRandom}, character crit chance: {characterCritChance}");
            
            if (characterDoubleChance * 100 >= doubleRandom)
            {
                /*for (int a = 0, a < 2; a++)
                {
                    if (characterCritChance * 100 >= critRandom)
                    {
                        enemyHp -= characterDamage * 2;
                        Debug.Log("You made a Crit Damage!!");
                    }
                    else
                    {
                        enemyHp -= characterDamage;
                        Debug.Log("You couldn't made a Crit Damage ");
                    }
                    Debug.LogError("Double Attack turn:" + i);
                }*/
            }
            if (characterCritChance*100 >= critRandom)
            {
                enemyHp -= characterDamage * 2;
                Debug.Log("You made a Crit Damage!!");
            }
            else
            {
                enemyHp -= characterDamage;
                Debug.Log("You couldn't made a Crit Damage ");
            }
                
            //Debug.Log($"Character attacks: Enemy HP = {enemyHp}");
            if (enemyHp <= 0)
                {
                    arenaResultBool = true;
                    ArenaResult();
                    Debug.Log("Enemy defeated, Character wins");
                    return;
                }

                //Enemy attacks
                character.hp -= enemyDamage;
                //Debug.Log($"Enemy attacks: Character HP = {character.hp}");
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

    private float CalculateDoubleChance(int charisma, int intelligence)
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
}
