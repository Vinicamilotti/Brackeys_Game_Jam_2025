using Assets.Types;
using System;
using UnityEngine;

namespace ARiskyGame.Types
{

    public static class EventFactory
    {
        
        public static ExpeditionEvent GetEvent()
        {
            EventType eventType = (EventType) UnityEngine.Random.Range(0, 3);
            return eventType switch
            {
                EventType.Resource => ScriptableObject.CreateInstance<ResourceEncounter>(),
                EventType.RandomOutput => ScriptableObject.CreateInstance<ResourceEncounter>(),
                EventType.Combat => ScriptableObject.CreateInstance<CombatEvent>(),
                _ => ScriptableObject.CreateInstance<CombatEvent>(),
            };
        }
    }
}
