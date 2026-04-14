using Cas15.Models;

namespace Cas15.Operations;

//seul endroit où les classes concrètes sont mentionnées
public class OperationFactory
{
    private readonly int[] _disponibles;
    private readonly Random _rng = new();

    private static readonly Dictionary<int, Func<IOperation>> _catalogue = new()
    {
        { 1,  () => new Op01() },
        { 2,  () => new Op02() },
        { 3,  () => new Op03() },
        { 4,  () => new Op04() },
        { 5,  () => new Op05() },
        { 6,  () => new Op06() },
        { 7,  () => new Op07() },
        { 8,  () => new Op08() },
        { 9,  () => new Op09() },
        { 10, () => new Op10() },
        { 11, () => new Op11() },
        { 12, () => new Op12() },
        { 13, () => new Op13() },
        { 14, () => new Op14() },
        { 15, () => new Op15() },
    };

    public OperationFactory(int[] operationsDisponibles)
    {
        if (operationsDisponibles.Length == 0)
            throw new ArgumentException("au moins une opération doit être disponible");
        _disponibles = operationsDisponibles;
    }

    public IOperation Creer(int type)
    {
        if (!_disponibles.Contains(type))
            throw new ArgumentException($"opération {type} non autorisée dans ce plan");
        return _catalogue[type]();
    }

    //choisit un type aléatoire parmi ceux autorisés
    public IOperation CreerAleatoire() =>
        Creer(_disponibles[_rng.Next(_disponibles.Length)]);
}
