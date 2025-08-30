using System.Collections.Generic;
using ARiskyGame.Types;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class LastNodeEvent : ExpeditionEvent
{
    public override void Init()
    {
        Title = "This galaxy sector is cleared";
        List<EventStepAction> stepActions = new List<EventStepAction>();
        EventStep step = new()
        {
            Description = "You have reached the last node of this galaxy sector. There is nothing more to explore here.",

        };
        if (Expedition.ExpeditionController.GalaxyDepth != Assets.Types.GalaxyDepth.Depth3)
        {
            stepActions.Add(new EventStepAction
            {
                Description = "Proceed to the next sector",
                StepAction = () =>
                {
                    Expedition.ExpeditionController.StartLevel(Expedition.ExpeditionController.GalaxyDepth + 1);
                    End();
                }
            });
        }
        stepActions.Add(new EventStepAction
        {
            Description = "Go home",
            StepAction = () =>
            {
                Expedition.ExpeditionController.EndExpedition();
                End();
            }
        });
       }
}
