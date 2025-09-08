using AoC.Common;

namespace AoC.AoC2024;

internal class Day02 : AoC<List<string>, int, int>
{
    private List<List<int>> _reports;

    public Day02(string dayName) : base(dayName)
    {
        ParseReports();
    }
    
    public Day02(string dayName, List<string> inputData) : base(dayName, inputData)
    {
        ParseReports();
    }
    private void ParseReports()
    {
        _reports = new List<List<int>>();

        foreach (var reportLine in InputData)
        {
            var report = new List<int>();
            var substrings = reportLine.Split();

            foreach (var level in substrings)
                report.Add(int.Parse(level));

            _reports.Add(report);
        }
    }

    public override int CalculatePart1()
    {
        return _reports.Count(IsReportSafe);
    }

    public override int CalculatePart2()
    {
        return _reports.Count(report =>
            IsReportSafe(report) || IsReportSafeWithSingleBadTolerance(report));
    }

    private static bool IsReportSafe(IReadOnlyList<int> report)
    {
        if (report.Count <= 1) return true; // single element lists are considered consistent

        bool? isIncreasing = null;

        for (var i = 1; i < report.Count; i++)
        {
            if (report[i] == report[i - 1]) return false; // neither increasing nor decreasing
            if (Math.Abs(report[i] - report[i - 1]) > 3) // levels differ by at least one and at most three
                return false;

            if (isIncreasing == null)
                isIncreasing = report[i] > report[i - 1];

            // either gradually increasing and gradually decreasing
            else if ((isIncreasing == true && report[i] < report[i - 1]) ||
                     (isIncreasing == false && report[i] > report[i - 1])
                    ) return false;
        }

        return true;
    }

    private static bool IsReportSafeWithSingleBadTolerance(IReadOnlyList<int> report)
    {
        for (var i = 0; i < report.Count; i++)
        {
            var tempReport = new List<int>();

            for (var level = 0; level < report.Count; level++)
                if (level != i)
                    tempReport.Add(report[level]);

            if (IsReportSafe(tempReport))
                return true;
        }

        return false;
    }
}