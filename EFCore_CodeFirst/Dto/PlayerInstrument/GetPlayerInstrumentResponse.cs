namespace EFCore_CodeFirst.Dto.PlayerInstrument
{
    public class GetPlayerInstrumentResponse
    {
        public int PlayerInstrumentId { get; set; }
        public int InstrumentTypeId { get; set; }
        public string InstrumentTypeName { get; set; }
        public string ModelName { get; set; }
        public string Level { get; set; }
    }
}