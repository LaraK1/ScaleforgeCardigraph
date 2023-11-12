using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO make this base for npc and player
public class Npc : MonoBehaviour
{
    [field: SerializeField]
    public string Name
    { get; private set; }

    [field: SerializeField]
    public Sprite Picture
    { get; private set; }

    [field: SerializeField]
    public FightParticipant Fighter
    {
        get; private set;
    }


    // Add Conversation Participant

    // Add Inventory?
}
