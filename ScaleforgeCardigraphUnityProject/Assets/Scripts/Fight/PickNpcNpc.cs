using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickNpcNpc : MonoBehaviour, IPickNpc
{
    public Npc PickNpc(List<Npc> npcs, BaseCard card)
    {
        return npcs[Random.Range(0, npcs.Count-1)];
    }

    public FightParticipant PickNpc(List<FightParticipant> npcs, FightCard card)
    {
        return npcs[Random.Range(0, npcs.Count - 1)];
    }
}
