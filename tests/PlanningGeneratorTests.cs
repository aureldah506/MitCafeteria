namespace src;

using src; 
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
}