using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MinhaAppMvcDesafio.Models;
using MinhaAppMvcDesafio.Repositories.Interfaces;
using MinhaAppMvcDesafio.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MinhaAppMvcDesafio.Controllers
{
    public class FornecedorController : Controller
    {
        private readonly IFornecedorRepository _fornecedorRepository;
        private readonly IMapper _mapper;
        private const int _paginaInicial = 110;

        public FornecedorController(IFornecedorRepository fornecedorRepository, IMapper mapper)
        {
            _fornecedorRepository = fornecedorRepository;
            _mapper = mapper;
        }

        [Route("lista-fornecedores/{pagina:int}")]
        public async Task<IActionResult> Index(int pagina = 1)
        {
            ViewData["Titulo"]     = "Lista de Fornecedores";
            ViewData["Acao"]       = "";
            ViewData["Pagina"]     = pagina.ToString();
            // Posição do campo: dezena do valor do parâmetro
            ViewData["OrdCampo"] = pagina.ToString().Substring(pagina.ToString().Length - 2, 1);
            // Sentido do campo: unidade do valor do parâmetro
            ViewData["OrdSentido"] = pagina.ToString().Substring(pagina.ToString().Length - 1, 1);
            ViewData["QtdPaginas"] = _fornecedorRepository.ObterQtdPaginas().ToString();
            return View(_mapper.Map<IEnumerable<FornecedorViewModel>>(await _fornecedorRepository.ObterPaginados(pagina)));
        }
        //public JsonResult Listar(int pagina = 1, int registros = 5)
        //{
        //    var lista = _mapper.Map<IEnumerable<FornecedorViewModel>>(_fornecedorRepository.ObterTodos());
        //    lista = lista.Skip((pagina - 1) * registros).Take(registros);
        //    return Json(lista);
        //}

        [Route("dados-fornecedor/{id:int}")]
        public async Task<IActionResult> Cadastro(int id = 0)
        {
            var fornecedor = new FornecedorViewModel();
            if (id != 0)
            {
                fornecedor = await ObterFornecedorEndereco(id);
                if (fornecedor == null) return NotFound();
            }
            ViewData["Titulo"] = "Cadastro de Fornecedores";
            ViewData["Id"] = id.ToString();
            return View(fornecedor);
        }

        [Route("salvar-fornecedor")]
        [HttpPost]
        public async Task<IActionResult> Salvar(FornecedorViewModel fornecedorViewModel)
        {
            int id = int.Parse(Request.Form["Id"].ToString());
            if (id != 0 && id != fornecedorViewModel.Id) return NotFound();
            if (!ModelState.IsValid) return View(fornecedorViewModel);
            var fornecedor = _mapper.Map<Fornecedor>(fornecedorViewModel);
            if (id == 0)
                await _fornecedorRepository.Adicionar(fornecedor);
            else
                await _fornecedorRepository.Atualizar(fornecedor);
            //if (!OperacaoValida()) return View(fornecedorViewModel);
            //if (!OperacaoValida()) return View(await ObterFornecedorProdutosEndereco(id));
            ViewData["Acao"] = "S";
            ViewBag.Acao = "S";
            return RedirectToAction("Index", new { pagina = _paginaInicial });
        }

        [Route("excluir-fornecedor/{id:int}")]
        public async Task<IActionResult> Excluir(int id)
        {
            var fornecedor = await ObterFornecedorEndereco(id);
            if (fornecedor == null) return NotFound();
            await _fornecedorRepository.Remover(id);
            //if (!OperacaoValida()) return View(fornecedor);
            ViewData["Acao"] = "E";
            return RedirectToAction("Index", new { pagina = _paginaInicial });
        }

        private async Task<FornecedorViewModel> ObterFornecedorEndereco(int id)
        {
            return _mapper.Map<FornecedorViewModel>(await _fornecedorRepository.ObterFornecedorEndereco(id));
        }

        private async Task<FornecedorViewModel> ObterFornecedorProdutosEndereco(int id)
        {
            return _mapper.Map<FornecedorViewModel>(await _fornecedorRepository.ObterFornecedorProdutosEndereco(id));
        }
    }
}