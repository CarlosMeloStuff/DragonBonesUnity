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
using Com.Viperstudio.Geom;
using DragonBones;
using UnityEngine;
using Com.Viperstudio.Utils;

namespace DragonBones
{
	public class UnityDisplayBridge :IDisplayBridge
		{
	

	
		private UnityBoneDisplay _display;
		/**
		 * @inheritDoc
		 */
		public System.Object Display
		{
			get  { return _display; }

			set { 
				_display = value as UnityBoneDisplay;
			}
		}

		public bool Visible
		{
			get { return _display!=null?(_display as UnityBoneDisplay).Visible:false; }
			set {
					if(_display!=null)
					{
					(_display as UnityBoneDisplay).Visible = value;
					}

				}
		}

		/**
		 * @inheritDoc
		 */
		public void Dispose()
		{
			if(_display!=null)
			{
			 (_display as UnityBoneDisplay).Dispose();
			 _display = null;
			}

		}
		
		/**
		 * @inheritDoc
		 */

		//private Vector3 vector = new Vector3 ();
		//private float angle;
		public void UpdateTransform(Com.Viperstudio.Geom.Matrix matrix, DBTransform transform)
		{
			if(_display!=null)
			{
				(_display as UnityBoneDisplay).Update(matrix);
			}

		
		}
		
		/**
		 * @inheritDoc
		 */
		public void UpdateColor(
			float aOffset, 
			float rOffset, 
			float gOffset, 
			float bOffset, 
			float aMultiplier, 
			float rMultiplier, 
			float gMultiplier, 
			float bMultiplier
		)
		{
			/*
			_display.alpha = aMultiplier;
			if (_display is Quad)
			{
				(_display as Quad).color = (uint(rMultiplier * 0xff) << 16) + (uint(gMultiplier * 0xff) << 8) + uint(bMultiplier * 0xff);
			}
			*/
			return;
		}
		
		/**
         * @inheritDoc
         */
		public void UpdateBlendMode(string blendMode)
		{
			if (_display is GameObject)
			{
				//_display.blendMode = blendMode;
			}
			return;
		}
		
		/**
		 * @inheritDoc
		 */
		public void AddDisplay(System.Object container, float index = -1f)
		{
		
         
		}
		
		/**
		 * @inheritDoc
		 */
		public void RemoveDisplay()
		{

			//GameObject.Destroy (_display as GameObject);

		}
	}
}

