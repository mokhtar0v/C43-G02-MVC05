using Demo.BusinessLogic.DTOs;
using Demo.BusinessLogic.Services;
using Demo.DataAccess.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Presentation.Controllers
{
    public class DepartmentController(IDepartmentService _departmentService,
        ILogger<DepartmentController> _logger,
        IWebHostEnvironment _environment) : Controller
    {
        public IActionResult Index()
        {
            var departments = _departmentService.GetAllDepartments();

            return View(departments);
        }
        #region CreateDept
        [HttpGet]
        public IActionResult Create()
        {

            return View();
        }
        [HttpPost]
        public IActionResult Create(CreateDepartmentDTO createDepartmentDTO)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    int res = _departmentService.CreateDepartment(createDepartmentDTO);
                    if (res > 0) return View(nameof(Index)); //Back to list
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Department Can't be Created");
                    }
                }
                catch (Exception ex)
                {
                    //log exception
                    //1. developement=>console
                    //2. deployment=>file

                    if(_environment.IsDevelopment())
                    {
                        ModelState.AddModelError(string.Empty, ex.Message);
                    }
                    else
                    {
                        _logger.LogError(ex.Message);
                    }

                }
            }

            return View(createDepartmentDTO);
        }
        #endregion

        #region Details
        public IActionResult Details(int? id)
        {
            if (!id.HasValue) return BadRequest();

            var department = _departmentService.GetDepartmentByID(id.Value);
            if(department == null) return NotFound();

            return View(department);
        } 
        #endregion
    }
}
