using Itmo.ObjectOrientedProgramming.Lab1.ResultTypes;
using Itmo.ObjectOrientedProgramming.Lab1.TrainEntity;

namespace Itmo.ObjectOrientedProgramming.Lab1.RouteEntity;

public class ForceMagneticRoute : IRoutePart
{
    public double Distance { get; private set; }

    public double Force { get; private set; }

    public ForceMagneticRoute(double distance, double force)
    {
        if (distance < 0)
        {
            throw new ArgumentException("Distance can not be negative", nameof(distance));
        }

        Distance = distance;
        Force = force;
    }

    public RouteResult GetThrough(Train train)
    {
        TrainResult result = train.ApplyForce(Force);
        if (result is TrainResult.ForceLimitReached)
        {
            return new RouteResult.ForceLimitReached();
        }

        result = train.GetThrough(Distance);

        if (result is TrainResult.SuccessPass success)
        {
            return new RouteResult.Success(success.Time);
        }

        return new RouteResult.Fail();
    }
}