using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp2
{
    class Program
    {
        static void Main(string[] args)
        {
            var cuenta = new Cuenta
            {
                NombreTitular = "Sebis",
                Movimientos = new List<Movimiento>()
                {
                    new Movimiento
                    {
                        Emisor = "Sebis",
                        Monto = 1000M,
                        Tipo = TipoMovimiento.Deposito
                    },
                    new Movimiento
                    {
                        Emisor = "Diego",
                        Monto = 52.12M,
                        Tipo = TipoMovimiento.Extraccion
                    }
                }
            };

            var ordenadas = cuenta.Movimientos.OrderBy(x => x.Monto)
                .ThenByDescending(x => x.Emisor).ToList();

            var maximoMovimiento = cuenta.Movimientos.Max(x => x.Monto);

            var movimientosDistintoOrigen = cuenta.Movimientos
                .Where(x => x.Emisor != cuenta.NombreTitular).ToList();

            var saldoInicial = cuenta.Movimientos.Select(x => x.Monto).First();
            var saldos = new List<decimal>();
            saldos.Add(saldoInicial);

            for (int i = 1; i < cuenta.Movimientos.Count; i++)
            {
                var movimiento = cuenta.Movimientos[i];
                saldoInicial = saldoInicial + (movimiento.Tipo == TipoMovimiento.Extraccion
                                                ? movimiento.Monto * -1
                                                : movimiento.Monto);
                saldos.Add(saldoInicial);
            }

            var promedio = saldos.Average(x => x);

            var primero = cuenta.Movimientos.First();
            //var primero = cuenta.Movimientos.FirstOrDefault();

            var ultimo = cuenta.Movimientos.Last();
            //var ultimo = cuenta.Movimientos.LastOrDefault();

            var primerDeposito = cuenta.Movimientos.FirstOrDefault(x => x.Tipo == TipoMovimiento.Deposito);
            var ultimaExtraccion = cuenta.Movimientos.LastOrDefault(x => x.Tipo == TipoMovimiento.Extraccion);
        }
    }

    public class Cuenta
    {
        public string NombreTitular { get; set; }

        public List<Movimiento> Movimientos { get; set; }
    }

    public class Movimiento
    {
        public string Emisor { get; set; }

        public decimal Monto { get; set; }

        public TipoMovimiento Tipo { get; set; }
    }

    public enum TipoMovimiento
    {
        Deposito,
        Extraccion
    }
}