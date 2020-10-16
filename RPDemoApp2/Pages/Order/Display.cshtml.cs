using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLibrary.Data;
using DataLibrary.Db.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RPDemoApp2.Models;

namespace RPDemoApp2.Pages.Order
{
    public class DisplayModel : PageModel
    {
        private readonly IOrderData orderData;
        private readonly IFoodData foodData;

        [BindProperty(SupportsGet = true)]
        public int Id { get; set; }
        public OrderModel Order { get; set; }
        public string ItemPurchsed { get; set; }
        [BindProperty]
        public OrderUpdateModel UpdateModel { get; set; }

        public DisplayModel(IOrderData orderData, IFoodData foodData)
        {
            this.orderData = orderData;
            this.foodData = foodData;
        }
        public async Task<IActionResult> OnGet()
        {
            Order = await orderData.GetOrderById(Id);

            if (Order!=null)
            {
                var food = await foodData.GetFood();
                ItemPurchsed = food.Where(x => x.Id == Order.FoodId).FirstOrDefault()?.Title;
            }
            return Page();
        }
        public async Task<ActionResult> OnPost()
        {
            if (ModelState.IsValid == false)
            {
                return Page();
            }
            await orderData.UpdateOrderName(UpdateModel.Id, UpdateModel.OrderName);
            return RedirectToPage("./Display", new { UpdateModel.Id });
        }
    }
}
