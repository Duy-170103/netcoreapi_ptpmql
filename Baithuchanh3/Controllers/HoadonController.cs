using Microsoft.AspNetCore.Mvc;
public class HoadonController : Controller
{
    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Index(int quantity, double unitPrice)
    {
        var hoadon = new Hoadon { Quantity = quantity, UnitPrice = unitPrice };
        ViewBag.TotalAmount = hoadon.TotalAmount;
        return View();
    }
}
