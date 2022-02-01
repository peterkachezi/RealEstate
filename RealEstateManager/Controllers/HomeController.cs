using AspNetCore.Reporting;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RealEstateManager.Data.Services.TenantModule;
using RealEstateManager.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace RealEstateManager.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IWebHostEnvironment env;
        private readonly ItenantService itenantService;

        public HomeController(ItenantService itenantService, IWebHostEnvironment env, ILogger<HomeController> logger)
        {
            _logger = logger;
            this.env = env;
            this.itenantService = itenantService;
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Print()
        {
            var dt = new DataTable();

            dt = await GetTenantList();

            string mimetype = "";

            int extension = 1;

            var path = $"{this.env.WebRootPath}\\Reports\\rptTenants.rdlc";

            Dictionary<string, string> parameters = new Dictionary<string, string>();

            parameters.Add("prm", "Report");

            LocalReport localReport = new LocalReport(path);

            localReport.AddDataSource("dsTenants", dt);

            var result = localReport.Execute(RenderType.Pdf, extension, parameters, mimetype);

            return File(result.MainStream, "application/pdf");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task<DataTable> GetTenantList()
        {
            var list = await itenantService.GetAll();

            var dt = new DataTable();

            dt.Columns.Add("TenantId");

            dt.Columns.Add("FirstName");

            dt.Columns.Add("LastName");

            dt.Columns.Add("PhoneNumber");

            dt.Columns.Add("Email");

            DataRow row;

            foreach (var item in list)
            {
                row = dt.NewRow();

                row["TenantId"] = item.Id.ToString();

                row["FirstName"] = item.FirstName;

                row["LastName"] = item.LastName;

                row["PhoneNumber"] = item.PhoneNumber;

                row["Email"] = item.Email;

                dt.Rows.Add(row);
            }
            return dt;
        }


        public DataTable GetTenantList1()
        {
            var dt = new DataTable();
            dt.Columns.Add("TenantId");
            dt.Columns.Add("FirstName");
            dt.Columns.Add("LastName");
            dt.Columns.Add("PhoneNumber");
            dt.Columns.Add("Email");

            DataRow row;

            for (int i = 101; i <= 120; i++)
            {
                row = dt.NewRow();
                row["TenantId"] = i;
                row["FirstName"] = "Peter";
                row["LastName"] = "Kachezi";
                row["PhoneNumber"] = "0704509484";
                row["Email"] = "peter@gmail.com";

                dt.Rows.Add(row);
            }

            return dt;
        }
    }
}
