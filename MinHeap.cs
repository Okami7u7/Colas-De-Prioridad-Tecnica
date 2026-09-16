using System;
using System.Collections.Generic;

namespace ColasDePrioridad_Elias_Hernandez
{
    public class MinHeap
    {
        // Aqui se van guardando los tickets del Heap
        private List<Ticket> tickets;

        public MinHeap()
        {
            tickets = new List<Ticket>();
        }

        // Devuelve la cantidad de tickets que hay actualmente
        public int Cantidad()
        {
            return tickets.Count;
        }

        // Revisa si el Heap esta vacio
        public bool EstaVacio()
        {
            return tickets.Count == 0;
        }

        // Compara dos tickets por su prioridad
        // Una prioridad menor significa que debe atenderse primero
        private bool TieneMayorPrioridad(Ticket primero, Ticket segundo)
        {
            return primero.Prioridad < segundo.Prioridad;
        }

        // Intercambia dos posiciones del Heap
        private void Intercambiar(int posicion1, int posicion2)
        {
            Ticket temporal = tickets[posicion1];
            tickets[posicion1] = tickets[posicion2];
            tickets[posicion2] = temporal;
        }

        // Agrega un nuevo ticket al Heap
        public void Insertar(Ticket ticket)
        {
            // Primero se agrega al final
            tickets.Add(ticket);

            // Despues se acomoda hacia arriba
            BubbleUp(tickets.Count - 1);
        }

        // Acomoda un elemento hacia arriba para mantener el Min Heap
        private void BubbleUp(int posicion)
        {
            while (posicion > 0)
            {
                // Formula para encontrar el padre
                int padre = (posicion - 1) / 2;

                // Si el ticket actual tiene menor prioridad numerica
                // que su padre, se intercambian
                if (TieneMayorPrioridad(tickets[posicion], tickets[padre]))
                {
                    Intercambiar(posicion, padre);
                    posicion = padre;
                }
                else
                {
                    // Si ya esta en su lugar, terminamos
                    break;
                }
            }
        }

        // Método para clonar la estructura actual
        // Permite extraer elementos en orden sin modificar la cola real
        public MinHeap Clonar()
        {
            MinHeap copia = new MinHeap();

            foreach (Ticket t in this.tickets)
            {
                // Creamos una nueva instancia de Ticket para desvincularlas por completo
                copia.tickets.Add(new Ticket(t.Codigo, t.Cliente, t.Descripcion, t.Prioridad));
            }

            return copia;
        }

        // Muestra el ticket que esta primero sin eliminarlo
        public Ticket Peek()
        {
            if (EstaVacio())
            {
                return null;
            }

            return tickets[0];
        }

        // Saca el ticket con la prioridad mas alta
        public Ticket ExtractMin()
        {
            if (EstaVacio())
            {
                return null;
            }

            // El primero siempre esta en la raiz
            Ticket ticketAtendido = tickets[0];

            // Si solo hay un ticket, simplemente lo quitamos
            if (tickets.Count == 1)
            {
                tickets.RemoveAt(0);
                return ticketAtendido;
            }

            // El ultimo ticket pasa temporalmente a la raiz
            tickets[0] = tickets[tickets.Count - 1];

            // Quitamos el ultimo porque ya lo pasamos a la raiz
            tickets.RemoveAt(tickets.Count - 1);

            // Ahora acomodamos la raiz hacia abajo
            BubbleDown(0);

            return ticketAtendido;
        }

        // Acomoda un elemento hacia abajo
        // para volver a dejar bien el Min Heap
        private void BubbleDown(int posicion)
        {
            while (true)
            {
                int hijoIzquierdo = (posicion * 2) + 1;
                int hijoDerecho = (posicion * 2) + 2;

                // Al inicio suponemos que la posicion actual es la menor
                int menor = posicion;

                // Revisamos el hijo izquierdo
                if (hijoIzquierdo < tickets.Count &&
                    TieneMayorPrioridad(tickets[hijoIzquierdo], tickets[menor]))
                {
                    menor = hijoIzquierdo;
                }

                // Revisamos el hijo derecho
                if (hijoDerecho < tickets.Count &&
                    TieneMayorPrioridad(tickets[hijoDerecho], tickets[menor]))
                {
                    menor = hijoDerecho;
                }

                // Si la posicion actual ya es la menor, terminamos
                if (menor == posicion)
                {
                    break;
                }

                // Si uno de los hijos tiene mayor prioridad,
                // hacemos el intercambio
                Intercambiar(posicion, menor);

                posicion = menor;
            }
        }

        // Devuelve los tickets tal como estan actualmente
        // en la estructura del Heap
        public List<Ticket> ObtenerTickets()
        {
            return tickets;
        }
    }
}