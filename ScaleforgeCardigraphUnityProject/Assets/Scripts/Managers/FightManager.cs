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
            if (Teams.TryGetValue(npc.Fighter.Team, out team))
            {
                team.Add(npc.Fighter);
            }
            else
            {
                Teams[npc.Fighter.Team] = new List<FightParticipant>();
                Teams[npc.Fighter.Team].Add(npc.Fighter);
            }
        }
    }

    public void StartRound()
    {
        while (maxRounds > round)
        {
            foreach (KeyValuePair<int, List<FightParticipant>> team in Teams)
            {
                // TODO do some ui stuff
                Debug.Log($"Team {team.Key} will start its round");

                foreach (var participant in team.Value)
                {
                    var participantEffects = participant.ParticipantEffects.GetComplete;

                    // Needs to skip turn?
                    var skip = participant.IsDead || participantEffects.Any(x => x.SkipRound);

                    if (skip)
                        return;

                    // Pick a card
                    var card = participant.PickFightCard;

                    // Pick a target
                    var possibleTargets = card.IsAttack ? GetNotTeamNpcs(team.Key) : GetTeamNpcs(team.Key);
                    var target = participant.PickNpcTargetForFight(possibleTargets, card);
                    participant.UseFightCard(card);
                    Debug.Log($"{participant.name} uses card {card.name} on {target.name}");
                    target.TargetOfCard(card, participantEffects);
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
