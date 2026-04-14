using Cas15;

//point d'entrée (passer un chemin en arg pour charger un fichier, sinon génération auto)
var sim = new Simulateur();
sim.ChargerPlan(args.Length > 0 ? args[0] : null);
sim.Executer();
sim.AfficherRapport();
