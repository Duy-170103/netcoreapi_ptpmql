public class Diemtb
{
    public double A { get; set; }
    public double B { get; set; }
    public double C { get; set; }
    public double FinalGrade => (A * 0.2) + (B * 0.3) + (C * 0.5);
}
