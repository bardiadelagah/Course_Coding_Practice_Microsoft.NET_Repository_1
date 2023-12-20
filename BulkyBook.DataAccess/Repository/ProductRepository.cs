﻿using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.DataAcess.Data;
using BulkyBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.DataAccess.Repository 
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private ApplicationDbContext _db;
        public ProductRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        // public void Save()
        // {
        //     //throw new NotImplementedException();
        //     //_db.SaveChanges();
        // }

        public void Update(Product obj)
        {
            //throw new NotImplementedException();
            _db.Products.Update(obj);
        }
    }
}
 