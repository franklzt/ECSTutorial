using Unity.Entities;
using Unity.Burst;
using Unity.Collections;

[BurstCompile]
public struct LevelData : IComponentData
{
    public float Level;
}

[BurstCompile]
public struct SpeedData : IComponentData
{
    public float Speed;
}