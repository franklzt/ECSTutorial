using Unity.Entities;
using Unity.Burst;
using System;
using Unity.Collections;

public static class EntityExtensions
{
    public static Entity EasyEntityCreate(this object obj, params ComponentType[] types)
    {
        EntityManager entityManager = World.Active.EntityManager;
        EntityArchetype entityArchetype = entityManager.CreateArchetype(
            types);
        Entity entity = entityManager.CreateEntity(entityArchetype);
        return entity;
    }

    public static EntityArchetype CreateArchetype(this object obj,params ComponentType[] types)
    {
        return GetEntityManger().CreateArchetype(types);
    }




    public static EntityManager GetEntityManger(this object obj)
    {
        return World.Active.EntityManager;
    }

    public static EntityManager GetEntityManger()
    {
        return World.Active.EntityManager;
    }

    public static void SetComponentData<T>(this Entity entity, T componentData) where T : struct, IComponentData
    {
        entity.GetEntityManger().SetComponentData(entity, componentData);
    }

    public static void SetSharedComponentData<T>(this Entity entity, T componentData) where T : struct, ISharedComponentData
    {
        GetEntityManger().SetSharedComponentData(entity, componentData);
    }

}
