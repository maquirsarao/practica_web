using Microsoft.AspNetCore.Mvc;
using Microsoft.Win32;
using Registro_estacionamiento.BR;
using Registro_estacionamiento.Models;
using System.Diagnostics;

namespace Registro_estacionamiento.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly RegistrosBR _registrosBR;
        private readonly VehiculosBR _vehiculosBR;

        public HomeController(RegistrosBR registroBR, ILogger<HomeController> logger, VehiculosBR vehiculosBR)
        {
            _registrosBR = registroBR;
            _logger = logger;
            _vehiculosBR = vehiculosBR;
        }

        public IActionResult Index()
        {
            var registros = _registrosBR.ObtenerTodosLosRegistros();
            return View(registros);
        }

        public IActionResult Details(int id)
        {
            var registro = _registrosBR.ObtenerRegistroPorId(id);
            if (registro == null)
            {
                return NotFound();
            }
            return View(registro);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(string numeroDePlaca)
        {
            var registro = _registrosBR.GetVehiculoPlaca(numeroDePlaca);
            if (registro == null) {
                var vehiculo = new VehiculosModel { NumeroDePlaca = numeroDePlaca };
                _vehiculosBR.AddVehiculo(vehiculo);
                registro = new RegistrosModel
                {
                    FechaHoraEntrada = DateTime.Now,
                    VehiculoId = vehiculo.Id
                };

                _registrosBR.AddRegistro(registro);
            }
            else
            {
                registro.FechaHoraSalida = DateTime.Now.AddHours(1);
                registro.CostoPorHora = 10;
                registro.CostoTotal = 10 * 1;
                _registrosBR.ActualizarRegistro(registro);
            }

            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var registro = _registrosBR.ObtenerRegistroPorId(id);
            if (registro == null)
            {
                return NotFound();
            }
            return View(registro);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, RegistrosModel registro)
        {
            if (id != registro.Id)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                _registrosBR.ActualizarRegistro(registro);
                return RedirectToAction(nameof(Index));
            }
            return View(registro);
        }

        public IActionResult Delete(int id)
        {
            var registro = _registrosBR.ObtenerRegistroPorId(id);
            if (registro == null)
            {
                return NotFound();
            }
            return View(registro);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _registrosBR.EliminarRegistro(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
