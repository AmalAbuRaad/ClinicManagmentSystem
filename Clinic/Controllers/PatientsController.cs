using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PagedList;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Clinic.Data;
using Clinic.Models;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using Clinic.Helper;

namespace Clinic.Controllers
{
    public class PatientsController : Controller
    {
        private readonly ApplicationDbContext _context;
        string Baseurl = "https://restcountries.eu/rest/v2/all";

        public PatientsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Patients
        public async Task<IActionResult> Index(string sortOrder, string currentFilter, string searchString, int? pageNumber)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewBag.FNameSortParm = String.IsNullOrEmpty(sortOrder) ? "FName_Desc" : "";
            ViewBag.LNameSortParm = sortOrder == "LName_Asc" ? "LName_Desc" : "LName_Asc";
            ViewBag.BirthdaySortParm = sortOrder == "Birthday_Asc" ? "Birthday_Desc" : "Birthday_Asc";
            ViewBag.GenderSortParm = sortOrder == "Gender_Asc" ? "Gender_Desc" : "Gender_Asc";
            ViewBag.PhoneNumSortParm = sortOrder == "PhoneNum_Asc" ? "PhoneNum_Desc" : "PhoneNum_Asc";
            ViewBag.EmailSortParm = sortOrder == "Email_Asc" ? "Email_Desc" : "Email_Asc";
            ViewBag.AddrSortParm = sortOrder == "Address_Asc" ? "Address_Desc" : "Address_Asc";
            ViewBag.RegDSortParm = sortOrder == "RegDate_Asc" ? "RegDate_Desc" : "RegDate_Asc";
            ViewBag.SSNSortParm = sortOrder == "SSN_Asc" ? "SSN_Desc" : "SSN_Asc";
            ViewBag.CountrySortParm = sortOrder == "Country_Asc" ? "Country_Desc" : "Country_Asc";
            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewData["CurrentFilter"] = searchString;
            var patients = from p in _context.Patients select p;
            if (!String.IsNullOrEmpty(searchString))
            {
                patients = patients.Where(s => (s.FirstName+' '+s.LastName).Contains(searchString));
            }
            switch (sortOrder)
            {
                case "FName_Desc":
                    patients = patients.OrderByDescending(p => p.FirstName);
                    break;
                case "LName_Asc":
                    patients = patients.OrderBy(p => p.LastName);
                    break;
                case "LName_Desc":
                    patients = patients.OrderByDescending(p => p.LastName);
                    break;
                case "Birthday_Asc":
                    patients = patients.OrderBy(p => p.Birthday);
                    break;
                case "Birthday_Desc":
                    patients = patients.OrderByDescending(p => p.Birthday);
                    break;
                case "Gender_Asc":
                    patients = patients.OrderBy(p => p.Gender);
                    break;
                case "Gender_Desc":
                    patients = patients.OrderByDescending(p => p.Gender);
                    break;
                case "PhoneNum_Asc":
                    patients = patients.OrderBy(p => p.PhoneNumber);
                    break;
                case "PhoneNum_Desc":
                    patients = patients.OrderByDescending(p => p.PhoneNumber);
                    break;
                case "Email_Asc":
                    patients = patients.OrderBy(p => p.Email);
                    break;
                case "Email_Desc":
                    patients = patients.OrderByDescending(p => p.Email);
                    break;
                case "Address_Asc":
                    patients = patients.OrderBy(p => p.Address);
                    break;
                case "Address_Desc":
                    patients = patients.OrderByDescending(p => p.Address);
                    break;
                case "RegDate_Asc":
                    patients = patients.OrderBy(p => p.RegDate);
                    break;
                case "RegDate_Desc":
                    patients = patients.OrderByDescending(p => p.RegDate);
                    break;
                case "SSN_Asc":
                    patients = patients.OrderBy(p => p.SSN);
                    break;
                case "SSN_Desc":
                    patients = patients.OrderByDescending(p => p.SSN);
                    break;
                case "Country_Asc":
                    patients = patients.OrderBy(p => p.Country);
                    break;
                case "Country_Desc":
                    patients = patients.OrderByDescending(p => p.Country);
                    break;
                default:
                    patients = patients.OrderBy(p => p.FirstName);
                    break;
            }
            int pageSize = 3;
  
            return View(await PaginatedList<Patient>.CreateAsync(patients.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        // GET: Patients/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var patient = await _context.Patients
                .FirstOrDefaultAsync(m => m.Id == id);
            if (patient == null)
            {
                return NotFound();
            }

            return View(patient);
        }

        // GET: Patients/Create
        public async Task<IActionResult> Create()
        {
            ViewData["Country"] = new SelectList(await this.GetCountries(), "Name", "Name");
            return View();
        }

        // POST: Patients/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,LastName,Birthday,Gender,PhoneNumber,Email,Address,RegDate,SSN,Country")] Patient patient, CountryModel country)
        {
            if (ModelState.IsValid)
            {
                _context.Add(patient);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Country"] = new SelectList(await this.GetCountries(), "Name", "Name", country.Name);
            return View(patient);
        }

        // GET: Patients/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var patient = await _context.Patients.FindAsync(id);
            if (patient == null)
            {
                return NotFound();
            }
            ViewData["Country"] = new SelectList(await this.GetCountries(), "Name", "Name");
            return View(patient);
        }

        // POST: Patients/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,FirstName,LastName,Birthday,Gender,PhoneNumber,Email,Address,RegDate,SSN,Country")] Patient patient, CountryModel country)
        {
            if (id != patient.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(patient);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PatientExists(patient.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["Country"] = new SelectList(await this.GetCountries(), "Name", "Name", country.Name);
            return View(patient);
        }

        // GET: Patients/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var patient = await _context.Patients
                .FirstOrDefaultAsync(m => m.Id == id);
            if (patient == null)
            {
                return NotFound();
            }

            return View(patient);
        }

        // POST: Patients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var patient = await _context.Patients.FindAsync(id);
            _context.Patients.Remove(patient);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PatientExists(long id)
        {
            return _context.Patients.Any(e => e.Id == id);
        }

        public async Task<IEnumerable<CountryModel>> GetCountries()
        {
            List<CountryModel> country = new List<CountryModel>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);

                client.DefaultRequestHeaders.Clear();
                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage Res = await client.GetAsync("https://restcountries.eu/rest/v2/all");

                if (Res.IsSuccessStatusCode)
                {
                    var CountryResponse = Res.Content.ReadAsStringAsync().Result;
                    country = JsonConvert.DeserializeObject<List<CountryModel>>(CountryResponse);


                }
            }

                return country;
            }

        }
    }

