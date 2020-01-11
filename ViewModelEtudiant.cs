using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.Windows.Controls;

namespace TelerikWpfApp1
{
    class ViewModelEtudiant : ViewModelBase
    {
        private ObservableCollection<etudiant> etudiantList;

        public ObservableCollection<etudiant> EtudiantList
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

        private ObservableCollection<etudiant> CreateEtudiants()
        {

            ProjetDataContext bd = new ProjetDataContext();

            var data = from f in bd.etudiant
                       select f;


            List<etudiant> list = data.ToList();
            
            ObservableCollection<etudiant> etudiantList = new ObservableCollection<etudiant>(list);


            return etudiantList;
        }
    }
}
