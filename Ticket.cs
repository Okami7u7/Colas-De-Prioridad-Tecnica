namespace ColasDePrioridad_Elias_Hernandez
{
    public class Ticket
    {
        // Datos que va a tener cada ticket
        public string Codigo { get; set; }
        public string Cliente { get; set; }
        public string Descripcion { get; set; }
        public int Prioridad { get; set; }

        // Constructor para crear un ticket con sus datos
        public Ticket(string codigo, string cliente, string descripcion, int prioridad)
        {
            Codigo = codigo;
            Cliente = cliente;
            Descripcion = descripcion;
            Prioridad = prioridad;
        }

        // Esto sirve para mostrar el ticket de una forma más ordenada
        public override string ToString()
        {
            string tipoPrioridad = "";

            switch (Prioridad)
            {
                case 1:
                    tipoPrioridad = "Critica";
                    break;
                case 2:
                    tipoPrioridad = "Alta";
                    break;
                case 3:
                    tipoPrioridad = "Media";
                    break;
                case 4:
                    tipoPrioridad = "Baja";
                    break;
                case 5:
                    tipoPrioridad = "Muy Baja";
                    break;
                default:
                    tipoPrioridad = "Desconocida";
                    break;
            }

            return $"Codigo: {Codigo}\n" +
                   $"Cliente: {Cliente}\n" +
                   $"Problema: {Descripcion}\n" +
                   $"Prioridad: {Prioridad} - {tipoPrioridad}";
        }
    }
}