using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
using System.IO;

namespace Pathfinder
{
    class DijkstraBot : AiBotBase
    {
        public Dijkstra Dijkstra;
        int index;

        public DijkstraBot(int x, int y)
            : base(x, y)
        {
            Dijkstra = new Dijkstra();
            index = 0;
        }
        protected override void ChooseNextGridLocation(Level level, Player plr)
        {
            if (Dijkstra.finalPath.Count() == 0)
            {
                Dijkstra.Build(level, this, plr);
            }
            Coord2 CurrentPos;
            CurrentPos = GridPosition;
            if (GridPosition != plr.GridPosition)
            {
                CurrentPos = Dijkstra.finalPath[index];
            }
            SetNextGridPosition(CurrentPos, level);
            index++;
        }
    }
}
