using Itmo.ObjectOrientedProgramming.Lab1.ResultTypes;
using Itmo.ObjectOrientedProgramming.Lab1.TrainEntity;

namespace Itmo.ObjectOrientedProgramming.Lab1.RouteEntity;

public class StationRoute : IRoutePart
{
    public double MaxSpeed { get; private set; }

    public double BoardingTime { get; private set; }

    public double Force { get; private set; }

    public StationRoute(double maxSpeed, double boardingTime, double force)
    {
        if (maxSpeed < 0)
        {
            throw new ArgumentException("Max speed can not be negative", nameof(maxSpeed));
        }

        if (boardingTime < 0)
        {
            throw new ArgumentException("Boarding time can not be negative", nameof(boardingTime));
        }

        MaxSpeed = maxSpeed;
        BoardingTime = boardingTime;
        Force = force;
    }

    public RouteResult GetThrough(Train train)
    {
        if (train.Speed > MaxSpeed)
        {
            return new RouteResult.SpeedLimitReached();
        }

        double timeSpent = 0;
        double requiredSpeed = train.Speed;

        TrainResult result = train.ApplyForce(-Force);
        if (result is TrainResult.ForceLimitReached)
        {
            return new RouteResult.ForceLimitReached();
        }

        result = train.Stop();
        if (result is TrainResult.FailedPass)
        {
            return new RouteResult.Fail();
        }
        else if (result is TrainResult.SuccessPass success)
        {
            timeSpent += success.Time;
        }

        train.ApplyForce(Force);
        result = train.Accelerate(requiredSpeed);
        if (result is TrainResult.FailedPass)
        {
            return new RouteResult.Fail();
        }
        else if (result is TrainResult.SuccessPass success)
        {
            timeSpent += success.Time;
        }

        return new RouteResult.Success(timeSpent + BoardingTime);
    }
}