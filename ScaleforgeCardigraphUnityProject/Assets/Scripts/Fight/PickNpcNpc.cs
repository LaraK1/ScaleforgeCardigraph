using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PickNpcNpc : PickNpcBase
{
    public override Npc PickNpc(List<Npc> npcs, BaseCardData card)
    {
        if (npcs == null || npcs.Count == 0) return null;

        return npcs[Random.Range(0, npcs.Count-1)];
    }

    public override FightParticipant PickNpc(List<FightParticipant> npcs, FightCardData card)
    {
        if (npcs == null || npcs.Count == 0) return null;

        return npcs[Random.Range(0, npcs.Count - 1)];
    }
}
