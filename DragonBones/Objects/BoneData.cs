// ------------------------------------------------------------------------------
//  <autogenerated>
//      This code was generated by a tool.
//      Mono Runtime Version: 4.0.30319.1
// 
//      Changes to this file may cause incorrect behavior and will be lost if 
//      the code is regenerated.
//  </autogenerated>
// ------------------------------------------------------------------------------
using System;
namespace DragonBones.Objects
{
	public class BoneData
	{
		public string Name;
		public string Parent;
		public float Length;
		
		public DBTransform Global;
		public DBTransform Transform;
		
		public int ScaleMode;
		public bool FixedRotation;
		
		public BoneData()
		{
			Length = 0;
			Global = new DBTransform();
			Transform = new DBTransform();
			ScaleMode = 1;
			FixedRotation = false;
		}
		
		public void Dispose()
		{
			Global = null;
			Transform = null;
		}
	}
}

