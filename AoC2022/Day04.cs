namespace AoC.AoC2022;

internal class Day04 : AoC<List<string>, int, int>
{
    private readonly List<AssignmentSections> _assignmentSections;

    public Day04(string dayName) : base(dayName)
    {
        _assignmentSections = GetAssignmentSections();
    }

    private List<AssignmentSections> GetAssignmentSections()
    {
        var assignmentSections = new List<AssignmentSections>();
        var separator = new[] { "-", "," };

        if (InputData == null) return assignmentSections;

        foreach (var section in InputData)
        {
            if (string.IsNullOrWhiteSpace(section)) continue;

            var assignmentParts = section.GetWords(separator);
            if (assignmentParts.Length < 4)
                throw new FormatException($"Invalid assignment section line: '{section}'");

            if (!int.TryParse(assignmentParts[0], out var a0) ||
                !int.TryParse(assignmentParts[1], out var a1) ||
                !int.TryParse(assignmentParts[2], out var a2) ||
                !int.TryParse(assignmentParts[3], out var a3))
                throw new FormatException($"Invalid numeric values in line: '{section}'");

            var assignmentSection = new AssignmentSections
            {
                FirstSectionStart = a0,
                FirstSectionEnd = a1,
                SecondSectionStart = a2,
                SecondSectionEnd = a3
            };
            assignmentSections.Add(assignmentSection);
        }

        return assignmentSections;
    }

    public override int CalculatePart1()
    {
        return CalculateDuplicatedAssignments();
    }

    private int CalculateDuplicatedAssignments()
    {
        return _assignmentSections.Count(IsEntireSectionOverlapped);
    }

    private static bool IsEntireSectionOverlapped(AssignmentSections assignment)
    {
        return (assignment.FirstSectionStart >= assignment.SecondSectionStart &&
                assignment.FirstSectionEnd <= assignment.SecondSectionEnd)
               || (assignment.SecondSectionStart >= assignment.FirstSectionStart &&
                   assignment.SecondSectionEnd <= assignment.FirstSectionEnd);
    }

    public override int CalculatePart2()
    {
        return CalculateOverlapsAtAll();
    }

    private int CalculateOverlapsAtAll()
    {
        return _assignmentSections.Count(IsOverlap);
    }

    private static bool IsOverlap(AssignmentSections assignment)
    {
        for (var i = assignment.FirstSectionStart; i <= assignment.FirstSectionEnd; i++)
        for (var j = assignment.SecondSectionStart; j <= assignment.SecondSectionEnd; j++)
            if (i == j)
                return true;
        return false;
    }

    private struct AssignmentSections
    {
        public int FirstSectionStart;
        public int FirstSectionEnd;
        public int SecondSectionStart;
        public int SecondSectionEnd;
    }
}