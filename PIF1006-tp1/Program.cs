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
using PIF1006_tp1;

namespace PIF1006_tp1
{
    public class Program
    {
        static void Main(string[] args)
        {
            // Construire le dictionnaire
            var automates = new Dictionary<string, (string FilePath, string RejectionMessage)>
            {
                {
                    "Automate Conforme",
                    (
                        Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\Automates\AutomateConforme.txt"),
                        "\n-------------------------------------------------\n[REJET] : L'automate n'est pas valide. Appuyez sur ENTER pour revenir au menu principal.\n-------------------------------------------------\n"
                    )
                },
                {
                    "Automate Non Déterministe",
                    (
                        Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\Automates\AutomateNonDeterministe.txt"),
                        "\n-------------------------------------------------\n[REJET] : L'automate n'est pas valide. Appuyez sur ENTER pour revenir au menu principal.\n-------------------------------------------------\n"
                    )
                },
                {
                    "Automate Partiellement Conforme",
                    (
                        Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\Automates\AutomatePartiellementConforme.txt"),
                        "\n-------------------------------------------------\n[REJET] : L'automate n'est pas valide. Appuyez sur ENTER pour revenir au menu principal.\n-------------------------------------------------\n"
                    )
                },
                {
                    "Automate Sans État",
                    (
                        Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\Automates\AutomateSansEtat.txt"),
                        "\n-------------------------------------------------\n[REJET] : L'automate n'est pas valide. Un automate sans états ne peut pas fonctionner, et donc, ne peut pas prendre de valeur d'entrée\n-------------------------------------------------\n"
                    )
                },
                {
                    "Automate Sans État Initial",
                    (
                        Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\Automates\AutomateSansEtatInitial.txt"),
                        "\n-------------------------------------------------\n[REJET] : L'automate n'est pas valide. Un automate sans état initial ne peut pas accepter de valeur d'entrée.\n-------------------------------------------------\n"
                    )
                },
                {
                    "Automate Avec Jambon",
                    (
                        Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\Automates\AutomateAvecJambon.txt"),
                        "\n-------------------------------------------------\n[REJET] : L'automate n'est pas valide. Appuyez sur ENTER pour revenir au menu principal.\n-------------------------------------------------\n"
                    )
                }
            };
            
            
            // Définir les options de menu
            string[] options = { "Automate Conforme", "Automate Non Déterministe", "Automate Partiellement Conforme", "Automate Sans Etat", "Automate Sans Etat Initial", "Automate avec des erreurs", "Quitter" };
            int selectedIndex = 0;

            while (true)
            {
                Console.Clear();
                AfficherHeader();
                DisplayMenu(options, selectedIndex);

                // Lire l'entrée de l'utilisateur pour la navigation dans le menu
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
                        // Si l'utilisateur choisit de quitter
                        if (selectedIndex == options.Length - 1)
                        {
                            Console.WriteLine("\nL'application va se fermer après avoir appuyé sur ENTER.");
                            Console.WriteLine("Merci d'avoir utilisé notre application, n'oubliez pas de nous laisser un 100% en partant! Merci :)");
                            Console.ReadLine();
                            return;
                        }
                        else if (selectedIndex == 0)
                        {
                            // Automate Conforme
                            Console.Clear();
                            var nom = "Automate Conforme";
                            Console.WriteLine($"Vous avez sélectionné {nom}.");
                            Automate automate1 = new Automate(automates[nom].FilePath);

                            if (!automate1.isValid)
                            {
                                Console.WriteLine(automates[nom].RejectionMessage);
                                Console.ReadLine();
                                continue;
                            }

                            Console.WriteLine(automate1.ToString());

                            DemanderInput(automate1);

                            Console.WriteLine("Appuyez sur une touche pour revenir au menu...");
                            Console.ReadKey(true);
                        }
                        else if (selectedIndex == 1)
                        {
                            // Automate Non Déterministe
                            Console.Clear();
                            var nom = "Automate Non Déterministe";
                            Console.WriteLine($"Vous avez sélectionné {nom}.");
                            Automate automate2 = new Automate(automates[nom].FilePath);

                            if (!automate2.isValid)
                                
                            {
                                Console.WriteLine(automates[nom].RejectionMessage);
                                Console.ReadLine();
                                continue;
                            } 
                            
                            Console.WriteLine(automate2.ToString());
                        
                            DemanderInput(automate2);
                            
                            Console.WriteLine("Appuyez sur une touche pour revenir au menu...");
                            Console.ReadKey(true);
                        }
                        else if (selectedIndex == 2)
                        {
                            // Automate Partiellement Conforme
                            Console.Clear();
                            var nom = "Automate Partiellement Conforme";
                            Console.WriteLine($"Vous avez sélectionné {nom}.");
                            Automate automate3 = new Automate(automates[nom].FilePath);

                            if (!automate3.isValid)
                            {
                                Console.WriteLine(automates[nom].RejectionMessage);
                                Console.ReadLine();
                                continue;
                            }

                            Console.WriteLine(automate3.ToString());

                            DemanderInput(automate3);

                            Console.WriteLine("Appuyez sur une touche pour revenir au menu...");
                            Console.ReadKey(true);
                        }
                        else if (selectedIndex == 3)
                        {
                            // Automate Sans Etat
                            Console.Clear();
                            var nom = "Automate Sans État";
                            Console.WriteLine($"Vous avez sélectionné {nom}.");
                            Automate automate4 = new Automate(automates[nom].FilePath);

                            if (!automate4.isValid)
                            {
                                Console.WriteLine(automates[nom].RejectionMessage);
                                Console.ReadLine();
                                continue;
                            }

                            Console.WriteLine(automate4.ToString());

                            DemanderInput(automate4);

                            Console.WriteLine("Appuyez sur une touche pour revenir au menu...");
                            Console.ReadKey(true);
                        }
                        else if (selectedIndex == 4)
                        {
                            // Automate Sans Etat Initial
                            Console.Clear();
                            var nom = "Automate Sans État Initial";
                            Console.WriteLine($"Vous avez sélectionné {nom}.");
                            Automate automate5 = new Automate(automates[nom].FilePath);


                            if (!automate5.isValid)
                            {
                                Console.WriteLine(automates[nom].RejectionMessage);
                                Console.ReadLine();
                                continue;
                            }

                            Console.WriteLine(automate5.ToString());

                            DemanderInput(automate5);

                            Console.WriteLine("Appuyez sur une touche pour revenir au menu...");
                            Console.ReadKey(true);
                        }
                        else if (selectedIndex == 5)
                        {
                            // Automate Avec Mot non reconnue
                            Console.Clear();
                            var nom = "Automate Avec Jambon";
                            Console.WriteLine($"Vous avez sélectionné {nom}.");
                            Automate automate6 = new Automate(automates[nom].FilePath);
                            
                            if (!automate6.isValid)
                            {
                                Console.WriteLine(automates[nom].RejectionMessage);
                                Console.ReadLine();
                            }

                            Console.WriteLine(automate6.ToString());

                            DemanderInput(automate6);

                            Console.WriteLine("Appuyez sur une touche pour revenir au menu...");
                            Console.ReadKey(true);
                        }
                        break;
                }
            }
        }

        // Méthode pour afficher le menu avec la sélection actuelle
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
        
        // Affiche l'entête avec les noms et codes permanents
        static void AfficherHeader()
        {
            Console.WriteLine("==========================================================\nORJL - Les internationaux et Julien : Gestion d'automates");
            Console.WriteLine("Membres de l'équipe :");
            Console.WriteLine("Julien Desrosiers (DESJ70100201)");
            Console.WriteLine("Océane Rakotoarisoa (RAKS77350500)");
            Console.WriteLine("Lily Occhibelli (OCCL72360500)");
            Console.WriteLine("Abderraouf Guessoum (GUEA80320400)");
            Console.WriteLine("==========================================================\n");
        }

        private static void DemanderInput(Automate automate)
        {
            // Demander et valider les chaînes d'input
            string input;
            do
            {
                Console.WriteLine("\nEntrez une chaîne de 0 et de 1 (ou 'q' pour quitter):");
                input = Console.ReadLine();

                if (input.ToLower() == "q")
                    break;

                if (input.All(c => c == '0' || c == '1'))
                {
                    bool isAccepted = automate.ValidateInput(input);
                    Console.WriteLine(isAccepted ? "L'automate accepte la chaîne." : "L'automate rejette la chaîne.");
                }
                else
                {
                    Console.WriteLine("Entrée invalide. Veuillez n'utiliser que des 0 et des 1.");
                }
            } while (true);
            
        }
        
    }
}