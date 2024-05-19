using EA.AdminPanel.Services.Interfaces;
using Enrollment.Domain.Models.Enum;
using Enrollment.Domain.Models.Query;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Models.Enums;

namespace EA.AdminPanel.Controllers
{
    [Authorize(Policy = "Privileged")]
    public class AdmissionsController : Controller
    {
        private readonly IAdmissionService _admissionService;

        public AdmissionsController(IAdmissionService admissionService)
        {
            _admissionService = admissionService;
        }

        public async Task<IActionResult> Index(int? page, int? size, Guid? programId, StatusEnum? status, bool? unassignedOnly, bool? firstPriorityOnly, SortEnum sortBy = SortEnum.CreatedTimeAsc)
        {
            var query = new AdmissionsFilterQuery
            {
                Page = page,
                Size = size,
                ProgramId = programId,
                Status = status,
                UnassignedOnly = unassignedOnly,
                FirstPriorityOnly = firstPriorityOnly,
                SortBy = sortBy
            };

            var admissions = await _admissionService.GetAdmissionsWithFilter(query);
            return View("Admissions", admissions);
        }

        /*        public async Task<IActionResult> Details(int id)
                {
                    var admission = await _admissionService.GetAdmissionByIdAsync(id);
                    if (admission == null)
                    {
                        return NotFound();
                    }
                    return View(admission);
                }

                [HttpPost]
                public async Task<IActionResult> AssignManager(int id)
                {
                    await _admissionService.AssignManagerAsync(id);
                    return RedirectToAction("Index");
                }*/
    }
}
