namespace Banking.UnitTests;

public class UnitTest1
{
    [Fact]
    public void Test1()
    {
        // "Create the world anew" - Given (Arrange)
        int a = 10, b = 20, answer;
        // Poke at it - When (Act)
        answer = a + b; // "Unit" we are testing here (SUT) addition

        // Observer the results - Then (Assert)
        Assert.Equal(30, answer);
    }

    [Theory]
    [InlineData(2,2, 4)]
    [InlineData(10,5, 15)]
    [InlineData(15, 0, 15)]
    public void CanAddAnyTwoIntegers(int a, int b, int expected)
    {
        int answer = a + b;

        Assert.Equal(expected, answer);
    }
}