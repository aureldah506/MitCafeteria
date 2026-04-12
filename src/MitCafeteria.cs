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
        var schedule = new List<ShiftPlan>();

        foreach (var id in shifts)
        {
            var plan = new ShiftPlan { ShiftId = id };

            // Logique de rotation : on crée un décalage basé sur l'ID du shift
            // On "fait tourner" la liste des employés pour que les binômes changent
            var rotatedStaff = staff.Skip(id % staff.Length)
                .Concat(staff.Take(id % staff.Length))
                .ToArray();

            for (int i = 0; i < sections.Length; i++)
            {
                plan.Assignments.Add(new Assignment
                {
                    Section = sections[i],
                    Employee1 = rotatedStaff[i * 2],
                    Employee2 = rotatedStaff[i * 2 + 1]
                });
            }
            schedule.Add(plan);
        }
        return schedule;
    }
}