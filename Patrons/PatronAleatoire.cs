using Cas15.Operations;

namespace Cas15.Patrons;

//génération aléatoire pour le prototype
public class PatronAleatoire : Patron
{
    public PatronAleatoire(int niveau, OperationFactory factory) : base(niveau)
    {
        //remplit la liste avec exactement 'niveau' opérations aléatoires
        for (int i = 0; i < niveau; i++)
            Operations.Add(factory.CreerAleatoire());
    }
}
