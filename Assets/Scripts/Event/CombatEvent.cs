using ARiskyGame.Types;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum EnemyType : byte
{
    Pirate,
    RogueIA,
    AncientAlien,
}

public class Enemy
{
    public string Title;
    public string Description;
    public int CombatPower;
}
public class CombatEvent : ExpeditionEvent
{
    Dictionary<EnemyType, Enemy> enemies = new();
    int CombatPower = 2;
    float combatOdds = 0;
    public CombatEvent() : base()
    {
    }
    private void OnEnable()
    {
        enemies = new()
    {
        {
            EnemyType.Pirate, new()
            {
                Title = "Combat: Alien Pirates",
                Description = "Some wierd lookin green creatures with eye patches are attacking your ship",
                CombatPower = Random.Range(2, 4)
            }
        },
        {
            EnemyType.RogueIA,
            new()
            {
                Title = "Combat: Rogue IA",
                Description = " 01010011 01101000 01101001 01110000 00100000 01100100 01100101 01110100 01100101 01100011 01110100 01100101 01100100 00101100 00100000 01000100 01000101 01010011 01010100 01010010 01001111 01011001 00100001",
                CombatPower = Random.Range(3, 5)
            }
        },
        {
            EnemyType.AncientAlien,
            new()
            {
                Title = "Combat: Ancient alien race",
                Description = "B̝̪͈̪͙̱͐̀̌͐ͣ̑ͨ͐ͪͪ̕ͅe̸̸̶̵̷̛̛̤̪̯͇̖̯̤̪̯͙̯ͧͥ͒̅̌͋̋̄ͥ̈́͆͛̋̒ͣ̾ͥ̚͜͢͡ͅ_ a͓͉̗̿̂͘f̝̱͎̪͋̋ͩ́̃ͨ̾̏͗̈͛͛̿͑͜͞ͅf̣͈͈͕̀͊ͭ͝͝͠͝r̸̢̟̭͍͎̲͚̝̥̙̥͔̬͖͈̞̮̝̎͗͐̾͗̂ͪ̉ͫͮ̊͆͒ͭ̅̃̔ͭ̔̕̕͞͝ͅa̢͇̝̘͉̜̹̬͊̅ͩ̉͑ͯ̿͟͞͝i͇̟͒_̮͍̺͚̱̳̮̯͇͍̲̐̏̀̑̏̈́ͯ̐̂͂͐͘̕͘͜_̷̴̝̣̃̚̕d̴̛̛͉̲̟̳̎͂͆͂͘",
                CombatPower = Random.Range(4, 6)
            }
       }
    };

    }
    Enemy DeterminateEncouter()
    {
        EnemyType type = Expedition.ExpeditionController.GalaxyDepth switch
        {
            Assets.Types.GalaxyDepth.Depth1 => EnemyType.Pirate,
            Assets.Types.GalaxyDepth.Depth2 => (EnemyType)Random.Range(0, 2),
            Assets.Types.GalaxyDepth.Depth3 => (EnemyType)Random.Range(1, 3),
            _ => EnemyType.Pirate
        };

        return enemies[type];
    }
    public override void Init()
    {
        Enemy getEnemy = DeterminateEncouter();
        CombatPower = getEnemy.CombatPower;
        var combatFactor = (Expedition.Player.CombatPower + CombatPower);

        combatOdds = (float)Expedition.Player.CombatPower / (float)combatFactor;

        Title = getEnemy.Title;
        Steps.Enqueue(new()
        {
            Description = getEnemy.Description,
            StepAction = new()
            {
                new()
                {
                    Description = $"Attack ({(combatOdds * 100).ToString("F2")}%)",
                    StepAction = Fight
                },
                new()
                {
                    Description = "Flee",
                    StepAction = Flee,
                }
            }

        });
    }

    void WinBattle()
    {
        var loseHealth = Random.value > combatOdds;
        var lootType = (ResourceType)Random.Range(0, 2);
        var lootQuant = Random.Range(2, 5) * (int)Expedition.ExpeditionController.GalaxyDepth;

        string desc = loseHealth ? "You win the fight! But not without a scratch\n\n You lose 1 Health" : "You win the fight!";
        desc += $"\n Loot: {lootQuant}x {lootType.ToString()}";

        Expedition.ExpeditionController.AddLoot(lootType, lootQuant);
        if (loseHealth)
        {
            Expedition.Player.TakeDamage(1);

        }
        Steps.Enqueue(new()
        {
            Description = desc,
            StepAction = new() { new() { StepAction = End, Description = "Ok" } }
        });


    }

    void LoseBattle()
    {
        Expedition.Player.TakeDamage(2);

        Steps.Enqueue(new()
        {
            Description = "You lose the fight! \n You looe 2 health",
            StepAction = new() { new() { StepAction = End, Description = "Ok" } }
        });
    }
    void Fight()
    {

        if (Random.value < combatOdds)
        {
            WinBattle();
        }
        else
        {
            LoseBattle();
        }

        NextStep();
    }

    void Flee()
    {
        Steps.Enqueue(new()
        {
            Description = "You flee",
            StepAction = new() { new() { StepAction = End, Description = "Voltar" } }
        });

        NextStep();
    }





}
