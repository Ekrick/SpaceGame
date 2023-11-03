# SpaceGame

I used Jobs to handle the asteroids' movement and collision checking. I did this to be able to multi-thread the calculations for the asteroids.
I also used the burst compiler for these Jobs.
This improved the perfomance by a very small amount.

Later I added pooling systems to both the asteroids and lasers in order to reduce the amount of garbage collection needed.
This actually reduced the overall performance by a negligible amount, but it reduced/removed some perfomance spikes that otherwise occured when creating new gameobjects.
