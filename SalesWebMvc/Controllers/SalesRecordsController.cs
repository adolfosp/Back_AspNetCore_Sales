using Microsoft.AspNetCore.Mvc;
using SalesWebMvc.Models;
using SalesWebMvc.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesWebMvc.Controllers
{
    public class SalesRecordsController : Controller
    {

        private readonly SalesRecordService _salesRecordService;

        public SalesRecordsController(SalesRecordService salesRecordService)
        {
            _salesRecordService = salesRecordService;
        }

        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> SimpleSearch(DateTime? dataInicial , DateTime? dataFinal)
        {
            if (!dataInicial.HasValue )
            {
                dataInicial = new DateTime(DateTime.Now.Year, 1, 1);
            }
            if (!dataFinal.HasValue)
            {
                dataFinal =  DateTime.Now;
            }

            ViewData["dataInicial"] = dataInicial.Value.ToString("yyyy-MM-dd");
            ViewData["dataFinal"] = dataFinal.Value.ToString("yyyy-MM-dd");

            List<SalesRecord> result  = await _salesRecordService.FindByDateAsync(dataInicial, dataFinal);
            return View(result);
        }
        public IActionResult GroupingSearch()
        {
            return View();
        }
    }
}
