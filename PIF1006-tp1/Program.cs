using System;
using System.Collections.Generic;
using System.IO;
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
            // Vous devez faire une application dont les étapes d'interactions utilisateurs vont exactement comme suit:
            //
            //      (1) Afficher une entête en console comportant:  // Fait
            //          -> Nom de votre application
            //          -> Liste de vos noms complets et codes permanents
           
            
            //
            //      (2) Charger un fichier en spécifiant le chemin (relatif) du fichier.  Vous pouvez charger un fichier par défaut au démarrage;
            //          ->  Pour le format et la façon de charger le fichier, référez-vous aux détails en commentaire dans la méthode LoadFromFile()
            //              de la classe Automate.
            //          ->  Si après chargement du fichier l'automate est invalide (sa propriété IsValid est à faux), l'application se ferme suite à
            //              l'appuie sur ENTER par l'utilisateur.
            
            ////////////////////////////////////////////////////////////////////////////////////////////////
            ///
        
          string[] options = { "Automate 1", "Automate 2", "Automate 3", "Automate 4", "Automate 5", "Quitter" };
        int selectedIndex = 0;
        Console.WriteLine($"Initialisation:");
        //string filepath3 = "D:\\École\\Session 5\\MathInfo\\PIF1006\\PIF1006-tp1\\Automates\\Automate2.txt";
        string filePath1 = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\Automates\Automate.txt");
        string filePath2 = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\Automates\Automate2.txt");
        Console.WriteLine($"FP:{filePath2}");
        Automate automate = new Automate(filePath2);
        
        
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
            
            Console.WriteLine("NOM DE NOTRE APPLICATION");      // TODO(): Trouver un nom pour notre application
            foreach (var (nom, code_permanent) in nom_CP)
            {
                Console.WriteLine($"Nom complet: {nom}, code permanent: {code_permanent}");
            }
            
            DisplayMenu(options, selectedIndex);

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
                    if (selectedIndex == options.Length - 1)
                    {
                        Console.WriteLine("\nL'application va se fermer après avoir appuyé sur ENTER.");
                        Console.ReadLine();
                        return;
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine($"Vous avez choisi : {options[selectedIndex]}");
                        Console.WriteLine("Appuyez sur une touche pour revenir au menu...");
                        Console.ReadKey(true);
                    }
                    break;
            }
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
            /////////////////////////////////////////////////////////////////////////////////////////////////
            
            
                        
            //TODO : utiliser un chemin relatif au projet et non à l'ordi local
            /*     
           // Obtenez le chemin de base du projet (répertoire de sortie)
           string baseDirectory = AppContext.BaseDirectory;
           
           // Construisez les chemins relatifs vers les fichiers dans le dossier "Automates"
           string filepath1 = Path.Combine(baseDirectory, "Automates", "AutomatePartiellementConforme.txt");
           string filepath2 = Path.Combine(baseDirectory, "..", "..", "..", "Automates", "AutomateNonDeterministe.txt");
           string filepath3 = Path.Combine(baseDirectory, "..", "..", "..", "Automates", "AutomatPartiellementConforme.txt");
           string filepath4 = Path.Combine(baseDirectory, "..", "..", "..", "Automates", "AutomateSansEtat.txt");
           string filepath5 = Path.Combine(baseDirectory, "..", "..", "..", "Automates", "AutomateSansEtatInitial.txt");
           
           // Instanciez l'automate en utilisant le chemin relatif
           Automate automate1 = new Automate(filepath1);
           Automate automate2 = new Automate(filepath2);
           Automate automate3 = new Automate(filepath3);
           Automate automate4 = new Automate(filepath4);
           Automate automate5 = new Automate(filepath5);

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

            
            //      (5) Au moment où l'utilisateur choisit de quitter, un message s'affiche lui disant que l'application va se fermer après
            //          avoir appuyé sur ENTER.
            
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
        }
    }
}
