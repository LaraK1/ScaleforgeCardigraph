using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseCard : ScriptableObject
{
    private static uint CardCount = 0;

    [field: SerializeField]
    public uint Id  // Is the unique Id of the specific card
    { get; private set; }

    [field: SerializeField]
    public static uint CardId // Is the Id of the type of card
    { get; protected set; }

    [field: SerializeField]
    public CardType CardType
    { get; protected set; }

    [field: SerializeField]
    public string Name
    { get; protected set; }

    [field: SerializeField]
    public Sprite Picture
    { get; protected set; }

    [SerializeField]
    [Range(0, 1)]
    protected float corrupted;

    /// <summary>
    /// Percantage of how corrupted the card is. Will affect value of card.
    /// <para>0 -> Not corrupted / Card has full value </para>
    /// <para>1 -> Fully corrupted / Card has no value  </para>
    /// </summary>
    public float Corrupted
    {
        get => 1 - corrupted; // gets the inverted percentage for easy math
        set
        {
            corrupted = Mathf.Max(0, Mathf.Min(1, value)); // Cant be smaller than 0 and not higher than 1
        }
    }

    [SerializeField]
    protected int baseValue;

    public int Value => Mathf.RoundToInt(baseValue * Corrupted);
    protected int ConvertToCorruptedValue(int val) => Mathf.RoundToInt(val * Corrupted);

    /// <summary>
    /// Will stay in deck after usage
    /// </summary>
    [field: SerializeField]
    public bool StaysInDeck
    { get; protected set; } = true;

    public abstract void UseCard();

    public void Awake()
    {
        CardCount++;
        Id = CardCount;
    }
}
