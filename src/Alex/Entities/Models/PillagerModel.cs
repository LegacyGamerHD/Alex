


using Alex.ResourcePackLib.Json.Models.Entities;
using Microsoft.Xna.Framework;

namespace Alex.Entities.Models 
{

	public partial class PillagerModel : EntityModel
	{
		public PillagerModel()
		{
			Name = "geometry.pillager";
			VisibleBoundsWidth = 2;
			VisibleBoundsHeight = 2;
			VisibleBoundsOffset = new Vector3(0f, 0f, 0f);
			Texturewidth = 64;
			Textureheight = 64;
			Bones = new EntityModelBone[9]
			{
				new EntityModelBone(){ 
					Name = "head",
					Parent = "body",
					Pivot = new Vector3(0f,24f,0f),
					Rotation = new Vector3(0f,0f,0f),
					NeverRender = false,
					Mirror = false,
					Reset = false,
					Cubes = new EntityModelCube[1]{
						new EntityModelCube()
						{
							Origin = new Vector3(-4f,24f,-4f),
							Size = new Vector3(8f, 10f, 8f),
							Uv = new Vector2(0f, 0f)
						},
					}
				},
				new EntityModelBone(){ 
					Name = "nose",
					Parent = "head",
					Pivot = new Vector3(0f,26f,0f),
					Rotation = new Vector3(0f,0f,0f),
					NeverRender = false,
					Mirror = false,
					Reset = false,
					Cubes = new EntityModelCube[1]{
						new EntityModelCube()
						{
							Origin = new Vector3(-1f,23f,-6f),
							Size = new Vector3(2f, 4f, 2f),
							Uv = new Vector2(24f, 0f)
						},
					}
				},
				new EntityModelBone(){ 
					Name = "body",
					Parent = "waist",
					Pivot = new Vector3(0f,0f,0f),
					Rotation = new Vector3(0f,0f,0f),
					NeverRender = false,
					Mirror = false,
					Reset = false,
					Cubes = new EntityModelCube[2]{
						new EntityModelCube()
						{
							Origin = new Vector3(-4f,12f,-3f),
							Size = new Vector3(8f, 12f, 6f),
							Uv = new Vector2(16f, 20f)
						},
						new EntityModelCube()
						{
							Origin = new Vector3(-4f,6f,-3f),
							Size = new Vector3(8f, 18f, 6f),
							Uv = new Vector2(0f, 38f)
						},
					}
				},
				new EntityModelBone(){ 
					Name = "waist",
					Parent = "",
					Pivot = new Vector3(0f,12f,0f),
					Rotation = new Vector3(0f,0f,0f),
					NeverRender = true,
					Mirror = false,
					Reset = false,
					Cubes = new EntityModelCube[0]
				},
				new EntityModelBone(){ 
					Name = "leftLeg",
					Parent = "body",
					Pivot = new Vector3(2f,12f,0f),
					Rotation = new Vector3(0f,0f,0f),
					NeverRender = false,
					Mirror = false,
					Reset = false,
					Cubes = new EntityModelCube[1]{
						new EntityModelCube()
						{
							Origin = new Vector3(0f,0f,-2f),
							Size = new Vector3(4f, 12f, 4f),
							Uv = new Vector2(0f, 22f)
						},
					}
				},
				new EntityModelBone(){ 
					Name = "rightLeg",
					Parent = "body",
					Pivot = new Vector3(-2f,12f,0f),
					Rotation = new Vector3(0f,0f,0f),
					NeverRender = false,
					Mirror = true,
					Reset = false,
					Cubes = new EntityModelCube[1]{
						new EntityModelCube()
						{
							Origin = new Vector3(-4f,0f,-2f),
							Size = new Vector3(4f, 12f, 4f),
							Uv = new Vector2(0f, 22f)
						},
					}
				},
				new EntityModelBone(){ 
					Name = "rightarm",
					Parent = "body",
					Pivot = new Vector3(-5f,22f,0f),
					Rotation = new Vector3(0f,0f,0f),
					NeverRender = false,
					Mirror = false,
					Reset = false,
					Cubes = new EntityModelCube[1]{
						new EntityModelCube()
						{
							Origin = new Vector3(-8f,12f,-2f),
							Size = new Vector3(4f, 12f, 4f),
							Uv = new Vector2(40f, 46f)
						},
					}
				},
				new EntityModelBone(){ 
					Name = "rightItem",
					Parent = "rightArm",
					Pivot = new Vector3(-6f,15f,-1f),
					Rotation = new Vector3(0f,0f,0f),
					NeverRender = true,
					Mirror = false,
					Reset = false,
					Cubes = new EntityModelCube[0]
				},
				new EntityModelBone(){ 
					Name = "leftarm",
					Parent = "body",
					Pivot = new Vector3(5f,22f,0f),
					Rotation = new Vector3(0f,0f,0f),
					NeverRender = false,
					Mirror = true,
					Reset = false,
					Cubes = new EntityModelCube[1]{
						new EntityModelCube()
						{
							Origin = new Vector3(4f,12f,-2f),
							Size = new Vector3(4f, 12f, 4f),
							Uv = new Vector2(40f, 46f)
						},
					}
				},
			};
		}

	}

}