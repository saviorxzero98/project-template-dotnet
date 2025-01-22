using UUIDNext;

namespace CommonEx.Utilities.GuidGenerators
{
    public class GuidV4Generator : IGuidGenerator
    {
        public static GuidV4Generator Instance { get; } = new GuidV4Generator();

        public Guid Create()
        {
            return Uuid.NewRandom();
        }
    }
}
