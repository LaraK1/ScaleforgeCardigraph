using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class FightManager : MonoBehaviour
{
    [SerializeField]
    private List<Npc> participants;

    private Dictionary<int, List<FightParticipant>> Teams;
    private int round = 0;
    private int maxRounds = 10;
    private List<FightParticipant> GetTeamNpcs(int teamId) => Teams[teamId];
    private List<FightParticipant> GetNotTeamNpcs(int teamId) => Teams.Where(x => x.Key != teamId).SelectMany(d => d.Value).ToList();

    public void StartFight(List<Npc> participants)
    {
        this.participants = participants;
        StartFight();
    }

    public void StartFight()
    {
        Teams = new Dictionary<int, List<FightParticipant>>();
        foreach (var npc in participants)
        {
            List<FightParticipant> team;
            if (npc != null && npc.Fighter != null && Teams.TryGetValue(npc.Fighter.Team, out team))
            {
                team.Add(npc.Fighter);
            }
            else
            {
                Teams[npc.Fighter.Team] = new List<FightParticipant>();
                Teams[npc.Fighter.Team].Add(npc.Fighter);
            }
        }

        StartRound();
    }

    public void StartRound()
    {
        // Main fight loop
        // Will stop after only one team remains or the max Rounds where hit
        while (maxRounds > round)
        {
            // For each team:
            foreach (KeyValuePair<int, List<FightParticipant>> team in Teams)
            {
                // TODO do some ui stuff
                Debug.Log($"Team {team.Key} will start its round");

                // For each npc in team:
                foreach (var attacker in team.Value)
                {
                    Debug.Log($"{attacker.name} turn:");
                    var attackerEffects = attacker.ParticipantEffects;

                    // Needs to skip turn because it is dead?
                    var skip = attacker.IsDead;
                    if (skip)
                    {
                        Debug.Log($"{attacker.name} is dead and skips.");
                        continue; 
                    }

                    // Sum up turn begin boni from effects
                    var healthEffectBonus = 0;
                    var armorEffectBonus = 0;
                    foreach (var effect in attackerEffects)
                    {
                        if (effect.Data.SkipRound)
                        {
                            skip = true;
                            break;
                        }

                        healthEffectBonus += effect.Data.OneTimeHeal;
                        armorEffectBonus += effect.Data.OneTimeArmor;
                    }

                    // Needs to skip because of a effect?
                    if (skip)
                    {
                        Debug.Log($"{attacker.name} needs to skip.");
                        continue;
                    }

                    // Add effect boni
                    Debug.Log($"{attacker.name} gets following effect boni:");
                    attacker.AddHealth(healthEffectBonus);
                    attacker.AddArmor(armorEffectBonus);

                    // Pick a card
                    var card = attacker.PickFightCard;
                    if(card == null)
                    {
                        Debug.Log($"{attacker.name} has no cards and will skip");
                        continue;
                    }

                    // Find all possible targets
                    var possibleTargets = card.Data.IsAttack ? GetNotTeamNpcs(team.Key) : GetTeamNpcs(team.Key);

                    // TODO Can there be no targets?
                    if (possibleTargets == null || possibleTargets.Count == 0)
                    {
#if DEBUG
                        Debug.LogWarning($"{attacker.name} has no targets and will skip");
#endif
                        continue;
                    }

                    // Pick a target
                    var target = attacker.PickNpcTargetForFight(possibleTargets, card.Data);
                    Debug.Log($"{attacker.name} uses card {card.Data.name} on {target.name}");
                    attacker.UseFightCard(card);
                    target.TargetOfCard(card, attackerEffects);
                }
            }

            // Update effects:
            foreach (KeyValuePair<int, List<FightParticipant>> team in Teams)
            {
                foreach (var participant in team.Value)
                {
                    participant.UpdateEffects();
                }
            }

            // Check if only one team is left
            if (OneTeamStanding())
            {
                EndFight();
                return;
            }
            round += 1;
        }
        Debug.Log("Fight ended after max rounds was reached");
        EndFight();
    }

    private bool OneTeamStanding()
    {
        int teamsAlive = 0;

        foreach (KeyValuePair<int, List<FightParticipant>> team in Teams)
        {
            foreach (var participant in team.Value)
            {
                if(!participant.IsDead)
                {
                    teamsAlive += 1;
                    break; // at least one member in team is alive
                }
            }
            if (teamsAlive > 1) // more than one team has at least one alive member
                return false;

        }

        return true;
    }

    private void EndFight()
    {
        Debug.Log("End");
    }
}
