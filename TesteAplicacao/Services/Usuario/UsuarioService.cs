﻿using Projeto_Aplicado_II_API.Infrastructure.Extensions;
using TesteAplicacao.DTO;
using TesteAplicacao.Entities;
using TesteAplicacao.Infraestructure.Context;
using TesteAplicacao.Infraestructure.Exceptions;
using TesteAplicacao.Infraestructure.Interfaces;

namespace TesteAplicacao.Services
{
    public class UsuarioService
    {
        private readonly MainDBContext _dbContext;
        private readonly IProdutoRep _produtoRep;
        private readonly IUsuarioRep _usuarioRep;
        
        
        public UsuarioService(MainDBContext dbContext, IProdutoRep produtoRep, IUsuarioRep usuarioRep)
        {
            this._dbContext = dbContext;
            this._produtoRep = produtoRep;
            this._usuarioRep = usuarioRep;
        }

        public async Task<bool> InserirUsuario(InserirUsuarioRequestDto request)
        {
            (byte[] senhaHash, byte[] saltHash) = request.senha.HashPassword();

            var usuario = new Usuario
            {
                nome = request.nome,
                email = request.email,
                telefone = request.telefone,
                ativo = true,
                datahora_insercao = DateTime.UtcNow,
                senha_hash = senhaHash,
                senha_salt = saltHash
            };

            await _dbContext.RunInTransactionAsync(async () =>
            {
                await _usuarioRep.Add(usuario);
            });

            return true;
        }


        public async Task<bool> AlterarUsuario(AlterarUsuarioRequestDto request, uint usuario_id)
        {
            var usuario = await _usuarioRep.GetById(usuario_id);

            if (usuario is null)
            {
                throw new BusinessException("Usuário não encontrado.");
            }

            if (!usuario.ativo)
            {
                throw new BusinessException("Você não pode alterar um usuário que está inativo, antes disso deixe-o ativo");
            }

            usuario.nome = request.nome;
            usuario.email = request.email;
            usuario.telefone = request.telefone;
            

            await _dbContext.RunInTransactionAsync(async () =>
            {
                await _usuarioRep.Update(usuario);
            });

            return true;
        }

        public async Task<bool> AlterarSenha(AlterarSenhaUsuarioRequestDto request, uint usuario_id)
        {
            var usuario = await _usuarioRep.GetById(usuario_id);

            if (usuario is null)
            {
                throw new BusinessException("Usuário não encontrado.");
            }

            if (!usuario.ativo)
            {
                throw new BusinessException("Você não pode alterar um usuário inativo. Ative-o primeiro.");
            }

            var senhaCorreta = request.SenhaAtual.VerifyPassword(usuario.senha_hash, usuario.senha_salt);
            if (!senhaCorreta)
            {
                throw new BusinessException("Senha atual incorreta.");
            }

            (byte[] novaSenhaHash, byte[] novoSalt) = request.NovaSenha.HashPassword();

            usuario.senha_hash = novaSenhaHash;
            usuario.senha_salt = novoSalt;

            await _dbContext.RunInTransactionAsync(async () =>
            {
                await _usuarioRep.Update(usuario);
            });

            return true;
        }


        public async Task<bool> AtivarInativarUsuario(uint usuario_id)
        {
            var usuario = await _usuarioRep.GetById(usuario_id);

            if (usuario is null)
            {
                throw new BusinessException("Usuário não encontrado.");
            }

           usuario.ativo = !usuario.ativo;

            await _dbContext.RunInTransactionAsync(async () =>
            {
                await _usuarioRep.Update(usuario);
            });

            return true;
        }
    }
}
