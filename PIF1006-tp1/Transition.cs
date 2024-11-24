/*---------------------------------------------------------------------------------
 *  Transition : Représente un tuple -> (input entré par l'utilisateur, nouvel état transité)
 *  Auteurs : Abderraouf Guessoum, Julien Desrosiers(DESJ70100201),
 *            Lily Occhibelli (OCCL72360500), Océane Rakotoarisoa(RAKS77350500)               
 *  Date : 26/11/2024
 *  Cours : PIF1006 - Automne 2024 
 *----------------------------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIF1006_tp1
{
    public class Transition
    {
        public char Input { get; set; }                      // Un caractère de l'input entré par l'utilisateur
        public State TransiteTo { get; set; }               // État cible vers lequel la transition se fait.
        
        public Transition(char input, State transiteTo)     //Construit une transition       
        {
            Input = input;
            TransiteTo = transiteTo;
        }

        public override string ToString()
        {
            return $"{Input} -> {TransiteTo.Name}";         //Affiche une transition
        }
    }
}