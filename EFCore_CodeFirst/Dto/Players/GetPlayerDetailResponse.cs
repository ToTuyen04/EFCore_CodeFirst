using EFCore_CodeFirst.Db.Models;
using EFCore_CodeFirst.Dto.PlayerInstrument;

namespace EFCore_CodeFirst.Dto.Players
{
    public class GetPlayerDetailResponse
    {
        public int PlayerId { get; set; }
        public string NickName { get; set; }
        public DateTime JoinedDate { get; set; }
        public List<GetPlayerInstrumentResponse> PlayerInstruments { get; set; } = new List<GetPlayerInstrumentResponse>();
        public int TotalInstruments => PlayerInstruments?.Count ?? 0;
    }
}
