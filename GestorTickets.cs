using System;
using System.Collections.Generic;

namespace ColasDePrioridad_Elias_Hernandez
{
    public class GestorTickets
    {
        private MinHeap cola;
        // Guardamos las fotos/copias de cada cambio en la cola
        private List<MinHeap> historial;

        public GestorTickets()
        {
            cola = new MinHeap();
            historial = new List<MinHeap>();
        }

        // Registra un nuevo ticket y guarda el estado en el historial
        public bool RegistrarTicket(string codigo, string cliente, string descripcion, int prioridad)
        {
            if (BuscarTicket(codigo) != null)
            {
                return false;
            }

            Ticket nuevoTicket = new Ticket(codigo, cliente, descripcion, prioridad);
            cola.Insertar(nuevoTicket);

            // Guardamos una copia del estado actual
            historial.Add(cola.Clonar());
            return true;
        }

        // Busca un ticket recorriendo el Heap
        public Ticket BuscarTicket(string codigo)
        {
            foreach (Ticket ticket in cola.ObtenerTickets())
            {
                if (ticket.Codigo.Equals(codigo, StringComparison.OrdinalIgnoreCase))
                {
                    return ticket;
                }
            }
            return null;
        }

        // Devuelve el siguiente ticket sin eliminarlo
        public Ticket SiguienteTicket()
        {
            return cola.Peek();
        }

        // Atiende el ticket y guarda el nuevo estado en el historial
        public Ticket AtenderTicket()
        {
            Ticket atendido = cola.ExtractMin();

            if (atendido != null)
            {
                // Guardamos la copia de la cola ya sin el ticket atendido
                historial.Add(cola.Clonar());
            }

            return atendido;
        }

        // Imprime el historial completo de estados
        public void MostrarCola()
        {
            if (historial.Count == 0)
            {
                Console.WriteLine("\nNo hay registros guardados en la cola.");
                return;
            }

            // Recorremos cada estado guardado (Registro 1, Registro 2, etc.)
            for (int i = 0; i < historial.Count; i++)
            {
                Console.WriteLine($"\n========= REGISTRO #{i + 1} =========");

                MinHeap copia = historial[i].Clonar();

                if (copia.EstaVacio())
                {
                    Console.WriteLine("(La cola quedó vacía)");
                }
                else
                {
                    int turno = 1;
                    while (!copia.EstaVacio())
                    {
                        Ticket t = copia.ExtractMin();
                        Console.WriteLine($"\nTurno {turno}");
                        Console.WriteLine("-------------------------");
                        Console.WriteLine(t);
                        turno++;
                    }
                }
            }
        }

        public int CantidadTickets()
        {
            return cola.Cantidad();
        }
    }
}