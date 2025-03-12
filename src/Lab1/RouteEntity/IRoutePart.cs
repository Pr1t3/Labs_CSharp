using Itmo.ObjectOrientedProgramming.Lab1.ResultTypes;
using Itmo.ObjectOrientedProgramming.Lab1.TrainEntity;

namespace Itmo.ObjectOrientedProgramming.Lab1.RouteEntity;

public interface IRoutePart
{
    RouteResult GetThrough(Train train);
}