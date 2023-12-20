﻿using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.DataAcess.Data;
using BulkyBook.Models;
using BulkyBook.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace BulkyBookWeb.Areas.Admin.Controllers;
[Area("Admin")]
public class ProductController : Controller
{
    // private readonly ApplicationDbContext _db;
    // public ProductController(ApplicationDbContext db)
    
    private readonly IUnitOfWork _unitOfWork; 
    public ProductController(IUnitOfWork unitOfWork)
    {
        //_db = db;
        _unitOfWork = unitOfWork;
    }
    public IActionResult Index()
    {
        List<Product> objProductList = _unitOfWork.Product.GetAll().ToList(); //_db.Categories
        return View(objProductList);
    }
    public IActionResult Create()
    {
        // IEnumerable<SelectListItem> CategoryList = _unitOfWork.Category
        //     .GetAll().Select(u=> new SelectListItem
        //     {
        //     Text = u.Name,
        //     Value = u.Id.ToString()
        //     });
        // // ViewBag.CategoryList = CategoryList;
        // ViewData["CategoryList"] = CategoryList;
        // return View();
        ProductVM productVM = new()
        {
            CategoryList = _unitOfWork.Category.GetAll().Select(u=> new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString()
            }),
            Product = new Product()
        };
        return View(productVM);
        // if (id == null || id == 0)
        // {
        //     //create
        //     return View(productVM);
        // }
        // else
        // {
        //     //update
        //     productVM.Product = _unitOfWork.Product.Get(u=>u.Id==id);
        //     return View(productVM);
        // }
    }
    [HttpPost]
    public IActionResult Create(ProductVM productVM)
    {   
        
        if (ModelState.IsValid)
        {
            //_db.Categories.Add(obj);
            //_db.SaveChages();
            _unitOfWork.Product.Add(productVM.Product);
            _unitOfWork.Save();
            TempData["success"] =  "Product created successfully";
            return RedirectToAction("Index");
        }
        else 
        {
            productVM.CategoryList = _unitOfWork.Category.GetAll().Select(u=> new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString()
            });
            return View(productVM);
        }
    }

    // public IActionResult Edit(int? id)
    // {
    //     if(id==null || id == 0)
    //     {
    //         return NotFound();
    //     }
    //     // Product? productFromDb = _db.Products.Find(id);
    //     Product? productFromDb = _unitOfWork.Product.Get(u=>u.Id==id);
    //     // Product? productFromDb1 = _db.Products.FirstOrDefault(u=>u.Id==id);
    //     // Product? productFromDb2 = _db.Products.Where(u=>u.Id==id).FirstOrDefault();
    //     if (productFromDb == null)
    //     {
    //         return NotFound();
    //     }
    //     return View(productFromDb);
    // }
    // [HttpPost]
    // public IActionResult Edit(Product obj)
    // {   
    //     if (ModelState.IsValid){
    //         //_db.Categories.Add(obj);
    //         //_db.SaveChages();
    //         _unitOfWork.Product.Update(obj);
    //         _unitOfWork.Save();
    //         TempData["success"] =  "Product update successfully";
    //         return RedirectToAction("Index");
    //     }
    //     return View();
        
    // }


    public IActionResult Delete(int? id)
    {
        if(id==null || id == 0)
        {
            return NotFound();
        }
        // Product? productFromDb = _db.Categories.Find(id);
        Product? productFromDb = _unitOfWork.Product.Get(u=>u.Id==id);
        // Product? productFromDb1 = _db.Categories.FirstOrDefault(u=>u.Id==id);
        // Product? productFromDb2 = _db.Categories.Where(u=>u.Id==id).FirstOrDefault();
        if (productFromDb == null)
        {
            return NotFound();
        }
        return View(productFromDb);
    }
    [HttpPost, ActionName("Delete")]
    public IActionResult DeletePOST(int? id)
    {   
        // Product? obj = _db.Categories.Find(id);
        Product? obj  = _unitOfWork.Product.Get(u=>u.Id==id);
        if (obj == null)
        {
            return NotFound();
        }
        //_db.Categories.Add(obj);
        //_db.SaveChages();
        _unitOfWork.Product.Remove(obj);
        _unitOfWork.Save();
        TempData["success"] =  "Product delete successfully";
        return RedirectToAction("Index");
        
    }
}