using Microsoft.AspNetCore.Mvc;
using AnimalShelter.Models;
using System.Collections.Generic;
using System.Linq;
using System;

namespace AnimalShelter.Controllers
{
  public class AnimalsController : Controller
  {
    private readonly AnimalShelterContext _db;

    public AnimalsController(AnimalShelterContext db)
    {
      _db = db;
    }
    [HttpGet("/index")]
    public ActionResult Index()
    {
      List<Animal> model = _db.Animals.ToList();
      return View(model);
    }

    [HttpGet("/create")]
    public ActionResult Create()
    {
      return View();
    }

    [HttpPost("/create")]
    public ActionResult Create(Animal animal)
    {
      _db.Animals.Add(animal);
      _db.SaveChanges();
      Console.WriteLine("hi");
      return RedirectToAction("Index");
    }

    public ActionResult Details(int id)
    {
      Animal thisAnimal = _db.Animals.FirstOrDefault(animals => animals.AnimalId == id);
      return View(thisAnimal);
    }
    [HttpGet("/breed")]
    public ActionResult Breed()
    {
      List<Animal> model = _db.Animals.ToList();
      List<Animal> sortedModel = model.OrderBy(o => o.Breed).ToList();
      return View(sortedModel);
    }

    [HttpGet("/animaltype")]

    public ActionResult AnimalType()
    {
      List<Animal> model = _db.Animals.ToList();
      List<Animal> sortedModel = model.OrderBy(o => o.AnimalType).ToList();
      return View(sortedModel);
    }

    [HttpGet("/search")]

    public ActionResult Search(string search)
    {
      var model = from m in _db.Animals select m;

      if (!string.IsNullOrEmpty(search))
      {
        model = model.Where(m => m.Name.Contains(search));
      }
      List<Animal> matches = model.ToList();
      return View(matches);
    }
  }
}