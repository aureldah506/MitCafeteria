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
                
            for (int i = 0; i < 3; i++)
            {
                plan.Assignments.Add(new Assignment {
                    Section = sections[i],
                    Employee1 = staff[i*2],
                    Employee2 = staff[i*2+1]
                });
            }
            schedule.Add(plan);
        }
        return schedule;
    }
}