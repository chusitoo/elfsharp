﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using ELFSharp.Utilities;

namespace ELFSharp.MachO
{
	public class UUID : Command
	{
        internal UUID(SimpleEndianessAwareReader reader, Stream stream) : base(reader, stream)
        {
            ID = ReadUUID();
        }

        public Guid ID { get; }

        private Guid ReadUUID()
        {
            var rawBytes = Reader.ReadBytes(16).TakeWhile(x => x != 0).ToArray();

            // Deal here with UUID endianess. Switch scheme is 4(r)-2(r)-2(r)-8(o)
            // where r is reverse, o is original order.
            Array.Reverse(rawBytes, 0, 4);
            Array.Reverse(rawBytes, 4, 2);
            Array.Reverse(rawBytes, 6, 2);

            var guid = new Guid(rawBytes);
            return guid;
        }
    }
}

