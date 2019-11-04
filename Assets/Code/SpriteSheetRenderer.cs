using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;
using Unity.Jobs;
using Unity.Burst;
using Unity.Rendering;
using UnityEngine;

public class SpriteSheetRenderer : ComponentSystem
{
    protected override void OnUpdate()
    {
        Entities.ForEach((ref Translation translation) =>
        {
            MaterialPropertyBlock materialPropertyBlock = new MaterialPropertyBlock();
            float uv_Width = 1f / 4;
            float uv_Height = 1f;
            float uv_OffsetX = 0f;
            float uv_OffsetY = 0f;
            Vector4 uv = new Vector4(uv_Width, uv_Height, uv_OffsetX, uv_OffsetY);

            Material material = SpriteSheetHander.instance.material;
            material.SetTextureScale("_MainTex", new Vector2(uv.x,uv.y));
            material.SetTextureOffset("_MainTex", new Vector2(uv.z,uv.w));

            Graphics.DrawMesh(SpriteSheetHander.instance.spriteMesh, translation.Value, Quaternion.identity,
                material, 0, Camera.main, 0, materialPropertyBlock);
        });
    }
}

public class SpriteSheetAnimationSystem : ComponentSystem
{
    protected override void OnUpdate()
    {
        
    }
}
