﻿using UUIDNext.Generator;

namespace CommonEx.Utilities.GuidGenerators
{
    public class GuidV7Generator : IGuidGenerator
    {
        private static readonly UuidV7Generator _generator = new UuidV7Generator();
        public static GuidV7Generator Instance { get; } = new GuidV7Generator();

        public Guid Create()
        {
            return _generator.New();
        }
    }
}
