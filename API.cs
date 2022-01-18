using Exiled.API.Features;
using MEC;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;

namespace SCPSense
{
    static internal class API
    {
        static private float interval = 1f;
        static public IEnumerator<float> InfoLoop()
        {
            while (true)
            {
                SendHints();
                yield return Timing.WaitForSeconds(interval);
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

        static private void SendHints()
        {
            foreach (Player player in Player.Get(Team.SCP))
            {
                var hint = new StringBuilder();
                hint.Append("<size=");
                if (Main.TextConfigs.TryGetValue(player.UserId, out var textConfig))
                {
                    hint.Append(textConfig.size);
                    hint.Append("%><align=\"");
                    hint.Append(textConfig.align);
                    hint.Append("\">");
                }
                else
                {
                    hint.Append("60%><align=\"left\">");
                }
                foreach (Player teammate in Player.Get(Team.SCP))
                {                 
                    hint.Append("\n");
                    string RoleName = Regex.Replace(teammate.Role.ToString().ToUpper(), @"(?<=SCP)", "-");
                    hint.Append(teammate.Nickname);
                    hint.Append(" | ");
                    hint.Append(RoleName);
                    hint.Append(" | ");
                    if (teammate.Role != RoleType.Scp079 && Main.Instance.Config.SeeHP)
                    {
                        if (teammate.ArtificialHealth > 0)
                        {
                            hint.Append("<color=green>AHP: ");
                            hint.Append((int)Math.Round((double)(100 * teammate.ArtificialHealth) / teammate.MaxArtificialHealth)); // calculates percetage
                            hint.Append("%</color> | ");
                        }
                        hint.Append("<color=");
                        hint.Append(GetHPColor(teammate));
                        hint.Append(">HP: ");
                        hint.Append((int)Math.Round((double)(100 * teammate.Health) / teammate.MaxHealth));
                        hint.Append("%</color> | ");
                    }

                    if (Main.Instance.Config.SeeDistance)
                    {
                        if (teammate.Role == RoleType.Scp079)
                        {
                            hint.Append("Distance: ");
                            hint.Append(Math.Round(Vector3.Distance(player.Position, teammate.Camera.targetPosition.position)));
                            hint.Append(" m");
                        }
                        else
                        {
                            hint.Append("Distance: ");
                            hint.Append(Math.Round(Vector3.Distance(player.Position, teammate.Position)));
                            hint.Append(" m");
                        }
                    }
                }
                player.ShowHint(hint.ToString(), interval);
            }
        }
    }
}
