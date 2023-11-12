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
        while (maxRounds > round)
        {
            foreach (KeyValuePair<int, List<FightParticipant>> team in Teams)
            {
                // TODO do some ui stuff
                Debug.Log($"Team {team.Key} will start its round");

                foreach (var attacker in team.Value)
                {
                    Debug.Log($"{attacker.name} turn:");
                    var attackerEffects = attacker.ParticipantEffects;

                    // Needs to skip turn?
                    var skip = attacker.IsDead;
                    if (skip)
                    {
                        Debug.Log($"{attacker.name} is dead and skips.");
                        continue; 
                    }

                    // Add turn begin boni from effects
                    var healthEffectBonus = 0;
                    var armorEffectBonus = 0;
                    foreach (var effect in attackerEffects)
                    {
                        if (effect.SkipRound)
                        {
                            skip = true;
                            break;
                        }

                        healthEffectBonus += effect.OneTimeHeal;
                        armorEffectBonus += effect.OneTimeArmor;
                    }

                    if (skip)
                    {
                        Debug.Log($"{attacker.name} needs to skip.");
                        continue;
                    }

                    Debug.Log($"{attacker.name} gets bonus health of {healthEffectBonus} and bonus armor of {armorEffectBonus}");
                    attacker.AddHealth(healthEffectBonus);
                    attacker.AddArmor(armorEffectBonus);

                    // Pick a card
                    var card = attacker.PickFightCard;
                    if(card == null)
                    {
                        Debug.Log($"{attacker.name} has no cards and will skip");
                        continue;
                    }
                    // Pick a target
                    var possibleTargets = card.IsAttack ? GetNotTeamNpcs(team.Key) : GetTeamNpcs(team.Key);

                    // TODO Can there be no targets?
                    if (possibleTargets == null || possibleTargets.Count == 0)
                    {
                        Debug.Log($"{attacker.name} has no targets and will skip");
                        continue;
                    }

                    var target = attacker.PickNpcTargetForFight(possibleTargets, card);
                    Debug.Log($"{attacker.name} uses card {card.name} on {target.name}");
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

            if (OneTeamStanding())
            {
                EndFight();
                return;
            }
            round += 1;
        }
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
