#define DEBUG

using BrokeProtocol.API;
using BrokeProtocol.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using BrokeProtocol.Managers;
using UnityEngine;

namespace CustomTriggerBoxesLib
{
    public partial class Core : Plugin
    {
        public static Core Instance { get; internal set; }

        private void Log(string message)
        {
#if DEBUG
            UnityEngine.Debug.Log(message);
#endif
        }

        public Core()
        {
            Instance = this;
            Info = new PluginInfo("CustomTriggerBoxesLib", "CustomTriggerBoxesLib")
            {
                Description = "Made to allow custom trigger boxes to be spawned. By The-g#7731",
            };
            UnityEngine.Debug.Log("TriggerBoxesLib by: The-g#7731 loaded");
        }

        public Dictionary<Guid, TriggerBox> TriggerBoxes { get; } = new Dictionary<Guid, TriggerBox>();
        private Dictionary<int, EPlayer> PlayerHandler { get; } = new Dictionary<int, EPlayer>();
        public class TriggerBox
        {

            public Vector3 Pos1 { get; set; }
            public Vector3 Pos2 { get; set; }

            public Action<ShPlayer> OnEnter { get; }

            public Action<ShPlayer> OnExit { get; }

            private TriggerBox(Vector3 pos1, Vector3 pos2)
            {
                Pos1 = pos1;
                Pos2 = pos2;
            }

            public TriggerBox(Vector3 pos1, Vector3 pos2, Action<ShPlayer> onEnter) : this(pos1, pos2)
            {
                OnEnter = onEnter;
            }

            public TriggerBox(Vector3 pos1, Vector3 pos2, Action<ShPlayer> onEnter, Action<ShPlayer> onExit) : this(pos1, pos2, onEnter)
            {
                OnExit = onExit;
            }
        }
    }
    public static class Extensions
    {
        public static Core.TriggerBox SpawnTriggerBox(this ShManager manager, Vector3 pos1, Vector3 pos2, Action<ShPlayer> onEnter, Action<ShPlayer> onExit)
        {
            var box = new Core.TriggerBox(pos1, pos2, onEnter, onExit);
            Core.Instance.TriggerBoxes.Add(Guid.NewGuid(), box);
            return box;
        }

        public static Core.TriggerBox SpawnTriggerBox(this ShManager manager, Vector3 pos1, Vector3 pos2, Action<ShPlayer> OnEnter)
        {
            return manager.SpawnTriggerBox(pos1, pos2, OnEnter, null);
        }
    }
}