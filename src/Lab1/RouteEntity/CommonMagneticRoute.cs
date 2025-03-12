using Itmo.ObjectOrientedProgramming.Lab1.ResultTypes;
using Itmo.ObjectOrientedProgramming.Lab1.TrainEntity;

namespace Itmo.ObjectOrientedProgramming.Lab1.RouteEntity;

public class CommonMagneticRoute : IRoutePart
{
    public double Distance { get; private set; }

    private readonly double zeroForce = 0;

    public CommonMagneticRoute(double distance)
    {
        if (distance < 0)
        {
            throw new ArgumentException("Distance can not be negative", nameof(distance));
        }

        Distance = distance;
    }

    public RouteResult GetThrough(Train train)
    {
        train.ApplyForce(zeroForce);
        TrainResult result = train.GetThrough(Distance);

        if (result is TrainResult.SuccessPass success)
        {
            return new RouteResult.Success(success.Time);
        }

        return new RouteResult.Fail();
    }
}