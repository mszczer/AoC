using System.Linq;

namespace AoC.AoC2022
{
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

            foreach (var section in InputData)
            {
                var assignmentParts = section.GetWords(separator);
                var assignmentSection = new AssignmentSections
                {
                    FirstSectionStart = int.Parse(assignmentParts[0]),
                    FirstSectionEnd = int.Parse(assignmentParts[1]),
                    SecondSectionStart = int.Parse(assignmentParts[2]),
                    SecondSectionEnd = int.Parse(assignmentParts[3])
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
            //foreach (var assignment in _assignmentSections)
            //    if (IsEntireSectionOverlapped(assignment)) numberOfOverlaps++;
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
}