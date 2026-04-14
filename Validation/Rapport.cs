using System.Text;
using Cas15.Models;
using Cas15.Patrons;

namespace Cas15.Validation;

//collecte les résultats et produit le rapport final
public class Rapport
{
    private readonly List<ResultatExecution> _resultats = [];
    private readonly List<IAnalyseur> _analyseurs = [];

    public void Ajouter(ResultatExecution r) => _resultats.Add(r);

    public void AjouterAnalyseur(IAnalyseur a) => _analyseurs.Add(a);

    //calcule score + estRejete pour chaque résultat (se fait une seule fois après l'exécution)
    public void Analyser(SequenceMesure mesure, CalculateurPrecision calc)
    {
        foreach (var r in _resultats)
        {
            r.ScorePrecision = calc.Calculer(r, mesure);
            r.EstRejete      = !calc.EstAccepte(r.ScorePrecision);
        }
    }

    public string GenererTexte()
    {
        var sb = new StringBuilder();

        int total   = _resultats.Count;
        int rejetes = _resultats.Count(r => r.EstRejete);
        double pctRejet = total > 0 ? (double)rejetes / total * 100 : 0;

        sb.AppendLine("╔══════════════════════════════════════╗");
        sb.AppendLine("║         RAPPORT D'EXÉCUTION          ║");
        sb.AppendLine("╚══════════════════════════════════════╝");
        sb.AppendLine($"  Nombre de tests     : {total}");
        sb.AppendLine($"  Rejets              : {rejetes} ({pctRejet:F1}%)");
        sb.AppendLine();

        //chaque analyseur injecté ajoute sa section
        foreach (var analyseur in _analyseurs)
        {
            sb.AppendLine(analyseur.Analyser(this));
        }

        return sb.ToString();
    }

    //helpers utilisés par les analyseurs
    public IEnumerable<ResultatExecution> GetNonRejetes() =>
        _resultats.Where(r => !r.EstRejete);

    public IEnumerable<(Patron patron, double scoreMoyen)> GetMeilleursPatronsPrecision(int n) =>
        _resultats
            .Where(r => !r.EstRejete)
            .GroupBy(r => r.Patron)
            .Select(g => (patron: g.Key, scoreMoyen: g.Average(r => r.ScorePrecision)))
            .OrderBy(t => t.scoreMoyen)
            .Take(n);

    public IEnumerable<(Patron patron, double tempsMoyen)> GetMeilleursPatronsTemps(int n) =>
        _resultats
            .GroupBy(r => r.Patron)
            .Select(g => (patron: g.Key, tempsMoyen: g.Average(r => r.TempsMs)))
            .OrderBy(t => t.tempsMoyen)
            .Take(n);
}
