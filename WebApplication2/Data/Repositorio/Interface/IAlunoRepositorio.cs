using System.Collections.Generic;
using WebApplication2.Models;
namespace WebApplication2.Data.Repositorio.Interface
{
    public interface IAlunoRepositorio
    {
        void InserirAluno(AlunoModel alunos);
        List<AlunoModel> BuscarAluno();
        AlunoModel BuscarId(int id);
        void ExcluirAluno(AlunoModel aluno);
        void EditarAluno(AlunoModel aluno);
    }
}