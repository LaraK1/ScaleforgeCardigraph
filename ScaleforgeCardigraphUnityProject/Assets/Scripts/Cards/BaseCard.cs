using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCard
{
    private static uint CardCount = 0;

    [field: SerializeField]
    public uint Id  // Is the unique Id of the specific card
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
    protected int ConvertToCorruptedValue(int val) => Mathf.RoundToInt(val * Corrupted);
    public BaseCard()
    {
        CardCount++;
        Id = CardCount;
    }
}
