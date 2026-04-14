using Cas15.Models;
using Cas15.Operations;
using Cas15.Patrons;
using Cas15.Validation;

namespace Cas15;

//coordonne les trois couches (ne contient que la boucle principale)
public class Simulateur
{
    private PlanDeTest? _plan;
    private readonly Rapport _rapport = new();
    private readonly PlanDeTestFactory _factory = new();

    public void ChargerPlan(string? chemin = null)
    {
        _plan = chemin is not null
            ? _factory.Charger(chemin)
            : _factory.Generer();

        Console.WriteLine($"Plan chargé : {_plan.NbSequences} séquences, " +
                          $"{_plan.NiveauPatrons.Length} patrons, " +
                          $"seuil={_plan.SeuilPrecision:F0}");
    }

    public void Executer()
    {
        if (_plan is null)
            throw new InvalidOperationException("charger un plan avant d'exécuter");

        var opFactory = new OperationFactory(_plan.OperationsDisponibles);
        var rng       = new Random();

        //construit les patrons une seule fois
        var patrons = _plan.NiveauPatrons
            .Select(niveau => (Patron)new PatronAleatoire(niveau, opFactory))
            .ToList();

        _rapport.AjouterAnalyseur(new AnalyseurPrecision());
        _rapport.AjouterAnalyseur(new AnalyseurTemps());

        //l'algorithme du document (POUR chaque séquence POUR chaque patron)
        for (int i = 0; i < _plan.NbSequences; i++)
        {
            Sequence seq = GenererSequenceAleatoire(rng);

            foreach (var patron in patrons)
            {
                var (resultat, tempsMs) = patron.Appliquer(seq);

                _rapport.Ajouter(new ResultatExecution(
                    patron, seq.Copier(), resultat, tempsMs));
            }
        }

        _rapport.Analyser(_plan.SequenceMesure, new CalculateurPrecision(_plan.SeuilPrecision));
    }

    public void AfficherRapport()
    {
        Console.WriteLine(_rapport.GenererTexte());
    }

    private static Sequence GenererSequenceAleatoire(Random rng)
    {
        int[] valeurs = Enumerable.Range(0, 10)
            .Select(_ => rng.Next(int.MinValue, int.MaxValue))
            .ToArray();
        return new Sequence(valeurs);
    }
}
