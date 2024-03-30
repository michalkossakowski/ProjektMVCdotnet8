using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjektMVCdotnet8.Areas.Identity.Data;
using ProjektMVCdotnet8.Entities;
using ProjektMVCdotnet8.Interfaces;
using ProjektMVCdotnet8.Repository;

namespace ProjektMVCdotnet8.Controllers
{
    public class ReportPostEntitiesController : Controller
    {
        private readonly IReportRepository _reportRepository;
        private readonly IPostRepository _postRepository;

        public ReportPostEntitiesController(IReportRepository reportRepository, IPostRepository postRepository)
        {
            _reportRepository = reportRepository;
            _postRepository = postRepository;
        }

        // GET: ReportPostEntities
        public async Task<IActionResult> Index()
        {
            return View(await _reportRepository.GetAll());
        }

        // GET: ReportPostEntities/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var reportPostEntity = await _reportRepository.GetById(id);
            return View(reportPostEntity);
        }

        // GET: ReportPostEntities/Create
        public IActionResult Create(int repPost)
        {
            ViewBag.repPost = repPost;
            return View();
        }

        // POST: ReportPostEntities/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ReportContent")] ReportPostEntity reportPostEntity,int repPost)
        {
            reportPostEntity.ReportedPost = await _postRepository.GetById(repPost);
            reportPostEntity.postId = repPost;
            _reportRepository.Add(reportPostEntity);
            return RedirectToAction("ThxForReport","Home");
        }

        // GET: ReportPostEntities/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var reportPostEntity = await _reportRepository.GetById(id);
            return View(reportPostEntity);
        }

        // POST: ReportPostEntities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            _reportRepository.DeleteById(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
