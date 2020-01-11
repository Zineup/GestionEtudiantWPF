using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelerikWpfApp1
{
    class FromLinq
    {
        private ObservableCollection<ClasseEtudiant> etudiantList;

        public ObservableCollection<ClasseEtudiant> EtudiantList
        {
            get
            {
                if (this.etudiantList == null)
                {
                    this.etudiantList = this.CreateEtudiants();
                }

                return this.etudiantList;
            }
        }

        private ObservableCollection<ClasseEtudiant> CreateEtudiants()
        {

            ProjetDataContext bd = new ProjetDataContext();

            var data = from etudiant in bd.etudiant
                     join filiere in bd.Filiere
                     on etudiant.id_fil equals filiere.Id_filiere
                     select etudiant;

            List<etudiant> list = data.ToList();
            List<ClasseEtudiant> etudiants = new List<ClasseEtudiant>();

            foreach(etudiant e in data)
            {
                ClasseEtudiant etu = convertEtudiant(e);
                etudiants.Add(etu);               

            }

            ObservableCollection<ClasseEtudiant> etudiantList = new ObservableCollection<ClasseEtudiant>(etudiants);
            
            return etudiantList;
        }

        public ClasseEtudiant convertEtudiant(etudiant e)
        {
            ClasseEtudiant etu = new ClasseEtudiant();
            etu.Cne = e.cne;
            etu.Nom = e.nom;
            etu.Prenom = e.prenom;
            etu.Filiere = e.Filiere.Nom_filiere;
            try
            {
                etu.Date_naiss = (DateTime)e.date_naiss;
            } catch (Exception ex)
            {
                etu.Date_naiss = new DateTime();
            }

            if (e.sexe.Equals('H'))
            {
                etu.Sexe = Gender.Homme;
            }
            else
            {
                etu.Sexe = Gender.Femme;
            }
            return etu;
        }
    }
}
