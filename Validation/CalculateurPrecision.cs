using Cas15.Models;

namespace Cas15.Validation;

//encapsule la formule de comparaison — changer la formule ne touche rien d'autre
public class CalculateurPrecision
{
    public double SeuilAcceptation { get; }

    public CalculateurPrecision(double seuil)
    {
        SeuilAcceptation = seuil;
    }

    //somme des écarts absolus valeur par valeur
    public double Calculer(ResultatExecution r, SequenceMesure mesure) =>
        mesure.Comparer(r.SequenceModifiee);

    public bool EstAccepte(double score) => score <= SeuilAcceptation;
}
