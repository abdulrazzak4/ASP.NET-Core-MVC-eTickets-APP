using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using webApp.Data;
using webApp.Data.Services;
using webApp.Models;

namespace webApp.Controllers
{
    public class ProducersController : Controller
    {
        private readonly IProducersService _service; 
         public ProducersController(IProducersService service)
         {
             _service = service ;
         }
        public async Task<IActionResult> Index()
        {
            var allProducer = await _service.GetAllAsync();
            return View(allProducer);
        }
        //Get:Producers/Create
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create([Bind("FullName,ProfilePictureURL,Bio")]Producer producer){
            if(!ModelState.IsValid) 
            {
                return View(producer);
            }
            await _service.AddAsync(producer);
            return RedirectToAction(nameof(Index));
        }
        //Get:Producers/Details/1
        public async Task<IActionResult> Details(int id)
        {
            var ProducerDetails =await _service.GeytByIdAsync(id);
            if(ProducerDetails == null) return View("NotFound");
            return View(ProducerDetails);
        }
        //Get:producers/Edit/1
        public async Task<IActionResult> Edit(int id)
        {
            // await Details(id);
            var ProducerDetails =await _service.GeytByIdAsync(id);
            if(ProducerDetails == null) return View("NotFound");
            return View(ProducerDetails);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ProfilePictureURL,FullName,Bio")] Producer producer)
        {
            if (!ModelState.IsValid) return View(producer);

            if(id == producer.Id)
            {
                await _service.UpdateAsync(id, producer);
                return RedirectToAction(nameof(Index));
            }
            return View(producer);
        }
        //Get:producers/Delete/1
        public async Task<IActionResult> Delete(int id)
        {
            var ProducerDetails =await _service.GeytByIdAsync(id);
            if(ProducerDetails == null) return View("NotFound");
            return View(ProducerDetails);
        }
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id){
            var ProducerDetails =await _service.GeytByIdAsync(id);
            if(ProducerDetails == null) return View("NotFound");

            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}