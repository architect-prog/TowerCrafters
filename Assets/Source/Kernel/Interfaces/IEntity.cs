﻿namespace Source.Kernel.Interfaces
{
    public interface IEntity<out TId>
    {
        TId Id { get; }
    }
}