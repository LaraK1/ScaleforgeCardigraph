using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPickNpc
{
    public FightParticipant PickNpc(List<FightParticipant> npcs, FightCard card);
    public Npc PickNpc(List<Npc> npcs, BaseCard card);

    // TODO all of them?
}
