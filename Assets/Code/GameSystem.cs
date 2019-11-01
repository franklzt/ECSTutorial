using UnityEngine;
using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;
using Unity.Jobs;
using Unity.Burst;

public class LevelUpSystem : JobComponentSystem
{
    [BurstCompile]
    public struct LevelUpJob : IJobForEach<LevelData>
    {
        public float DeltaTime;
        public void Execute(ref LevelData levelData)
        {
            levelData.Level += DeltaTime;
            if(levelData.Level >= 5.0f)
            {
                levelData.Level = 0.001f;
            }
        }
    }

    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        return new LevelUpJob { DeltaTime = Time.deltaTime }.Schedule(this, inputDeps);
    }
}


public class MoveSystem : JobComponentSystem
{

    [BurstCompile]
    public struct MoveSystemJob : IJobForEach<Translation, SpeedData,LevelData>
    {
        public float DeltaTime;

        public void Execute(ref Translation translation, ref SpeedData speedData,ref LevelData levelData)
        {
            translation.Value += new float3(0, speedData.Speed * DeltaTime * levelData.Level, 0);
            if (translation.Value.y > 5f)
            {
                speedData.Speed = -math.abs(speedData.Speed);
            }

            if (translation.Value.y < -5f)
            {
                speedData.Speed = math.abs(speedData.Speed);
            }
        }
    }

    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        return new MoveSystemJob() { DeltaTime = Time.deltaTime }.Schedule(this, inputDeps);
    }
}