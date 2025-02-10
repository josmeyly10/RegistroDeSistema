using Microsoft.EntityFrameworkCore;
using RegistroSistemas.DAL;
using RegistroSistemas.Models;
using System.Linq.Expressions;

namespace RegistroSistemas.Services
{
    public class SistemaService(IDbContextFactory<Contexto> DbFactory)
    {
        public async Task<bool> Guardar(Sistema sistema)
        {
            if (!await Existe(sistema.SistemaId))
                return await Insertar(sistema);
            else
                return await Modificar(sistema);
        }

        private async Task<bool> Existe(int sistemaId)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            return await contexto.Sistema
                .AnyAsync(s => s.SistemaId == sistemaId);
        }

        private async Task<bool> Insertar(Sistema sistema)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            contexto.Sistema.Add(sistema);
            return await contexto.SaveChangesAsync() > 0;
        }

        private async Task<bool> Modificar(Sistema sistema)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            contexto.Update(sistema);
            return await contexto
                .SaveChangesAsync() > 0;
        }

        public async Task<Sistema?> Buscar(int sistemaId)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            return await contexto.Sistema
                .FirstOrDefaultAsync(s => s.SistemaId == sistemaId);
        }

        public async Task<bool> Eliminar(int sistemaId)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            return await contexto.Sistema
                .Where(s => s.SistemaId == sistemaId)
                .ExecuteDeleteAsync() > 0;

        }

        public async Task<List<Sistema>> Listar(Expression<Func<Sistema, bool>> criterio)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            return await contexto.Sistema
                .Where(criterio)
                .ToListAsync();
        }

        public async Task<bool> ExisteSistema(int sistemaId, string descripcion)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            return await contexto.Sistema
                .AnyAsync(s => s.SistemaId != sistemaId &&
                    s.Descripcion.ToLower().Equals(descripcion.ToLower()));

        }
    }

}
