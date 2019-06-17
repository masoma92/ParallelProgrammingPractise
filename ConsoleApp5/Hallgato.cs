using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp5
{
    enum HallgatoStatusz { KintVar, BentVar, Kidolgoz, Felel, Végzett}
    class Hallgato
    {
        public int hallgatoId { get; set; }
        public HallgatoStatusz statusz { get; set; }
        public int tetelSzam { get; set; }
        private static Random rnd = new Random();
        public List<Oktato> oktatok { get; set; }
        Oktato aktualisoktato;

        public Hallgato(int hallgatoId, List<Oktato> oktatok)
        {
            this.hallgatoId = hallgatoId;
            this.statusz = HallgatoStatusz.KintVar;
            this.oktatok = oktatok;
        }

        public void HallgatoProcess()
        {
            Terem.sem.Wait();
            this.tetelSzam = Terem.getTetel();
            this.statusz = HallgatoStatusz.Kidolgoz;
            Thread.Sleep(rnd.Next(5000, 10000));

            Oktato oktato = this.oktatok[rnd.Next(0, 3)];


            lock (oktato)
            {
                Monitor.Wait(oktato);
            }
                
            oktato.hallgato = this;
            this.aktualisoktato = oktato;
            this.statusz = HallgatoStatusz.Felel;
            Thread.Sleep(rnd.Next(5000, 10000));
            this.statusz = HallgatoStatusz.Végzett;
            oktato.hallgato = null;
            Terem.sem.Release();
        }

        public override string ToString()
        {
            string aktualisOktato = this.aktualisoktato == null ? "épp nem felel" : this.aktualisoktato.oktatoId.ToString();
            return $"{this.hallgatoId}. hallgató, választottOktató: {aktualisOktato}, tételszám: {this.tetelSzam}, státusz: {this.statusz}";
        }
    }
}
