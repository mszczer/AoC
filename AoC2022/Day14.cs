using System.Drawing;
using System.Text.RegularExpressions;

namespace AoC.AoC2022;

internal partial class Day14 : AoC<List<string>, int, int>
{
    private HashSet<Point> _rockPoints;
    private int _maxDepth;

    public Day14(string dayName) : base(dayName)
    {
        GetInitialPaths();
    }

    private void GetInitialPaths()
    {
        _maxDepth = 0;
        _rockPoints = new HashSet<Point>();

        if (InputData == null) return;

        foreach (var path in InputData)
        {
            if (string.IsNullOrWhiteSpace(path)) continue;

            // get points of direction change
            var coordinates = SearchedPattern().Split(path).Where(s => !string.IsNullOrEmpty(s)).ToArray();
            var turningPoints = new List<Point>();
            var x = 0;
            var y = 0;

            for (var i = 0; i < coordinates.Length; i++)
                if (i % 2 == 0)
                {
                    x = int.Parse(coordinates[i]);
                }
                else
                {
                    y = int.Parse(coordinates[i]);
                    turningPoints.Add(new Point(x, y));
                }

            // get all points on path of rocks
            for (var i = 1; i < turningPoints.Count; i++)
            {
                var pathPoints = GetPointsOnPath(turningPoints[i - 1], turningPoints[i]);
                foreach (var point in pathPoints)
                    if (_rockPoints.Add(point))
                        // get max depth (the last one before 'infinite' fall)
                        if (point.Y > _maxDepth)
                            _maxDepth = point.Y;
            }
        }
    }

    private static List<Point> GetPointsOnPath(Point start, Point end)
    {
        var pointsOnPath = new List<Point>();

        if (start.X == end.X)
        {
            if (start.Y > end.Y)
                (start.Y, end.Y) = (end.Y, start.Y);

            for (var i = start.Y; i <= end.Y; i++)
                pointsOnPath.Add(new Point(start.X, i));
        }
        else
        {
            if (start.X > end.X)
                (start.X, end.X) = (end.X, start.X);
            for (var i = start.X; i <= end.X; i++)
                pointsOnPath.Add(new Point(i, start.Y));
        }

        return pointsOnPath;
    }

    public override int CalculatePart1()
    {
        var sandUnits = 0;
        var restPoint = new Point();

        var rockPoints = new HashSet<Point>(_rockPoints);

        while (restPoint.Y < _maxDepth)
        {
            restPoint = GetRestPoint(new Point(500, 0), rockPoints, _maxDepth);
            rockPoints.Add(restPoint);
            sandUnits++;
        }

        sandUnits--; // do not count endless fall

        return sandUnits;
    }

    private static Point GetRestPoint(Point sandPoint, ICollection<Point> rockPoints, int maxDepth)
    {
        Point restPoint;

        if (sandPoint.Y > maxDepth)
            return sandPoint;
        if (!rockPoints.Contains(new Point(sandPoint.X, sandPoint.Y + 1)))
            restPoint = GetRestPoint(new Point(sandPoint.X, sandPoint.Y + 1), rockPoints, maxDepth);
        else if (!rockPoints.Contains(new Point(sandPoint.X - 1, sandPoint.Y + 1)))
            restPoint = GetRestPoint(new Point(sandPoint.X - 1, sandPoint.Y + 1), rockPoints, maxDepth);
        else if (!rockPoints.Contains(new Point(sandPoint.X + 1, sandPoint.Y + 1)))
            restPoint = GetRestPoint(new Point(sandPoint.X + 1, sandPoint.Y + 1), rockPoints, maxDepth);
        else
            restPoint = sandPoint;

        return restPoint;
    }

    public override int CalculatePart2()
    {
        var sandUnits = 0;
        var falling = true;

        var rockPoints = new HashSet<Point>(_rockPoints);

        while (falling)
        {
            var restPoint = GetRestPointWithDepthLimit(new Point(500, 0), rockPoints, _maxDepth + 2);
            rockPoints.Add(restPoint);
            if (restPoint.Y == 0)
                falling = false;
            sandUnits++;
        }

        return sandUnits;
    }

    private static Point GetRestPointWithDepthLimit(Point sandPoint, ICollection<Point> rockPoints, int maxDepth)
    {
        Point restPoint;

        if (sandPoint.Y + 1 == maxDepth)
            restPoint = sandPoint;
        else if (!rockPoints.Contains(new Point(sandPoint.X, sandPoint.Y + 1)))
            restPoint = GetRestPointWithDepthLimit(new Point(sandPoint.X, sandPoint.Y + 1), rockPoints, maxDepth);
        else if (!rockPoints.Contains(new Point(sandPoint.X - 1, sandPoint.Y + 1)))
            restPoint = GetRestPointWithDepthLimit(new Point(sandPoint.X - 1, sandPoint.Y + 1), rockPoints, maxDepth);
        else if (!rockPoints.Contains(new Point(sandPoint.X + 1, sandPoint.Y + 1)))
            restPoint = GetRestPointWithDepthLimit(new Point(sandPoint.X + 1, sandPoint.Y + 1), rockPoints, maxDepth);
        else
            restPoint = sandPoint;

        return restPoint;
    }

    [GeneratedRegex(@"\D+")]
    private static partial Regex SearchedPattern();
}