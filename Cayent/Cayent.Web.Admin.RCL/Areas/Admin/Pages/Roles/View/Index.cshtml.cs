using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Cayent.Web.Admin.RCL.Areas.Admin.Pages.Roles.View
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public string Id { get; set; }

        public void OnGet(string id)
        {
            Id = id;
        }


        public async Task<PartialViewResult> OnGetIndexPartialAsync()
        {
            return await Task.FromResult(Partial("_Index"));
        }
    }
}