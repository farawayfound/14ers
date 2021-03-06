﻿using Microsoft.AspNetCore.Mvc;
using ProjectStep8.Models;
using System.Linq;

namespace ProjectStep8.Controllers
{
   public class PeakController
      : Controller
   {
      //   F i e l d s   &   P r o p e r t i e s

      private IPeakRepository _repository;
      private IUserRepository _userRepository;

      //   C o n s t r u c t o r s

      public PeakController(IPeakRepository repository, IUserRepository userRepository)
      {
         _repository = repository;
         _userRepository = userRepository;
      }

      //   M e t h o d s

      //   C r e a t e

      [HttpGet]
      public IActionResult Add()
      {
         if (!_userRepository.IsUserAdmin())
         {
            return RedirectToAction("Index");
         }

         return View();
      } // end Add( )

      [HttpPost]
      public IActionResult Add(Peak p)
      {
         if (!_userRepository.IsUserAdmin())
         {
            return RedirectToAction("Index");
         }

         if (ModelState.IsValid)
         {
            _repository.AddPeak(p);
            return RedirectToAction("Detail", new { peakId = p.Id });
         }

         return View(p);
      } // end Add( )

      //   R e a d

      public IActionResult Index()
      {
         IQueryable<Peak> allPeaks = _repository.GetAllPeaks();
         return View(allPeaks);
      } // end Index( )

      public IActionResult Detail(int peakId)
      {
         Peak p = _repository.GetPeakById(peakId);
         return View(p);
      } // end PeakDetail( )


      //   U p d a t e

      [HttpGet]
      public IActionResult Edit(int peakId)
      {
         if (!_userRepository.IsUserAdmin())
         {
            return RedirectToAction("Index");
         }

         Peak p = _repository.GetPeakById(peakId);
         if (p != null)
         {
            return View(p);
         }

         return RedirectToAction("Index");
      }

      [HttpPost]
      public IActionResult Edit(Peak p)
      {
         if (!_userRepository.IsUserAdmin())
         {
            return RedirectToAction("Index");
         }

         if (ModelState.IsValid)
         {
            _repository.UpdatePeak(p);
            return RedirectToAction("Detail", new { peakId = p.Id });
         }

         return View(p);
      }

      //   D e l e t e

      [HttpGet]
      public IActionResult Delete(int peakId)
      {
         if (!_userRepository.IsUserAdmin())
         {
            return RedirectToAction("Index");
         }

         return View(_repository.GetPeakById(peakId));
      } // end DeletePeak( )

      [HttpPost]
      public IActionResult Delete(Peak p)
      {
         if (!_userRepository.IsUserAdmin())
         {
            return RedirectToAction("Index");
         }

         _repository.DeletePeak(p.Id);
         return RedirectToAction("Index");
      } // end DeletePeak( )
   }
}
