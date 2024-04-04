using SqlKata;

namespace CommonEx.Database.Entities
{
    public class RecordCountEntity
    {
        [Column("count")]
        public int Count { get; set; }
    }
}