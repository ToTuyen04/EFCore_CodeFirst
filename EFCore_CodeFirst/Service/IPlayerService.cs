using EFCore_CodeFirst.Dto;
using EFCore_CodeFirst.Dto.Players;

namespace EFCore_CodeFirst.Service
{
    public interface IPlayerService
    {
        Task CreatePlayerAsync(CreatePlayerRequest playerRequest);
        Task<bool> UpdatePlayerAsync(int id, UpdatePlayerRequest playerRequest);
        Task<bool> DeletePlayerAsync(int id);
        Task<List<GetPlayerDetailResponse>> GetPlayerDetailAsync(int id);
        Task<PagedResponse<GetPlayerResponse>> GetPlayersAsync(UrlQueryParameters urlQueryParameters);
    }
}