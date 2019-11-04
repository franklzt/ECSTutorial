using Unity.Entities;
using Unity.Burst;
using Unity.Collections;
using UnityEngine;

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

[BurstCompile]
public struct TextureIndexData : IComponentData
{
    public int index;
}

