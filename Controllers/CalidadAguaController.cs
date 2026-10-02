using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore; // <-- Librería necesaria para el ToListAsync
using API;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CalidadAguaController : ControllerBase
{
    private readonly AppDbContext _context;

    public CalidadAguaController(AppDbContext context)
    {
        _context = context;
    }

    [HttpPost("evaluar")]
    public async Task<ActionResult<ResultadoEvaluacion>> EvaluarCalidad([FromBody] CalidadAguaRequest datos)
    {
        var resultado = new ResultadoEvaluacion();
        
        if (datos.Ph < 6.5 || datos.Ph > 8.5)
        {
            resultado.Alertas.Add($"pH fuera de rango operacional ({datos.Ph}). Rango ideal: 6.5 - 8.5.");
        }

        if (datos.Conductividad > 50.0)
        {
            resultado.Alertas.Add($"Conductividad elevada ({datos.Conductividad} µS/cm). Indica desgaste de membranas.");
        }

        double flujoTotal = datos.FlujoPermeado + datos.FlujoRechazo;
        if (flujoTotal <= 0)
        {
            return BadRequest("Los flujos de agua permeada y rechazo deben ser mayores a cero.");
        }

        resultado.PorcentajeRecuperacion = Math.Round((datos.FlujoPermeado / flujoTotal) * 100, 2);

        if (resultado.PorcentajeRecuperacion < 50.0 || resultado.PorcentajeRecuperacion > 75.0)
        {
            resultado.Alertas.Add($"El porcentaje de recuperación ({resultado.PorcentajeRecuperacion}%) está fuera del rango óptimo.");
        }

        if (resultado.Alertas.Count == 0)
        {
            resultado.CumpleParametros = true;
            resultado.EstadoGeneral = "ÓPTIMO: El agua tratada cumple perfectamente con todos los parámetros.";
        }
        else
        {
            resultado.CumpleParametros = false;
            resultado.EstadoGeneral = "ALERTA: Se detectaron anomalías en los parámetros físicos o de operación.";
        }

        var nuevoRegistro = new RegistroAgua
        {
            Ph = datos.Ph,
            Conductividad = datos.Conductividad,
            FlujoPermeado = datos.FlujoPermeado,
            FlujoRechazo = datos.FlujoRechazo,
            PorcentajeRecuperacion = resultado.PorcentajeRecuperacion,
            CumpleParametros = resultado.CumpleParametros,
            EstadoGeneral = resultado.EstadoGeneral
        };

        _context.HistorialPlanta.Add(nuevoRegistro);
        await _context.SaveChangesAsync();

        return Ok(resultado);
    }

    // NUEVA RUTA PARA EL NAVEGADOR WEB
    [HttpGet("historial")]
    public async Task<ActionResult> ObtenerHistorial()
    {
        var historial = await _context.HistorialPlanta.ToListAsync();
        return Ok(historial);
    }
}
