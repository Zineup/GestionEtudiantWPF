using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace TelerikWpfApp1
{
    class ClasseEtudiant
    {
        private int cne;
        private string nom;
        private string prenom;
        private DateTime _date_naiss;
        private Gender sexe;
        private String filiere;

        public int Cne { get => cne; set => cne = value; }
        public string Nom { get => nom; set => nom = value; }
        public string Prenom { get => prenom; set => prenom = value; }
        public DateTime Date_naiss { get => _date_naiss; set => _date_naiss = value; }
        public Gender Sexe { get => sexe; set => sexe = value; }
        public string Filiere { get => filiere; set => filiere = value; }
    }

    public enum Gender
    {
        Femme,
        Homme,
    }

}
