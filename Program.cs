using System;

namespace ColasDePrioridad_Elias_Hernandez
{
    internal class Program
    {
        static void Main(string[] args)
        {
            GestorTickets gestor = new GestorTickets();

            int opcion;

            do
            {
                Console.Clear();

                Console.WriteLine("========================================");
                Console.WriteLine("       SISTEMA DE SOPORTE TECNICO");
                Console.WriteLine("========================================");
                Console.WriteLine("1. Registrar ticket");
                Console.WriteLine("2. Ver siguiente ticket");
                Console.WriteLine("3. Atender ticket");
                Console.WriteLine("4. Mostrar cola de prioridad");
                Console.WriteLine("5. Buscar ticket");
                Console.WriteLine("6. Mostrar cantidad de tickets");
                Console.WriteLine("7. Salir");
                Console.WriteLine("========================================");
                Console.Write("Seleccione una opcion: ");

                if (!int.TryParse(Console.ReadLine(), out opcion))
                {
                    opcion = 0;
                }

                Console.Clear();

                switch (opcion)
                {
                    case 1:
                        RegistrarTicket(gestor);
                        break;

                    case 2:
                        MostrarSiguiente(gestor);
                        break;

                    case 3:
                        AtenderTicket(gestor);
                        break;

                    case 4:
                        gestor.MostrarCola();
                        Pausar();
                        break;

                    case 5:
                        BuscarTicket(gestor);
                        break;

                    case 6:
                        MostrarCantidad(gestor);
                        break;

                    case 7:
                        Console.WriteLine("Saliendo del programa...");
                        break;

                    default:
                        Console.WriteLine("Opcion no valida.");
                        Pausar();
                        break;
                }

            } while (opcion != 7);
        }

        // Opcion para registrar un nuevo ticket
        static void RegistrarTicket(GestorTickets gestor)
        {
            Console.WriteLine("========== REGISTRAR TICKET ==========\n");

            Console.Write("Codigo del ticket (TCK0001): ");
            string codigo = Console.ReadLine();

            // Validamos que tenga el formato esperado
            if (!ValidarCodigo(codigo))
            {
                Console.WriteLine("\nEl codigo debe tener el formato TCK0000.");
                Pausar();
                return;
            }

            // Revisamos que el codigo no este repetido
            if (gestor.BuscarTicket(codigo) != null)
            {
                Console.WriteLine("\nEse codigo ya existe.");
                Pausar();
                return;
            }

            Console.Write("Nombre del cliente: ");
            string cliente = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(cliente))
            {
                Console.WriteLine("\nEl nombre del cliente no puede quedar vacio.");
                Pausar();
                return;
            }

            Console.Write("Descripcion del problema: ");
            string descripcion = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(descripcion))
            {
                Console.WriteLine("\nLa descripcion no puede quedar vacia.");
                Pausar();
                return;
            }

            Console.WriteLine("\nPrioridades:");
            Console.WriteLine("1 - Critica");
            Console.WriteLine("2 - Alta");
            Console.WriteLine("3 - Media");
            Console.WriteLine("4 - Baja");
            Console.WriteLine("5 - Muy Baja");

            Console.Write("\nSeleccione la prioridad: ");

            int prioridad;

            if (!int.TryParse(Console.ReadLine(), out prioridad) ||
                prioridad < 1 || prioridad > 5)
            {
                Console.WriteLine("\nLa prioridad debe estar entre 1 y 5.");
                Pausar();
                return;
            }

            bool registrado = gestor.RegistrarTicket(
                codigo,
                cliente,
                descripcion,
                prioridad
            );

            if (registrado)
            {
                Console.WriteLine("\nTicket registrado correctamente.");
            }
            else
            {
                Console.WriteLine("\nNo se pudo registrar el ticket.");
            }

            Pausar();
        }

        // Opcion para mostrar el ticket que esta primero
        static void MostrarSiguiente(GestorTickets gestor)
        {
            Console.WriteLine("========== SIGUIENTE TICKET ==========\n");

            Ticket ticket = gestor.SiguienteTicket();

            if (ticket == null)
            {
                Console.WriteLine("No hay tickets pendientes.");
            }
            else
            {
                Console.WriteLine("El siguiente ticket es:\n");
                Console.WriteLine(ticket);
            }

            Console.WriteLine("\nEste ticket no fue eliminado de la cola.");

            Pausar();
        }

        // Opcion para atender el ticket que esta primero
        static void AtenderTicket(GestorTickets gestor)
        {
            Console.WriteLine("========== ATENDER TICKET ==========\n");

            // SE CORRIGIÓ AQUÍ: Antes llamabas dos veces seguidas a gestor.AtenderTicket()
            Ticket atendido = gestor.AtenderTicket();

            if (atendido != null)
            {
                Console.WriteLine($"Ticket atendido con éxito:\n");
                Console.WriteLine(atendido);
            }
            else
            {
                Console.WriteLine("No hay tickets para atender.");
            }

            Pausar();
        }

        // Opcion para buscar un ticket por codigo
        static void BuscarTicket(GestorTickets gestor)
        {
            Console.WriteLine("========== BUSCAR TICKET ==========\n");

            Console.Write("Ingrese el codigo del ticket: ");
            string codigo = Console.ReadLine();

            Ticket ticket = gestor.BuscarTicket(codigo);

            if (ticket == null)
            {
                Console.WriteLine("\nNo se encontro un ticket con ese codigo.");
            }
            else
            {
                Console.WriteLine("\nTicket encontrado:\n");
                Console.WriteLine(ticket);
            }

            Pausar();
        }

        // Muestra la cantidad actual de tickets
        static void MostrarCantidad(GestorTickets gestor)
        {
            Console.WriteLine("========== CANTIDAD DE TICKETS ==========\n");

            int cantidad = gestor.CantidadTickets();

            Console.WriteLine($"Cantidad de tickets pendientes: {cantidad}");

            Pausar();
        }

        // Revisa que el codigo tenga TCK + 4 numeros
        static bool ValidarCodigo(string codigo)
        {
            if (string.IsNullOrWhiteSpace(codigo))
            {
                return false;
            }

            if (codigo.Length != 7)
            {
                return false;
            }

            if (!codigo.StartsWith("TCK", StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }

            for (int i = 3; i < codigo.Length; i++)
            {
                if (!char.IsDigit(codigo[i]))
                {
                    return false;
                }
            }

            return true;
        }

        // Hace una pausa para que se pueda leer el resultado
        static void Pausar()
        {
            Console.WriteLine("\nPresione ENTER para regresar al menu...");
            Console.ReadLine();
        }
    }
}