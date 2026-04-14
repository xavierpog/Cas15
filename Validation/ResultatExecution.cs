using Cas15.Models;
using Cas15.Patrons;

namespace Cas15.Validation;

//capture tout ce qu'on sait sur une exécution patron+séquence
public class ResultatExecution
{
    public Patron Patron { get; }
    public Sequence SequenceOriginale { get; }
    public Sequence SequenceModifiee { get; }
    public long TempsMs { get; }

    //remplis lors de l'analyse, pas de l'exécution
    public double ScorePrecision { get; set; }
    public bool EstRejete { get; set; }

    public ResultatExecution(Patron patron, Sequence originale, Sequence modifiee, long tempsMs)
    {
        Patron           = patron;
        SequenceOriginale = originale;
        SequenceModifiee = modifiee;
        TempsMs          = tempsMs;
    }
}
