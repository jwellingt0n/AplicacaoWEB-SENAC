﻿using Microsoft.AspNetCore.Mvc;
namespace WebApplication2.Controllers
{
    public class ProfessoresController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}