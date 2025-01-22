using UUIDNext;
using UUIDNext.Generator;

namespace CommonEx.Utilities.GuidGenerators
{
    public class GuidV8Generator : IGuidGenerator
    {        public static GuidV8Generator Instance { get; } = new GuidV8Generator();

        public Guid Create()
        {
            return Uuid.NewDatabaseFriendly(Database.SqlServer);
        }
    }
}
