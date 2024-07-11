using Microsoft.AspNetCore.Mvc;
using Microsoft.Win32;
using Registro_estacionamiento.BR;
using Registro_estacionamiento.Data;
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
            RegistrosModel registro = _registrosBR.GetVehiculoPlaca(numeroDePlaca);
            VehiculosModel vehiculo = _vehiculosBR.GetVehiculoPlaca(numeroDePlaca);
            double CostoEstacionamientoHora = _registrosBR.GetParametrosActivos("CostoEstacionamientoHora");
            if (registro == null)
            {
                if (vehiculo == null)
                {
                    vehiculo = new VehiculosModel { NumeroDePlaca = numeroDePlaca };
                    registro = new RegistrosModel
                    {
                        FechaHoraEntrada = DateTime.Now,
                        CostoPorHora = CostoEstacionamientoHora,
                        VehiculoId = vehiculo.Id
                    };
                }
                else
                { 
                    registro = new RegistrosModel
                    {
                        FechaHoraEntrada = DateTime.Now,
                        CostoPorHora = CostoEstacionamientoHora,
                        VehiculoId = vehiculo.Id
                    };
                }
                int id=_vehiculosBR.CrearVehiculo(vehiculo, registro);
                return RedirectToAction("Index");
            }
            else
            {
                registro.FechaHoraSalida = DateTime.Now;
                var diferenciaHoras = (registro.FechaHoraSalida.HasValue ?
                 (registro.FechaHoraSalida.Value - registro.FechaHoraEntrada).TotalHours : 0);
                registro.CostoTotal = registro.CostoPorHora * diferenciaHoras;
                _registrosBR.ActualizarRegistro(registro);
            }


            ViewBag.FechaHoraSalida = registro.FechaHoraSalida;
            if (registro.CostoTotal.HasValue) { 
                string costoTotalFormateado = registro.CostoTotal.Value.ToString("C2");
                ViewBag.CostoTotal = costoTotalFormateado;
            }
            else{
                ViewBag.CostoTotal = "Sin costo registrado";
            }
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateRegistroAsync(string numeroDePlaca)
        {
            RegistrosModel registro = _registrosBR.GetVehiculoPlaca(numeroDePlaca);

            double CostoEstacionamientoHora = 0.5f;
            CostoEstacionamientoHora=_registrosBR.GetParametrosActivos("CostoEstacionamientoHora");
            if (registro == null)
            {
                var vehiculo = new VehiculosModel { NumeroDePlaca = numeroDePlaca };
                _vehiculosBR.AddVehiculo(vehiculo);
                registro = new RegistrosModel
                {
                    FechaHoraEntrada = DateTime.Now,
                    CostoPorHora = CostoEstacionamientoHora,
                    VehiculoId = vehiculo.Id
                };

                _registrosBR.AddRegistro(registro);
            }
            else
            {
                registro.FechaHoraSalida = DateTime.Now.AddHours(1);
                var diferenciaHoras = (registro.FechaHoraSalida.HasValue ?
                 (registro.FechaHoraSalida.Value - registro.FechaHoraEntrada).TotalHours : 0);
                registro.CostoTotal = registro.CostoPorHora * diferenciaHoras;
                _registrosBR.ActualizarRegistro(registro);
            }


            ViewBag.FechaHoraSalida = registro.FechaHoraSalida;
            ViewBag.CostoTotal = registro.CostoTotal;
            return View();
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

        public IActionResult CreateVehiculo()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateVehiculo(string numeroDePlaca)
        {
                VehiculosModel existingVehiculo = _vehiculosBR.GetVehiculoPlaca(numeroDePlaca);
                if (existingVehiculo != null)
                {
                    ViewBag.Error = numeroDePlaca+" Ya se encuentra registrado.";
                    //ModelState.AddModelError("NumeroDePlaca", "El número de placa ya existe.");
                    return View();
                }
                else { 
                    existingVehiculo = new VehiculosModel();
                    existingVehiculo.NumeroDePlaca = numeroDePlaca;
                }
                _vehiculosBR.AddVehiculo(existingVehiculo);
                ViewBag.Message = "Vehículo creado correctamente.";
                return View();
        }
    }
}
