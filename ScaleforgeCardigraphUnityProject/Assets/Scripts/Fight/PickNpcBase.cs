using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PickNpcBase : MonoBehaviour
{
    public abstract FightParticipant PickNpc(List<FightParticipant> npcs, FightCardData card);
    public abstract Npc PickNpc(List<Npc> npcs, BaseCardData card);

    // TODO all of them?
}
