using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelerikWpfApp1
{
    class MaClasse : etudiant
    {
        private int nbr;

        public int Nbr { get => nbr; set => nbr = value; }

        private List<string> mesFilieres;

        public List<string> MesFilieres
        {
            get
            {
                if (this.mesFilieres == null)
                {
                    this.mesFilieres = new List<string>();
                    this.mesFilieres.Add("Femme");
                    this.mesFilieres.Add("Homme");
                }

                return this.mesFilieres;
            }
        }


        private Gender genre;
        public Gender Genre
        {
            get
            {
                return this.genre;
            }

            set
            {
                if (this.genre != value)
                    genre = value;
            }
        }
    }


    public enum Genderr
    {
    }
}
