using EFCore_CodeFirst.Db.Models;
using EFCore_CodeFirst.Dto;
using EFCore_CodeFirst.Dto.PlayerInstrument;
using EFCore_CodeFirst.Dto.Players;
using Microsoft.EntityFrameworkCore;

namespace EFCore_CodeFirst.Service
{
    public class PlayerService : IPlayerService
    {
        private readonly CodeFirstDemoContext _context;
        public PlayerService(CodeFirstDemoContext context)
        {
            _context = context;
        }
        public async Task CreatePlayerAsync(CreatePlayerRequest playerRequest)
        {
            var player = new Player
            {
                NickName = playerRequest.NickName,
                JoinedDate = DateTime.Now,
                Instruments = new List<PlayerInstrument>()
            };

            _context.Players.Add(player);
            await _context.SaveChangesAsync();

            //Tạo PlayerInstrument (nếu có)
            if (playerRequest.PlayerInstruments != null && playerRequest.PlayerInstruments.Any())
            {
                foreach (var instrumentRequest in playerRequest.PlayerInstruments)
                {
                    var playerInstrument = new PlayerInstrument
                    {
                        PlayerId = player.PlayerId,
                        InstrumentTypeId = instrumentRequest.InstrumentTypeId,
                        ModelName = instrumentRequest.ModelName ?? string.Empty,
                        Level = instrumentRequest.Level ?? "Beginner"
                    };

                    _context.PlayerInstruments.Add(playerInstrument);
                }
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> DeletePlayerAsync(int id)
        {
            var player = await _context.Players
                .Include(p => p.Instruments)
                .FirstOrDefaultAsync(p => p.PlayerId == id);
            if (player == null)
                return false;
            if (player.Instruments.Any())
            {
                _context.PlayerInstruments.RemoveRange(player.Instruments);
            }
            _context.Players.Remove(player);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<GetPlayerDetailResponse>> GetPlayerDetailAsync(int id)
        {
            var player = await _context.Players
                .Include(p => p.Instruments)
                .ThenInclude(pi => pi.InstrumentType)
                .Where(p => p.PlayerId == id)
                .Select(p => new GetPlayerDetailResponse
                {
                    NickName = p.NickName,
                    JoinedDate = p.JoinedDate,
                    PlayerInstruments = p.Instruments.Select(pi => new GetPlayerInstrumentResponse
                    {
                        PlayerInstrumentId = pi.PlayerInstrumentId,
                        InstrumentTypeId = pi.InstrumentTypeId,
                        InstrumentTypeName = pi.InstrumentType.Name,
                        ModelName = pi.ModelName,
                        Level = pi.Level
                    }).ToList()
                })
                .ToListAsync();
            return player;
        }

        public async Task<PagedResponse<GetPlayerResponse>> GetPlayersAsync(UrlQueryParameters urlQueryParameters)
        {
            var query = _context.Players.AsQueryable();

            //Tìm theo NickName nếu có SearchTerm
            if (!string.IsNullOrEmpty(urlQueryParameters.SearchTerm))
            {
                query = query.Where(p => p.NickName.Contains(urlQueryParameters.SearchTerm));
            }

            //Đếm tổng số record
            var totalRecords = await query.CountAsync();

            //Pagination
            var players = await query
                .Skip((urlQueryParameters.PageNumber - 1) * urlQueryParameters.PageSize)
                .Take(urlQueryParameters.PageSize)
                .Select(p => new GetPlayerResponse
                {
                    PlayerId = p.PlayerId,
                    NickName = p.NickName,
                    JoinedDate = p.JoinedDate,
                    InstrumentSubmittedCount = p.Instruments.Count()
                })
                .ToListAsync();

            var totalPages = (int)Math.Ceiling((double)totalRecords / urlQueryParameters.PageSize);

            return new PagedResponse<GetPlayerResponse>
            {
                Data = players,
                TotalRecords = totalRecords,
                PageNumber = urlQueryParameters.PageNumber,
                PageSize = urlQueryParameters.PageSize,
                TotalPages = totalPages
            };
        }

        public async Task<bool> UpdatePlayerAsync(int id, UpdatePlayerRequest playerRequest)
        {
            var player = await _context.Players.FirstOrDefaultAsync(p => p.PlayerId == id);
            if (player == null)
            {
                return false;
            }

            //Cập nhật player
            player.NickName = playerRequest.NickName;

            _context.Players.Update(player);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
