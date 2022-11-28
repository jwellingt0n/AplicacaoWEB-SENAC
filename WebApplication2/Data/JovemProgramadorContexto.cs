using Microsoft.EntityFrameworkCore;
using WebApplication2.Data.Mapeamento;
using WebApplication2.Models;
namespace WebApplication2.Data
{
    public class JovemProgramadorContexto : DbContext
    {
        public JovemProgramadorContexto(DbContextOptions<JovemProgramadorContexto> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AlunoMapping());
        }
        public DbSet<AlunoModel> Aluno { get; set; }
    }
}