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
        const double maxEcartParValeur = (double)uint.MaxValue; // 4 294 967 295
        double ecartNormalise = 0;
        for (int i = 0; i < 10; i++)
        {
            double ecart = Math.Abs((long)Valeurs[i] - v[i]);
            ecartNormalise += ecart / maxEcartParValeur * 100.0;
        }
        return ecartNormalise / 10.0; // moyenne sur 10 valeurs → score 0–100
    }
    public override string ToString() =>
        $"[{string.Join(", ", Valeurs)}]";
}
