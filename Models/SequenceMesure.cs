namespace Cas15.Models;

//la référence fixe, passe pas par un patron
public class SequenceMesure
{
    public int[] Valeurs { get; }

    public SequenceMesure(int[] valeurs)
    {
        if (valeurs.Length != 10)
            throw new ArgumentException("la séquence de mesure doit avoir 10 valeurs");
        Valeurs = (int[])valeurs.Clone();
    }

    //calcule l'écart absolu total entre la référence et une séquence modifiée
    public double Comparer(Sequence s)
    {
        int[] v = s.GetValeurs();
        double ecartTotal = 0;
        for (int i = 0; i < 10; i++)
            ecartTotal += Math.Abs((long)Valeurs[i] - v[i]);
        return ecartTotal;
    }

    public override string ToString() =>
        $"[{string.Join(", ", Valeurs)}]";
}
