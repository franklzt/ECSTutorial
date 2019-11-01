using UnityEngine;
using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;
using Unity.Jobs;
using Unity.Burst;
using Unity.Rendering;

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
                levelData.Level = 3.0f;
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
            translation.Value += new float3(0, speedData.Speed * DeltaTime, -levelData.Level * DeltaTime);
            if (translation.Value.y > 5f)
            {
                speedData.Speed = -math.abs(speedData.Speed);
            }

            if (translation.Value.y < -5f)
            {
                speedData.Speed = math.abs(speedData.Speed);
            }

            if(translation.Value.z < -5)
            {
                translation.Value = new float3(translation.Value.x, translation.Value.y, 20f);
            }
        }
    }

    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        return new MoveSystemJob() { DeltaTime = Time.deltaTime }.Schedule(this, inputDeps);
    }
}

public class ChangeMatTextureSystem : JobComponentSystem
{

    [BurstCompile]
    public struct TextureJob : IJobForEach<MatData, TextureIndexData, TextureData>
    {
        public float DeltaTime;

        public void Execute(ref MatData mesh, ref TextureIndexData index, ref TextureData texture)
        {
            mesh.material.SetTexture("_MainTex", texture.textures[index.index]);
        }
    }

    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        return new TextureJob() { DeltaTime = Time.deltaTime }.Schedule(this, inputDeps);
    }
}