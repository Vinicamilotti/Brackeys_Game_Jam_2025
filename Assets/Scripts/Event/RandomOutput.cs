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
    
    public override void Init()
    {
        OutputType = (RandomOutputType)Random.Range(0, 3);
        switch (OutputType)
        {
            case RandomOutputType.Nothing:
                Title = "Nothing Here";
                Steps.Enqueue(new()
                {
                    Description = "You found nothing of interest on this planet.",
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
                Steps.Enqueue();
                break;
        }
    }
}
