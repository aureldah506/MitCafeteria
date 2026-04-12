namespace src;

public class Assignment 
{
    public string Section { get; set; } = string.Empty;
    public string Employee1 { get; set; } = string.Empty;
    public string Employee2 { get; set; } = string.Empty;
}

public class ShiftPlan 
{
    public int ShiftId { get; set; }
    public List<Assignment> Assignments { get; set; } = new();
}

public static class MitCafeteriaGenerator
{
    public static List<ShiftPlan> Generate(string[] staff, string[] sections, int[] shifts)
    {
        // On transforme chaque ID de shift en un ShiftPlan
        return shifts.Select(id => CreateShiftPlan(id, staff, sections)).ToList();
    }

    private static ShiftPlan CreateShiftPlan(int shiftId, string[] staff, string[] sections)
    {
        var plan = new ShiftPlan { ShiftId = shiftId };
        var rotatedStaff = GetRotatedStaff(staff, shiftId);

        for (int i = 0; i < sections.Length; i++)
        {
            plan.Assignments.Add(new Assignment
            {
                Section = sections[i],
                Employee1 = rotatedStaff[i * 2],
                Employee2 = rotatedStaff[i * 2 + 1]
            });
        }
        return plan;
    }

    private static string[] GetRotatedStaff(string[] staff, int shiftId)
    {
        int offset = shiftId % staff.Length;
        return staff.Skip(offset).Concat(staff.Take(offset)).ToArray();
    }
}