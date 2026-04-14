using System.Text;
using Cas15.Patrons;

namespace Cas15.Validation;

//top 10 séquences précises + top 3 patrons par score
public class AnalyseurPrecision : IAnalyseur
{
    public string Analyser(Rapport rapport)
    {
        var sb = new StringBuilder();
        var nonRejetes = rapport.GetNonRejetes();

        sb.AppendLine("=== Top 10 séquences les plus précises ===");
        var top10 = nonRejetes
            .OrderBy(r => r.ScorePrecision)
            .Take(10);

        int rang = 1;
        foreach (var r in top10)
            sb.AppendLine($"  {rang++}. score={r.ScorePrecision:F0}  seq={r.SequenceOriginale}  patron={r.Patron.GetNom()}");

        sb.AppendLine();
        sb.AppendLine("=== Top 3 patrons les plus précis (score moyen) ===");
        var top3Patrons = rapport.GetMeilleursPatronsPrecision(3);

        rang = 1;
        foreach (var (patron, scoreMoyen) in top3Patrons)
            sb.AppendLine($"  {rang++}. {patron.GetNom()}  score moyen={scoreMoyen:F0}");

        return sb.ToString();
    }
}

//top 3 patrons par temps moyen d'exécution
public class AnalyseurTemps : IAnalyseur
{
    public string Analyser(Rapport rapport)
    {
        var sb = new StringBuilder();
        sb.AppendLine("=== Top 3 patrons les plus rapides (temps moyen ms) ===");

        var top3 = rapport.GetMeilleursPatronsTemps(3);

        int rang = 1;
        foreach (var (patron, tempsMoyen) in top3)
            sb.AppendLine($"  {rang++}. {patron.GetNom()}  moy={tempsMoyen:F2}ms");

        return sb.ToString();
    }
}
