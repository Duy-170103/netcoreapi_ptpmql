using Microsoft.AspNetCore.Mvc;
public class DiemtbController : Controller
{
    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Index(double A, double B, double C)
    {
        var diemtb = new Diemtb { A = A, B = B, C = C };
        ViewBag.FinalGrade = diemtb.FinalGrade;
        return View();
    }
}
