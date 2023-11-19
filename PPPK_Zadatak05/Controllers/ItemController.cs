using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json.Serialization;
using PPPK_Zadatak05.CosmosServices;
using PPPK_Zadatak05.Models;

namespace PPPK_Zadatak05.Controllers
{
    public class ItemController : Controller
    {
        private readonly ICosmosDBItemService _itemService;
        private readonly ICosmosDBPersonService _personService;

        public ItemController(ICosmosDBItemService itemService, ICosmosDBPersonService personService)
        {
            _itemService = itemService;
            _personService = personService;
          
        }

        private async Task initViewBag() {
            var people = await _personService.GetPeopleAsync("SELECT * FROM Person");
            ViewBag.PersonId = new SelectList(people, "Id", "LastName");
        }

        public async Task<ActionResult> Index()
        {
            var dbItems = await _itemService.GetItemsAsync("SELECT * FROM Item");
            var itemsVM = new List<ItemVM>();

            foreach (var item in dbItems)
            {
                Person person = await _personService.GetPersonAsync(item.PersonId);
                itemsVM.Add(
                    new ItemVM { 
                        Id = item.Id,
                        Name = item.Name,
                        Description = item.Description,
                        Completed = item.Completed,
                        PersonId = item.PersonId,
                        FirstName = person.FirstName,
                        LastName = person.LastName
                    }    
                );
            }
            return View(itemsVM);
        }

        // GET: TaskController/Create
        public async Task<ActionResult> Create()
        {
            await initViewBag();
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(Item item)
        {
            if (ModelState.IsValid)
            {
                item.Id = Guid.NewGuid().ToString();
                await _itemService.AddItemAsync(item);
                return RedirectToAction("Index");
            }
            await initViewBag();
            return View(item);
        }

        public async Task<ActionResult> Edit(string id) => await ShowTheItem(id);

        private async Task<ActionResult> ShowTheItem(string id)
        {
            await initViewBag();
            if (id == null)
            {
                return BadRequest();
            }
            var item = await _itemService.GetItemAsync(id);

            if (item == null)
            {
                return NotFound();
            }
           
            return View(item);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(Item item)
        {
            if (ModelState.IsValid)
            {
                await _itemService.UpdateItemAsync(item);
                return RedirectToAction("Index");
            }
            await initViewBag();
            return View(item);
        }

        public async Task<ActionResult> Delete(string id) => await ShowTheItem(id);

        [HttpPost]
        public async Task<ActionResult> Delete(Item item)
        {
            await _itemService.DeleteItemAsync(item);
            return RedirectToAction("Index");
        }

    }
}
