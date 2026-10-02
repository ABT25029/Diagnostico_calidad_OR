namespace API
{
    public class CalidadAguaRequest
    {
        public double Ph { get; set; }
        public double Conductividad { get; set; } // En µS/cm
        public double FlujoPermeado { get; set; }  // En m³/h o L/min
        public double FlujoRechazo { get; set; }   // En m³/h o L/min
    }
}
