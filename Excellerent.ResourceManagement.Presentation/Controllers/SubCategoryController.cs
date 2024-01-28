using Excellerent.ResourceManagement.Domain.Interfaces.Services;
using Excellerent.ResourceManagement.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excellerent.ResourceManagement.Presentation.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class SubCategoryController : Controller
    {
        private readonly ISubCategoryService _subCategoryService;
        public SubCategoryController(ISubCategoryService subCategoryService)
        {
            _subCategoryService = subCategoryService;
        }

        /*[HttpGet]
        public List<SubCategory> List()
        {
            return _subCategoryService.GetSubCategories();
        }*/
        [HttpGet]
        public List<SubCategory> GetByCategory(Guid id)
        {
            return _subCategoryService.GetByCategoryId(id);
        }
    }
}
