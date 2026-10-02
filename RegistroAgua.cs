using System;

namespace API;

public class RegistroAgua
{
    public int Id { get; set; } // Llave primaria automática
    public DateTime FechaRegistro { get; set; } = DateTime.Now;
    public double Ph { get; set; }
    public double Conductividad { get; set; }
    public double FlujoPermeado { get; set; }
    public double FlujoRechazo { get; set; }
    public double PorcentajeRecuperacion { get; set; }
    public bool CumpleParametros { get; set; }
    public string EstadoGeneral { get; set; } = string.Empty;
}
