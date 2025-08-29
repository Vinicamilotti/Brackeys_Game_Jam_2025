using System;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
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
    public abstract class ExpeditionEvent : ScriptableObject
    {
        public bool IsEventRunning = false;
        Transform Container;
        GameObject DialogScreenPrefab;

        GameObject DialogScreenInstance;
        EventControllerScript Controller;

        protected LevelStateController Expedition;
        public Node Node;
        public string Title;
        public Sprite Image;
        public Queue<EventStep> Steps = new Queue<EventStep>();
        EventStep CurrentStep;
        public ExpeditionEvent()
        {
        }

        public abstract void Init();
        public void InitiateEvent(LevelStateController levelStateController, GameObject canva, GameObject dialogScreen)
        {
            Container = canva.transform;
            DialogScreenPrefab = dialogScreen;
            Expedition = levelStateController;
            Init();
            IsEventRunning = true;
            DialogScreenInstance = Instantiate(DialogScreenPrefab, Container);
            Controller = DialogScreenInstance.GetComponent<EventControllerScript>();
            Controller.SetTitle(Title);
            NextStep();
        }
        public void NextStep()
        {
            if (!Steps.TryDequeue(out CurrentStep))
            {
                End();
            }

            Controller.SetStep(CurrentStep);
        }

        public void OnDestroy()
        {
            Destroy(DialogScreenInstance);
        }

        public void End()
        {
           Destroy(this);
        }
    }

}