using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.Text;
using System.Threading.Tasks;
using BrokeProtocol.API;
using BrokeProtocol.Entities;

namespace CustomTriggerBoxesLib
{
    public class TestCommand : IScript
    {
        public TestCommand()
        {
            var cmds = new List<string> {"setbox"};
            CommandHandler.RegisterCommand(cmds, new Action<ShPlayer>(OnCommandInvoke));
            UnityEngine.Debug.Log("Registered " + nameof(TestCommand));
        }

        public void OnCommandInvoke(ShPlayer player)
        {
            player.manager.SpawnTriggerBox(new Vector3(player.GetPosition.x + 100, player.GetPosition.x + 100, player.GetPosition.x + 100), new Vector3(player.GetPosition.x - 100, player.GetPosition.x - 100, player.GetPosition.x - 100), tPlayer=>
            {
                UnityEngine.Debug.Log(tPlayer.username);
            });
        }
    }
}
