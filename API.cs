using Exiled.API.Features;
using MEC;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;

namespace SCPSense
{
    static internal class API
    {
        static private List<Player> SCPs = new();
        static private float interval = 1f;
        static public IEnumerator<float> InfoLoop()
        {
            while (true)
            {
                yield return Timing.WaitForSeconds(interval);
                string hint = GetHint().ToString();
                string[] lines = hint.Split('\n');
                foreach (Player player in SCPs)
                {
                    if (Main.Instance.Config.SeeDistance)
                    {
                        int index = 0;
                        var Sb = new StringBuilder("<size=");
                        if (Main.TextConfigs.TryGetValue(player.UserId, out var textConfig))
                        {
                            Sb.Append(textConfig.size);
                            Sb.Append("%><align=\"");
                            Sb.Append(textConfig.align);
                            Sb.Append("\">");
                        }
                        else
                        {
                            Sb.Append("60%><align=\"left\">");
                        }
                        foreach (string line in lines)
                        {
                            if (index == 0) // ignoring that first line with Rich Text Tags. ugly, but works.
                            {
                                index++;
                                continue;
                            }
                            var player1 = SCPs[index - 1];
                            if (player1 == player)
                            {
                                index++;
                                continue;
                            }
                            Sb.Append(line);
                            if (player1.Role == RoleType.Scp079)
                            {
                                Sb.Append("Distance: ");
                                Sb.Append(Math.Round(Vector3.Distance(player.Position, player1.Camera.targetPosition.position)));
                                Sb.Append(" m");
                            }
                            else
                            {
                                Sb.Append("Distance: ");
                                Sb.Append(Math.Round(Vector3.Distance(player.Position, player1.Position)));
                                Sb.Append(" m");
                            }
                            Sb.Append("\n");
                            index++;
                        }
                        player.ShowHint(Sb.ToString(), interval);
                    }
                    else
                    {
                        player.ShowHint(hint, interval);
                    }
                }
            }
        }

        static private string GetHPColor(Player player)
        {
            float hp = player.Health / player.MaxHealth;
            if (hp > 0.7f)
            {
                return "green";
            }
            else if (hp <= 0.7f && hp >= 0.4f)
            {
                return "yellow";
            }
            else
            {
                return "red";
            }
        }

        static private StringBuilder GetHint()
        {
            var hint = new StringBuilder();
            SCPs.Clear();
            foreach (Player player in Player.List)
            {
                if (player.IsScp && player.IsAlive)
                {
                    hint.Append("\n");
                    SCPs.Add(player);
                    string RoleName = Regex.Replace(player.Role.ToString().ToUpper(), @"(?<=SCP)", "-");
                    hint.Append(player.Nickname);
                    hint.Append(" | ");
                    hint.Append(RoleName);
                    hint.Append(" | ");
                    if (player.Role != RoleType.Scp079 && Main.Instance.Config.SeeHP)
                    {
                        if (player.ArtificialHealth > 0)
                        {
                            hint.Append("<color=green>AHP: ");
                            hint.Append((int)Math.Round((double)(100 * player.ArtificialHealth) / player.MaxArtificialHealth)); // calculates percetage
                            hint.Append("%</color> | ");
                        }
                        hint.Append("<color=");
                        hint.Append(GetHPColor(player));
                        hint.Append(">HP: ");
                        hint.Append((int)Math.Round((double)(100 * player.Health) / player.MaxHealth));
                        hint.Append("%</color> | ");
                    }
                }
            }
            return hint;
        }
    }
}
