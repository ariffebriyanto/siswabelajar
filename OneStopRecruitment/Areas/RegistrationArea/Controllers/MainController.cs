using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Model.DBConstraint;
using Model.Subdomains.DropdownSubdomain;
using Model.Subdomains.RegistrationSubdomain;
using OneStopRecruitment.Areas.RegistrationArea.ViewModels.Main;
using OneStopRecruitment.Controllers;
using OneStopRecruitment.Helpers.DropdownHelpers;
using OneStopRecruitment.Models;
using Service.Modules.RegistrationModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace OneStopRecruitment.Areas.RegistrationArea.Controllers
{
    [Area("RegistrationArea")]
    public class MainController : BaseController
    {
        private readonly IMainService mainService;
        public MainController(IMainService mainService)
        {
            this.mainService = mainService;
        }

        #region dropdown
        public IEnumerable<SelectListItem> GetPositionDropdown()
        {
            List<DropdownItem> positionDropdownList = new List<DropdownItem>();
            var PositionList = mainService.GetPositions();
            foreach (var item in PositionList)
            {
                DropdownItem positionDropdown = new DropdownItem();
                positionDropdown.Value = item.IDPosition.ToString();
                positionDropdown.Text = item.PositionName;
                positionDropdownList.Add(positionDropdown);
            }
            IEnumerable<SelectListItem> dropdownPosition = new SelectListItemBuilder()
                .AddRangeDropdownItems(positionDropdownList.AsEnumerable())
                .Build();
            return dropdownPosition;
        }
        #endregion

        public IActionResult Index()
        {
            var IsOpenRegistration = mainService.IsOpenRegistration();

            if (!IsOpenRegistration)
            {
                return RedirectToAction("Index", "Auth", new { area = "LoginArea" });
            }

            RegistrationViewModel registrationViewModel = new RegistrationViewModel();

            registrationViewModel.PositionList = GetPositionDropdown().ToList();

            return View("Index", registrationViewModel);
        }

        public IActionResult Register(RegistrationViewModel registrationViewModel)
        {
            registrationViewModel.PositionList = GetPositionDropdown().ToList();

            try
            {
                if (registrationViewModel.Registration.NIM == null)
                {
                    TempData["ErrorMessage"] = AlertConstraint.Registration.EmptyNIM;
                    return View("Index", registrationViewModel);
                }
                else if (registrationViewModel.Registration.Email == null)
                {
                    TempData["ErrorMessage"] = AlertConstraint.Registration.EmptyEmail;
                    return View("Index", registrationViewModel);
                }
                else if (!Regex.IsMatch(registrationViewModel.Registration.Email, BaseConstraint.Regex.Email, RegexOptions.IgnoreCase))
                {
                    TempData["ErrorMessage"] = AlertConstraint.Registration.WrongEmailFormat;
                    return View("Index", registrationViewModel);
                }
                else if (!registrationViewModel.Registration.Email.EndsWith(BaseConstraint.Uniian.EmailEnds))
                {
                    TempData["ErrorMessage"] = AlertConstraint.Registration.NonUniEmail;
                    return View("Index", registrationViewModel);
                }
                else if (registrationViewModel.Registration.IDPosition == null)
                {
                    TempData["ErrorMessage"] = AlertConstraint.Registration.EmptyPosition;
                    return View("Index", registrationViewModel);
                }
                else if (registrationViewModel.Registration.IDPosition.Equals("0"))
                {
                    TempData["ErrorMessage"] = AlertConstraint.Registration.EmptyPosition;
                    return View("Index", registrationViewModel);
                }


                try
                {
                    Candidate candidate = new Candidate();
                    candidate.Email = registrationViewModel.Registration.Email;
                    candidate.NIM = registrationViewModel.Registration.NIM;
                    candidate.IDPosition = registrationViewModel.Registration.IDPosition;

                    bool isExists = mainService.CheckCandidate(candidate);
                    if(isExists)
                    {
                        TempData["ErrorMessage"] = AlertConstraint.Registration.AlreadyRegistered;
                        return View("Index", registrationViewModel);
                    }

                    bool insertCandidate = mainService.InsertCandidate(candidate);
                    if(!insertCandidate)
                    {
                        TempData["ErrorMessage"] = AlertConstraint.Registration.InsertCandidateUnknownError;
                        return View("Index", registrationViewModel);
                    }

                    TempData["SuccessMessage"] = AlertConstraint.Registration.RegistrationSuccess;
                    return View("Index", registrationViewModel);
                }
                catch (Exception ex)
                {
                    throw ex;
                }                
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = AlertConstraint.Default.Error + " [" + ex.Message + "]";
                return View("Index", registrationViewModel);
            }
        }
    }
}
