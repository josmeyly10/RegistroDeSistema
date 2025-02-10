using Microsoft.EntityFrameworkCore;
using RegistroSistemas.Models;
using System.Net.Sockets;

namespace RegistroSistemas.DAL
{
    public class Contexto : DbContext{

        public Contexto(DbContextOptions<Contexto> options) :base(options){ }
        public DbSet<Sistema> Sistema { get; set; } // tabla de sistemas
    }
}