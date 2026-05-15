using GameHub.Application.Interface.Repository;
using GameHub.Domain.Entities;
using GameHub.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GameHub.Infrastructure.Repository
{
    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        private readonly ApplicationDbContext _context;

        public RefreshTokenRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task ActualizarAsync(RefreshToken token)
        {
            _context.RefreshTokens.Update(token);
            await _context.SaveChangesAsync();
        }

        public async Task GuardarAsync(RefreshToken token)
        {
            await _context.RefreshTokens.AddAsync(token);

            await _context.SaveChangesAsync();
        }

        public async Task<RefreshToken?> ObtenerAsync(string token)
        {
            return await _context.RefreshTokens
                .Include(x => x.Usuario)
                .FirstOrDefaultAsync(x => x.Token == token);
        }
    }
}