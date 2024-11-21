using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using PIF1006_tp1;

namespace PIF1006_tp1
{
    /// <summary>
    /// AVANT D'ENTAMER VOTRE TRAVAIL, SVP, VEUILLEZ BIEN LIRE ATTENTIVEMENT LES INSTRUCTIONS ET DIRECTIONS EN COMMENTAIRES DANS LES DIFFÉRENTS
    /// FICHIERS.
    /// 
    /// LES CLASSES ET LEURS MEMBRES PRÉDÉFINIS DOIVENT RESTER TELS QUELS.  VOUS POUVEZ AJOUTER DES MÉTHODES PRIVÉES AU BESOIN, MAIS AU MINIMUM
    /// AJOUTER LE CODE MANQUANT (ET CRÉER LES FICHIERS EN ENTRÉE PERTINENTS) PERMETTANT DE RÉALISER LES FONCTIONNALITÉS DEMANDÉES.
    /// 
    /// VOUS DEVEZ TRAVAILLER EN C# .NET.  LE PROJET EST EN .NET 5.0 AFIN DE S'ASSURER D'UNE COMPATIBILITÉ POUR TOUS ET TOUTES, MAIS VOUS ÊTES
    /// INVITÉ/E/S À UTILISER LA DERNIÈRE VERSION DU FRAMEWORK .NET (8.0).
    /// </summary>
    public class Program
    {
        static void Main(string[] args)
        {
            //---------------------------------------------------------------------------------------------------------------------------
            
            //      (2) Charger un fichier en spécifiant le chemin (relatif) du fichier.  Vous pouvez charger un fichier par défaut au démarrage;
            //          ->  Pour le format et la façon de charger le fichier, référez-vous aux détails en commentaire dans la méthode LoadFromFile()
            //              de la classe Automate.
            //          ->  Si après chargement du fichier l'automate est invalide (sa propriété IsValid est à faux), l'application se ferme suite à
            //              l'appuie sur ENTER par l'utilisateur.
            
           
           // Construisez les chemins relatifs vers les fichiers dans le dossier "Automates"
           string filePath1 = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, 
               @"..\..\..\Automates\AutomateConforme.txt");
           string filePath2 = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, 
               @"..\..\..\Automates\AutomateNonDeterministe.txt");
           string filePath3 = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, 
               @"..\..\..\Automates\AutomatePartiellementConforme.txt");
           string filePath4 = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, 
               @"..\..\..\Automates\AutomateSansEtat.txt");
           string filePath5 = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, 
               @"..\..\..\Automates\AutomateSansEtatInitial.txt");

           // Instanciez l'automate en utilisant le chemin relatif
           
           
           //----------------------------------------------------------------------------------------------------------------------------

            //      (3) La représentation de l'automate doit être affichée à la console sous la forme d'une liste des états et la liste des
            //          transitions de chacune d'entre elles, à la manière d'une pseudo table d'action. Si l'état est un état final cela
            //          doit être apparent;
            //              Par exemple:
            //                  [(s0)]
            //                      --0--> s1
            //                      --1--> s0
            //                  s1
            //                      --0--> s1
            //                      --1--> s2
            //                  s2
            //                      --0--> s1
            //                      --1--> s3
            //                  (s3)
            //              Où s0 et s3 sont des états finaux (parenthèses), s0 est l'état initial (square brackets) et
            //              s3 n'a pas de transition vers d'autres états
            //          ->  Vous DEVEZ surdéfinir les méthodes ToString() des différentes classes fournies de sorte à faciliter l'affichage


            //      (4) Soumettre un input en tant que chaîne de 0 ou de 1
            //          ->  Assurez-vous que la chaine passée ne contient QUE ces caractères
            //              avant d'envoyer n'est pas obligatoire, mais cela ne doit pas faire planter de l'autre coté;
            //          ->  Un message doit indiquer si c'est accepté ou rejeté.
            //          ->  Suite à cela, on doit demander à l'utilisateur s'il veut enter un nouvel input plutôt que de quitter
            //              afin de faire des validations en rafales.


            // todo: mettre ce code là où il est nécessaire (vraiment pas ici) - AG
            /*
            Console.WriteLine("L'application va se fermer après avoir appuyé sur ENTER.");
            Console.ReadLine();

            // Demander l'input et valider avec l'automate choisi
            string input;
            do
            {
                Console.WriteLine("Entrez une chaîne de 0 et de 1:");
                input = Console.ReadLine();
                if (input == "true")
                {
                    Console.WriteLine("L'automate accepte la chaîne.");
                }
                else
                {
                    Console.WriteLine("L'automate rejette la chaîne.");
                }
                Console.WriteLine("Voulez-vous entrer une autre chaîne? (O/N)");
            } while (Console.ReadLine().ToUpper() == "O");

            Console.WriteLine("L'application va se fermer après avoir appuyé sur ENTER.");
            Console.ReadLine();
            */
            
            //---------------------------------------------------------------------------------------------------------------------------
            
            //---------------------------------------------------------------------------------------------------------------------------
            // todo: question 1 (entête fait, menu est fonctionnel, reste plus qu'à implémenter les fonctions de chaque option)
            // Vous devez faire une application dont les étapes d'interactions utilisateurs vont exactement comme suit:
            //
            //      (1) Afficher une entête en console comportant:  // Fait
            //          -> Nom de votre application
            //          -> Liste de vos noms complets et codes permanents
            string[] options = { "Automate Conforme", "Automate Non Déterministe", "Automate Partiellement Conforme", "Automate Sans Etat", "Automate Sans Etat Initial", "Quitter" };
            int selectedIndex = 0;
                   
            while (true)
            {
                Console.Clear();
                List<(string, string)> nom_CP = new List<(string, string)>
                {
                    ("Julien Desrosiers", "DESJ70100201"),
                    ("Océane Rakotoarisoa", "RAKS77350500"),
                    ("Lily Occhibelli", "OCCL72360500"),
                    ("Abderraouf Guessoum", "GUEA80320400"),
                           
                };
                       
                Console.WriteLine("Les internationaux (et Julien)"); 
                foreach (var (nom, code_permanent) in nom_CP)
                {
                    Console.WriteLine($"Nom complet: {nom}, code permanent: {code_permanent}");
                }
                       
                DisplayMenu(options, selectedIndex);
           
                string input;
                
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                switch (keyInfo.Key)
                {
                    case ConsoleKey.UpArrow:
                        selectedIndex = (selectedIndex - 1 + options.Length) % options.Length;
                        break;
                    case ConsoleKey.DownArrow:
                        selectedIndex = (selectedIndex + 1) % options.Length;
                        break;
                    case ConsoleKey.Enter:
                        //      todo: Question 5
                        //      (5) Au moment où l'utilisateur choisit de quitter, un message s'affiche lui disant que l'application va se fermer après
                        //          avoir appuyé sur ENTER.
                        if (selectedIndex == options.Length - 1)
                        {
                            Console.WriteLine("\nL'application va se fermer après avoir appuyé sur ENTER.");
                            Console.WriteLine("Merci d'avoir utilisé notre application, n'oubliez pas de nous laisser un 100% en partant! Merci :)");
                            Console.ReadLine();
                            return;
                        }
                        else if (selectedIndex == 0)
                        {
                            Console.Clear();
                            Console.WriteLine("Vous avez sélectionné l'automate Conforme.");
                            Automate automate1 = new Automate(filePath1);
    
                            if (!automate1.IsValid)
                            {
                                Console.WriteLine("L'automate n'est pas valide. Appuyez sur ENTER pour revenir au menu principal.");
                                Console.ReadLine();
                                continue;
                            }
    
                            Console.WriteLine(automate1.ToString());

                            do
                            {
                                Console.WriteLine("\nEntrez une chaîne de 0 et de 1 (ou 'q' pour quitter):");
                                input = Console.ReadLine();

                                if (input.ToLower() == "q")
                                    break;

                                if (input.All(c => c == '0' || c == '1'))
                                {
                                    bool isAccepted = automate1.Validate(input);
                                    Console.WriteLine(isAccepted ? "L'automate accepte la chaîne." : "L'automate rejette la chaîne.");
                                }
                                else
                                {
                                    Console.WriteLine("Entrée invalide. Veuillez n'utiliser que des 0 et des 1.");
                                }
                            } while (true);

                            Console.WriteLine("Appuyez sur une touche pour revenir au menu...");
                            Console.ReadKey(true);
                        }

                        else if (selectedIndex == 1)
                        {
                            Console.Clear();
                            Console.WriteLine("Vous avez sélectionné l'automate Non Déterministe.");
                            Automate automate2 = new Automate(filePath2);
    
                            if (!automate2.IsValid)
                            {
                                Console.WriteLine("L'automate n'est pas valide. Appuyez sur ENTER pour revenir au menu principal.");
                                Console.ReadLine();
                                continue;
                            }
    
                            Console.WriteLine(automate2.ToString());

                            do
                            {
                                Console.WriteLine("\nEntrez une chaîne de 0 et de 1 (ou 'q' pour quitter):");
                                input = Console.ReadLine();

                                if (input.ToLower() == "q")
                                    break;

                                if (input.All(c => c == '0' || c == '1'))
                                {
                                    bool isAccepted = automate2.Validate(input);
                                    Console.WriteLine(isAccepted ? "L'automate accepte la chaîne." : "L'automate rejette la chaîne.");
                                }
                                else
                                {
                                    Console.WriteLine("Entrée invalide. Veuillez n'utiliser que des 0 et des 1.");
                                }
                            } while (true);

                            Console.WriteLine("Appuyez sur une touche pour revenir au menu...");
                            Console.ReadKey(true);
                        }
                        else if (selectedIndex == 2)
                        {
                            Console.Clear();
                            Console.WriteLine("Vous avez sélectionné l'automate Partiellement Conforme.");
                            Automate automate3 = new Automate(filePath3);
    
                            if (!automate3.IsValid)
                            {
                                Console.WriteLine("L'automate n'est pas valide. Appuyez sur ENTER pour revenir au menu principal.");
                                Console.ReadLine();
                                continue;
                            }
    
                            Console.WriteLine(automate3.ToString());

                            do
                            {
                                Console.WriteLine("\nEntrez une chaîne de 0 et de 1 (ou 'q' pour quitter):");
                                input = Console.ReadLine();

                                if (input.ToLower() == "q")
                                    break;

                                if (input.All(c => c == '0' || c == '1'))
                                {
                                    bool isAccepted = automate3.Validate(input);
                                    Console.WriteLine(isAccepted ? "L'automate accepte la chaîne." : "L'automate rejette la chaîne.");
                                }
                                else
                                {
                                    Console.WriteLine("Entrée invalide. Veuillez n'utiliser que des 0 et des 1.");
                                }
                            } while (true);

                            Console.WriteLine("Appuyez sur une touche pour revenir au menu...");
                            Console.ReadKey(true);
                        }
                        else if (selectedIndex == 3)
                        {
                            Console.Clear();
                            Console.WriteLine("Vous avez sélectionné l'automate Sans Etat.");
                            Automate automate4 = new Automate(filePath4);
    
                            if (!automate4.IsValid)
                            {
                                Console.WriteLine("L'automate n'est pas valide. Appuyez sur ENTER pour revenir au menu principal.");
                                Console.ReadLine();
                                continue;
                            }
    
                            Console.WriteLine(automate4.ToString());

                            do
                            {
                                Console.WriteLine("\nEntrez une chaîne de 0 et de 1 (ou 'q' pour quitter):");
                                input = Console.ReadLine();

                                if (input.ToLower() == "q")
                                    break;

                                if (input.All(c => c == '0' || c == '1'))
                                {
                                    bool isAccepted = automate4.Validate(input);
                                    Console.WriteLine(isAccepted ? "L'automate accepte la chaîne." : "L'automate rejette la chaîne.");
                                }
                                else
                                {
                                    Console.WriteLine("Entrée invalide. Veuillez n'utiliser que des 0 et des 1.");
                                }
                            } while (true);

                            Console.WriteLine("Appuyez sur une touche pour revenir au menu...");
                            Console.ReadKey(true);
                        }
                        else if (selectedIndex == 4)
                        {
                            Console.Clear();
                            Console.WriteLine("Vous avez sélectionné l'automate Sans Etat Initial.");
                            Automate automate5 = new Automate(filePath5);
    
                            if (!automate5.IsValid)
                            {
                                Console.WriteLine("L'automate n'est pas valide. Appuyez sur ENTER pour revenir au menu principal.");
                                Console.ReadLine();
                                continue;
                            }
    
                            Console.WriteLine(automate5.ToString());

                            do
                            {
                                Console.WriteLine("\nEntrez une chaîne de 0 et de 1 (ou 'q' pour quitter):");
                                input = Console.ReadLine();

                                if (input.ToLower() == "q")
                                    break;

                                if (input.All(c => c == '0' || c == '1'))
                                {
                                    bool isAccepted = automate5.Validate(input);
                                    Console.WriteLine(isAccepted ? "L'automate accepte la chaîne." : "L'automate rejette la chaîne.");
                                }
                                else
                                {
                                    Console.WriteLine("Entrée invalide. Veuillez n'utiliser que des 0 et des 1.");
                                }
                            } while (true);

                            Console.WriteLine("Appuyez sur une touche pour revenir au menu...");
                            Console.ReadKey(true);
                        }
                        break;
                }
            }
                    
            static void DisplayMenu(string[] options, int selectedIndex)
            {
                Console.WriteLine("Choisissez un automate à utiliser:\n");
                for (int i = 0; i < options.Length; i++)
                {
                    if (i == selectedIndex)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkCyan;
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.Write("> ");
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.DarkBlue;
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.Write("  ");
                    }
                    
                    Console.WriteLine(options[i]);
                    Console.ResetColor();
                }
            }
            
        }
    }
}