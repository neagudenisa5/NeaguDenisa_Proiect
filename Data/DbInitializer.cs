using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NeaguDenisa_Proiect.Models;

namespace NeaguDenisa_Proiect.Data
{
    public class DbInitializer
    {
        public static void Initialize(SpitalContext context)
        {
            context.Database.EnsureCreated();
            if (context.Medici.Any())
            {
                return; //baza de date a fost deja creata
            }
            var medici = new Medic[]
            {
                new Medic{Nume="Caciula Dorin",Sef="Mihai Dragnea",Salariu=Decimal.Parse("55000")},
                new Medic{Nume="Gavris Monica",Sef="Anca Popescu",Salariu=Decimal.Parse("76000")},
                new Medic{Nume="Popa Diana",Sef="Diana Manea",Salariu=Decimal.Parse("43000")},
                new Medic{Nume="Nistor Ramona",Sef="Alin Mihu",Salariu=Decimal.Parse("90000")},
                new Medic{Nume="Coman Laura",Sef="Dan Popa",Salariu=Decimal.Parse("75000")},
                new Medic{Nume="Diaconu Raluca",Sef="Camelia Popescu",Salariu=Decimal.Parse("85000")}

            };
            foreach (Medic s in medici)
            {
                context.Medici.Add(s);
            }
            context.SaveChanges();

            var pacienti = new Pacient[]
            {
                new Pacient{PacientID=10,Nume="Neagu Denisa",DataNasterii=DateTime.Parse("1999-04-05")},
                new Pacient{PacientID=20,Nume="Voicu Lavinia",DataNasterii=DateTime.Parse("1999-05-06")},
                new Pacient{PacientID=30,Nume="Danila Marian",DataNasterii=DateTime.Parse("1998-08-01")},
                new Pacient{PacientID=40,Nume="Popa Iuliana",DataNasterii=DateTime.Parse("1979-09-01")},
                new Pacient{PacientID=50,Nume="Ivan Antonia",DataNasterii=DateTime.Parse("1970-09-29")},
                new Pacient{PacientID=60,Nume="Mihu Ioan",DataNasterii=DateTime.Parse("1950-08-03")}
            };
            foreach (Pacient c in pacienti)
            {
                context.Pacienti.Add(c);
            }
            context.SaveChanges();

            var programari = new Programare[]
             {
             new Programare{MedicID=1,PacientID=10},
             new Programare{MedicID=3,PacientID=20},
             new Programare{MedicID=2,PacientID=60},
             new Programare{MedicID=1,PacientID=50},
             new Programare{MedicID=6,PacientID=40},
             new Programare{MedicID=5,PacientID=30},
             };
            foreach (Programare e in programari)
            {
                context.Programari.Add(e);
            }
            context.SaveChanges();
        }
    }
}
