using lopuhhh.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lopuhhh.ViewModels
{
    public class MainViewModels : ViewModelsBase
    {
        private List<Product> _products = new List<Product>();
        public List<Product> Products
        {
            get => _products;
            set => Set(ref _products, value, nameof(Products));
        }

        public MainViewModels()
        {
            using (prazdnikova_lopushokContext context = new prazdnikova_lopushokContext())
            {
                _products = context.Products
                    .Include(t => t.ProductType)
                    .Include(pm => pm.ProductMaterials)
                    .ThenInclude(m => m.Material)
                    .ToList();
            }
            
        }
    }
}
