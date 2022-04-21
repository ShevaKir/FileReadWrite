using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _20220421_FileReadWrite
{
    class Player
    {
        public const int NAME_MAX_BYTES = 30;
        public const int SURNAME_MAX_BYTES = 30;
        public const int TOTAL_BYTES = NAME_MAX_BYTES + SURNAME_MAX_BYTES + (sizeof(int) * 2) + sizeof(byte);

        public string Name { get; set; }

        public string Surname { get; set; }

        public int IdTeam { get; set; }     // 4 bytes

        public int NumberPlayer { get; set; } // 4 bytes

        public Position Role { get; set; }  // 4 bytes

        public override string ToString()
        {
            return string.Format("{0};{1};{2};{3};{4}", Name, Surname, IdTeam, NumberPlayer, Role);
        }
    }
}
