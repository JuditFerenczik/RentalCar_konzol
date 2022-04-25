using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalCar_konzol
{
    class Auto
    {
        int ar;
        int id;
        List<Kolcsonzes> kolcsonzesek;
        string marka;
        string model;
        string rendszam;

        public Auto(int id, string rendszam, string marka, string model, int ar)
        {
            this.id = id;
            this.rendszam = rendszam;
            this.marka = marka;
            this.model = model;
            this.ar = ar;
        }

        public int Ar { get => ar;  }
        public int Id { get => id; }

        public string Marka { get => marka; }
        public string Model { get => model; }
        public string Rendszam { get => rendszam; }
    }
}
