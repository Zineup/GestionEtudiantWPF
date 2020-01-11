using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.Windows.Controls;

namespace TelerikWpfApp1
{
    class UsersViewModel : ViewModelBase
    {
        public class FiliereCount

        {

            public int nbEtudiant { get; set; }

            public string Filiere { get; set; }

        }

        static ProjetDataContext cl = new ProjetDataContext();

        public ObservableCollection<FiliereCount> ListF

        {

            get; set;

        }

        public UsersViewModel()

        {

            this.ListF = new ObservableCollection<FiliereCount>();

            var filCount = from etudiant in cl.etudiant
                           join filiere in cl.Filiere
                           on etudiant.id_fil equals filiere.Id_filiere
                           group new { etudiant, filiere } by filiere.Nom_filiere
                      into grouping
                           select new
                           {
                               grouping.Key,
                               nbEtudiant = grouping.Count()

                           };
            foreach (var gr in filCount)
            {
                ListF.Add(new FiliereCount { Filiere = gr.Key, nbEtudiant = gr.nbEtudiant });

            }



        }


    }
}
