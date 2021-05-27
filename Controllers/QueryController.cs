using CompatibilityWebSite.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompatibilityWebSite.Controllers
{
    public class QueryController : Controller
    {
        private const string CONNECTION = "Server= DESKTOP-TKKMN11;Database=CompatibilityWebSite;Trusted_Connection=True;MultipleActiveResultSets=true";

        private const string Q1_PATH = @"C:\Crth\CompatibilityWebSite\Queries\Q1.sql";
        private const string Q2_PATH = @"C:\Crth\CompatibilityWebSite\Queries\Q2.sql"; 
        private const string Q3_PATH = @"C:\Crth\CompatibilityWebSite\Queries\Q3.sql"; 
        private const string Q4_PATH = @"C:\Crth\CompatibilityWebSite\Queries\Q4.sql"; 
        private const string Q5_PATH = @"C:\Crth\CompatibilityWebSite\Queries\Q5.sql"; 
        private const string Q6_PATH = @"C:\Crth\CompatibilityWebSite\Queries\A1.sql";

        private const string ERR_DES = "Хвороби, що задовольняють дану умову, відсутні";
        private const string ERR_AS = "Діючі речовини, що задовольняють дану умову, відсутні";
        private const string ERR_MED = "Ліки, що задовольняють дану умову, відсутні";

        private readonly CompatibilityWebSiteContext _context;
        public QueryController(CompatibilityWebSiteContext context)
        {
            _context = context;
        }

        public IActionResult Index(int error)
        {
           
            if (error == 1)
            {
                ViewBag.ErrorFlag = 1;
                ViewBag.QuantityError = "Введіть коректне число";
            }
            var empty = new SelectList(new List<string> { "---" });
            var anyDeseases = _context.Deseases.Any();
            var anyActiveSubstances = _context.ActiveSubstances.Any();
            var anyCompatibilityStatuses = _context.CompatibilityStatuses.Any();

            ViewBag.DeseaseIds = anyDeseases ? new SelectList(_context.Deseases, "Id", "Name") : empty;
            ViewBag.ActiveSubstanceIds = anyActiveSubstances ? new SelectList(_context.ActiveSubstances, "Id", "Name") : empty;
            ViewBag.CompatibilityStatusIds = anyCompatibilityStatuses ? new SelectList(_context.CompatibilityStatuses, "Id", "Name") : empty;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Query1(Query queryModel) // всі діючі речовини з d1
        {
            string query = System.IO.File.ReadAllText(Q1_PATH);
            query = query.Replace("d1", queryModel.DeseaseId.ToString());            
            query = query.Replace("\r\n", " ");
            query = query.Replace('\t', ' ');

            queryModel.QueryName = "Q1";
            queryModel.ActiveSubstanceNames = new List<string>();
            using (var connection = new SqlConnection(CONNECTION))
            {
                connection.Open();
                using (var command = new SqlCommand(query, connection))
                {
                    command.ExecuteNonQuery();
                    using (var reader = command.ExecuteReader())
                    {
                        int flag = 0;
                        while (reader.Read())
                        {
                            queryModel.ActiveSubstanceNames.Add(reader.GetString(0));
                            flag++;
                        }

                        if (flag == 0)
                        {
                            queryModel.ErrorFlag = 1;
                            queryModel.ErrorName = ERR_AS;
                        }
                    }
                }
                connection.Close();
            }
            return RedirectToAction("Results", queryModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Query2 (Query queryModel) // всі хвороби, для яких використовується as1
        {
            string query = System.IO.File.ReadAllText(Q2_PATH);
            query = query.Replace("as1", queryModel.ActiveSubstanceId.ToString());
            query = query.Replace("\r\n", " ");
            query = query.Replace('\t', ' ');

            queryModel.QueryName = "Q2";
            queryModel.DeseaseNames = new List<string>();
            using (var connection = new SqlConnection(CONNECTION))
            {
                connection.Open();
                using (var command = new SqlCommand(query, connection))
                {
                    command.ExecuteNonQuery();
                    using (var reader = command.ExecuteReader())
                    {
                        int flag = 0;
                        while(reader.Read())
                        {
                            queryModel.DeseaseNames.Add(reader.GetString(0));
                            flag++;
                        }

                        if(flag == 0)
                        {
                            queryModel.ErrorFlag = 1;
                            queryModel.ErrorName = ERR_DES;
                        }
                    }
                }
                connection.Close();
            }
            return RedirectToAction("Results", queryModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Query3(Query queryModel)// всі лікарські засоби для as1
        {
            string query = System.IO.File.ReadAllText(Q3_PATH);
            query = query.Replace("as1", queryModel.ActiveSubstanceId.ToString());
            query = query.Replace("\r\n", " ");
            query = query.Replace('\t', ' ');

            queryModel.QueryName = "Q3";
            queryModel.MedicineNames = new List<string>();
            using (var connection = new SqlConnection(CONNECTION))
            {
                connection.Open();
                using (var command = new SqlCommand(query, connection))
                {
                    command.ExecuteNonQuery();
                    using (var reader = command.ExecuteReader())
                    {
                        int flag = 0;
                        while (reader.Read())
                        {
                            queryModel.MedicineNames.Add(reader.GetString(0));
                            flag++;
                        }

                        if (flag == 0)
                        {
                            queryModel.ErrorFlag = 1;
                            queryModel.ErrorName = ERR_MED;
                        }
                    }
                }
                connection.Close();
            }
            return RedirectToAction("Results", queryModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Query4 (Query queryModel) //знайти всі діючі речовини, які з s1 з >= n іншими
        {
           if (ModelState.IsValid)
           {
                string query = System.IO.File.ReadAllText(Q4_PATH);
                query = query.Replace("s1", queryModel.CompatibilityStatusId.ToString());
                query = query.Replace("N1", queryModel.Quantity.ToString());
                query = query.Replace("\r\n", " ");
                query = query.Replace('\t', ' ');

                queryModel.QueryName = "Q4";
                queryModel.ActiveSubstanceNames = new List<string>();

                using (var connection = new SqlConnection(CONNECTION))
                {
                    connection.Open();
                    using (var command = new SqlCommand(query, connection))
                    {
                        command.ExecuteNonQuery();
                        using (var reader = command.ExecuteReader())
                        {
                            int flag = 0;
                            while (reader.Read())
                            {
                                queryModel.ActiveSubstanceNames.Add(reader.GetString(0));
                                flag++;
                            }

                            if (flag == 0)
                            {
                                queryModel.ErrorFlag = 1;
                                queryModel.ErrorName = ERR_AS;
                            }
                        }
                    }
                    connection.Close();
                }
                return RedirectToAction("Results", queryModel);
            }
            return RedirectToAction("Index", new { error = 1 });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Query5(Query queryModel) //знайти всі ліки, в яких >= n діючих речовин
        {
            if (ModelState.IsValid)
            {
                string query = System.IO.File.ReadAllText(Q5_PATH);
                query = query.Replace("N1", queryModel.Quantity.ToString());
                query = query.Replace("\r\n", " ");
                query = query.Replace('\t', ' ');

                queryModel.QueryName = "Q5";
                queryModel.MedicineNames = new List<string>();

                using (var connection = new SqlConnection(CONNECTION))
                {
                    connection.Open();
                    using (var command = new SqlCommand(query, connection))
                    {
                        command.ExecuteNonQuery();
                        using (var reader = command.ExecuteReader())
                        {
                            int flag = 0;
                            while (reader.Read())
                            {
                                queryModel.MedicineNames.Add(reader.GetString(0));
                                flag++;
                            }

                            if (flag == 0)
                            {
                                queryModel.ErrorFlag = 1;
                                queryModel.ErrorName = ERR_MED;
                            }
                        }
                    }
                    connection.Close();
                }
                return RedirectToAction("Results", queryModel);
            }
            return RedirectToAction("Index", new { error = 1 });
        }

        public IActionResult Query6(Query queryModel) //хвороби, які мають усі ті ж самі ліки, що і хвороба X1
        {
                string query = System.IO.File.ReadAllText(Q6_PATH);
                query = query.Replace("X1", queryModel.Quantity.ToString());
                query = query.Replace("\r\n", " ");
                query = query.Replace('\t', ' ');

                queryModel.QueryName = "Q6";
                queryModel.DeseaseNames = new List<string>();

                using (var connection = new SqlConnection(CONNECTION))
                {
                    connection.Open();
                    using (var command = new SqlCommand(query, connection))
                    {
                        command.ExecuteNonQuery();
                        using (var reader = command.ExecuteReader())
                        {
                            int flag = 0;
                            while (reader.Read())
                            {
                                queryModel.DeseaseNames.Add(reader.GetString(0));
                                flag++;
                            }

                            if (flag == 0)
                            {
                                queryModel.ErrorFlag = 1;
                                queryModel.ErrorName = ERR_DES;
                            }
                        }
                    }
                    connection.Close();
                }
                return RedirectToAction("Results", queryModel);
        }

        public IActionResult Results(Query queryResult)
        {
            return View(queryResult);
        }
    }
}
