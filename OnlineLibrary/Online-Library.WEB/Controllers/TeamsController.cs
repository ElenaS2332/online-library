using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Online_Library.Domain.Entities;
using Online_Library.Repository;
using Online_Library.Service.Implementations;

namespace Online_Library.WEB.Controllers
{
    public class TeamsController : Controller
    {
        private readonly ITeamsService _teamService;

        public TeamsController(ITeamsService teamService)
        {
            _teamService = teamService;
        }
        public IActionResult Index()
        {
            var data = _teamService.GetAllTeams();
            return View(data);
        }
    }
}
