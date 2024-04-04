using SqlKata;

namespace CommonEx.Database.Entities
{
    internal class RecordCountEntity
    {
        [Column("count")]
        public int Count { get; set; }
    }
}