using Microsoft.EntityFrameworkCore;
using TesteAplicacao.DTO;
using TesteAplicacao.Entities;
using TesteAplicacao.Infraestructure.Context;
using TesteAplicacao.Infraestructure.Extensions;
using TesteAplicacao.Infraestructure.Interfaces;

namespace TesteAplicacao.Infraestructure.Repository
{
    public class ContaRep : RepositoryBase<Conta>, IContaRep
    {

        public ContaRep(MainDBContext repositoryContext) : base(repositoryContext)
        {

        }

        public async Task<PagedResult<GetContasDto>> GetContas(PaginacaoRequestDTO dto/*, bool ativo*/)
        {
            var contas = await db.Contas
                .Include(u => u.Usuario)
                .Where(u => u.ativa == dto.ativo
                    && (EF.Functions.Like(u.banco, dto.filtro_generico)
                    || (String.IsNullOrWhiteSpace(dto.filtro_generico)
                    || EF.Functions.Like((string)u.numero!, dto.filtro_generico)
                     )))
                .Select(u => new GetContasDto
                {
                    usuario_id = u.usuario_id,
                    conta_id = u.conta_id,
                    numero = u.numero,
                    banco = u.banco,
                    ativa = u.ativa,
                    data_criacao = u.data_criacao,
                    saldo_disponivel = u.saldo_disponivel,
                    saldo_pendente = u.saldo_pendente,
                    saldo_total = u.saldo_total,
                    Usuario = new UsuarioBaseDto()
                    {
                        nome = u.Usuario.nome,
                        usuario_id = u.Usuario.usuario_id,
                    }

                })
                .PaginateAsync(dto);

            return contas.Result.Any() ? contas : PagedResult<GetContasDto>.Empty;
        }
    }
}
