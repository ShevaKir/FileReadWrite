using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;

namespace _20220421_FileReadWrite
{
    class PlayerRepository 
    {
        const uint FINAL_SIGNATURE = 0xFFFFFFFF;
        const int FINAL_SIGNATURE_BYTES = 4;

        private Stream _resource;
        private BinaryWriter _writer;
        private BinaryReader _reader;
        private long _lastPosition;

        public PlayerRepository(Stream resource)
        {
            _resource = resource;
            _writer = new BinaryWriter(_resource);
            _reader = new BinaryReader(_resource);
        }

        // проверка пустой ли ресурс
        public bool Empty => _resource.Length <= FINAL_SIGNATURE_BYTES;

        //колличество игроков
        public int Count => (int)(_resource.Length - FINAL_SIGNATURE_BYTES) / Player.TOTAL_BYTES;

        // проверка в конце ли ресурса
        public bool IsEnd => _resource.Position >= (_resource.Length - FINAL_SIGNATURE_BYTES);

        // вернуться в начало
        public void ToStart()
        {
            _resource.Position = 0;
        }

        public int Write(Player player)
        {
            int sizeCurrentPlayer = 0;

            long startPosition = _resource.Position;

            //запись фиксированных размеров
            _writer.Write(player.NumberPlayer);
            _writer.Write(player.IdTeam);
            _writer.Write((byte)player.Role);

            long position = _resource.Position + Player.NAME_MAX_BYTES;
            _writer.Write(player.Name);
            _resource.Position = position;

            _writer.Write(player.Surname);
            _resource.Position = position + Player.SURNAME_MAX_BYTES;

            if (_resource.Position > _lastPosition)
            {
                _lastPosition = _resource.Position;
            }

            sizeCurrentPlayer = (int)(_resource.Position - startPosition);

            return sizeCurrentPlayer;
        }

        public int Write(Player player, int index)
        {
            if (index < 0)
            {
                throw new IndexOutOfRangeException(nameof(index));
            }

            _resource.Position = index * Player.TOTAL_BYTES;

            return Write(player);
        }

    }
}
