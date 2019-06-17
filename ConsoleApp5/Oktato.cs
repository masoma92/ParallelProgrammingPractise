using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp5
{
    enum OktatoStatusz { Szabad, Feleltet}
    class Oktato
    {
        public int oktatoId { get; set; }
        public OktatoStatusz statusz { get; set; }
        public Hallgato hallgato { get; set; }
        public List<Hallgato> hallgatok { get; set; }

        public Oktato(int oktatoId)
        {
            this.oktatoId = oktatoId;
        }

        public void Feleltet()
        {
            while (!hallgatok.All(x => x.statusz == HallgatoStatusz.Végzett))
            {
                while (hallgato==null)
                {
                    this.statusz = OktatoStatusz.Szabad;
                    lock (this)
                    {
                        Monitor.Pulse(this);
                    }
                    Thread.Sleep(100);
                }
                this.statusz = OktatoStatusz.Feleltet;
            }
            this.statusz = OktatoStatusz.Szabad;
        }

        public override string ToString()
        {
            string hallgatoString = this.hallgato == null ? "nincs" : this.hallgato.hallgatoId.ToString();
            return $"{this.oktatoId}. oktató, státusz: {this.statusz}, hallgató: {hallgatoString}";
        }
    }
}
