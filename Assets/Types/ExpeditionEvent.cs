using System;
using System.Collections.Generic;
using UnityEngine;
namespace ARiskyGame.Types
{
    public class EventStepAction
    {
        public string Description;
        public Action StepAction;
    }
    public class EventStep
    {
        public string Description;
        public List<EventStepAction> StepAction;
    }
    public abstract class ExpeditionEvent : MonoBehaviour
    {
        LevelStateController Expedition;
        public Node Node;
        public string Title;
        public Sprite Image;
        public Queue<EventStep> Steps = new Queue<EventStep>();
        EventStep CurrentStep;

        public void InitiateEvent()
        {
            
        }
        public void NextStep()
        {

        }
    }

}