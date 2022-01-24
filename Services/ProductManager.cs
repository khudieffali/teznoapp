using DataAccess;
using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ProductManager
    {
        private readonly ApplicationDbContext _context;

        public ProductManager(ApplicationDbContext context)
        {
            _context = context;
        }
        public List<Product> GetProducts()
        {
         return   _context.Products.Include(x=>x.Category).Where(x=>!x.IsDeleted).ToList();
        }
        public Product GetById(int? id)
        {
            var selectedProduct = _context.Products.Include(x => x.Category).Where(x => !x.IsDeleted).FirstOrDefault(x => x.ID == id);
            if (selectedProduct != null) ;
           return selectedProduct ;
        }
        public void Add(Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges(); 
        }
        public void Update(Product product)
        {
            _context.Products.Update(product);
            _context.SaveChanges();
        }
        public void Delete(Product product)
        {
            product.IsDeleted = true;
            _context.SaveChanges();
        }
        public bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.ID == id);
        }
    }
}
