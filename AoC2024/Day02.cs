using AoC.Common;

namespace AoC.AoC2024;

internal class Day02 : AoC<List<string>, int, int>
{
    private List<List<int>> _reports;

    public Day02(string dayName) : base(dayName)
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
        var safeReportsNum = 0;

        foreach (var report in _reports)
            if (IsReportSafe(report))
                safeReportsNum++;

        return safeReportsNum;
    }

    private bool IsReportSafe(List<int> report)
    {
        if (report.Count <= 1) return true;                 // single element lists are considered consistent

        bool? isIncreasing = null;

        for (var i = 1; i < report.Count; i++)
        {
            if (report[i] == report[i - 1]) return false;   // neither increasing nor decreasing
            if (Math.Abs(report[i] - report[i - 1]) > 3)    // levels differ by at least one and at most three
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

    public override int CalculatePart2()
    {
        throw new NotImplementedException();
    }
}