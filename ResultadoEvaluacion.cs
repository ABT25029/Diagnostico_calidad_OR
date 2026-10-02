using System.Collections.Generic;

namespace API
{
    public class ResultadoEvaluacion
    {
        public bool CumpleParametros { get; set; }
        public string EstadoGeneral { get; set; } = string.Empty;
        public double PorcentajeRecuperacion { get; set; }
        public List<string> Alertas { get; set; } = new List<string>();
    }
}

