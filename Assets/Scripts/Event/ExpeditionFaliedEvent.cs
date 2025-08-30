using System.Collections.Generic;
using ARiskyGame.Types;
using UnityEngine;

public class ExpeditionFaliedEvent : ExpeditionEvent
{
    public void SetMotive(ExpeditionFaliedMotive motive)
    {
        switch (motive)
        {
            case ExpeditionFaliedMotive.OutOfFuel:
                Steps.Clear();
                Steps.Enqueue(new EventStep
                {
                    Description = "Your ship has run out of fuel and is stranded in space.",
                    StepAction = new List<EventStepAction>
                    {
                        new EventStepAction
                        {
                            Description = "End Expedition",
                            StepAction = () =>
                            {
                                Expedition.ExpeditionController.EndExpedition();
                            }
                        }
                    }
                });
                break;
            case ExpeditionFaliedMotive.Destroyed:
                Steps.Clear();
                Steps.Enqueue(new EventStep
                {
                    Description = "Your ship has been destroyed in a fierce battle.",
                    StepAction = new List<EventStepAction>
                    {
                        new EventStepAction
                        {
                            Description = "End Expedition",
                            StepAction = () =>
                            {
                                Expedition.ExpeditionController.EndExpedition();
                            }
                        }
                    }
                });
                break;
            default:
                break;
        }
    }
    public override void Init()
    {
        Title = "Expedition Failed";

    }
}
