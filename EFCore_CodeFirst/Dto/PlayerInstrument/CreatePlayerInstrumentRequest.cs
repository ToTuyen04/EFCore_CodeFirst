using System.ComponentModel.DataAnnotations;

namespace EFCore_CodeFirst.Dto.PlayerInstrument
{
    public class CreatePlayerInstrumentRequest
    {
        [Required]
        public int InstrumentTypeId { get; set; }
        public string ModelName { get; set; }
        public string Level { get; set; }
    }
}