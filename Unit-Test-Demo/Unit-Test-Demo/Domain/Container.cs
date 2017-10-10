using System;

namespace Unit_Test_Demo.Domain
{
    public class Container
    {
        public int Id { get; private set; }
        public int MaxCapacity { get; private set; }
        public int CurrentCapacity { get; private set; }

        public Container()
        {
        }

        public Container(int maxCapacity)
        {
            if (maxCapacity < 0)
            {
                throw new ArgumentException("Create container failed: max capacity must be a positive integer!");
            }
            else
            {
                MaxCapacity = maxCapacity;
            }
        }

        public void UpdateCurrentCapacity(int capacity)
        {
            CurrentCapacity = capacity;
        }

        public void Pack()
        {
            if (MaxCapacity == 0 || CurrentCapacity < MaxCapacity)
            {
                CurrentCapacity++;
            }
            else
            {
                throw new InvalidOperationException("Pack failed: container is already at maximum capacity!");
            }
        }
    }
}
