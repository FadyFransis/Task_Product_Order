using Admin.MVC.Helper.Alerts;
using Admin.MVC.Services;
using Admin.MVC.ViewModels;
using App.Common.Services.Logger;
using App.Core.Interfaces.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Admin.MVC.Controllers
{
    public class OrderController : Controller
    {
        private readonly IMapper _mapper;
        protected readonly Ilogger _logger;
        protected readonly IOrderService _orderService;
      
        protected readonly CommonLocalizationService _localizer;

        public OrderController(Ilogger logger, IMapper mapper, CommonLocalizationService localizer,
             IOrderService OrderService)
        {
            _logger = logger;
            _mapper = mapper;
            _orderService = OrderService;
            _localizer = localizer;
        }
        public async Task<IActionResult> Index(bool saved)
        {
            var result = await _orderService.GetAllOrders();
            var list = _mapper.Map<List<OrderViewModel>>(result.ToList());
            if (saved)
                return View(list).WithSuccess(_localizer.Get("Saved Successfully"));
            return View(list);
        }


        public async Task<IActionResult> Edit(long? id)
        {
            try
            {
                if (id.HasValue && id.Value > 0)
                {
                    var OrderModel = await _orderService.GetOrderById(id.Value);
                    var OrderViewModel = _mapper.Map<OrderViewModel>(OrderModel);
                    return View(OrderViewModel);
                }
                else
                    return RedirectToAction("Index").WithError("Invalid data");
            }
            catch (Exception ex)
            {
                _logger.Error("Error occured OrderController\\Edit(Get) with id" + id + " with EX: " + ex.Message);
                return RedirectToAction("Index").WithError("Invalid data");
            }

        }

        [HttpPost, ActionName("DeleteOrder")]
        public async Task<IActionResult> Delete(long Id)
        {
            try
            {
                await _orderService.ChangeStatus(Id, false);

                return RedirectToAction("Index").WithSuccess(_localizer.Get("Deleted !"));
            }
            catch (Exception ex)
            {
                _logger.Error("Error occured OrderController\\DeleteOrder" + Id + " with EX: " + ex.ToString());
                return RedirectToAction("Index", new { area = "Admin" }).WithError(_localizer.Get("Error In Deleting"));
            }
        }
    }
}
