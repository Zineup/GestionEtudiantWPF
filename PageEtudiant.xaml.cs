using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Telerik.Windows.Controls;

namespace TelerikWpfApp1
{
    /// <summary>
    /// Logique d'interaction pour PageEtudiant.xaml
    /// </summary>
    public partial class PageEtudiant : Page
    {
        public PageEtudiant()
        {
            InitializeComponent();
        }

        ProjetDataContext bd = new ProjetDataContext();


        private void RadComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RadComboBox com = (RadComboBox)sender;
            int index = com.SelectedIndex;
            var fil = from f in bd.Filiere
                      select f;


            List<Filiere> filieres = fil.ToList();
            try
            {
                titre_grid.Content = "Les Etudiants de " + filieres[index].Nom_filiere;
                info_filiere.Content = "Filière : " + filieres[index].Nom_filiere;
                info_responsable.Content = "Responsable : " + filieres[index].Nom_responsable;
                myBorder.Visibility = Visibility.Visible;

                int id = filieres[index].Id_filiere;

                var et = from etudiant in bd.etudiant
                         join filiere in bd.Filiere
                         on etudiant.id_fil equals filiere.Id_filiere
                         where filiere.Id_filiere == id
                         select etudiant;

                ObservableCollection<etudiant> etudiantList = new ObservableCollection<etudiant>(et.ToList());
                gridView.ItemsSource = et.ToList();

            }
            catch (Exception ex) { }


        }

        private void Click_btn_etudiant(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new PageGestionEt(null));
        }

        private void Click_btn_modifier(object sender, RoutedEventArgs e)
        {
            etudiant etu = gridView.SelectedItem as etudiant;
            if(etu == null)
            {
                MessageBox.Show("Selectionner un étudiant Svp !");
            } else
            {
                this.NavigationService.Navigate(new PageGestionEt(etu));
            }
        }

        private void Click_btn_filiere(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new PageFiliere());
        }

        private void Click_btn_statistiques(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new PageStatis());
        }

        private void Quitter(object sender, RoutedEventArgs e)
        {
            
            foreach (Window window in App.Current.Windows)
            {
                if (window.IsActive)
                {
                    window.Hide();
                }
            }
            
        }

    }
}
