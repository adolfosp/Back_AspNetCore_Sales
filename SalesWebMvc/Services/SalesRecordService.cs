using Microsoft.EntityFrameworkCore;
using SalesWebMvc.Data;
using SalesWebMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesWebMvc.Services
{
    public class SalesRecordService
    {

        private readonly SalesWebMvcContext _context;

        public SalesRecordService(SalesWebMvcContext context)
        {
            _context = context;
        }

        public async Task<List<SalesRecord>> FindByDateAsync(DateTime? dataInicial, DateTime? dataFinal)
        {
            var result = from obj in _context.SalesRecord select obj;

            if (dataInicial.HasValue)
            {
                result =  result.Where(x => x.Date >= dataInicial.Value);
            }
            if (dataFinal.HasValue)
            {
                result = result.Where(x => x.Date <= dataFinal.Value);
            }
            return await result
                .Include(x => x.Seller)
                .Include(x => x.Seller.Department)
                .OrderByDescending(x => x.Date)
                .ToListAsync();
        }

        public async Task<List<IGrouping<Department,SalesRecord>>> FindByDateGroupAsync(DateTime? dataInicial, DateTime? dataFinal)
        {
            var result = from obj in _context.SalesRecord select obj;

            if (dataInicial.HasValue)
            {
                result = result.Where(x => x.Date >= dataInicial.Value);
            }
            if (dataFinal.HasValue)
            {
                result = result.Where(x => x.Date <= dataFinal.Value);
            }
            return await result
                .Include(x => x.Seller)
                .Include(x => x.Seller.Department)
                .OrderByDescending(x => x.Date)
                .GroupBy(x => x.Seller.Department)
                .ToListAsync();
        }
    }
}
