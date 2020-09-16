using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MinhaAppMvcDesafio.Models;
using MinhaAppMvcDesafio.Repositories.Interfaces;
using MinhaAppMvcDesafio.ViewModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;

namespace MinhaAppMvcDesafio.Controllers
{
    public class ProdutoController : Controller
    {
        private readonly IProdutoRepository _produtoRepository;
        private readonly IFornecedorRepository _fornecedorRepository;
        private readonly IMapper _mapper;
        private const int _paginaInicial = 110;

        public ProdutoController(IProdutoRepository produtoRepository, IFornecedorRepository fornecedorRepository, IMapper mapper)
        {
            _produtoRepository = produtoRepository;
            _fornecedorRepository = fornecedorRepository;
            _mapper = mapper;
        }

        [Route("lista-produtos/{pagina:int}")]
        public async Task<IActionResult> Index(int pagina = _paginaInicial)
        {
            ViewData["Titulo"]     = "Lista de Produtos";
            ViewData["Acao"]       = "";
            ViewData["Pagina"]     = pagina.ToString();
            // Posição do campo: dezena do valor do parâmetro
            ViewData["OrdCampo"]   = pagina.ToString().Substring(pagina.ToString().Length - 2, 1);
            // Sentido do campo: unidade do valor do parâmetro
            ViewData["OrdSentido"] = pagina.ToString().Substring(pagina.ToString().Length - 1, 1);  
            ViewData["QtdPaginas"] = _produtoRepository.ObterQtdPaginas().ToString();
            return View(_mapper.Map<IEnumerable<ProdutoViewModel>>(await _produtoRepository.ObterPaginados(pagina)));
        }

        [Route("dados-produto/{id:int}")]
        public async Task<IActionResult> Cadastro(int id = 0)
        {
            var produto = new ProdutoViewModel();
            string fornecedorId = "";
            ViewData["NomeImagem"] = "";
            if (id != 0)
            {
                produto = await ObterProdutoFornecedor(id);
                if (produto == null) return NotFound();
                fornecedorId = produto.FornecedorId.ToString();
                if (!string.IsNullOrEmpty(produto.Imagem))
                    ViewData["NomeImagem"] = produto.Imagem.Split("_")[1].ToUpper(); // obtém apenas o nome físico do arquivo, após o "_"
            }
            ViewData["Titulo"] = "Cadastro de Produtos";
            ViewData["Id"] = id.ToString();
            ViewData["ComboFornecedores"] = ComboFornecedores(fornecedorId);
            return View(produto);
        }

        public string ComboFornecedores(string id = "")
        {
            string resultado = "";
            Task<List<Fornecedor>> itens = _fornecedorRepository.ObterTodos();
            if (itens == null) return "";
            foreach (var item in itens.Result)
            {
                string vChecked = (item.Id.ToString() == id ? " selected" : "");
                resultado += "<option value='" + item.Id.ToString() + "'" + vChecked + ">" + item.Nome.ToUpper() + "</option>";
            }
            return resultado;
        }

        [Route("salvar-cadastro")]
        [HttpPost]
        public async Task<IActionResult> Salvar(ProdutoViewModel produtoViewModel)
        {
            produtoViewModel.Valor /= 100;  // Essa divisão serve para evidenciar a parte decimal do valor
            int id = int.Parse(Request.Form["Id"].ToString());
            if (id != 0 && id != produtoViewModel.Id) return NotFound();
            if (!ModelState.IsValid) return View(produtoViewModel);
            var produto = _mapper.Map<Produto>(produtoViewModel);
            if (produtoViewModel.ImagemUpload != null)
            {
                var imgPrefixo = DateTime.Now.ToString("yyyyMMddHHmmss") + "_";
                if (!await UploadArquivo(produtoViewModel.ImagemUpload, imgPrefixo))
                    return View(produtoViewModel);
                produto.Imagem = imgPrefixo + produtoViewModel.ImagemUpload.FileName;
            }
            if (id == 0)
                await _produtoRepository.Adicionar(produto);
            else
                await _produtoRepository.Atualizar(produto);
            ViewData["Acao"] = "S";
            ViewBag.Acao = "S";
            return RedirectToAction("Index", new { pagina = _paginaInicial });
        }

        private async Task<bool> UploadArquivo(IFormFile arquivo, string imgPrefixo)
        {
            if (arquivo.Length <= 0) return false;
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/imagens/produtos", imgPrefixo + arquivo.FileName);
            if (System.IO.File.Exists(path))
            {
                ModelState.AddModelError(string.Empty, "Já existe um arquivo com este nome!");
                return false;
            }
            using (var stream = new FileStream(path, FileMode.Create))
            {
                await arquivo.CopyToAsync(stream);
            }
            return true;
        }

        [Route("excluir-produto/{id:int}")]
        public async Task<IActionResult> Excluir(int id)
        {
            var produto = await ObterProdutoFornecedor(id);
            if (produto == null) return NotFound();
            await _produtoRepository.Remover(id);
            ViewData["Acao"] = "E";
            return RedirectToAction("Index", new { pagina = _paginaInicial });
        }

        private async Task<ProdutoViewModel> ObterProdutoFornecedor(int id)
        {
            return _mapper.Map<ProdutoViewModel>(await _produtoRepository.ObterProdutoFornecedor(id));
        }

        //[Route("movimentacao-estoque/{pagina:int}")]
        //public async Task<IActionResult> MovimentacaoEstoque(int pagina = _paginaInicial)
        //{
        //    ViewData["Titulo"] = "Movimentação de Estoque";
        //    ViewData["Pagina"] = pagina.ToString();
        //    // Posição do campo: dezena do valor do parâmetro
        //    ViewData["OrdCampo"] = pagina.ToString().Substring(pagina.ToString().Length - 2, 1);
        //    // Sentido do campo: unidade do valor do parâmetro
        //    ViewData["OrdSentido"] = pagina.ToString().Substring(pagina.ToString().Length - 1, 1);
        //    ViewData["QtdPaginas"] = _produtoRepository.ObterQtdPaginas().ToString();
        //    return View("MovimentacaoEstoque", _mapper.Map<IEnumerable<EstoqueViewModel>>(await _produtoRepository.MovimentacaoEstoque(1, pagina)));
        //}
    }
}