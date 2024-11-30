using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using Newtonsoft.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace PIF1006_tp1
{
    public class Automate
    {
        public State InitialState { get; private set; }
        public State CurrentState { get; private set; }
        public List<State> States { get; private set; }
        public bool IsValid { get; private set; }
        
        // 
        public List<Tuple<string, string>> _erreurs;
        
        public Automate(string filePath) //string filePath
        {
            States = new List<State>();
            _erreurs = new List<Tuple<string, string>>();
            LoadFromFile(filePath);
        }

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
            
            
            IsValid = true;
            
            // 
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"File not found: {filePath}");
            }
            if (string.IsNullOrWhiteSpace(filePath))
            {
                throw new ArgumentNullException(nameof(filePath), "File path cannot be null or empty.");
            }   
            
            //
            foreach (var (ligne, index) in File.ReadLines(filePath).Select((line, index) => (line, index + 1)))
            {

                try
                {  
                    string[] mots = ligne.Split(' ');

                    switch (mots[0])
                    {   
                        // 
                        case "state":
                            if (mots.Length < 4)
                            {
                                //_erreurs.Add(new Tuple<string, string>(index + " " + ligne, "State definition is incomplete."));
                                throw new Exception("State definition is incomplete");  
                                break;
                            }
                            
                            // Nom de l'etat
                            string nomEtat = mots[1];
                            
                            // si etat final
                            bool isFinal = mots[2] == "1";
                            
                            // creation de letat
                            State state = new State(nomEtat, isFinal);
                            
                            // Ajout de l'etat a la liste
                            States.Add(state);
                            
                            // Si etat initial
                            if (mots[3] == "1")
                            {
                                if (InitialState != null)
                                {
                                    throw new Exception("InitialState is already defined");      // Todo(): J'ai fait que l'etat ne va juste pas etre letat initial mais on garde quand meme letat dans la liste
                                }
                                InitialState = state;
                            }
                            break;
                        
                        //
                        case "transition":
                            if (mots.Length < 4)
                            {
                                //_erreurs.Add(new Tuple<string, string>(index + " " + ligne, "Transition definition is incomplete."));
                                throw new Exception("Transition definition is incomplete.");  
                                break;
                            }
                            
                            // Valeurs
                            string initialStateTransition = mots[1];
                            char input = mots[2][0];
                            string transiteTo = mots[3];
                            
                            // Cherche si les etats font bien partie de la liste d'etat
                            State stateFound = States.FirstOrDefault(state => state.Name == initialStateTransition);
                            State transitStateFound = States.FirstOrDefault(state => state.Name == transiteTo);
                            
                            // Si state ne sont pas dans la liste message derreur
                            if (stateFound == null || transitStateFound == null)
                            {
                                //_erreurs.Add(new Tuple<string, string>(index + " " + ligne, "Initial state or transition state not found."));
                                throw new Exception("Initial state or transition state not found."); 
                            }

                            bool inputExiste = stateFound.Transitions.Any(t => t.Input == input);

                            if (inputExiste)
                            {
                                //_erreurs.Add(new Tuple<string, string>(index + " " + ligne, $"L'input:{input} était déjà utilisé donc on la supprime -- (Determinisation facile)."));
                                throw new Exception($"L'input: {input} était déjà utilisé donc on la supprime -- (Determinisation facile)."); 
                            }
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
                    _erreurs.Add(new Tuple<string, string>($"Error on line: {index}: " + ligne, ex.Message));
                }
            }
            
            // Verifie si automate valide
            IsValid = ValidateAutomate();
            
            // Log errors if any
            if (_erreurs.Any())
            {
                foreach (var error in _erreurs)
                {
                    Console.WriteLine($"{error.Item1}, {error.Item2}");
                }
            }
            else
            {
                Console.WriteLine("File processed without errors.");
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

        private bool ValidateAutomate()
        {
            bool valide = true;
            try
            {
                // Verifie si l'automate a des etats
                if (!States.Any())
                {
                    //_erreurs.Add(new Tuple<string, string>("", "Il ny a pas d'etats dans l'automate"));
                    valide = false;
                    throw new Exception("Il ny a pas d'etats dans l'automate"); 
                }

                // Verifie si l'automate a un etat initial
                if (InitialState != null)
                {
                    CurrentState = InitialState;
                }
                else
                {
                    //_erreurs.Add(new Tuple<string, string>("", "Il ny a pas d'etat inital dans l'automate")); 
                    valide = false;
                    throw new Exception("Il ny a pas d'etat inital dans l'automate"); 
                }
                
                State currentState = InitialState;
                valide = TrouverCheminFinal(currentState);
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
                        Console.WriteLine($"L'automate a un etat final accessible!");
                        return true;
                    }
                    else
                    {
                        Console.WriteLine($"L'état {state.Name} a déjà été vérifié.");
                    }
                
                }
            
                _erreurs.Add(new Tuple<string, string>("Erreur lors de la validation de l'atteinte d'un chemin final", "Il ny a pas de chemin atteignant un etat final dans l'automate"));
                return false;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        
        
        public bool ValidateInput(string userInput)
        {
            bool isValid = true;
            Reset();
            
            char[] inputs = userInput.ToCharArray();

            foreach (var input in inputs)
            {
                State EtatInitialTransition = CurrentState;
                State etatTransite = EtatInitialTransition.Transitions.FirstOrDefault(transit => transit.Input == input)
                    ?.TransiteTo;   // Todo erreur: Etat transiter existe pas, input aussi

                if (etatTransite == null)
                {
                    // No valid transition found for the current input; input is invalid.
                    Console.WriteLine($"No transition found for state '{EtatInitialTransition.Name}' with input '{input}'");
                    isValid = false;
                    break;
                }
                
                CurrentState = etatTransite;
                Console.WriteLine($"Transitioned from state '{EtatInitialTransition.Name}' with input '{input}' to state '{etatTransite.Name}'");
            }

            if (!CurrentState.IsFinal)
            {
                isValid = false;
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

        public void Reset()
        {
            // Vous devez faire du code pour indiquer ce que signifie réinitialiser l'automate avant chaque validation.
            CurrentState = InitialState;
        }
        
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            Console.WriteLine("\n--- Représentation de l'automate ---\n");
            foreach (var state in States)
            {
                // Afficher l'état initial avec des crochets carrés
                if (state == InitialState)
                {
                    sb.AppendLine($"[{state.Name}]{(state.IsFinal ? ")" : "")}");
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
