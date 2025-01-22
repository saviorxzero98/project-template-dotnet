using UUIDNext;

namespace CommonEx.Utilities.GuidGenerators
{
    public class GuidV7Generator : IGuidGenerator
    {
        public static GuidV7Generator Instance { get; } = new GuidV7Generator();

        public Guid Create()
        {
            return Uuid.NewDatabaseFriendly(Database.PostgreSql);
        }
    }
}
