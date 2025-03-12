using Itmo.ObjectOrientedProgramming.Lab1.ResultTypes;
using Itmo.ObjectOrientedProgramming.Lab1.TrainEntity;

namespace Itmo.ObjectOrientedProgramming.Lab1.RouteEntity;

public class Route
{
    private readonly IReadOnlyCollection<IRoutePart> _routeParts;

    public double Time { get; private set; } = 0;

    public double MaxSpeed { get; private set; }

    public Route(IReadOnlyCollection<IRoutePart> routeParts, double maxSpeed)
    {
        if (routeParts == null)
        {
            throw new ArgumentException("Collection can not be null", nameof(routeParts));
        }

        if (routeParts.Any(part => part == null))
        {
            throw new ArgumentException("Route parts can not be null", nameof(routeParts));
        }

        if (maxSpeed < 0)
        {
            throw new ArgumentException("Max speed can not be negative", nameof(maxSpeed));
        }

        MaxSpeed = maxSpeed;
        _routeParts = routeParts;
    }

    public RouteResult GetThrough(Train train)
    {
        foreach (IRoutePart part in _routeParts)
        {
            RouteResult result = part.GetThrough(train);
            if (result is RouteResult.Success success)
            {
                Time += success.Time;
            }
            else
            {
                return result;
            }
        }

        return train.Speed <= MaxSpeed ? new RouteResult.Success(Time) : new RouteResult.Fail();
    }
}