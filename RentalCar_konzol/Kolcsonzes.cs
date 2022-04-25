using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalCar_konzol
{
    class Kolcsonzes
    {
        int id;
        DateTime tol;
        DateTime ig;
        public int Id { get => id; }
        public DateTime Tol { get =>  tol; }
        public DateTime Ig{ get => ig;  }

        public Kolcsonzes(int id, DateTime tol, DateTime ig)
        {
            this.id = id;
            this.tol = tol;
            this.ig = ig;
        }
    }
}
