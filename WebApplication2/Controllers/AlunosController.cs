﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using WebApplication2.Data.Repositorio.Interface;
using WebApplication2.Models;
namespace WebApplication2.Controllers
{
    public class AlunosController : Controller
    {
        private readonly IAlunoRepositorio _alunoRepositorio;
        private readonly IConfiguration _configuration;
        public AlunosController(IAlunoRepositorio alunoRepositorio, IConfiguration configuration)
        {
            _alunoRepositorio = alunoRepositorio;
            _configuration = configuration;
        }
        public IActionResult Index()
        {
            try
            {
                var alunos = _alunoRepositorio.BuscarAluno();
                return View(alunos);
            }
            catch (System.Exception)
            {
                TempData["MensagemErro"] = "Ops! Houve algum problema no sistema! Volte mais tarde.";
                return View();
            }
        }
        public IActionResult Adicionar()
        {
            return View();
        }
        public IActionResult InserirAluno(AlunoModel alunos)
        {
            try
            {
                _alunoRepositorio.InserirAluno(alunos);
                TempData["MensagemSucesso"] = "Aluno adicionado com sucesso!";

                return RedirectToAction("Index");
            }
            catch (System.Exception)
            {
                TempData["MensagemErro"] = "Ops! Houve algum problema no sistema! Volte mais tarde.";
                return View();

            }
        }
        public IActionResult Editar(int id)
        {
            try
            {
                var aluno = _alunoRepositorio.BuscarId(id);
                return View("Editar", aluno);
            }
            catch (System.Exception)
            {
                TempData["MensagemErro"] = "Ops! Houve algum problema no sistema! Volte mais tarde.";
                return View();

            }
        }
        public IActionResult RemoverAluno(int id)
        {
            try
            {
                var aluno = _alunoRepositorio.BuscarId(id);
                _alunoRepositorio.ExcluirAluno(aluno);
                TempData["MensagemSucesso"] = "Aluno removido com sucesso!";
                return RedirectToAction("Index");
            }
            catch (System.Exception)
            {
                TempData["MensagemErro"] = "Ops! Houve algum problema no sistema! Volte mais tarde.";
                return View();

            }
        }
        public IActionResult AtualizarAluno(AlunoModel aluno)
        {
            try
            {
                _alunoRepositorio.EditarAluno(aluno);
                TempData["MensagemSucesso"] = "Aluno editado com sucesso!";
                return RedirectToAction("Index");
            }
            catch (System.Exception)
            {
                TempData["MensagemErro"] = "Ops! Houve algum problema no sistema! Volte mais tarde.";
                return View();

            }
        }
        public async Task<IActionResult> BuscarEndereco(string cep)
        {
            try
            {
                cep = cep.Replace("-", "");
                EnderecoModel enderecoModel = new EnderecoModel();
                using var client = new HttpClient();
                var result = await client.GetAsync(_configuration.GetSection("ApiCep")["BaseUrl"] + cep + "/json");
                if (result.IsSuccessStatusCode)
                {
                    enderecoModel = JsonSerializer.Deserialize<EnderecoModel>(
                        await result.Content.ReadAsStringAsync(), new JsonSerializerOptions() { });
                }
                return View(enderecoModel);
            }
            catch(System.Exception)
            {
                TempData["MensagemErro"] = "Ops! Houve algum problema no sistema! Volte mais tarde.";
                return View();
            }
        }
    }
}