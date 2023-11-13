using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCardData : ScriptableObject
{
    private static uint CardTypeCount = 0;

    [field: SerializeField]
    public static uint CardId // Is the Id of the type of card, not unique
    { get; protected set; }

    public CardType CardType
    { get; protected set; }

    [field: SerializeField]
    public string Name
    { get; protected set; }

    [field: SerializeField]
    public Sprite Picture
    { get; protected set; }

    [field: SerializeField]
    public int BaseValue
    { get; protected set; }

    /// <summary>
    /// Will stay in deck after usage
    /// </summary>
    [field: SerializeField]
    public bool StaysInDeck
    { get; protected set; } = true;

    public void Awake()
    {
        CardTypeCount++;
        CardId = CardTypeCount;
    }

}
