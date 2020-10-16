using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLibrary.Data;
using DataLibrary.Db.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RPDemoApp2.Pages.Order
{
    public class DeleteModel : PageModel
    {
        private readonly IOrderData orderData;
        [BindProperty(SupportsGet = true)]
        public int Id { get; set; }
        public OrderModel Order { get; set; }
        public DeleteModel(IOrderData orderData)
        {
            this.orderData = orderData;
        }
        public async Task OnGet()
        {
            Order = await orderData.GetOrderById(Id);
        }
        public async Task<IActionResult> OnPost()
        {
            await orderData.DeleteOrder(Id);
            return RedirectToAction("./create");
        }
    }
}
