using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Cayent.Web.Admin.RCL.Areas.Admin.Pages.Permissions
{
    public class IndexModel : PageModel
    {
        public void OnGet()
        {
        }

        public PartialViewResult OnGetIndexPartial()
        {

            return Partial("_Index");
        }
    }
}