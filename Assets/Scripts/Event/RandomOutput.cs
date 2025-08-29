using System.Collections.Generic;
using ARiskyGame.Types;
using Unity.VisualScripting;
using UnityEngine;

public enum RandomOutputType
{
    Nothing,
    JollyCooperation,
    WreckedShip,
}

public enum WreckedShipOutcome
{
    Nothing,
    Loot,
    Ambush,
    Fuel,
}

public class RandomOutput : ExpeditionEvent
{
    public RandomOutputType OutputType;
    EventStep JollyCooperationStep()
    {
        ResourceType type = (ARiskyGame.Types.ResourceType)Random.Range(0, 3);
        int amount = type switch
        {
            ResourceType.Food => Random.Range(2, 5),
            ResourceType.Fuel => 1,
            ResourceType.SpareParts => Random.Range(3, 4),
            _ => 2
        } + (int)Expedition.ExpeditionController.GalaxyDepth;
        EventStep step = new()
        {
            Description = $"You found cool aliens with cute faces, waving their little arms. They gift you with {amount} x {type} ",
            StepAction = new()
            {
                new EventStepAction
                {
                    Description = $"Cooperate!",
                    StepAction = () =>
                    {
                        Expedition.ExpeditionController.AddLoot(type, amount);
                        End();
                    }
                }
            }
        };
        return step;
    }

    EventStep WreckedShipInit()
    {
        var step = new EventStep
        {
            Description = "You found a wrecked ship drifting in space. It looks salvageable.",
            StepAction = new List<EventStepAction>
            {
                new EventStepAction
                {
                    Description = "Salvage the ship",
                    StepAction = () =>
                    {
                        Steps.Enqueue(WreckedShipResolve());
                        NextStep();
                    }
                },
                new EventStepAction
                {
                    Description = "Leave it alone",
                    StepAction = () =>
                    {
                        End();
                    }
                }
            }
        };

        return step;
    }
    EventStep WreckedShipResolve()
    {
        WreckedShipOutcome outcome = (WreckedShipOutcome)Random.Range(0, 4);
        EventStep step = outcome switch
        {
            WreckedShipOutcome.Nothing => new EventStep
            {
                Description = "You found nothing of value in the wrecked ship. What a waste of fuel...",
                StepAction = new()
                   {
                       new EventStepAction
                       {
                           Description = "Continue",
                           StepAction = () => End(),
                       }
                   }
            },
            WreckedShipOutcome.Loot => LootOutcome(),
            WreckedShipOutcome.Ambush => AmbusOutome(),
            WreckedShipOutcome.Fuel => FuelOutcome(),
            _ => FuelOutcome(),
        };

        return step;
        
    }
    EventStep FuelOutcome()
    {
        
        EventStep step = new()
        {
            Description = $"You found a hidden fuel cache in the wrecked ship. You gain 1 x Fuel. At least it cost us nothing.",
            StepAction = new()
            {
                new EventStepAction
                {
                    Description = $"Take the fuel",
                    StepAction = () =>
                    {
                        Expedition.ExpeditionController.AddLoot(ResourceType.Fuel, 1);
                        End();
                    }
                }
            }
        };
        return step;
    }
    EventStep AmbusOutome()
    {
        ResourceType type = (ARiskyGame.Types.ResourceType)Random.Range(0, 3);
        int amount = type switch
        {
            ResourceType.Food => Random.Range(2, 5),
            ResourceType.Fuel => Random.Range(2, 3),
            ResourceType.SpareParts => Random.Range(3, 4),
            _ => 2
        } + (int)Expedition.ExpeditionController.GalaxyDepth;
        EventStep step = new()
        {
            Description = $"As you board the wrecked ship, hostile aliens ambush your crew and steal {amount} x {type} from your supplies before fleeing into space.",
            StepAction = new()
            {
                new EventStepAction
                {
                    Description = $"Continue",
                    StepAction = () =>
                    {
                        Expedition.ExpeditionController.RemoveLoot(type, amount);
                        End();
                    }
                }
            }
        };
        return step;
    }
    EventStep LootOutcome()
    {
        ResourceType type = (ARiskyGame.Types.ResourceType)Random.Range(0, 3);
        int amount = type switch
        {
            ResourceType.Food => Random.Range(2, 5),
            ResourceType.Fuel => Random.Range(2, 3),
            ResourceType.SpareParts => Random.Range(3, 4),
            _ => 2
        } * (int)Expedition.ExpeditionController.GalaxyDepth;
        EventStep step = new()
        {
            Description = $"You found {amount} x {type} in the wrecked ship.",
            StepAction = new()
            {
                new EventStepAction
                {
                    Description = $"Take the loot",
                    StepAction = () =>
                    {
                        Expedition.ExpeditionController.AddLoot(type, amount);
                        End();
                    }
                }
            }
        };
        return step;
    }
    public override void Init()
    {
        OutputType = (RandomOutputType)Random.Range(0, 3);
        switch (OutputType)
        {
            case RandomOutputType.Nothing:
                Title = "Nothing Here";
                Steps.Enqueue(new()
                {
                    Description = "What a waste of fuel...",
                    StepAction = new List<EventStepAction>
                    {
                        new EventStepAction
                        {
                            Description = "Continue",
                            StepAction = () => End(),
                        }
                    }
                });
                break;
            case RandomOutputType.JollyCooperation:
                Title = "Jolly Cooperation";
                Steps.Enqueue(JollyCooperationStep());
                break;
            case RandomOutputType.WreckedShip:
                Title = "Wrecked Ship";
                Steps.Enqueue(WreckedShipInit());
                break;
        }
    }
}
