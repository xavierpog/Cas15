namespace Cas15.Models;

//séquence de 10 entiers qui va à travers les patrons
public class Sequence
{
    private readonly int[] _valeurs = new int[10];

    public Sequence(int[] valeurs)
    {
        if (valeurs.Length != 10)
            throw new ArgumentException("une séquence doit contenir exactement 10 valeurs");
        Array.Copy(valeurs, _valeurs, 10);
    }

    //copie indépendante
    public Sequence Copier() => new Sequence(_valeurs);

    public int[] GetValeurs() => (int[])_valeurs.Clone();

    public void SetValeur(int index, int valeur)
    {
        if (index < 0 || index > 9)
            throw new IndexOutOfRangeException($"index {index} hors des bornes [0-9]");
        _valeurs[index] = valeur;
    }

    public int GetValeur(int index) => _valeurs[index];

    public override string ToString() =>
        $"[{string.Join(", ", _valeurs)}]";
}
