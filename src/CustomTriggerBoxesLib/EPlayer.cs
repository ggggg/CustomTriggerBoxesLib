#define DEBUG

using System;
using System.Collections.Generic;
using BrokeProtocol.Entities;

namespace CustomTriggerBoxesLib
{
    public partial class Core
    {
        private class EPlayer
        {
            private ShPlayer Player { get; }
            public List<Guid> Boxes { get; } = new List<Guid>();

            public EPlayer(ShPlayer player)
            {
                Player = player;
            }
        }
    }
}