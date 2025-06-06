//using Cuca_Api.Entities;
using System.Runtime.ExceptionServices;
using Microsoft.EntityFrameworkCore;

namespace Cuca_Api.Infraestructure.Context
{
    public class MainDBContext : DbContext
    {
        private readonly IHttpContextAccessor httpContext;

        public MainDBContext(DbContextOptions<MainDBContext> context, IHttpContextAccessor httpContext) : base(context)
        {
            this.httpContext = httpContext;
        }

        #region transaction
        public async Task RunInTransactionAsync(Func<Task> operations, Func<Task>? ifFails = null, MainDBContext? context = null)
        {
            context ??= this;

            if (context.Database.CurrentTransaction is not null)
            {
                await operations();

                return;
            }

            //await context.Database.OpenConnectionAsync();
            await using var transaction = await context.Database.BeginTransactionAsync();

            try
            {
                await operations();

                await context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();

                if (ifFails is not null)
                {
                    await ifFails();
                }

                ExceptionDispatchInfo.Capture(ex).Throw();
            }
            finally
            {
                //await context.Database.CloseConnectionAsync();
            }
        }
        #endregion

        #region DbSets

        //public DbSet<Membros> members { get; set; }
        //public DbSet<Rotacoes> rotations { get; set; }

        #endregion

        //public async Task<Usuario?> GetUsuarioLogado()
        //{
        //    var claim = GetClaimsTokenCognito();

        //    var usuario = await Usuarios
        //        .Where(u => u.userid_cognito == claim || u.email == claim)
        //        .FirstOrDefaultAsync();

        //    return usuario;
        //}

        //public async Task<uint> GetUsuarioLogadoId()
        //{
        //    var idUsuarioLogado = (await GetUsuarioLogado())?.usuario_id ?? 0;

        //    return idUsuarioLogado;
        //}
    }
}
