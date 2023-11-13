using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewFightDeck", menuName = "Cards/Card Collection")]
public class FightCardCollection : ScriptableObject
{
    [field: SerializeField]
    public List<FightCardData> FightCardsToAddToDeck
    { get; private set; } = new List<FightCardData>();
}
