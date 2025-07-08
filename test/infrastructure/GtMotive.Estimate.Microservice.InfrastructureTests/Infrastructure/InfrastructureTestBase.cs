using Xunit;

namespace GtMotive.Estimate.Microservice.InfrastructureTests.Infrastructure
{
    [Collection(TestCollections.TestServer)]
#pragma warning disable CA1515 // Consider making public types internal
    public abstract class InfrastructureTestBase(GenericInfrastructureTestServerFixture fixture)
#pragma warning restore CA1515 // Consider making public types internal
    {
        protected GenericInfrastructureTestServerFixture Fixture { get; } = fixture;
    }
}
