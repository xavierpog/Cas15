using Cas15.Models;

namespace Cas15.Operations;

//code commun aux 15 opérations
public abstract class OperationBase : IOperation
{
    public abstract string Nom { get; }
    public abstract void Executer(Sequence s);

    //garde un long puis ramène en int
    protected static int Clamp(long valeur) =>
        (int)Math.Clamp(valeur, int.MinValue, int.MaxValue);
}
