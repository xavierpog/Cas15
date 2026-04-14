namespace Cas15.Validation;

//chaque analyseur produit une section du rapport final
public interface IAnalyseur
{
    string Analyser(Rapport rapport);
}
