namespace BlazorApp1.Shared;

public class MapSpot
{
    public int X { get; set; }
    public int Y { get; set; }

    public MapSpot(int x, int y)
    {
        X = x;
        Y = y;
    }

    public int DistanceToSpot(MapSpot s2)
    {
        var distance = Math.Abs(s2.X - X) + Math.Abs(s2.Y - Y);
        return distance;
    }
}