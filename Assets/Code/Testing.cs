
using UnityEngine;
using Unity.Entities;
using Unity.Collections;
using Unity.Transforms;
using Unity.Rendering;
using Unity.Mathematics;
using Random = UnityEngine.Random;

public class Testing : MonoBehaviour
{

   [SerializeField] public Mesh entityMesh;
   [SerializeField] public Material entityMaterial;


    public Texture[] allTextures;

    public int totalEntities = 1000;

    void Start()
    {
        EntityManager entityManager = World.Active.EntityManager;
        EntityArchetype entityArchetype = entityManager.CreateArchetype(
            typeof(LevelData), 
            typeof(Translation), 
            typeof(RenderMesh), 
            typeof(LocalToWorld),
            typeof(SpeedData)
            );
        NativeArray<Entity> entityArray = new NativeArray<Entity>(totalEntities, Allocator.Temp);
        entityManager.CreateEntity(entityArchetype, entityArray);

        for (int i = 0; i < entityArray.Length; i++)
        {
            Entity entity = entityArray[i];
            entityManager.SetComponentData(entity, new LevelData() { Level = Random.Range(1,3) });
            entityManager.SetComponentData(entity, new SpeedData() { Speed = Random.Range(1f, 2f) });
            float3 startPos = new float3(Random.Range(-8, 8f), Random.Range(-5, 5), Random.Range(0, 100));
            entityManager.SetComponentData(entity, new Translation { Value = startPos });
            entityManager.SetSharedComponentData(entity, new RenderMesh { mesh = entityMesh, material = entityMaterial });


        }

        entityArray.Dispose();
    }
}
