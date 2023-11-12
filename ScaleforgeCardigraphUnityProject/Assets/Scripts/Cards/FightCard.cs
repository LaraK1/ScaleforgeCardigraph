using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewFightCard", menuName = "Cards/FightCard")]
public class FightCard : BaseCard
{
    /// <summary>
    /// If IsAttack this card is used on the opposing team. If not, this card can be used on your own team.
    /// </summary>
    [field: SerializeField]
    public bool IsAttack
    { get; private set; }

    [SerializeField]
    private int healing;

    public int Healing => ConvertToCorruptedValue(healing);

    [SerializeField]
    private int attack;
    public int Attack => ConvertToCorruptedValue(attack);

    [SerializeField]
    private int armor;
    public int Armor => ConvertToCorruptedValue(armor);

    /// <summary>
    /// Amount of armor that gets removed on enemy.
    /// </summary>
    [SerializeField]
    private int crush;
    public int Crush => ConvertToCorruptedValue(crush);

    [field: SerializeField]
    public CardEffect[] GiveCardEffects
    { get; private set; }

    [field: SerializeField]
    public CardEffect[] RemoveCardEffects
    { get; private set; }

    public FightCard()
    {
        CardType = CardType.Fight;
    }

    public override void UseCard()
    {
        throw new System.NotImplementedException();
    }
}
