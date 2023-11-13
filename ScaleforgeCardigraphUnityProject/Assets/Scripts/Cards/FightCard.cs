using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightCard : BaseCard
{
    public FightCardData Data
    { get; private set; }

    public FightCard(FightCardData data)
    {
        this.Data = data;
    }

    public int Healing => ConvertToCorruptedValue(Data.Healing);
    public int Attack => ConvertToCorruptedValue(Data.Attack);
    public int Armor => ConvertToCorruptedValue(Data.Armor);
    public int Crush => ConvertToCorruptedValue(Data.Crush);



}
