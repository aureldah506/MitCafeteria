namespace src;

public class PlanningGeneratorTests
{
    [Fact]
    public void should_create_three_assignments_for_one_shift_when_six_employees_and_three_sections_are_given()
    {
        // Arrange
        string[] staff = { "Marcus", "Lateefa", "Donald", "Rashad", "Quincy", "Mia" };
        string[] sections = { "Lobby", "Dining Room", "Kitchen" };
        int[] shifts = { 0 };

        // Act
        var result = MitCafeteriaGenerator.Generate(staff, sections, shifts);

        // Assert
        Assert.Single(result); 
        Assert.Equal(3, result[0].Assignments.Count); 
    }
    
    [Fact]
    public void should_change_partners_between_shifts_when_multiple_shifts_are_requested()
    {
        // Arrange
        string[] staff = { "Marcus", "Lateefa", "Donald", "Rashad", "Quincy", "Mia" };
        string[] sections = { "Lobby", "Dining Room", "Kitchen" };
        int[] shifts = { 0, 1 }; 
        
        // Act
        var schedule = MitCafeteriaGenerator.Generate(staff, sections, shifts);

        var partnerShift0 = schedule[0].Assignments.First(a => a.Employee1 == "Marcus").Employee2;
        var partnerShift1 = schedule[1].Assignments.First(a => a.Employee1 == "Marcus" || a.Employee2 == "Marcus");
    
        string currentPartnerShift1 = (partnerShift1.Employee1 == "Marcus") ? partnerShift1.Employee2 : partnerShift1.Employee1;

        // Assert
        Assert.NotEqual(partnerShift0, currentPartnerShift1); 
    }
}