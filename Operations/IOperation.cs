using Cas15.Models;

namespace Cas15.Operations;

//contrat minimal
public interface IOperation
{
    string Nom { get; }

    //modifie s EN PLACE, pas de retour
    void Executer(Sequence s);
}
