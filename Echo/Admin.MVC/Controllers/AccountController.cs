using App.Common.Services.Logger;
using App.Core.Entities;
using App.Core.Identity;
using App.Core.Interfaces.Services;
using Admin.MVC.Helper.Alerts;
using Admin.MVC.Services;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using Admin.MVC.ViewModels;
using App.Core.Models;
using System.Collections.Generic;

namespace Admin.MVC.Controllers
{

    public class AccountController : Controller
    {
        private readonly ApplicationUserManager _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        protected readonly CommonLocalizationService _localizer;
        protected readonly Ilogger _logger;
        protected readonly IAppUserManagerService _appUserManagerService;

        public AccountController(Ilogger logger, IMapper mapper,
                ApplicationUserManager userManager,
                SignInManager<AppUser> signInManager, CommonLocalizationService localizer,
               IAppUserManagerService appUserManagerService)
        {
            _logger = logger;
            _userManager = userManager;
            _signInManager = signInManager;
            _appUserManagerService = appUserManagerService;
            _localizer = localizer;
        }

        
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);

                    if (result.Succeeded)
                    {
                        AppUser appUser = await _userManager.FindByEmailAsync(model.Email);

                        if (appUser.EmailConfirmed)
                        {
                            if (model.ReturnUrl != null)
                                return Redirect(model.ReturnUrl);
                            else
                                return RedirectToAction("Index", "Home");
                        }
                        else
                        {
                            await _signInManager.SignOutAsync();
                            return View(model).WithError(_localizer.Get("Account not active"));
                        }
                    }
                    else
                    {
                        return View(model).WithError(_localizer.Get("Invalid Email Or Password"));
                    }
                }
                return View(model).WithError(ModelState.Values.First().Errors.FirstOrDefault().ErrorMessage);
            }
            catch (Exception ex)
            {
                _logger.Error("Error occured AccountController\\Login(Post)" + " with EX: " + ex.Message);
                //throw;
                return View(model).WithError(ex.Message);
            }
        }


        public IActionResult ForgetPassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgetPassword(ForgetPasswordViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var forgetpassword = await _appUserManagerService.ForgetPassword(model.Email);
                    if (forgetpassword.IsError)
                    {
                        return View(model).WithError(_localizer.Get(forgetpassword.Description));
                    }
                    else
                    {
                        TempData["EmailToRest"] = model.Email;
                        return RedirectToAction("ResetPassword").WithSuccess(_localizer.Get("Reset password code sent to you please check your email"));
                    }

                }
                else
                    return View(model);
            }
            catch (Exception ex)
            {

                _logger.Error("Error occured AccountController\\ForgetPassword(Post)" + " with EX: " + ex.Message);
                return View(model).WithError(ex.Message);
            }

        }

        public IActionResult ResetPassword()
        {
            if (TempData["EmailToRest"] != null)
            {
                ResetPasswordViewModel model = new ResetPasswordViewModel() { Email = TempData["EmailToRest"].ToString(), Code = string.Empty };
                return View(model);
            }
            else
                return RedirectToAction("ForgetPassword");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    AppUser user = new AppUser();
                    user = _userManager.Users.Where(x => x.Email == model.Email).FirstOrDefault();
                    if (user != null)
                    {
                        if (!string.IsNullOrWhiteSpace(model.Code) &&
                     !string.IsNullOrWhiteSpace(model.NewPassword) &&
                           model.Code == user.ForgetPasswordCode)
                        {

                            var hashPassword = _userManager.PasswordHasher.HashPassword(user, model.NewPassword);
                            user.PasswordHash = hashPassword;
                            var res = await _userManager.UpdateAsync(user);
                            if (res.Succeeded)
                                return RedirectToAction("Login").WithSuccess(_localizer.Get("Password Changed!"));
                            else
                                return View(model).WithError(res.Errors.ElementAt(0).Description);

                        }
                        else
                        {
                            return View(model).WithError(_localizer.Get("Invalid Code"));
                        }
                    }
                    else
                    {
                        return View(model).WithError(_localizer.Get("This email not exist"));
                    }

                }
                else
                    return View(model);
            }
            catch (Exception ex)
            {

                _logger.Error("Error occured AccountController\\ResetPassword(Post)" + " with EX: " + ex.Message);
                return View(model).WithError(ex.Message);
            }

        }
        public IActionResult AccessDenied()
        {
            return View();
        }

        public IActionResult LogOut()
        {
            _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }

        [HttpGet]
        [Route("[controller]/[action]")]
        public ResponseModel<BooleanResultDTO> IsAuthenticated()
        {

            try
            {
                var responseModel = new ResponseModel<BooleanResultDTO>();
                responseModel.IsError = false;
                responseModel.Result = new BooleanResultDTO() { Success=User.Identity.IsAuthenticated};
                return responseModel;
            }
            catch (Exception ex)
            {
                _logger.Error("Error occured AccountController\\IsAuthenticated" + " with EX: " + ex.Message);

                var responseModel = new ResponseModel<BooleanResultDTO>();
                responseModel.IsError = true;
                responseModel.Result = null;
                responseModel.Description = ex.Message;
                return responseModel;
            }
        }
    }
}