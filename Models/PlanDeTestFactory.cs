using Cas15.Models;

namespace Cas15;

//isole la logique de construction du plan — le reste ne sait pas d'où ça vient (faut être agile!)
public class PlanDeTestFactory
{
    private static readonly Random _rng = new();

    //génère un plan aléatoire pour le prototype
    public PlanDeTest Generer()
    {
        int nbPatrons = _rng.Next(3, 7);
        int[] niveaux = Enumerable.Range(0, nbPatrons)
            .Select(_ => _rng.Next(1, 6))
            .ToArray();

        return new PlanDeTest
        {
            NbSequences        = _rng.Next(5, 16),
            NiveauPatrons      = niveaux,
            OperationsDisponibles = Enumerable.Range(1, 15).ToArray(),
            SequenceMesure     = GenererSequenceMesure(),
            SeuilPrecision     = 60.0 //seuil en %
        };
    }

    //chargement depuis un fichier texte simple (une valeur par ligne, sections séparées par ---)
    public PlanDeTest Charger(string chemin)
    {
        if (!File.Exists(chemin))
            throw new FileNotFoundException($"plan introuvable : {chemin}");

        string[] lignes = File.ReadAllLines(chemin)
            .Where(l => !string.IsNullOrWhiteSpace(l))
            .ToArray();

        //format attendu: NbSequences / NiveauPatrons / OpsDisponibles / SeuilPrecision / SequenceMesure (10 valeurs)
        int idx = 0;
        int nbSeq = int.Parse(lignes[idx++]);
        int[] niveaux = lignes[idx++].Split(',').Select(int.Parse).ToArray();
        int[] ops     = lignes[idx++].Split(',').Select(int.Parse).ToArray();
        double seuil  = double.Parse(lignes[idx++]);
        int[] mesure  = lignes[idx++].Split(',').Select(int.Parse).ToArray();

        return new PlanDeTest
        {
            NbSequences           = nbSeq,
            NiveauPatrons         = niveaux,
            OperationsDisponibles = ops,
            SeuilPrecision        = seuil,
            SequenceMesure        = new SequenceMesure(mesure)
        };
    }

    private static SequenceMesure GenererSequenceMesure()
    {
        //valeurs arbitraires pour la cible du prototype
        int[] valeurs = [100, 200, 300, 400, 500, 600, 700, 800, 900, 1000];
        return new SequenceMesure(valeurs);
    }
}
