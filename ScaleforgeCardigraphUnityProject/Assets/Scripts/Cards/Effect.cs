using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Effect
{
    private static uint EffectCount = 0;
    public uint Id  // Is the unique Id of the specific effect
    { get; private set; }

    public EffectData Data
    { get; private set;}

    public int RoundsActive = 0;

    public Effect(EffectData data)
    {
        this.Data = data;
        EffectCount++;
        Id = EffectCount;
    }

    /// <summary>
    /// Effect was used in the current round.
    /// </summary>
    /// <returns>If the effect should be removed.</returns>
    public bool NextRound()
    {
        RoundsActive += 1;
        return RoundsActive >= Data.RoundDuration;
    }

}
