using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.Windows.Controls;

namespace TelerikWpfApp1
{
    class ViewModel : ViewModelBase
    {
        private ObservableCollection<Filiere> mesFilieres;

        public ObservableCollection<Filiere> MesFilieres
        {
            get
            {
                if (this.mesFilieres == null)
                {
                    this.mesFilieres = this.CreateFilieres();
                }

                return this.mesFilieres;
            }
        }

        private ObservableCollection<Filiere> CreateFilieres()
        {
            
            ProjetDataContext bd = new ProjetDataContext();

            var data = from f in bd.Filiere
                       select f;

            ObservableCollection<Filiere> filieres = new ObservableCollection<Filiere>(data.ToList());
            
            return filieres;
        }
    }
}
