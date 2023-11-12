using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PickNpcBase : MonoBehaviour
{
    public abstract FightParticipant PickNpc(List<FightParticipant> npcs, FightCard card);
    public abstract Npc PickNpc(List<Npc> npcs, BaseCard card);

    // TODO all of them?
}
