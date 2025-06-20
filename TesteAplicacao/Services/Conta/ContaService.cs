using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TesteAplicacao.DTO;
using TesteAplicacao.Entities;
using TesteAplicacao.Infraestructure.Context;
using TesteAplicacao.Infraestructure.Exceptions;
using TesteAplicacao.Infraestructure.Extensions;
using TesteAplicacao.Infraestructure.Interfaces;

namespace TesteAplicacao.Services
{
    public class ContaService
    {
        private readonly MainDBContext _dbContext;
        private readonly IProdutoRep _produtoRep;
        private readonly IUsuarioRep _usuarioRep;
        private readonly UsuarioAutenticadoService _usuarioAutenticado;
        private readonly IContaRep _contaRep;
        private readonly ITransacoesContaRep _transacoesContaRep;

        public ContaService(MainDBContext dbContext, IProdutoRep produtoRep, IUsuarioRep usuarioRep,
            UsuarioAutenticadoService usuarioAutenticado, IContaRep contaRep, ITransacoesContaRep transacoesContaRep)
        {
            this._dbContext = dbContext;
            this._produtoRep = produtoRep;
            this._usuarioRep = usuarioRep;
            this._usuarioAutenticado = usuarioAutenticado;
            this._contaRep = contaRep;
            this._transacoesContaRep = transacoesContaRep;
        }

        public async Task<bool> InserirConta(InserirContaRequestDto request, uint usuario_id)
        {
            string numeroGerado;
            bool existe;

            do
            {
                numeroGerado = NumeroContaGenerator.GerarNumeroConta();
                existe = await _dbContext.Contas.AnyAsync(c => c.numero == numeroGerado);
            } while (existe);

            if (await _contaRep.GetById(usuario_id) != null)
            {
                throw new BusinessException("Somente pode ter uma conta por usuário.");
            }

            var conta = new Conta
            {
                usuario_id = usuario_id,
                banco = request.banco,
                ativa = request.ativa,
                data_criacao = DateTime.UtcNow,
                saldo_total = request.saldo_pendente + request.saldo_disponivel,
                saldo_disponivel = request.saldo_disponivel,
                saldo_pendente = request.saldo_pendente,
                numero = numeroGerado,

            };

            await _dbContext.RunInTransactionAsync(async() =>
            {
                await _contaRep.Add(conta);
            });

            return true;
        }

        public async Task<bool> InserirSaldo(InserirSaldoRequestDto request, uint conta_id)
        {
        
            var transacaoSaldo = new TransacaoConta
            {
                valor = request.valor,
                descricao = request.descricao,
                datahora_transacao = DateTime.UtcNow,
                conta_id = conta_id
            };

            var saldo = await _contaRep.GetByIdThrowsIfNull(conta_id);

            saldo.saldo_total = saldo.saldo_total + request.valor;

            await _dbContext.RunInTransactionAsync(async () =>
            {
                await _contaRep.Update(saldo);
                await _transacoesContaRep.Add(transacaoSaldo);
            });

            return true;
        }

        public async Task<PagedResult<GetContasDto>> GetContas(PaginacaoRequestDTO dto, bool ativo)
        {
            return await _contaRep.GetContas(dto, ativo);
        }
    }
}