using Cas15.Models;

namespace Cas15.Operations;

//il y a sûrement un meilleur moyen de faire ça, mais j'ai commencé ce projet beaucoup trop tard pour me permettre de tout refactoriser

//d[0] = d[1] + d[2]
public class Op01 : OperationBase
{
    public override string Nom => "Op01_SommeDeux";
    public override void Executer(Sequence s) =>
        s.SetValeur(0, Clamp((long)s.GetValeur(1) + s.GetValeur(2)));
}

//d[3] = -d[3]
public class Op02 : OperationBase
{
    public override string Nom => "Op02_Inversion";
    public override void Executer(Sequence s) =>
        s.SetValeur(3, s.GetValeur(3) == int.MinValue ? int.MaxValue : -s.GetValeur(3));
}

//d[5] = d[5] * 2
public class Op03 : OperationBase
{
    public override string Nom => "Op03_Double";
    public override void Executer(Sequence s) =>
        s.SetValeur(5, Clamp((long)s.GetValeur(5) * 2));
}

//d[9] -= 1 000 000
public class Op04 : OperationBase
{
    public override string Nom => "Op04_DecalageNegatif";
    public override void Executer(Sequence s) =>
        s.SetValeur(9, Clamp((long)s.GetValeur(9) - 1_000_000));
}

//échange d[0] et d[9]
public class Op05 : OperationBase
{
    public override string Nom => "Op05_Swap09";
    public override void Executer(Sequence s)
    {
        int tmp = s.GetValeur(0);
        s.SetValeur(0, s.GetValeur(9));
        s.SetValeur(9, tmp);
    }
}

//d[4] = d[4] % 1000, évite division par zéro
public class Op06 : OperationBase
{
    public override string Nom => "Op06_Modulo1000";
    public override void Executer(Sequence s)
    {
        int v = s.GetValeur(4);
        s.SetValeur(4, v != 0 ? v % 1000 : 0);
    }
}

//d[2] = d[2] + d[7]
public class Op07 : OperationBase
{
    public override string Nom => "Op07_SommeDecalee";
    public override void Executer(Sequence s) =>
        s.SetValeur(2, Clamp((long)s.GetValeur(2) + s.GetValeur(7)));
}

//d[1] = |d[1]|
public class Op08 : OperationBase
{
    public override string Nom => "Op08_ValeurAbsolue";
    public override void Executer(Sequence s)
    {
        int v = s.GetValeur(1);
        s.SetValeur(1, v == int.MinValue ? int.MaxValue : Math.Abs(v));
    }
}

//toutes les valeurs += 1
public class Op09 : OperationBase
{
    public override string Nom => "Op09_IncrementAll";
    public override void Executer(Sequence s)
    {
        for (int i = 0; i < 10; i++)
            s.SetValeur(i, Clamp((long)s.GetValeur(i) + 1));
    }
}

//d[6] = d[3] - d[5]
public class Op10 : OperationBase
{
    public override string Nom => "Op10_Difference35";
    public override void Executer(Sequence s) =>
        s.SetValeur(6, Clamp((long)s.GetValeur(3) - s.GetValeur(5)));
}

//d[7] = d[7] >> 1 (décalage bit droit)
public class Op11 : OperationBase
{
    public override string Nom => "Op11_ShiftDroit";
    public override void Executer(Sequence s) =>
        s.SetValeur(7, s.GetValeur(7) >> 1);
}

//d[8] = d[0] XOR d[9]
public class Op12 : OperationBase
{
    public override string Nom => "Op12_XOR09";
    public override void Executer(Sequence s) =>
        s.SetValeur(8, s.GetValeur(0) ^ s.GetValeur(9));
}

//d[3] += d[4] + d[5]
public class Op13 : OperationBase
{
    public override string Nom => "Op13_AccumulTrois";
    public override void Executer(Sequence s) =>
        s.SetValeur(3, Clamp((long)s.GetValeur(3) + s.GetValeur(4) + s.GetValeur(5)));
}

//d[5] = d[5] | d[2] (OR binaire)
public class Op14 : OperationBase
{
    public override string Nom => "Op14_OrBinaire";
    public override void Executer(Sequence s) =>
        s.SetValeur(5, s.GetValeur(5) | s.GetValeur(2));
}

//inverse l'ordre du tableau complet
public class Op15 : OperationBase
{
    public override string Nom => "Op15_Inversion";
    public override void Executer(Sequence s)
    {
        int[] v = s.GetValeurs();
        Array.Reverse(v);
        for (int i = 0; i < 10; i++)
            s.SetValeur(i, v[i]);
    }
}
