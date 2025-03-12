using Itmo.ObjectOrientedProgramming.Lab1.ResultTypes;

namespace Itmo.ObjectOrientedProgramming.Lab1.TrainEntity;

public class Train
{
    public double Weight { get; private set; }

    public double MaxForce { get; private set; }

    public double Speed { get; private set; } = 0;

    public double Acceleration { get; private set; } = 0;

    public double Accuracy { get; private set; }

    public Train(double weight, double maxForce, double accuracy)
    {
        if (weight <= 0)
        {
            throw new ArgumentException("Weight can not be negative or zero", nameof(weight));
        }

        if (maxForce < 0)
        {
            throw new ArgumentException("Max force can not be negative", nameof(maxForce));
        }

        if (accuracy <= 0)
        {
            throw new ArgumentException("Accuracy can not be negative or zero", nameof(accuracy));
        }

        Weight = weight;
        MaxForce = maxForce;
        Accuracy = accuracy;
    }

    public TrainResult ApplyForce(double force)
    {
        if (force > MaxForce)
        {
            return new TrainResult.ForceLimitReached();
        }

        Acceleration = force / Weight;
        return new TrainResult.SuccessApplyingForce();
    }

    public TrainResult GetThrough(double distance)
    {
        double time = 0;

        while (distance > 0)
        {
            Move(ref time);

            if (Speed <= 0)
            {
                return new TrainResult.FailedPass();
            }

            distance -= Accuracy * Speed;
        }

        return new TrainResult.SuccessPass(time);
    }

    public TrainResult Stop()
    {
        if (Acceleration >= 0)
        {
            return new TrainResult.FailedPass();
        }

        double time = 0;

        while (Speed > 0)
        {
            Move(ref time);
        }

        Speed = 0;

        return new TrainResult.SuccessPass(time);
    }

    public TrainResult Accelerate(double requiredSpeed)
    {
        if (Acceleration <= 0)
        {
            return new TrainResult.FailedPass();
        }

        double time = 0;

        while (Speed <= requiredSpeed)
        {
            Move(ref time);
        }

        Speed = requiredSpeed;

        return new TrainResult.SuccessPass(time);
    }

    private void Move(ref double time)
    {
        Speed += Acceleration * Accuracy;
        time += Accuracy;
    }
}