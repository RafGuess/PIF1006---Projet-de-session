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
            // Construisez les chemins relatifs vers les fichiers dans le dossier "Automates"
            string filePath1 = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\Automates\AutomateConforme.txt");
            string filePath2 = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\Automates\AutomateNonDeterministe.txt");
            string filePath3 = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\Automates\AutomatePartiellementConforme.txt");
            string filePath4 = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\Automates\AutomateSansEtat.txt");
            string filePath5 = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\Automates\AutomateSansEtatInitial.txt");
            string filePath6 = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\Automates\AutomateAvecJambon.txt");

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
                            Console.WriteLine("Vous avez sélectionné l'automate Conforme.");
                            Automate automate1 = new Automate(filePath1);

                            if (!automate1.isValid)
                            {
                                Console.WriteLine("\n-------------------------------------------------\n[REJET] : L'automate n'est pas valide. Appuyez sur ENTER pour revenir au menu principal.\n-------------------------------------------------\n");
                                Console.ReadLine();
                                continue;
                            }

                            Console.WriteLine(automate1.ToString());

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
                                    bool isAccepted = automate1.ValidateInput(input);
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
                            // Automate Non Déterministe
                            Console.Clear();
                            Console.WriteLine("Vous avez sélectionné l'automate Non Déterministe.");
                            Automate automate2 = new Automate(filePath2);

                            if (!automate2.isValid)
                                
                            {
                                Console.WriteLine("\n-------------------------------------------------\n[REJET] : L'automate n'est pas valide. Appuyez sur ENTER pour revenir au menu principal.\n-------------------------------------------------\n");
                                Console.ReadLine();
                                continue;
                            } 
                            
                            Console.WriteLine(automate2.ToString());
                        
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
                                    bool isAccepted = automate2.ValidateInput(input);
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
                            // Automate Partiellement Conforme
                            Console.Clear();
                            Console.WriteLine("Vous avez sélectionné l'automate Partiellement Conforme.");
                            Automate automate3 = new Automate(filePath3);

                            if (!automate3.isValid)
                            {
                                Console.WriteLine("\n-------------------------------------------------\n[REJET] : L'automate n'est pas valide. Appuyez sur ENTER pour revenir au menu principal.\n-------------------------------------------------\n");
                                Console.ReadLine();
                                continue;
                            }

                            Console.WriteLine(automate3.ToString());

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
                                    bool isAccepted = automate3.ValidateInput(input);
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
                            // Automate Sans Etat
                            Console.Clear();
                            Console.WriteLine("Vous avez sélectionné l'automate Sans Etat.");
                            Automate automate4 = new Automate(filePath4);

                            if (!automate4.isValid)
                            {
                                Console.WriteLine("\n-------------------------------------------------\n[REJET] : L'automate n'est pas valide.Un automate sans états ne peut pas fonctionner, et donc, ne peut pas prendre de valeur d'entrée.\nAppuyez sur ENTER pour revenir au menu principal.\n-------------------------------------------------\n");
                                Console.ReadLine();
                                continue;
                            }

                            Console.WriteLine(automate4.ToString());

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
                                    bool isAccepted = automate4.ValidateInput(input);
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
                            // Automate Sans Etat Initial
                            Console.Clear();
                            Console.WriteLine("Vous avez sélectionné l'automate Sans Etat Initial.");
                            Automate automate5 = new Automate(filePath5);


                            if (!automate5.isValid)
                            {
                                Console.WriteLine("\n-------------------------------------------------\n[REJET] : L'automate n'est pas valide.Un automate sans état initial ne peut pas accepter de valeur d'entrée.\nAppuyez sur ENTER pour revenir au menu principal\n-------------------------------------------------\n");
                                Console.ReadLine();
                                continue;
                            }

                            Console.WriteLine(automate5.ToString());

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
                                    bool isAccepted = automate5.ValidateInput(input);
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
                        else if (selectedIndex == 5)
                        {
                            // Automate Avec Mot non reconnue
                            Console.Clear();
                            Console.WriteLine("Vous avez sélectionné l'automate avec des mots non reconnue.");
                            Automate automate6 = new Automate(filePath6);
                            
                            if (!automate6.isValid)
                            {
                                Console.WriteLine("\nL'automate n'est pas valide.");
                                Console.ReadLine();
                            }

                            Console.WriteLine(automate6.ToString());

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
                                    bool isAccepted = automate6.ValidateInput(input);
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
        
    }
}