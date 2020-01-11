using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
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
    /// Logique d'interaction pour PageGestionEt.xaml
    /// </summary>
    public partial class PageGestionEt : Page
    {
        public PageGestionEt(etudiant et)
        {
            InitializeComponent();
            this.DataContext = this;
            model = new FromLinq();
            list = model.EtudiantList.ToList();
            radDataForm1.ItemsSource = list;
            if(et != null)
            {
                etudiant = model.convertEtudiant(et);
                radDataForm1.CurrentIndex = getIndex();

                string url = et.prenom.ToString();
            }
            
            path = Directory.GetParent(Environment.CurrentDirectory).Parent.FullName;

        }

        string path;
        ProjetDataContext db = new ProjetDataContext();
        FromLinq model;
        List<ClasseEtudiant> list;
        ClasseEtudiant etudiant;

        private int getIndex()
        {
            int i = 0;
            foreach(ClasseEtudiant et in list)
            {
                if(et.Cne == etudiant.Cne)
                    return i;

                i++;
            }
            
            return 0;
        }

        private etudiant getEtudiant()
        {
            ClasseEtudiant student = radDataForm1.CurrentItem as ClasseEtudiant;
            var et = (from etudiant in db.etudiant
                     where etudiant.cne == student.Cne
                     select etudiant).SingleOrDefault();

            return et;
        }


        private void Data_EditEnding(object sender, Telerik.Windows.Controls.Data.DataForm.EditEndingEventArgs e)
        {
            ClasseEtudiant student = radDataForm1.CurrentItem as ClasseEtudiant;

            var x = (from etudiant in db.etudiant
                       join filiere in db.Filiere
                       on etudiant.id_fil equals filiere.Id_filiere
                       where etudiant.cne == student.Cne
                       select etudiant).SingleOrDefault();
            
            if (x != null)
            {
                x.nom = student.Nom;
                x.prenom = student.Prenom;
                x.date_naiss = student.Date_naiss;
                if (student.Sexe == Gender.Femme)
                    x.sexe = 'F' ;
                else if (student.Sexe == Gender.Homme)
                    x.sexe = 'H';
                else
                    x.sexe = 'X';

                bool find = false;
                int id = 0;
                bool erreur = false;

                var fil = from f in db.Filiere
                          select f;
                foreach (Filiere f in fil)
                {
                    if (f.Nom_filiere.Equals(student.Filiere))
                    {
                        find = true;
                        id = f.Id_filiere;
                    }

                }

                if (find)
                    x.id_fil = id;

                else
                {
                    MessageBox.Show("Veuillez Entrer une filiere valide !");
                    erreur = true;
                }
                if (!erreur)
                {
                    db.SubmitChanges();
                    MessageBox.Show("L'étudiant s'est bien modifié !");
                } else
                {
                    MessageBox.Show("la modification n'est pas enregistré !");
                }

            }
            else
            {
                bool erreur = false;
                ClasseEtudiant item = radDataForm1.CurrentItem as ClasseEtudiant;
                etudiant et = new etudiant();
                et.cne = item.Cne;
                et.nom = item.Nom;
                et.prenom = item.Prenom;
                et.date_naiss = item.Date_naiss;
                if (et.image == null)
                {
                    et.image = "default.png";
                }
                if (student.Sexe == Gender.Femme)
                    et.sexe = 'F';
                else if (student.Sexe == Gender.Homme)
                    et.sexe = 'H';
                else
                    et.sexe = 'X';

                bool find = false;
                int id = 0;

                var fil = from f in db.Filiere
                          select f;
                foreach (Filiere f in fil)
                {
                    if (f.Nom_filiere.Equals(item.Filiere))
                    {
                        find = true;
                        id = f.Id_filiere;
                    }

                }

                if (find)
                    et.id_fil = id;
                
                else
                {
                    MessageBox.Show("Veuillez Entrer une filiere valide !");
                    erreur = true;                
                }

                if (!erreur) {
                    
                    db.etudiant.InsertOnSubmit(et);
                    db.SubmitChanges();
                    MessageBox.Show("L'etudiant a été bien enregistré");
                } else
                {
                }

                
         
            }
        }

        private void Data_DeletingItem(object sender, System.ComponentModel.CancelEventArgs e)
        {
            ClasseEtudiant et = radDataForm1.CurrentItem as ClasseEtudiant;
            var x = (from c in db.etudiant
                     where c.cne == et.Cne
                     select c).SingleOrDefault();
            db.etudiant.DeleteOnSubmit(x);
            db.SubmitChanges();
        }

        private void RadDataForm1_AutoGeneratingField(object sender, Telerik.Windows.Controls.Data.DataForm.AutoGeneratingFieldEventArgs e)
        {
            ClasseEtudiant etudiant = this.radDataForm1.CurrentItem as ClasseEtudiant;
            if (e.PropertyName == "Cne")
            {
                e.DataField.Label = "CNE";
            }
            if (e.PropertyName == "Filiere")
            {
                e.DataField.Description = "Tappez un nom valide de filière Svp";
                //e.DataField.PreviewMouseDoubleClick += new MouseButtonEventHandler(changeFiliere);
                
                
            }
            if (etudiant.Sexe == Gender.Femme)
            {
                e.DataField.Foreground = new SolidColorBrush(Colors.Purple);
            }
            else
            {
                e.DataField.Foreground = new SolidColorBrush(Colors.Blue);
            }
        }

        

        private void Btn_retour_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new PageEtudiant());
        }

        private void Data_CurrentItemChanged(object sender, EventArgs e)
        {
            if(getEtudiant() != null)
            {
                photo.Source = new BitmapImage(new Uri(getEtudiant().myImage, UriKind.Absolute));
            } else
            {
                photo.Source = new BitmapImage(new Uri(path + @"\photos\default.png", UriKind.Absolute));
            }
        }

        
        private void Browse_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            String filepath;
            open.Multiselect = false;

            if (open.ShowDialog() == true)
            {
                filepath = open.FileName;

                string name = System.IO.Path.GetFileName(filepath);
                string appStartPathModified = String.Format(path + "\\{0}\\" + name, "photos");
                
                try
                {
                    File.Copy(filepath, appStartPathModified, true);
                }
                catch (Exception ex)
                {
                }

                try
                {
                    etudiant etudiant = getEtudiant();
                    etudiant.image = name;
                    db.SubmitChanges();
                    photo.Source = new BitmapImage(new Uri(etudiant.myImage, UriKind.Absolute));
                } catch (Exception ex)
                {
                    MessageBox.Show("Veuillez enregistrer l'etudiant avant de modifier la photo");
                }
            }
        }

        
    }
}
