using System.Diagnostics;
using EmployeeManagement.Models;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Reporting.WebForms;
using Mysqlx.Notice;

namespace EmployeeManagement.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly ILogger<EmployeeController> _logger;
        private readonly EmployeeService _employeeService;

        public EmployeeController(EmployeeService employeeService, ILogger<EmployeeController> logger)
        {
            _employeeService = employeeService;
            _logger = logger;

        }


        public  IActionResult Index()
        {
            var allEmployees =  _employeeService.GetAll();
            return View(allEmployees);
        }

        [HttpPost]
        public IActionResult Save(EmployeeModel model)
        {
            _employeeService.Save(model); 
            var allEmployees = _employeeService.GetAll();
            return View("Emplyeelist", allEmployees); 
        }
        [HttpPost]
        public IActionResult Delete(int id)
        {
            _employeeService.Delete(id);
            return RedirectToAction("Index");
        }
        

        public IActionResult Updateform(int Id)
        {
            var employeedetails=_employeeService.getemployeedetails(Id);
            return View(employeedetails);
        }
        public IActionResult Chart()
        {
            var employees = _employeeService.GetAll(); 
            return View("Chart", employees);
        }
        

//public IActionResult GenerateReport()
//    {
//        var employees = _employeeService.GetAll(); // Your data retrieval

//        var reportViewer = new ReportViewer
//        {
//            ProcessingMode = ProcessingMode.Local
//        };

//        reportViewer.LocalReport.ReportPath = Path.Combine(Directory.GetCurrentDirectory(), "Reports", "EmployeeReport.rdlc");

//        reportViewer.LocalReport.DataSources.Add(new ReportDataSource("EmployeeDataSet", employees));

//        var renderedBytes = reportViewer.LocalReport.Render("PDF");

//        return File(renderedBytes, "application/pdf");
//    }

}
}
