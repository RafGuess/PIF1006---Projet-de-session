/*
 Program.cs : Lance le programme principal 
 Auteurs : Julien Desrosiers, Lily Occhibelli, Océane Rakotoarisoa, Abderraouf Guessoum
 Cours : PIF1006 - Groupe 29
 Session : Automne 2024
 Projet : TP1
 */


using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;


namespace PIF1006_tp1
{   
    public class Automate
    {
        public State InitialState { get; private set; }     // Propriété représentant l'état initial de l'automate
        public State CurrentState { get; private set; }     // Propriété représentant l'état courant de l'automate
        public List<State> States { get; private set; }     // Liste des états de l'automate
        public bool isValid { get; private set; }           // Indicateur booléen pour savoir si l'automate est valide
        
        public List<Tuple<string, string>> _erreurs;        // Liste de Tuple contenant les erreurs de l'automate
        
        // Constructeur
        public Automate(string filePath) //string filePath
        {
            States = new List<State>();
            _erreurs = new List<Tuple<string, string>>();
            LoadFromFile(filePath);
        }
        
        // Méthode pour charger l'automate à partir d'un fichier
        private void LoadFromFile(string filePath)
        {
            // Vous devez pouvoir charger à partir d'un fichier quelconque. Cela peut être un fichier XML, JSON, texte, binaire, ...
            // P.ex. avec un fichier texte, vous pouvoir balayer ligne par ligne et interprété en séparant chaque ligne en un tableau de strings
            // dont le premier représente l'action, et la suite les arguments. L'équivalent de l'automate décrit manuellement dans la classe
            // Program pourrait être:
            //  state s0 1 1
            //  state s1 0 0
            //  state s2 0 0
            //  state s3 1 0
            //  transition s0 0 s1
            //  transition s0 1 s0
            //  transition s1 0 s1
            //  transition s1 1 s2
            //  transition s2 0 s1
            //  transition s3 1 s3
            //
            // Dans une boucle, on prend les lignes une par une:
            //   - Si le 1er terme est "state", on prend les arguments et on crée un état du même nom
            //     et on l'ajoute à une liste d'état; les 2 et 3e argument représentent alors si c'est un état final, puis si c'est l'état initial
            //   - Si c'est "transition" on cherche dans la liste d'état l'état qui a le nom en 1er argument et on ajoute la transition avec les 2 autres
            //     arguments à sa liste
            
            // Vérifie si le fichier existe avant de continuer
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"Fichier introuvable : {{filePath}}");
            }
            if (string.IsNullOrWhiteSpace(filePath))
            {
                throw new ArgumentNullException(nameof(filePath), "Le chemin du fichier ne peut pas être nul ou vide.");
            }  
            
            // Parcourt chaque ligne du fichier, identifie son type (état, transition ou erreur),
            foreach (var (ligne, index) in File.ReadLines(filePath).Select((line, index) => (line, index + 1)))
            {

                try
                {  
                    string[] mots = ligne.Split(' ');

                    switch (mots[0])
                    {   
                        // Traite la définition d'un état
                        case "state":
                            if (mots.Length!=4)
                            {
                                //_erreurs.Add(new Tuple<string, string>(index + " " + ligne, "La définition de l'état est incomplète"));
                                throw new Exception("La définition de l'état est incorecte");  
                                break;
                            }
                            
                            // Nom de l'état
                            string nomEtat = mots[1];
                            
                            // Si état final
                            bool isFinal = mots[2] == "1";
                            
                            // création de l'état
                            State state = new State(nomEtat, isFinal);
                            
                            // Ajout de l'état a la liste
                            States.Add(state);
                            
                            // Si état est initial
                            if (mots[3] == "1")
                            {
                                if (InitialState != null)
                                {
                                    throw new Exception("L'état initial est déjà défini");      // L'état ne sera pas initial, car il en avait déjà une, mais reste un état quand même
                                }
                                InitialState = state;
                            }
                            break;
                        
                        // Traite la définition d'une transition entre deux états
                        case "transition":
                            if (mots.Length!=4)
                            {
                                //_erreurs.Add(new Tuple<string, string>(index + " " + ligne, "La définition de la transition est incomplète."));
                                throw new Exception("La définition de la transition est incorecte.");  
                                break;
                            }
                            
                            string initialStateTransition = mots[1];    // L'état initial de transition
                            char input = mots[2][0];                    // L'input
                            string transiteTo = mots[3];                // L'état transité
                            
                            // Cherche si les états font bien partie de la liste d'état
                            State stateFound = States.FirstOrDefault(state => state.Name == initialStateTransition);
                            State transitStateFound = States.FirstOrDefault(state => state.Name == transiteTo);
                            
                            // Si un des états n'est pas dans la liste message d'erreur
                            if (stateFound == null || transitStateFound == null)
                            {
                                //_erreurs.Add(new Tuple<string, string>(index + " " + ligne, "L'état initial ou l'état de transition est introuvable."));
                                throw new Exception("L'état initial ou l'état de transition est introuvable."); 
                            }

                            // Vérification du déterminisme : Si l'état a déjà une transition pour cet input
                            bool inputExiste = stateFound.Transitions.Any(t => t.Input == input);
                            if (inputExiste)
                            {
                                isValid = false; // L'automate devient invalide
                                throw new Exception($"Automate non-déterministe détecté à l'état '{stateFound.Name}' avec l'entrée '{input}'.");
                            }
                            
                            // Ajouter la transition seulement si elle est valide
                            stateFound.Transitions.Add(new Transition(input, transitStateFound));
                            break;

                        default:
                            //_erreurs.Add(new Tuple<string, string>(index + " " + ligne, "Mot non reconnue"));
                            throw new Exception("Mot non reconnue"); 
                            break;
                    }
                }
                catch(Exception ex)
                {
                    _erreurs.Add(new Tuple<string, string>($"Erreur à la ligne: {index}: " + ligne, ex.Message));
                }
            }
            
            // Valide la structure de l'automate
            isValid = ValidateAutomate();
            
            // Si l'automate est invalide, afficher les erreurs et arrêter
            if (!isValid)
            {
                Console.WriteLine("L'automate n'est pas valide. Voici les erreurs détectées :");
                foreach (var error in _erreurs)
                {
                    Console.WriteLine($"[Erreur] {error.Item1}: {error.Item2}");
                }
                return; // Arrête le chargement
            }
            
            
            // Affiche les erreurs s'il y en a
            if (_erreurs.Any())
            {
                foreach (var error in _erreurs)
                {
                    Console.WriteLine($"{error.Item1}, {error.Item2}");
                }
            }
            else
            {
                Console.WriteLine("[ACCEPTATION] : Félicitations votre automate n'a aucune erreur !");
            }

            // Considérez que:
            //   - S'il y a d'autres termes, les lignes pourraient être ignorées;   // OK
            //   - Si l'état n'est pas trouvé dans la liste (p.ex. l'état est référencé mais n'existe pas (encore)), la transition est ignorée  // OK
            //   - Après lecture du fichier:
            //          - si l'automate du fichier n'est pas déterministe (vous devrez penser à comment vérifier cela -> l'état et la transition    // OK
            //            en défaut doit être indiquée à l'utilisateur), OU
            //          - si l'automate n'a aucun état, ou aucun état initial  // OK 
            //     l'automate est considéré comme "invalide" (la propriété IsValid doit alors valoir faux) // OK
            //   - Lorsque des lignes (ou l'automate) sont ignorées ou à la fin l'automate rejeté, cela doit être indiqué à l'utilisateur
            //     à la console avec la ligne/raison du "rejet".
        }
        
        // Méthode pour valider la structure complète de l'automate
        private bool ValidateAutomate()
        {
            bool valide = true;
            try
            {
                // Vérifie si l'automate à des états
                if (!States.Any())
                {
                    valide = false;
                    throw new Exception("Il n'y a pas d'états dans l'automate");
                }

                // Vérifie si l'automate a un état initial
                if (InitialState != null)
                {
                    CurrentState = InitialState;
                }
                else
                {
                    valide = false;
                    throw new Exception("Il n'y a pas d'état initial dans l'automate");
                }

                // Vérifie les transitions pour détecter les cas de non-déterminisme
                foreach (var state in States)
                {
                    // Regroupe les transitions par input
                    var groupedInputs = state.Transitions.GroupBy(t => t.Input);

                    foreach (var group in groupedInputs)
                    {
                        // Si un même input a plus d'une transition, l'automate est non-déterministe
                        if (group.Count() > 1)
                        {
                            valide = false;
                            _erreurs.Add(new Tuple<string, string>("", $"Automate non-déterministe détecté à l'état '{state.Name}' avec l'entrée '{group.Key}'."));
                        }
                    }
                }

                // Valide la présence d'un chemin vers un état final
                valide = valide && TrouverCheminFinal(InitialState);
            }
            catch (Exception ex)
            {
                _erreurs.Add(new Tuple<string, string>("Erreur lors de la validation de l'automate: ", ex.Message));
            }
            return valide;
        }


        private bool TrouverCheminFinal(State currentState, List<State> stateAlreadyCheck = null)
        {
            try
            {
                // Si stateAlreadyCheck est null, on l'initialise à une nouvelle liste.
                if (stateAlreadyCheck == null)
                {
                    stateAlreadyCheck = new List<State>();
                }
            
                // Ajouter l'état courant à la liste des états déjà vérifiés
                stateAlreadyCheck.Add(currentState);
            
                foreach (var transition in currentState.Transitions)
                {
                    State state = transition.TransiteTo;

                    if (!state.IsFinal && !stateAlreadyCheck.Contains(state))
                    {
                        Console.WriteLine($"Exploration de l'état: {state.Name}");
                        // Appel récursif pour explorer cet état
                        bool result = TrouverCheminFinal(state, stateAlreadyCheck);
                        if (result)
                        {
                            // Si l'état final a été trouvé dans les appels récursifs, retourner true
                            return true;
                        }
                    }
                
                    else if(state.IsFinal)
                    {   
                        Console.WriteLine($"L'automate à un état final accessible!");
                        return true;
                    }
                    else
                    {
                        Console.WriteLine($"L'état {state.Name} à déjà été vérifié.");
                    }
                
                }
                
                _erreurs.Add(new Tuple<string, string>("Erreur lors de la validation de l'atteinte d'un chemin final", "Il ny a pas de chemin atteignant un état final dans l'automate"));
                return false;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        
        
        // Méthode pour valider les inputs de l'utilisateur
        public bool ValidateInput(string userInput)
        {
            try
            {
                bool isValid = true;
                Reset();    // Reset l'automate à son état initial
            
                char[] inputs = userInput.ToCharArray();

                foreach (var input in inputs)
                {
                
                    State EtatInitialTransition = CurrentState;
                    State etatTransite = EtatInitialTransition.Transitions.FirstOrDefault(transit => transit.Input == input)
                        ?.TransiteTo;   // Cherche l'état de transition pour l'état courant et l'input donné

                    if (etatTransite == null)
                    {
                        // Il n'y a pas d'état à transiter
                        Console.WriteLine($"Aucune transition trouvée pour l'état '{EtatInitialTransition.Name}' avec l'input '{input}'");
                        isValid = false;
                        break;
                    }
                
                    // Met à jour l'état courant avec l'état de destination de la transition
                    CurrentState = etatTransite;
                    Console.WriteLine($"Transition de l'état '{EtatInitialTransition.Name}' avec l'input '{input}' vers l'état '{etatTransite.Name}'");
                }

                // Vérifie si le dernier état est un état final
                if (!CurrentState.IsFinal)
                {
                    isValid = false;   // Si l'état courant n'est pas final, l'input est invalide
                }
            
                // Vous devez transformer l'input en une liste / un tableau de caractères (char) et les lire un par un;         // OK
                // L'automate doit maintenant à jour son "CurrentState" en suivant les transitions et en respectant l'input.    // Ok
                // Considérez que l'automate est déterministe et que même si dans les faits on aurait pu mettre plusieurs
                // transitions possibles pour un état et un input donné, le 1er trouvé dans la liste est le chemin emprunté.    // Ne devrais pas arriver donc ok
                // Si aucune transition n'est trouvé pour un état courant et l'input donné, cela doit retourner faux;           // A verifier
                // Si tous les caractères ont été pris en compte, on vérifie si l'état courant est final ou non et on retourne  // OK
                // vrai ou faux selon.

                // VOUS DEVEZ OBLIGATOIREMENT AFFICHER la suite des états actuel, input lu, et état transité pour qu'on puisse
                // suivre le déroulement de l'analyse.

                return isValid;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public void Reset()
        {
            // Vous devez faire du code pour indiquer ce que signifie réinitialiser l'automate avant chaque validation.
            CurrentState = InitialState;
        }
        
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            Console.WriteLine("\n--- Représentation de l'automate ---");
            Console.WriteLine("- État Initial [], État Final () -\n");
            foreach (var state in States)
            {
                // Afficher l'état initial avec des crochets carrés
                if (state == InitialState && !state.IsFinal)
                {
                    sb.AppendLine($"[{state.Name}]");
                }
                else if (state == InitialState && state.IsFinal)
                {
                    sb.AppendLine($"([{state.Name}])");
                }
                // Afficher les états finaux avec des parenthèses
                else if (state.IsFinal)
                {
                    sb.AppendLine($"({state.Name})");
                }
                else
                {
                    sb.AppendLine(state.Name);
                }

                // Afficher les transitions pour chaque état
                foreach (var transition in state.Transitions)
                {
                    sb.AppendLine($"--{transition.Input}--> {transition.TransiteTo.Name}");
                }

                sb.AppendLine();
            }

            return sb.ToString();
        }
        
        
        
    }
}
