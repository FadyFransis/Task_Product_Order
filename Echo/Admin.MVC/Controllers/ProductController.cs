using App.Common.Services.Logger;
using App.Core.Interfaces.Services;
using Admin.MVC.Helper.Alerts;
using Admin.MVC.Services;
using Admin.MVC.ViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace Admin.MVC.Controllers
{
    //[Authorize(Roles = "Admin")]
    public class ProductController : Controller
    {
        private readonly IMapper _mapper;
        protected readonly Ilogger _logger;
        protected readonly IProductService _ProductService;
        protected readonly IOrderService _orderService;

        protected readonly CommonLocalizationService _localizer;
        public ProductController(Ilogger logger, IMapper mapper, IOrderService orderService,
             IProductService ProductService, CommonLocalizationService localizer)
        {
            _logger = logger;
            _mapper = mapper;
            _ProductService = ProductService;
            _localizer = localizer;
            _orderService = orderService;
        }
        public async Task<IActionResult> Index(bool saved)
        {
            var result = await _ProductService.GetAllProductsBaseModel();
            if (saved)
            {
                var list = _mapper.Map<List<ProductViewModel>>(result);
                return View(list).WithSuccess(_localizer.Get("Saved Successfully"));
            }
            else
            {
                var list = _mapper.Map<List<ProductViewModel>>(result);
                return View(list);
            }
            
        }
        public IActionResult Create()
        {
            return View();
        }

        public IActionResult Edit(long id)
        {
            return View();
        }

        [HttpPost, ActionName("DeleteProduct")]
        public async Task<IActionResult> Delete(long Id)
        {
            try
            {
                await _ProductService.ChangeStatus(Id, false);
                
                return RedirectToAction("Index").WithSuccess(_localizer.Get("Deleted !"));
            }
            catch (Exception ex)
            {
                _logger.Error("Error occured ProductController\\DeleteProduct" + Id + " with EX: " + ex.ToString());
                return RedirectToAction("Index").WithError(_localizer.Get("Error In Deleting"));
            }
        }
    }
}
