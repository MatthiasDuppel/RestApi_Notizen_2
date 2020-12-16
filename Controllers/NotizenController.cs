using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
// using Microsoft.AspNetCore.Mvc
using Microsoft.Extensions.Logging;
using NotizenApi.DomainObjects;
using NotizenApi.Persistence;
// using System.Threading.Tasks;

namespace NotizenApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NotizenController : ControllerBase
    {
        private static NotizenRepository _notizenRepository = new NotizenRepository();

        private readonly ILogger<NotizenController> _logger;

        public NotizenController(ILogger<NotizenController> logger)
        {
            _logger = logger;
            if(_notizenRepository.AnzahlNotizen == 0 )
                _notizenRepository.ErstelleZufallsnotizen(3);
        }


        [HttpGet]
        public IEnumerable<Notiz> HoleAlleNotizen()
        {
            return _notizenRepository.HoleAlleNotizen();
        }

        [HttpGet("{id}")]
        [HttpGet("view/{id}")]
        public Notiz HoleNotizById(int id)
        {
            return _notizenRepository.HoleNotizById(id);
        }

        [HttpPost]
        // public void SpeichereNotiz(Notiz eNotiz)
        public CreatedAtActionResult SpeichereNotiz(Notiz eNotiz)
        {
            _notizenRepository.SpeichereNotiz(eNotiz);

            return CreatedAtAction("SpeichereNotiz", new { id = eNotiz.Id }, eNotiz);
        }

        [HttpPut("update/{id}")]
        public StatusCodeResult  AktualisiereNotiz(int id, Notiz eNotiz)
        {
            if(id != eNotiz.Id)
                return BadRequest();
            
            if(_notizenRepository.HoleNotizById(id) == null)
                return NotFound();
            
            try{
                _notizenRepository.AktualisiereNotiz(eNotiz);
            }
            catch(Exception e)
            {
                _logger.LogError(e, "Fehler beim Speichern in der Datenbank", eNotiz);
                throw;
            }
            

            return NoContent();            
        }
        [HttpDelete("delete/{id}")]
        public StatusCodeResult LoescheNotiz(int id)
        {
            var n = _notizenRepository.HoleNotizById(id);
            if(n == null)
                return NotFound();

            try{
                _notizenRepository.LoescheNotiz(id);
            }
            catch(Exception e)
            {
                _logger.LogError(e, "Fehler beim Löschen der Notiz aus der Datenbank", id);
                throw;
            }
            

            return NoContent();
        }
    }
}
