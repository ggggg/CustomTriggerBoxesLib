#define DEBUG

using System.EnterpriseServices;
using System.Linq;
using BrokeProtocol.API;
using BrokeProtocol.Entities;
using BrokeProtocol.Managers;
using UnityEngine;
using UnityEngine.UIElements;

namespace CustomTriggerBoxesLib
{
    public partial class Core
    {
        public class Events : IScript
        {
            [Target(GameSourceEvent.ManagerUpdate, ExecutionMode.Override)]
            public void OnUpdate(SvManager svManager)
            {
                foreach (var box in Instance.TriggerBoxes)
                {
                    var center = (box.Value.Pos1 + box.Value.Pos2) * 0.5f;
                    var scale = box.Value.Pos1 - box.Value.Pos2;
                    var halfExtents = scale * 0.5f;
                    var check = Physics.BoxCastAll(center, halfExtents, Vector3.up);
                    if (check == null || !check.Any())
                    {
                        continue;
                    }
                    // Debug.Log("check: "+check.Length);
                    var players = check.Select(hit => hit.transform.gameObject.GetComponent<ShPlayer>())
                        .Where(player => player).ToArray();
                    //Debug.Log("players: "+players.Length);
                    foreach (var player in players)
                    {
                        var ePlayer = Instance.PlayerHandler[player.ID].Boxes;
                        if (ePlayer.Contains(box.Key))
                        {
                            // Debug.Log("continueing: " + player.username);
                            continue;
                        }
                        box.Value.OnEnter(player);
                        ePlayer.Add(box.Key);
                    }
                    foreach (var ePlayer in Instance.PlayerHandler.Where(x=>x.Value.Boxes.Contains(box.Key)))
                    {
                        if (players.All(x => x.ID != ePlayer.Key))
                        {
                            ePlayer.Value.Boxes.Remove(box.Key);
                        }
                    }
                }
            }

            [Target(GameSourceEvent.PlayerInitialize, ExecutionMode.Event)]
            public void OnLogin(ShPlayer player)
            {
                Instance.PlayerHandler.Add(player.ID, new EPlayer(player));
            }

            [Target(GameSourceEvent.PlayerDestroy, ExecutionMode.Event)]
            public void OnLeave(ShPlayer player)
            {
                Instance.PlayerHandler.Remove(player.ID);
            }

        }
    }
}