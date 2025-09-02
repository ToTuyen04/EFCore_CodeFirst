namespace EFCore_CodeFirst.Db.Models
{
    public class InstrumentType
    {
        public int InstrumentTypeId { get; set; }
        public string Name { get; set; }

        //Navigation property
        public List<PlayerInstrument> PlayerInstruments { get; set; }
    }
}
