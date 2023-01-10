namespace RelicKeeper
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;
    using SideLoader;
    using InstanceIDs;

    class TameBeastEffect : Effect
    {
        protected override void ActivateLocally(Character _affectedCharacter, object[] _infos)
        {
            if (new Character.Factions[] {Character.Factions.Deer, Character.Factions.Hounds, Character.Factions.Tuanosaurs }.Contains(_affectedCharacter.Faction))
            {
                _affectedCharacter.ChangeFaction(Character.Factions.Player);
            }
        }
    }
}
