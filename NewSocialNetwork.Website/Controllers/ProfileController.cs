﻿using System;
using System.Web;
using System.Web.Mvc;
using NewSocialNetwork.Domain;
using NewSocialNetwork.Repositories;
using NewSocialNetwork.Website.Controllers.Helper;
using NSN.Common;

namespace NewSocialNetwork.Website.Controllers
{
    public class ProfileController : ApplicationController
    {
        public IUserRepository userRepo { get; set; }
        public ICountryRepository countryRepo { get; set; }

        public ProfileController()
        {
            ViewBag.PageTitle = "NSN: Profile";
        }

        //
        // GET: /Profile/

        public ActionResult Info(string uid)
        {
            ViewBag.PageTitle += " Info";
            ViewBag.IsInfoPage = true;
            return View();
        }

        public ActionResult EditInfo()
        {
            if (ViewBag.UProfile.UserId != ViewBag.MyProfile.UserId)
            {
                return HttpNotFound("You do not have permission access this page.");
            }
            ViewBag.Countries = countryRepo.FindAll();
            ViewBag.IsEditInfoPage = true;
            return View();
        }

        [HttpPost]
        public JsonResult EditInfoSave(string username, string fullName, string email, byte gender,
            short birthday_year, byte birthday_month, byte birthday_day,
            string countryiso)
        {
            ResponseMessage msg = new ResponseMessage("ProfileEditInfo", RAction.EDIT, RStatus.FAIL,
                "Cannot update your profile. Please recheck your info.");
            try
            {
                User user = frontendService.UpdateProfileInfo(username, fullName, email, gender,
                    (birthday_year + "/" + birthday_month + "/" + birthday_day), countryiso);
                msg.SetStatusAndMessage(RStatus.SUCCESS, "Your profile have updated.");
                msg.ReturnedPath = Url.RouteUrl("ProfileInfo", new { uid = Globals.GetDisplayId(user) });
            }
            catch (Exception e)
            {
                msg.Message = e.Message;
            }

            return Json(msg);
        }

        public ActionResult ChangeAvatar()
        {
            if (ViewBag.UProfile.UserId != ViewBag.MyProfile.UserId)
            {
                return HttpNotFound("You do not have permission access this page.");
            }
            
            return View();
        }

        //[HttpPost]
        //public JsonResult ChangeSaveAvatar()
        //{
        //    ResponseMessage msg = new ResponseMessage("ProfilePhoto", RAction.EDIT, RStatus.FAIL,
        //        "Error occurs when changing your profile photo.");
        //    try
        //    {
        //    }
        //    catch (Exception e)
        //    {
        //        msg.Message = e.Message;
        //    }
        //    return Json(msg);
        //}

        [HttpPost]
        public ActionResult ChangeSaveAvatar(HttpPostedFileBase avatar)
        {
            try
            {
                ImageInfo avatarInfo = Globals.SaveImageInPlace(avatar, "/static/images/avatars/");
                User user = sessionManager.GetUser();
                user.UserImage = avatarInfo.FileName;
                userRepo.Save(user);
            }
            catch { }
            return RedirectToAction("Info");
        }
    }
}
