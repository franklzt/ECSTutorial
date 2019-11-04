using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;
using Unity.Jobs;
using Unity.Burst;
using Unity.Rendering;
using UnityEngine;

public class SpriteSheetHander : MonoBehaviour
{
    public Material material;
    public Mesh spriteMesh;

    public Material material_instance;
    public Texture spriteTexture;
    public static SpriteSheetHander instance;

    private void Awake()
    {
        instance = this;

        material_instance = new Material(material);
        EntityManager entityManager = World.Active.EntityManager;
        EntityArchetype entityArchetype = entityManager.CreateArchetype(typeof(Translation));
        Entity entity = entityManager.CreateEntity(entityArchetype);

    }



    //private void Update()
    //{
    //    DrawEntity();
    //}

    void DrawEntity()
    {
        MaterialPropertyBlock materialPropertyBlock = new MaterialPropertyBlock();
        float uv_Width = 0.25f;
        float uv_Height = 1f;
        float uv_OffsetX = 0f;
        float uv_OffsetY = 0f;
        Vector4 uv = new Vector4(uv_Width, uv_Height, uv_OffsetX, uv_OffsetY);

        materialPropertyBlock.SetVectorArray("_MainTex", new Vector4[] { uv });

        Graphics.DrawMesh(SpriteSheetHander.instance.spriteMesh, Vector3.zero, Quaternion.identity,
            material_instance, 0, Camera.main, 0, materialPropertyBlock);
    }


}
