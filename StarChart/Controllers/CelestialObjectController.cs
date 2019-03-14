using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StarChart.Data;

namespace StarChart.Controllers
{
    [Route("")]
    [ApiController]
    public class CelestialObjectController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public CelestialObjectController(ApplicationDbContext applicationDbContext)
        { _context = applicationDbContext; }
        [HttpGet("{id:int}",Name="GetById")]
        public IActionResult GetById(int id)
        {
        var celestialObject = _context.CelestialObjects.Find(id);
            if (celestialObject == null)
                return NotFound();
            celestialObject.Satellites = _context.CelestialObjects.Where(e => e.OrbitedObjectId == id).ToList();
            return Ok(celestialObject);
        }

        [HttpGet("{name}")]
        public IActionResult GetByName(string name)
        {
            var celestialObject = _context.CelestialObjects.Find(name);
            if (celestialObject == null)
            return NotFound();
            celestialObject.Satellites = _context.CelestialObjects.Where(e => e.Name == name).ToList();
            return Ok(celestialObject);
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            foreach (var celestialObject in _context.CelestialObjects)
                celestialObject.Satellites = _context.CelestialObjects.ToList();

            return Ok(_context.CelestialObjects);

        }
    }
}
