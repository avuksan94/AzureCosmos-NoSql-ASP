using Microsoft.AspNetCore.Mvc;
using PPPK_Zadatak05.CosmosServices;
using PPPK_Zadatak05.Models;

namespace PPPK_Zadatak05.Controllers
{
    public class PersonController : Controller
    {
        private readonly ICosmosDBPersonService _personService;

        public PersonController(ICosmosDBPersonService personService)
        {
            _personService = personService;
        }

        public async Task<ActionResult> Index()
        {
            return View(await _personService.GetPeopleAsync("SELECT * FROM Person"));
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(Person person)
        {

            if (ModelState.IsValid)
            {
                person.Id = Guid.NewGuid().ToString();
                await _personService.AddPersonAsync(person);
                return RedirectToAction("Index");
            }

            return View(person);
        }

        public async Task<ActionResult> Edit(string id) => await ShowThePerson(id);

        private async Task<ActionResult> ShowThePerson(string id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            var person = await _personService.GetPersonAsync(id);

            if (person == null)
            {
                return NotFound();
            }
            return View(person);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(Person person)
        {
            if (ModelState.IsValid)
            {
                await _personService.UpdatePersonAsync(person);
                return RedirectToAction("Index");
            }
            return View(person);
        }

        public async Task<ActionResult> Delete(string id) => await ShowThePerson(id);

        [HttpPost]
        public async Task<ActionResult> Delete(Person person)
        {
            await _personService.DeletePersonAsync(person);
            return RedirectToAction("Index");
        }

    }
}
