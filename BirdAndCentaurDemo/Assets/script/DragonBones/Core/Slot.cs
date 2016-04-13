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
using System.Collections.Generic;
using Com.Viperstudio.Geom;
using Com.Viperstudio.Utils;
namespace DragonBones
{
		public class Slot :DBObject
		{

	
		public bool _isShowDisplay;
		protected int _displayIndex;
		public float _originZOrder;
		public float _tweenZOrder;
		public float _offsetZOrder;
		protected DragonBones.BlendMode _blendMode;
		protected ColorTransform _colorTransform = new ColorTransform();

		// <displayOrArmature*, DisplayType>
		protected List<KeyValuePair<object, DragonBones.DisplayType>> _displayList;
		
		public SlotData _slotData;
		public object _display;
		public Armature _childArmature;


		public Slot (SlotData slotData)
		{

			_isShowDisplay = false;
			_displayIndex = -1;
			_originZOrder = 0f;
			_tweenZOrder = 0f;
			_offsetZOrder = 0f;
			_blendMode = DragonBones.BlendMode.BM_NORMAL;
			_slotData = slotData;
			_childArmature = null;
			_display = null;
			inheritRotation = true;
			inheritScale = true;
	    }


		public int getDisplayIndex() 
		{
			return _displayIndex;
		}
		
		public float getZOrder() 
		{
			return _originZOrder + _tweenZOrder + _offsetZOrder;
		}

		public void setZOrder(float value)
		{
			if (getZOrder() != value)
			{
				_offsetZOrder = value - _originZOrder - _tweenZOrder;
				
				if (_armature!=null)
				{
					_armature._slotsZOrderChanged = true;
				}
			}
		}
		
		public object getDisplay() 
		{
			return _display;
		}

		public void setDisplay(object display, DragonBones.DisplayType displayType, bool disposeExisting)
		{
			if (_displayIndex < 0)
			{
				_isShowDisplay = true;
				_displayIndex = 0;
			}
			
			if (_displayIndex >= (_displayList.Count))
			{
				//_displayList.Count = (_displayIndex + 1);
				_displayList.Add(new KeyValuePair<object, DragonBones.DisplayType>());
			}
			
			if (_displayList[_displayIndex].Key == display)
			{
				return;
			}
			
			_displayList[_displayIndex] = new KeyValuePair<object, DragonBones.DisplayType>(display, displayType);
			//_displayList[_displayIndex].Value = displayType;
			updateSlotDisplay(disposeExisting);
		}
		
		public Armature getChildArmature()
		{
			return _childArmature;
		}

		public void setChildArmature(Armature childArmature, bool disposeExisting)
		{
			setDisplay(childArmature, DragonBones.DisplayType.DT_ARMATURE, disposeExisting);
		}
		
		public List<KeyValuePair<object, DragonBones.DisplayType>> getDisplayList() 
		{
			return _displayList;
		}

		public void setDisplayList(List<KeyValuePair<object, DragonBones.DisplayType>> displayList, bool disposeExisting)
		{
            
			if (_displayIndex < 0)
			{
				_isShowDisplay = true;
				_displayIndex = 0;
			}
			
			if (disposeExisting)
			{
				disposeDisplayList();
				_childArmature = null;
				_display = null;
			}
			
			// copy
			_displayList = displayList;
			int displayIndexBackup = _displayIndex;
			_displayIndex = -1;
			changeDisplay(displayIndexBackup);
            
		}
		
		public void setVisible(bool visible)
		{
			if (_visible != visible)
			{
				_visible = visible;
				updateDisplayVisible(_visible);
			}
		}
		
		public override void setArmature(Armature armature)
		{
			base.setArmature(armature);
			
			if (_armature!=null)
			{
				_armature._slotsZOrderChanged = true;
				addDisplayToContainer(_armature._display, -1);
			}
			else
			{
				removeDisplayFromContainer();
			}
		}


		public void update()
		{
            
			if (_parent._needUpdate <= 0)
			{
				return;
			}

            //Logger.Log(name  + " " + _parent.origin.X + " " + origin.X );

            float x = origin.X + offset.X + _parent._tweenPivot.X;
            float y = origin.Y + offset.Y + _parent._tweenPivot.Y;
			Com.Viperstudio.Geom.Matrix parentMatrix = _parent.globalTransformMatrix;

            //globalTransformMatrix.Tx = global.X = parentMatrix.A * x + parentMatrix.C * y  + parentMatrix.Tx;
            //globalTransformMatrix.Ty = global.Y = parentMatrix.D * y + parentMatrix.B * x  + parentMatrix.Ty;
            globalTransformMatrix.Tx = global.X = parentMatrix.A * x * _parent.global.ScaleX + parentMatrix.C * y * _parent.global.ScaleY + parentMatrix.Tx;
            globalTransformMatrix.Ty = global.Y = parentMatrix.D * y * _parent.global.ScaleY + parentMatrix.B * x * _parent.global.ScaleX + parentMatrix.Ty;
			   
			if (inheritRotation)
			{
				global.SkewX = origin.SkewX + offset.SkewX + _parent.global.SkewX;
				global.SkewY = origin.SkewY + offset.SkewY + _parent.global.SkewY;
			}
			else
			{
				global.SkewX = origin.SkewX + offset.SkewX;
				global.SkewY = origin.SkewY + offset.SkewY;
			}

			if (inheritScale)
			{
				global.ScaleX = origin.ScaleX * offset.ScaleX * _parent.global.ScaleX;
				global.ScaleY = origin.ScaleY * offset.ScaleY * _parent.global.ScaleY;
			}
			else
			{
				global.ScaleX = origin.ScaleX * offset.ScaleX;
				global.ScaleY = origin.ScaleY * offset.ScaleY;
			}
			
			globalTransformMatrix.A = global.ScaleX * (float)Math.Cos(global.SkewY);
			globalTransformMatrix.B = global.ScaleX * (float)Math.Sin(global.SkewY);
			globalTransformMatrix.C = -global.ScaleY * (float)Math.Sin(global.SkewX);
			globalTransformMatrix.D = global.ScaleY * (float)Math.Cos(global.SkewX);
            
			updateDisplayTransform();
		}


		public void changeDisplay(int displayIndex)
		{
			if (displayIndex < 0)
			{
				if (_isShowDisplay)
				{
					_isShowDisplay = false;
					removeDisplayFromContainer();
					updateChildArmatureAnimation();
				}
			}
			else if (_displayList.Count>0)
			{
				if (displayIndex >= (int)(_displayList.Count))
				{
					displayIndex = _displayList.Count - 1;
				}
				
				if (_displayIndex != displayIndex)
				{
					_isShowDisplay = true;
					_displayIndex = displayIndex;
					updateSlotDisplay(false);
					
					if (
						_slotData!=null &&
						_slotData.displayDataList.Count>0 &&
						_displayIndex < (int)(_slotData.displayDataList.Count)
						)
					{
						origin = _slotData.displayDataList[_displayIndex].transform;
					}
				}
				else if (!_isShowDisplay)
				{
					_isShowDisplay = true;
					
					if (_armature!=null)
					{
						_armature._slotsZOrderChanged = true;
						addDisplayToContainer(_armature._display, -1);
					}
					
					updateChildArmatureAnimation();
				}
			}
		}


		public void updateChildArmatureAnimation()
		{
			if (_isShowDisplay)
			{
				playChildArmatureAnimation();
			}
			else
			{
				stopChildArmatureAnimation();
			}
		}

		
		public void playChildArmatureAnimation()
		{
			if (_childArmature!=null)
			{
				if (
					_armature!=null &&
					_armature._animation._lastAnimationState!=null &&
					_childArmature._animation.hasAnimation(_armature._animation._lastAnimationState.name)
					)
				{
					_childArmature._animation.gotoAndPlay(_armature._animation._lastAnimationState.name);
				}
				else
				{
					_childArmature._animation.play();
				}
			}
		}
		
		public void stopChildArmatureAnimation()
		{
			if (_childArmature!=null)
			{
				_childArmature._animation.stop();
				_childArmature._animation._lastAnimationState = null;
			}
		}


		public void updateSlotDisplay(bool disposeExisting)
		{
			int currentDisplayIndex = -1;
			
			if (_display!=null)
			{
				currentDisplayIndex = getDisplayZIndex();
				removeDisplayFromContainer();
			}
			
			if (disposeExisting)
			{
				if (_childArmature!=null)
				{
					_childArmature.dispose();
					//delete _childArmature;
					_childArmature = null;
				}
				else if (_display!=null)
				{
					disposeDisplay();
					_display = null;
				}
			}
			
			stopChildArmatureAnimation();
			
			Object display = _displayList[_displayIndex].Key;
			DragonBones.DisplayType displayType = _displayList[_displayIndex].Value;
			
			if (display!=null)
			{
				if (displayType == DragonBones.DisplayType.DT_ARMATURE)
				{
					_childArmature = display as Armature;
					_display = _childArmature._display;
				}
				else
				{
					_childArmature = null;
					_display = display;
				}
			}
			else
			{
				_display = null;
				_childArmature = null;
			}
			
			playChildArmatureAnimation();
			
			updateDisplay(_display);
			
			if (_display!=null)
			{
				if (_armature!=null && _isShowDisplay)
				{
					if (currentDisplayIndex < 0)
					{
						_armature._slotsZOrderChanged = true;
						addDisplayToContainer(_armature._display, currentDisplayIndex);
					}
					else
					{
						addDisplayToContainer(_armature._display, currentDisplayIndex);
					}
				}
				
				if (_blendMode != DragonBones.BlendMode.BM_NORMAL)
				{
					updateDisplayBlendMode(_blendMode);
				}
				else if (_slotData!=null)
				{
				    updateDisplayBlendMode(_slotData.blendMode);
				}
				
				updateDisplayColor(
					_colorTransform.AlphaOffset, _colorTransform.RedOffset, _colorTransform.GreenOffset, _colorTransform.BlueOffset,
					_colorTransform.AlphaMultiplier, _colorTransform.RedMultiplier, _colorTransform.GreenMultiplier, _colorTransform.BlueMultiplier
					);
                    
				updateDisplayVisible(_visible);
				updateDisplayTransform();
                
			}
            
		}

		public void updateDisplayColor(float aOffset, float rOffset, float gOffset, float bOffset, float aMultiplier, float rMultiplier, float gMultiplier, float bMultiplier)
		{
			_colorTransform.AlphaOffset = aOffset;
			_colorTransform.RedOffset = rOffset;
			_colorTransform.GreenOffset = gOffset;
			_colorTransform.BlueOffset = bOffset;
			_colorTransform.AlphaMultiplier = aMultiplier;
			_colorTransform.RedMultiplier = rMultiplier;
			_colorTransform.GreenMultiplier = gMultiplier;
			_colorTransform.BlueMultiplier = bMultiplier;
		}

		protected virtual int getDisplayZIndex()
		{
			return 0;
		}

		public virtual void addDisplayToContainer(Object container, int zIndex) 
		{

		}
		public virtual void removeDisplayFromContainer()
		{

		}
		protected virtual void disposeDisplay()
		{

		}
		protected virtual void disposeDisplayList()
		{

		}
		public virtual void updateDisplay(Object display)
		{
           
        }
		protected virtual void updateDisplayBlendMode(DragonBones.BlendMode blendMode)
		{

		}
		public virtual void updateDisplayVisible(bool visible)
		{

		}
		protected virtual void updateDisplayTransform()
		{
     
            ((UnityBoneDisplay)_display).Update(this.globalTransformMatrix);
            
        }

		public void dispose()
		{
			//Object::dispose();
			//
			_displayList.Clear();
			_slotData = null;
			_childArmature = null;
			_display = null;
		}

		}
}

