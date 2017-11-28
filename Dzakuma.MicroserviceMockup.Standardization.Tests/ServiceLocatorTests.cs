using Xunit;

namespace Dzakuma.MicroserviceMockup.Standardization.Tests
{
    public class ServiceLocatorTests
    {
	    public class LocateService
	    {
		    [Fact]
		    public void ShouldReturn_CountOfZero_IfSearchDirectoryDoesNotExist()
		    {
			    var testObject = new ServiceLocator(@"C:\animaniacs", "bash");
			    Assert.Equal(0, testObject.LocateService());
		    }

			[Fact]
		    public void ShouldReturn_CountOfZero_IfNoServicesFound()
		    {
				var testObject = new ServiceLocator(@"C:\Program Files\Git\bin", "gulliver");
			    Assert.Equal(0, testObject.LocateService());
			}

			[Fact]
		    public void ShouldReturn_CountOfGreaterThanZero_IfServicesFound()
		    {
			    var testObject = new ServiceLocator(@"C:\Program Files\Git\bin", "bash");
				Assert.True(testObject.LocateService() > 0);
		    }
	    }

	    public class IsServiceLocatedProperty
	    {
		    [Fact]
		    public void ShouldReturn_False_IfSearchDirectoryDoesNotExist()
		    {
			    var testObject = new ServiceLocator(@"C:\animaniacs", "bash");
			    Assert.False(testObject.IsServiceLocated);
		    }

		    [Fact]
		    public void ShouldReturn_False_IfNoServicesFound()
		    {
			    var testObject = new ServiceLocator(@"C:\Program Files\Git\bin", "gulliver");
				Assert.False(testObject.IsServiceLocated);
			}

		    [Fact]
		    public void ShouldReturn_True_IfServicesFound()
		    {
			    var testObject = new ServiceLocator(@"C:\Program Files\Git\bin", "bash");
				Assert.True(testObject.IsServiceLocated);
			}
	    }
	}
}
