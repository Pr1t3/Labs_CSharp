using Itmo.ObjectOrientedProgramming.Lab1.ResultTypes;
using Itmo.ObjectOrientedProgramming.Lab1.RouteEntity;
using Itmo.ObjectOrientedProgramming.Lab1.TrainEntity;
using Xunit;

namespace Lab1.Tests;

public class Lab1Test
{
    [Fact]
    public void Scenario1()
    {
        // Arrange
        double trainWeight = 100;
        double trainMaxForce = 100;
        double trainAccuracy = 0.1;
        var train = new Train(trainWeight, trainMaxForce, trainAccuracy);

        var forceMagneticRoute = new ForceMagneticRoute(100, 100);
        var commonMagneticRoute = new CommonMagneticRoute(100);

        var route = new Route(new List<IRoutePart> { forceMagneticRoute, commonMagneticRoute }, 1000);

        // Act
        RouteResult result = route.GetThrough(train);

        // Assert
        Assert.IsType<RouteResult.Success>(result);
    }

    [Fact]
    public void Scenario2()
    {
        // Arrange
        double trainWeight = 100;
        double trainMaxForce = 100;
        double trainAccuracy = 0.1;
        var train = new Train(trainWeight, trainMaxForce, trainAccuracy);

        var forceMagneticRoute = new ForceMagneticRoute(100, 101);
        var commonMagneticRoute = new CommonMagneticRoute(100);

        var route = new Route(new List<IRoutePart> { forceMagneticRoute, commonMagneticRoute }, 1000);

        // Act
        RouteResult result = route.GetThrough(train);

        // Assert
        Assert.IsNotType<RouteResult.Success>(result);
    }

    [Fact]
    public void Scenario3()
    {
        // Arrange
        double trainWeight = 100;
        double trainMaxForce = 100;
        double trainAccuracy = 0.1;
        var train = new Train(trainWeight, trainMaxForce, trainAccuracy);

        var forceMagneticRoute = new ForceMagneticRoute(100, 100);
        var commonMagneticRoute = new CommonMagneticRoute(100);
        var stationRoute = new StationRoute(10000000, 10, 100);

        var route = new Route(new List<IRoutePart> { forceMagneticRoute, commonMagneticRoute, stationRoute, commonMagneticRoute }, 1000);

        // Act
        RouteResult result = route.GetThrough(train);

        // Assert
        Assert.IsType<RouteResult.Success>(result);
    }

    [Fact]
    public void Scenario4()
    {
        // Arrange
        double trainWeight = 100;
        double trainMaxForce = 100;
        double trainAccuracy = 0.1;
        var train = new Train(trainWeight, trainMaxForce, trainAccuracy);

        var forceMagneticRoute = new ForceMagneticRoute(100, 100);
        var stationRoute = new StationRoute(14, 10, 100);
        var commonMagneticRoute = new CommonMagneticRoute(100);

        var route = new Route(new List<IRoutePart> { forceMagneticRoute, stationRoute, commonMagneticRoute }, 1000);

        // Act
        RouteResult result = route.GetThrough(train);

        // Assert
        Assert.IsNotType<RouteResult.Success>(result);
    }

    [Fact]
    public void Scenario5()
    {
        // Arrange
        double trainWeight = 100;
        double trainMaxForce = 100;
        double trainAccuracy = 0.1;
        var train = new Train(trainWeight, trainMaxForce, trainAccuracy);

        var forceMagneticRoute = new ForceMagneticRoute(100, 100);
        var commonMagneticRoute = new CommonMagneticRoute(100);
        var stationRoute = new StationRoute(10000, 10, 100);

        var route = new Route(new List<IRoutePart> { forceMagneticRoute, commonMagneticRoute, stationRoute, commonMagneticRoute }, 14);

        // Act
        RouteResult result = route.GetThrough(train);

        // Assert
        Assert.IsNotType<RouteResult.Success>(result);
    }

    [Fact]
    public void Scenario6()
    {
        // Arrange
        double trainWeight = 100;
        double trainMaxForce = 100;
        double trainAccuracy = 0.1;
        var train = new Train(trainWeight, trainMaxForce, trainAccuracy);

        var forceMagneticRoute1 = new ForceMagneticRoute(100, 100);
        var commonMagneticRoute = new CommonMagneticRoute(100);
        var forceMagneticRoute2 = new ForceMagneticRoute(100, -50);
        var stationRoute = new StationRoute(10, 10, 100);
        var forceMagneticRoute3 = new ForceMagneticRoute(100, -100);

        var route = new Route(new List<IRoutePart> { forceMagneticRoute1, commonMagneticRoute, forceMagneticRoute2, stationRoute, commonMagneticRoute, forceMagneticRoute1, commonMagneticRoute, forceMagneticRoute3 }, 10);

        // Act
        RouteResult result = route.GetThrough(train);

        // Assert
        Assert.IsType<RouteResult.Success>(result);
    }

    [Fact]
    public void Scenario7()
    {
        // Arrange
        double trainWeight = 100;
        double trainMaxForce = 100;
        double trainAccuracy = 0.1;
        var train = new Train(trainWeight, trainMaxForce, trainAccuracy);

        var commonMagneticRoute = new CommonMagneticRoute(100);

        var route = new Route(new List<IRoutePart> { commonMagneticRoute }, 10000);

        // Act
        RouteResult result = route.GetThrough(train);

        // Assert
        Assert.IsNotType<RouteResult.Success>(result);
    }

    [Fact]
    public void Scenario8()
    {
        // Arrange
        double trainWeight = 100;
        double trainMaxForce = 100;
        double trainAccuracy = 0.1;
        var train = new Train(trainWeight, trainMaxForce, trainAccuracy);

        var forceMagneticRoute1 = new ForceMagneticRoute(100, 100);
        var forceMagneticRoute2 = new ForceMagneticRoute(100, -200);

        var route = new Route(new List<IRoutePart> { forceMagneticRoute1, forceMagneticRoute2 }, 10000);

        // Act
        RouteResult result = route.GetThrough(train);

        // Assert
        Assert.IsNotType<RouteResult.Success>(result);
    }
}
