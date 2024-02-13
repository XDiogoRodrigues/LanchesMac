using LanchesMac.Models;

namespace LanchesMac.Repository.Interfaces;

public interface ILancheRepository
{
    IEnumerable<Lanche> Lanches { get; }
    IEnumerable<Lanche> LanchesPreferidos { get;}
    public Lanche GetLancheById(int lancheId);
}
