using System;
namespace joaopedro.api.Models;

public class Pressao
{
    public Pressao()
    {
        CriadoEm = DateTime.Now;
        Id = Guid.NewGuid().ToString();
    }

    public string? Id { get; set; } = Guid.NewGuid().ToString();
    public string? Nome { get; set; }
    public int PressaoMaxima { get; set; }
    public int PressaoMinima { get; set; }
    public bool Classificacao { get; set; } = false;
    public DateTime DataRegistro { get; set; } = DateTime.Now;
}