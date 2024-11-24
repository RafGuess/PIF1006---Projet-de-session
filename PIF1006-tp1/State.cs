/*---------------------------------------------------------------------------------
 *  Etat: Représente un état
 *                          -> a un nom (p.ex. "s0", "s1", ou peu importe)
 *                          -> peut être un état final
 *                          -> peut transiter vers d'autres états
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
    public class State
    {
        public bool IsFinal {get; private set;}                             //Indique si un état est final ou non 
        public string Name { get; private set; }                            //Nom de l'état (ex : sO) 
        public List<Transition> Transitions { get; private set; }           //Liste des transitions associés à un état

        
        public State (string name, bool isFinal)                            //Constructeur
        {
            Name = name;
            IsFinal = isFinal;
            Transitions = new List<Transition>();
        }

        public override string ToString()
        {
            return base.ToString(); // TODO ? : Modifier ce code pour retourner une représentation plus cohérente d'un état et de ses transitions vers d'autres états
        }
    }
}