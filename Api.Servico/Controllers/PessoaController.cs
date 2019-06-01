using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using API.Persistencia;
using API.Entidade;
using System;

namespace API.Servico.Controllers
{
    public class PessoaController : ApiController
    {
        #region Repositorios
        private IRepositorioGenerico<Pessoa> repositorioPessoa;

        public PessoaController() { }
        public PessoaController(IRepositorioGenerico<Pessoa> _repositorioPessoa)
        {
            repositorioPessoa = _repositorioPessoa;
        }
        #endregion
        
        [HttpGet]
        [Route("api/Pessoa/Listar")]
        public object ListaPessoas()
        {
            object result;
            result = repositorioPessoa.Selecionar().ToList();
            return Json(result);
        }

        [HttpGet]
        [Route("api/Pessoa/Pesquisa")]
        public object ListaPessoasFiltro(string filtro)
        {
            object result;
            if (filtro == null) result = ListaPessoas();            
            else
                result = repositorioPessoa.Selecionar().Where(a =>               
                       a.DscBairro.ToUpper().Contains(filtro.ToUpper())
                    || a.DscCEP.ToUpper().Contains(filtro.ToUpper())
                    || a.DscCPF.ToUpper().Contains(filtro.ToUpper())
                    || a.DscEmail.ToUpper().Contains(filtro.ToUpper())
                    || a.DscEndereco.ToUpper().Contains(filtro.ToUpper())
                    || a.DscMunicipio.ToUpper().Contains(filtro.ToUpper())
                    || a.DscNome.ToUpper().Contains(filtro.ToUpper())
                    || a.DscTelCelular.ToUpper().Contains(filtro.ToUpper())
                    || a.DscUF.ToUpper().Contains(filtro.ToUpper())
                    ).ToList();
            return Json(result);
        }

        [HttpPost]
        [Route("api/Pessoa/Inserir")]
        public object InserirPessoa(Pessoa pessoa)
        {
            object result;
            try
            {
                repositorioPessoa.Inserir(pessoa);
                result = new { status = "sucesso", mensagem = "Pessoa Salva: " + pessoa.DscNome };
            }
            catch (Exception ex)
            {
                result = new { status = "erro", mensagem = "OCORREU UM ERRO: " + ex.Message };
            }
            return Json(result);
        }
    }
}

