using AlgoritmProjekt.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgoritmProjekt.Utility.Handle_Levels.PCG
{
    class PCGEngine
    {
        LevelHandler levels;

        public PCGEngine(LevelHandler levels)
        {
            this.levels = levels;
        }
    }
}
