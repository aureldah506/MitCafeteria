using Xunit;
using System.Linq;
using System;

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
    
    [Fact]
    public void should_throw_exception_when_number_of_employees_is_odd()
    {
        // Arrange
        string[] staff = { "Marcus", "Lateefa", "Donald" }; // 3 employés = Impair 
        string[] sections = { "Lobby" };
        int[] shifts = { 0 };

        // Act
        Action act = () => MitCafeteriaGenerator.Generate(staff, sections, shifts);

        // Assert
        Assert.Throws<ArgumentException>(act);
    }
    
    [Fact]
    public void should_return_empty_schedule_when_inputs_are_empty()
    {
        // Arrange
        string[] staff = Array.Empty<string>();
        string[] sections = Array.Empty<string>();
        int[] shifts = Array.Empty<int>();

        // Act
        var result = MitCafeteriaGenerator.Generate(staff, sections, shifts);

        // Assert
        Assert.Empty(result);
    }
    
    [Fact]
    public void should_throw_exception_when_sections_count_does_not_match_pairs_count()
    {
        // Arrange
        string[] staff = { "Marcus", "Lateefa", "Donald", "Rashad" }; // 2 binômes (4/2)
        string[] sections = { "Lobby" }; 
        int[] shifts = { 0 };

        // Act
        Action act = () => MitCafeteriaGenerator.Generate(staff, sections, shifts);

        // Assert
        Assert.Throws<ArgumentException>(act);
    }
    
    [Fact]
    public void should_format_shift_correctly_following_subject_example()
    {
        // Arrange
        string[] staff = { "Marcus", "Lateefa", "Donald", "Rashad", "Quincy", "Mia" };
        string[] sections = { "Lobby", "Dining Room", "Kitchen" };
        int[] shifts = { 0 };
        var schedule = MitCafeteriaGenerator.Generate(staff, sections, shifts);

        // Act
        string output = MitCafeteriaGenerator.FormatShift(schedule[0]);

        // Assert
        // On vérifie que le format contient les éléments clés demandés
        Assert.Contains("Shift 0", output);
        Assert.Contains("Lobby [ Marcus & Lateefa ]", output);
        Assert.Contains("Dining Room [ Donald & Rashad ]", output);
        Assert.Contains("Kitchen [ Quincy & Mia ]", output);
    }
}