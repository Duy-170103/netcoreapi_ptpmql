public class Hoadon
{
    public int Quantity { get; set; }
    public double UnitPrice { get; set; }
    public double TotalAmount => Quantity * UnitPrice;
}

