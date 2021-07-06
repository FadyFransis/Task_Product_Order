using App.Common.Models;
using App.Common.Services.Mail;
using App.Core.Entities;
using App.Core.Identity;
using App.Core.Interfaces.Services;
using App.Core.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace App.Core.Services
{
    public class AppUserManagerService : IAppUserManagerService
    {
        #region Constructor
        private readonly ApplicationUserManager _userManager;
        private readonly IMailNotification _mailnotification;
        private readonly IMapper _mapper;
        public AppUserManagerService(
            ApplicationUserManager userManager,
            IMailNotification mailNotification, IMapper mapper)

        {
            _mapper = mapper;
            _userManager = userManager;
            _mailnotification = mailNotification;
           
        }
        #endregion

        public async Task<AppUserModel> GetUserByEmail(string email)
        {
            AppUser appUser = await _userManager.FindByEmailAsync(email);
            return _mapper.Map<AppUserModel>(appUser);

        }
        public async Task<AppUserModel> GetAntherUserByEmail(string email, string id)
        {
            AppUser appUser = await _userManager.FindByEmailAsync(email);
            if (appUser.Id == id) return null;
            return _mapper.Map<AppUserModel>(appUser);

        }

        public List<AppUserModel> LoadAllUsers()
        {
            var appUserModels =  _userManager.Users.Where(x=>x.RecordStatus==Entities.Base.RecordStatus.Enabled).Select(x => new AppUserModel
            {
                Id = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Email = x.Email,
                UserName = x.UserName,
                PhoneNumber = x.PhoneNumber,
                EmailConfirmed = x.EmailConfirmed,
                Roles = _userManager.GetRolesAsync(x).Result.FirstOrDefault()
            }).ToList();
     
            return appUserModels;
        }

        public async Task<List<AppUserModel>> LoadAllClients()
        {
            var appClients =await _userManager.GetUsersInRoleAsync("Client");
            var appUserModels = _mapper.Map<List<AppUserModel>>(appClients.Where(x=>x.RecordStatus==Entities.Base.RecordStatus.Enabled).ToList());
            return appUserModels;
        }

        public AppUserModel GetUserByMobile(string mobile)
        {
            var appUser = _userManager.Users.Where(x => x.PhoneNumber == mobile ).FirstOrDefault();
            return _mapper.Map<AppUserModel>(appUser);
        }

        public async Task<ResponseModel<BooleanResultDTO>> AddUser(AppUserModel model)
        {
            try
            {
                ResponseModel<BooleanResultDTO> responseModel = new ResponseModel<BooleanResultDTO>()
                {
                    IsError = false,
                    Result = new BooleanResultDTO() { Success = true },
                    Description = "Registeration success please check your email for activation code"
                };

                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user != null)
                {
                    responseModel.Result.Success = false;
                    responseModel.IsError = true;
                    responseModel.Description = "Email exists before";
                    return responseModel;
                }

                var findByPhone = GetUserByMobile(model.PhoneNumber);
                if (findByPhone != null)
                {
                    responseModel.Result.Success = false;
                    responseModel.IsError = true;
                    responseModel.Description = "Phone Number exists ";
                    return responseModel;
                }

                AppUser newuser = GetAppUser(model);
                newuser.DateAdded = DateTime.Now;
                var saveuser = await _userManager.CreateAsync(newuser, model.Password);
                if (!saveuser.Succeeded)
                {
                    responseModel.IsError = true;
                    responseModel.Result.Success = false;
                    responseModel.Description = saveuser.Errors.Select(x => x.Description).ElementAt(0).ToString();
                    return responseModel;
                }
                else
                {
                    var roles = new List<string> { model.Roles };
                    var saveroles = await _userManager.AddToRolesAsync(newuser, roles);
                    var claims = new[]
                    {new Claim("UserFullName", newuser.FirstName + " " + newuser.LastName),
                    new Claim("IsSocailAccount", "false")};
                    var saveClaims = await _userManager.AddClaimsAsync(newuser, claims);
                    if (saveroles.Succeeded && saveClaims.Succeeded)
                    {
                        
                        return responseModel;
                    }
                    else
                    {
                        await _userManager.DeleteAsync(newuser);
                        responseModel.IsError = true;
                        responseModel.Description = saveroles.Errors.Select(x => x.Description).ElementAt(0).ToString();
                        return responseModel;
                    }
                }



            }
            catch (Exception ex)
            {

                return new ResponseModel<BooleanResultDTO>()
                {
                    IsError = true,
                    Result = new BooleanResultDTO() { Success = false },
                    Description = ex.Message
                };
            }
        }

        public async Task<ResponseModel<BooleanResultDTO>> AddUserWithoutEmailNotification(AppUserModel model)
        {
            try
            {
                ResponseModel<BooleanResultDTO> responseModel = new ResponseModel<BooleanResultDTO>()
                {
                    IsError = false,
                    Result = new BooleanResultDTO() { Success = true },
                    Description = "User Added !"
                };

                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user != null)
                {
                    responseModel.IsError = true;
                    responseModel.Result.Success = false;
                    responseModel.Description = "This email exists before";
                    return responseModel;
                }

                var findByPhone = GetUserByMobile(model.PhoneNumber);
                if (findByPhone != null)
                {
                    responseModel.IsError = true;
                    responseModel.Result.Success = false;
                    responseModel.Description = "Phone number exists before";
                    return responseModel;
                }

                AppUser newuser = GetAppUser(model);
                newuser.DateAdded = DateTime.Now;
                var saveuser = await _userManager.CreateAsync(newuser, model.Password);
                if (!saveuser.Succeeded)
                {
                    responseModel.IsError = true;
                    responseModel.Result.Success = false;
                    responseModel.Description = saveuser.Errors.Select(x => x.Description).ElementAt(0).ToString();
                    return responseModel;
                }
                else
                {
                    var roles = new List<string> { model.Roles };
                    var saveroles = await _userManager.AddToRolesAsync(newuser, roles);
                    var claims = new[]
                    {new Claim("UserFullName", newuser.FirstName + " " + newuser.LastName),
                    new Claim("IsSocailAccount", "false")};
                    var saveClaims = await _userManager.AddClaimsAsync(newuser, claims);
                    if (saveroles.Succeeded && saveClaims.Succeeded)
                        return responseModel;
                    
                    else
                    {
                        await _userManager.DeleteAsync(newuser);
                        responseModel.IsError = true;
                        responseModel.Description = saveroles.Errors.Select(x => x.Description).ElementAt(0).ToString();
                        return responseModel;
                    }
                }



            }
            catch (Exception ex)
            {

                return new ResponseModel<BooleanResultDTO>()
                {
                    IsError = true,
                    Result = new BooleanResultDTO() { Success = false },
                    Description = ex.Message
                };
            }
        }

        public async Task<ResponseModel<BooleanResultDTO>> AddSocialUser(AppUserModel model)
        {
            try
            {
                ResponseModel<BooleanResultDTO> responseModel = new ResponseModel<BooleanResultDTO>()
                {
                    IsError = false,
                    Result = new BooleanResultDTO() { Success = true },
                    Description = "Registeration success"
                };

                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user != null)
                {
                    responseModel.IsError = true;
                    responseModel.Result.Success = true;
                    responseModel.Description = "Email exists before";
                    return responseModel;
                }

                var findByPhone = GetUserByMobile(model.PhoneNumber);
                if (findByPhone != null)
                {
                    responseModel.IsError = true;
                    responseModel.Result.Success = true;
                    responseModel.Description = "Phone Number exists ";
                    return responseModel;
                }

                AppUser newuser = GetAppUser(model);
                newuser.DateAdded = DateTime.Now;
                var saveuser = await _userManager.CreateAsync(newuser, model.Password);
                if (!saveuser.Succeeded)
                {
                    responseModel.IsError = true;
                    responseModel.Result.Success = false;
                    responseModel.Description = saveuser.Errors.Select(x => x.Description).ElementAt(0).ToString();
                    return responseModel;
                }
                else
                {
                    var roles = new List<string> { model.Roles };
                    var saveroles = await _userManager.AddToRolesAsync(newuser, roles);
                    var claims = new[]
                    {new Claim("UserFullName", newuser.FirstName + " " + newuser.LastName),
                    new Claim("IsSocailAccount", "true")};

                    var saveClaims = await _userManager.AddClaimsAsync(newuser, claims);
                    if (saveroles.Succeeded && saveClaims.Succeeded)
                    {
                        return responseModel;
                    }
                    else
                    {
                        await _userManager.DeleteAsync(newuser);
                        responseModel.IsError = true;
                        responseModel.Description = saveroles.Errors.Select(x => x.Description).ElementAt(0).ToString();
                        return responseModel;
                    }
                }



            }
            catch (Exception ex)
            {

                return new ResponseModel<BooleanResultDTO>()
                {
                    IsError = true,
                    Result = new BooleanResultDTO() { Success = false },
                    Description = ex.Message
                };
            }
        }

        public ResponseModel<BooleanResultDTO> IsExistUser(AppUserModel model)
        {

            ResponseModel<BooleanResultDTO> responseModel = new ResponseModel<BooleanResultDTO>()
            {
                IsError = false,
                Result = new BooleanResultDTO() { Success = true },
                Description = "Updated Successfully"
            };

            var users = _userManager.Users.Where(x => x.Id != model.Id
            && (x.Email == model.Email || x.PhoneNumber == model.PhoneNumber));
            foreach (var cst in users)
            {
                if (cst.Email == model.Email)
                {
                    responseModel.IsError = true;
                    responseModel.Description = "Email exists before";
                    return responseModel;
                }
                if (cst.PhoneNumber == model.PhoneNumber)
                {
                    responseModel.IsError = true;
                    responseModel.Description = "PhoneNumber exists before";
                    return responseModel;
                }
            }
            return responseModel;
        }
        public async Task<ResponseModel<BooleanResultDTO>> UpdateUser(AppUserModel model)
        {
            try
            {
                ResponseModel<BooleanResultDTO> responseModel = new ResponseModel<BooleanResultDTO>()
                {
                    IsError = false,
                    Result = new BooleanResultDTO() { Success = true },
                    Description = "User modified !"
                };

                AppUser appUser = await _userManager.FindByIdAsync(model.Id);
                appUser.FirstName = model.FirstName;
                appUser.LastName = model.LastName;
                appUser.EmailConfirmed = model.EmailConfirmed;
                appUser.PhoneNumber = model.PhoneNumber;
                appUser.ImageUrl = model.ImageUrl;
                appUser.Email = model.Email;
                appUser.UserName = model.Email;
                var res = await _userManager.UpdateAsync(appUser);
                if (res.Succeeded)
                {
                    List<string> roles = _userManager.GetRolesAsync(appUser).Result.ToList();
                    if (roles.Contains(model.Roles)) // no change in result
                        return responseModel;
                    else
                    {
                        for (int i = 0; i < roles.Count; i++) // remove last role
                        {
                            await _userManager.RemoveFromRoleAsync(appUser, roles[i]);
                        }

                        var AddRole = await _userManager.AddToRoleAsync(appUser, model.Roles);
                        if (AddRole.Succeeded)
                            return responseModel;

                        else
                        {
                            responseModel.IsError = true;
                            responseModel.Result.Success = false;
                            responseModel.Description = AddRole.Errors.ElementAt(0).Description;
                            return responseModel;
                        }
                    }

                }
                else
                {
                    responseModel.IsError = true;
                    responseModel.Result.Success = false;
                    responseModel.Description = res.Errors.ElementAt(0).Description;
                    return responseModel;
                }

                
            }
            catch (Exception ex)
            {
                return new ResponseModel<BooleanResultDTO>()
                {
                    IsError = true,
                    Result = new BooleanResultDTO() { Success = false },
                    Description = ex.Message
                };
            }
        }

        public async Task<ResponseModel<BooleanResultDTO>> DeleteUser(string id)
        {

            try
            {
                ResponseModel<BooleanResultDTO> responseModel = new ResponseModel<BooleanResultDTO>()
                {
                    IsError = false,
                    Result = new BooleanResultDTO() { Success = true },
                    Description = "User Deleted !"
                };

                AppUser updateUser = await _userManager.FindByIdAsync(id);
                updateUser.RecordStatus = Entities.Base.RecordStatus.Deleted;

                var result = await _userManager.UpdateAsync(updateUser);

                if (!result.Succeeded)
                {
                    responseModel.IsError = true;
                    responseModel.Result.Success = false;
                    responseModel.Description = result.Errors.Select(x => x.Description).ElementAt(0).ToString();
                    return responseModel;
                }
                return responseModel;
            }
            catch (Exception ex)
            {
                return new ResponseModel<BooleanResultDTO>()
                {
                    IsError = true,
                    Result = new BooleanResultDTO() { Success = false },
                    Description = ex.Message
                };
            }

        }

        public async Task<ResponseModel<BooleanResultDTO>> ActivateUser(string email, string code)
        {
            ResponseModel<BooleanResultDTO> responseModel = new ResponseModel<BooleanResultDTO>() { Result = new BooleanResultDTO() };

            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                responseModel.IsError = true;
                responseModel.Result.Success = false;
                responseModel.Description = "This email not exist";
                return responseModel;
            }
            else
            {
                if (user.ActivationCode != code)
                {
                    responseModel.IsError = true;
                    responseModel.Result.Success = false;
                    responseModel.Description = "Invalid Activation Code";
                    return responseModel;
                }

                else
                {
                    user.EmailConfirmed = true;
                    var res = await _userManager.UpdateAsync(user);
                    if (!res.Succeeded)
                    {
                        responseModel.IsError = true;
                        responseModel.Result.Success = false;
                        responseModel.Description = "Error in activate account try again";
                        return responseModel;
                    }
                    else
                    {
                        responseModel.IsError = false;
                        responseModel.Result.Success = true;
                        responseModel.Description = "Activation Success";
                        return responseModel;
                    }
                }

            }


        }

        public ResponseModel<AppUserModel> GetUserBySocialLoginId(string socialLoginId)
        {
            var appUser = _userManager.Users.Where(x => x.SocialLoginId == socialLoginId).FirstOrDefault();
            if (appUser == null)
                return new ResponseModel<AppUserModel>()
                {
                    IsError = true,
                    Timestamp = DateTime.Now,
                    Result = null,
                    Description = "User not exsit"
                };
            ResponseModel<AppUserModel> responseModel = new ResponseModel<AppUserModel>()
            {
                IsError = false,
                Timestamp = DateTime.Now,
                Result = _mapper.Map<AppUserModel>(appUser)
            };
            return responseModel;
        }

        public async Task<AppUserModel> GetUserById(string id)
        {
            var appUser = await _userManager.FindByIdAsync(id);
            var appUserModel= _mapper.Map<AppUserModel>(appUser);
            appUserModel.Roles = _userManager.GetRolesAsync(appUser).Result.FirstOrDefault();
            return appUserModel;
        }

        public async Task<ResponseModel<BooleanResultDTO>> ForgetPassword(string email)
        {
            try
            {
                ResponseModel<BooleanResultDTO> responseModel = new ResponseModel<BooleanResultDTO>() { Result = new BooleanResultDTO() };

                var user = await _userManager.FindByEmailAsync(email);
                if (user == null)
                {
                    responseModel.IsError = true;
                    responseModel.Result.Success = false;
                    responseModel.Description = "This email not exist";
                    return responseModel;
                }
                else
                {

                    user.ForgetPasswordCode = GenerateCode();
                    var res = await _userManager.UpdateAsync(user);
                    if (!res.Succeeded)
                    {
                        responseModel.IsError = true;
                        responseModel.Result.Success = false;
                        responseModel.Description = "Error in reset password";
                        return responseModel;
                    }
                    else
                    {
                        

                        responseModel.IsError = false;
                        responseModel.Result = new BooleanResultDTO() { Success = true };
                        responseModel.Description = "Please check your email  reset password code sent to you";


                    }

                }

                return responseModel;
            }
            catch (Exception ex)
            {
                return new ResponseModel<BooleanResultDTO>()
                {
                    IsError = true,
                    Result = new BooleanResultDTO() { Success = false },
                    Description = ex.Message
                };
            }
        }

        public async Task<ResponseModel<BooleanResultDTO>> SendActivationMail(string email)
        {
            try
            {
                ResponseModel<BooleanResultDTO> responseModel = new ResponseModel<BooleanResultDTO>() { Result = new BooleanResultDTO() };

                var user = await _userManager.FindByEmailAsync(email);
                if (user == null)
                {
                    responseModel.IsError = true;
                    responseModel.Result.Success = false;
                    responseModel.Description = "This email not exist";
                    return responseModel;
                }
                else
                {

                    user.ActivationCode = GenerateCode();
                    var res = await _userManager.UpdateAsync(user);
                    if (!res.Succeeded)
                    {
                        responseModel.IsError = true;
                        responseModel.Result.Success = false;
                        responseModel.Description = "Error in send activation code";
                        return responseModel;
                    }
                    else
                    {
                        

                        responseModel.IsError = false;
                        responseModel.Result = new BooleanResultDTO() { Success = true };
                        responseModel.Description = "Please check your email  activation code sent to you";


                    }

                }

                return responseModel;
            }
            catch (Exception ex)
            {
                return new ResponseModel<BooleanResultDTO>()
                {
                    IsError = true,
                    Result = new BooleanResultDTO() { Success = false },
                    Description = ex.Message
                };
            }
        }

        public async Task<ResponseModel<BooleanResultDTO>> ResetPassword(string email, string code, string newPassword)
        {
            try
            {
                ResponseModel<BooleanResultDTO> responseModel = new ResponseModel<BooleanResultDTO>() { Result = new BooleanResultDTO() };
                var user = await _userManager.FindByEmailAsync(email);
                if (user == null)
                {
                    responseModel.IsError = true;
                    responseModel.Result.Success = false;
                    responseModel.Description = "This email not exist";
                    return responseModel;
                }
                else
                {
                    if (string.IsNullOrWhiteSpace(code)
                        || string.IsNullOrWhiteSpace(newPassword) ||
                            code != user.ForgetPasswordCode)
                    {
                        responseModel.IsError = true;
                        responseModel.Result.Success = false;
                        responseModel.Description = "Invalid Code";
                        return responseModel;
                    }
                    else
                    {
                        var hashPassword = _userManager.PasswordHasher.HashPassword(user, newPassword);
                        user.PasswordHash = hashPassword;
                        var res = await _userManager.UpdateAsync(user);
                        if (!res.Succeeded)
                        {
                            responseModel.IsError = true;
                            responseModel.Result.Success = false;
                            responseModel.Description = "Error in update password";
                            return responseModel;
                        }
                        else
                        {
                            responseModel.IsError = false;
                            responseModel.Result.Success = true;
                            responseModel.Description = "Password Changed";
                            return responseModel;
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                return new ResponseModel<BooleanResultDTO>()
                {
                    IsError = true,
                    Result = new BooleanResultDTO() { Success = false },
                    Description = ex.Message
                };
            }


        }

        private bool SendEmail(EmailDTO emailDTO)
        {
            return _mailnotification.SendMail(emailDTO);
        }

        private AppUser GetAppUser(AppUserModel model)
        {
            string ActivationCode = DateTime.Now.Millisecond.ToString() + DateTime.Now.Second.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Hour.ToString();
            ActivationCode = ActivationCode.Length > 6 ? ActivationCode.Substring(0, 6) : ActivationCode;
            AppUser newuser = new AppUser()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                UserName = model.Email,
                PhoneNumber = model.PhoneNumber,
                SecurityStamp = Guid.NewGuid().ToString(),
                EmailConfirmed = model.EmailConfirmed,
                ActivationCode = ActivationCode,
                //Age = model.Age,
                SocialLoginId = model.SocialLoginId,
                DateAdded=DateTime.Now
            };
            return newuser;
        }

        private string GenerateCode()
        {
            string code = DateTime.Now.Millisecond.ToString() + DateTime.Now.Second.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Millisecond.ToString();
            code = code.Length > 6 ? code.Substring(0, 6) : code;
            return code;
        }

        public async Task<ResponseModel<BooleanResultDTO>> ResendActivationCode(string email)
        {
            try
            {
                ResponseModel<BooleanResultDTO> responseModel = new ResponseModel<BooleanResultDTO>() { Result = new BooleanResultDTO() };

                var user = await _userManager.FindByEmailAsync(email);
                if (user == null)
                {
                    responseModel.IsError = true;
                    responseModel.Result.Success = false;
                    responseModel.Description = "This email not exist";
                    return responseModel;
                }
                else
                {

                    user.ForgetPasswordCode = GenerateCode();
                    var res = await _userManager.UpdateAsync(user);
                    if (!res.Succeeded)
                    {
                        responseModel.IsError = true;
                        responseModel.Result.Success = false;
                        responseModel.Description = "Error in send activation code";
                        return responseModel;
                    }
                    else
                    {
                        string body = "reset password code: " + user.ForgetPasswordCode;
                        bool sendEmail = SendEmail(new EmailDTO
                        {
                            MailTo = new List<string>() { user.Email },
                            Subject = "Reset Account Password",
                            Body = body,
                            IsBodyHTML = true
                        });
                        if (!sendEmail)
                        {
                            responseModel.IsError = true;
                            responseModel.Result.Success = false;
                            responseModel.Description = "error in sending email with reset code try again";
                        }
                        else
                        {
                            responseModel.IsError = false;
                            responseModel.Result = new BooleanResultDTO() { Success = true };
                            responseModel.Description = "Please check your email  reset password code sent to you ";
                        }

                    }

                }

                return responseModel;
            }
            catch (Exception ex)
            {
                return new ResponseModel<BooleanResultDTO>()
                {
                    IsError = true,
                    Result = new BooleanResultDTO() { Success = false },
                    Description = ex.Message
                };
            }
        }
    }
}
