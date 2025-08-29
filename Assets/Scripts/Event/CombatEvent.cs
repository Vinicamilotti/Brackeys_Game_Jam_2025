using ARiskyGame.Types;
using UnityEngine;

public class CombatEvent : ExpeditionEvent
{
    int CombatPower = 2;

    public CombatEvent() : base()
    {
    }

    public override void Init()
    {

        Title = "Teste combate";
        Steps.Enqueue(new()
        {
            Description = "Somee pirates attack your ship",
            StepAction = new()
            {
                new()
                {
                    Description = "Attack",
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

    void Fight()
    {
        var odds = Expedition.Player.CombatPower / Expedition.Player.CombatPower + CombatPower;

        if (Random.value < odds)
        {
            Steps.Enqueue(new() {
                Description = "You win the fight!",
                StepAction = new() { new() { StepAction = End, Description = "Voltar" } }
            });
        }
        else
        {
            Steps.Enqueue(new() {
                Description = "You lose the fight!",
                StepAction = new() { new() { StepAction = End, Description = "Voltar" } }
            });
        }

        NextStep();
    }

    void Flee()
    {
        Steps.Enqueue(new() {
            Description = "You flee",
            StepAction = new() { new() { StepAction = End, Description = "Voltar" } }
        });

        NextStep();
    }

    

    

}
