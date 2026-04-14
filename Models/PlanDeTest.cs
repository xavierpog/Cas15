namespace Cas15.Models;

//contient tous les paramètres d'une session de test, rien de plus
public class PlanDeTest
{
    public int NbSequences { get; set; }

    //ex: [2, 3, 5]
    public int[] NiveauPatrons { get; set; } = [];

    //sous-ensemble des opérations pour cette session
    public int[] OperationsDisponibles { get; set; } = [];

    public SequenceMesure SequenceMesure { get; set; } = null!;

    //différence de score du seuil = rejet
    public double SeuilPrecision { get; set; }
}
