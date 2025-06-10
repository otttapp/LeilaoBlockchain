//using Cuca_Api.Entities;
using System.Reflection;
using System.Runtime.ExceptionServices;
using Microsoft.EntityFrameworkCore;
using TesteAplicacao.Entities;

namespace Cuca_Api.Infraestructure.Context
{
    public class MainDBContext : DbContext
    {
        public MainDBContext(DbContextOptions<MainDBContext> context) : base(context)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(modelBuilder);
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

        #region DBSets
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Produto> Produtos { get; set; }

        #endregion


    }
}
