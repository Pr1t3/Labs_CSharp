# Result
- Success/Fail (All routes must be completed succesfully)
- Time spent travelling

# Entities

## Train

### Attributes
- Weight
- Speed = 0 by default
- Acceleration = 0 by default
- Max force
### Requirements
- Calculate time spent traveling a part based on speed and acceleration
- Apply force on Magnetic routes. Recalculate acceleration: $\frac{force}{weight}$. `Fail` if force > max force.
- Accuracy. Calculate resulting speed = $speed + acceleration \cdot accurace$ and distance = $\text{resulting speed} * accuracy$. ???

## Part of the route - interface
Result - Succes/Fail
### Common magnetic route
- Distance
### Force magnetic route
- Distance
- Force (positive or negative)
### Station
- Disembarkation and boading people
- Number of people influence on time spent on station
- Max speed of train. speed > max speed $\Rightarrow$ Fail

## Route
- List of all route parts
- Max speed of train. speed > max speed $\Rightarrow$ Fail