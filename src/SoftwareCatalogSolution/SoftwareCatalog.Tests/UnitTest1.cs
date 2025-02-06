namespace SoftwareCatalog.Tests;

public class UnitTest1
{
    [Fact]
    public void CanAddTenAndTwentyInDotNet()
    {
        // "Given"
        int a = 10, b = 20, answer;

        // When
        answer = a + b; // System Under Test (SUT)

        // Then 
        Assert.Equal(30, answer);
    }

    [Theory]
    [InlineData(10, 20, 30)]
    [InlineData(2, 3, 5)]
    [InlineData(10, 2, 12)]
    public void CanAddAnyTwoIntegers(int a, int b, int expected)
    {
        var answer = a + b;

        Assert.Equal(expected, answer);
    }

    [Fact]
    public void DoingThingsWithACustomer()
    {
        var cust1 = new Customer(42, "Bob", 5000)
        {

            EmailAddress = "bob@aol.com"
        };


        var cust2 = new Customer(42, "Bob", 5000)
        {

            EmailAddress = "bob@aol.com"
        };

        Assert.Equal(cust1, cust2);

        var myName = "Jeff";

        var myBigName = myName.ToUpper();

        Assert.Equal("JEFF", myBigName);
        Assert.Equal("Jeff", myName);

        //var cust3 = new Customer()
        //{
        //    Id = cust1.Id,
        //    Name = "Robert",
        //    CreditLimit = cust2.Id,
        //};

        var cust3 = cust1 with { Name = "Robert" };
    }
}

public record Customer(int Id, string Name, decimal CreditLimit)
{
    //public required int Id { get; init; }
    //public required string Name { get; init; }
    //public required decimal CreditLimit { get; init; }

    public string EmailAddress { get; init; } = string.Empty;


}