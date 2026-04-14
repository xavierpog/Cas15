using System.Diagnostics;
using Cas15.Models;
using Cas15.Operations;

namespace Cas15.Patrons;

//groupe d'opérations séquentielles (mesure du temps est ici)
public abstract class Patron
{
    public int Niveau { get; }
    protected readonly List<IOperation> Operations = [];

    protected Patron(int niveau)
    {
        if (niveau < 1) throw new ArgumentException("un patron doit avoir au moins 1 opération");
        Niveau = niveau;
    }

    //copie la séquence, chronomètre, exécute, retourne résultat + temps
    public (Sequence resultat, long tempsMs) Appliquer(Sequence entree)
    {
        Sequence copie = entree.Copier();
        var chrono = Stopwatch.StartNew();

        foreach (var op in Operations)
            op.Executer(copie);

        chrono.Stop();
        return (copie, chrono.ElapsedMilliseconds);
    }

    public string GetNom() =>
        $"Patron-niv{Niveau}[{string.Join(",", Operations.Select(o => o.Nom))}]";

    public override string ToString() => GetNom();
}
