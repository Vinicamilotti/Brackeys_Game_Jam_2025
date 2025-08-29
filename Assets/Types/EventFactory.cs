using Assets.Types;
using System;
using UnityEngine;

namespace ARiskyGame.Types
{

    public static class EventFactory
    {
        public static ExpeditionEvent GetEvent()
        {
            return ScriptableObject.CreateInstance<CombatEvent>();
        }
    }
}
