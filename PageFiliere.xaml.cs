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

namespace TelerikWpfApp1
{
	/// <summary>
	/// Logique d'interaction pour PageFiliere.xaml
	/// </summary>
	public partial class PageFiliere : Page
	{
        ProjetDataContext cl = new ProjetDataContext();

        private ObservableCollection<Filiere> _collection;
        private bool? dialogResult;
        public PageFiliere()
		{
            InitializeComponent();
            //Collection_filliere col = new Collection_filliere(); 7iyedna dik class no neeeeeeeed
            var query = from c in cl.Filiere
                        select c;


            _collection = new ObservableCollection<Filiere>(query.ToList());

            this.MyCarousel.ItemsSource = _collection;
            information.Visibility = Visibility.Hidden;
            InitializeComponent();
        }
        int x = 0;
        private void Ajouter_Click(object sender, RoutedEventArgs e)
        {
            if(x == 0)
            {
                x = 1;
                information.Visibility = Visibility.Visible;
                id_box.Visibility = Visibility.Hidden;
                id_label.Visibility = Visibility.Hidden;
                nom_box.Text = "";
                
            } else if (x == 1)
            {
                Filiere f = new Filiere();

                //f.Id_filiere = Convert.ToInt32(id_box.Text);
                f.Nom_filiere = nom_box.Text;
                f.Nom_responsable = "OUARRACHI";
                cl.Filiere.InsertOnSubmit(f);
                cl.SubmitChanges();
                _collection.Add(f);
                information.Visibility = Visibility.Hidden;
                x = 0;
            }
        }

        private void Supprimer_Click(object sender, RoutedEventArgs e)
        {
            Filiere f = MyCarousel.SelectedItem as Filiere;
            if (f == null)
            {
                MessageBox.Show("Selectionner une filière Svp !");
            }
            else
            {
                id_box.Visibility = Visibility.Visible;
                id_label.Visibility = Visibility.Visible;

                var query = (from c in cl.Filiere
                             where c.Id_filiere == f.Id_filiere //Convert.ToInt32(id_box.Text)
                             select c).SingleOrDefault();
                cl.Filiere.DeleteOnSubmit(query);
                cl.SubmitChanges();
                _collection.Remove(f);

                information.Visibility = Visibility.Hidden;
            }
        }

        private void Modifier_Click(object sender, RoutedEventArgs e)
        {
            Filiere f = MyCarousel.SelectedItem as Filiere;
            if (information.Visibility == Visibility.Hidden)
            {
                MessageBox.Show("Cliquer une filière Svp !");
            }
            else
            {
                id_box.Visibility = Visibility.Visible;
                id_label.Visibility = Visibility.Visible;
                int id = f.Id_filiere;
                var query = (from c in cl.Filiere
                             where c.Id_filiere == id
                             select c).SingleOrDefault();
                query.Nom_filiere = nom_box.Text;
                cl.SubmitChanges();

                information.Visibility = Visibility.Hidden;
            }
        }

        private void mouse(object sender, MouseEventArgs e)
        {
            information.Visibility = Visibility.Hidden;
            
        }

        private void mouse1(object sender, MouseEventArgs e)
        {

            Filiere f = MyCarousel.SelectedItem as Filiere;
            if(f != null)
            {
                information.Visibility = Visibility.Visible;
                id_box.Visibility = Visibility.Visible;
                id_label.Visibility = Visibility.Visible;
                id_box.Text = f.Id_filiere.ToString();
                nom_box.Text = f.Nom_filiere;
            }

        }

        private void mouse0(object sender, MouseButtonEventArgs e)
        {
            information.Visibility = Visibility.Visible;
            id_box.Visibility = Visibility.Visible;
            id_label.Visibility = Visibility.Visible;
            Filiere f = MyCarousel.SelectedItem as Filiere;
            id_box.Text = f.Id_filiere.ToString();
            nom_box.Text = f.Nom_filiere;
        }

        

        private void Btn_retour_Click(object sender, RoutedEventArgs e)
        {

            this.NavigationService.Navigate(new PageEtudiant());
        }
    }
}
