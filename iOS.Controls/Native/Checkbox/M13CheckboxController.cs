using System;
using CoreAnimation;
using Messier16.Forms.iOS.Controls.Native.Checkbox.Paths;
using UIKit;

namespace Messier16.Forms.iOS.Controls.Native.Checkbox
{
	public  class M13CheckboxController
	{

		//----------------------------
		// MARK: - Properties
		//----------------------------

		/// The path presets for the manager.
		public M13CheckboxPathGenerator PathGenerator { get; set; } = new M13CheckboxCheckPathGenerator();

		/// The animation presets for the manager.
		//M13CheckboxAnimationGenerator animationGenerator = new M13CheckboxAnimationGenerator();

		/// The current state of the checkbox.
		public CheckState State { get; set; } =  CheckState.Unchecked;

		/// The current tint color.
		/// - Note: Subclasses should override didSet to update the layers when this value changes.
		public UIColor TintColor { get; set; } = UIColor.Black;

		/// The secondary tint color.
		/// - Note: Subclasses should override didSet to update the layers when this value changes.
		public UIColor SecondaryTintColor { get; set; } =  UIColor.LightGray;

		/// The secondary color of the mark.
		/// - Note: Subclasses should override didSet to update the layers when this value changes.
		public UIColor SecondaryCheckmarkTintColor { get; set; } =  UIColor.White;

		/// Whether or not to hide the box.
		/// - Note: Subclasses should override didSet to update the layers when this value changes.
		public bool HideBox { get; set; } = false;

		/// Whether or not to allow morphong between states.
		public bool EnableMorphing { get; set; } =  true;

		// The type of mark to display.
		MarkType _markType;
		public MarkType MarkType
		{
			set
			{
				if (_markType == value)
					return;
				SetMarkType(_markType, false);
			}
		}

		private void SetMarkType(MarkType type, bool animated)
		{
			M13CheckboxPathGenerator newPathGenerator = null;

			if (type != _markType)
			{
				switch (type)
				{
					case MarkType.Checkmark:
						newPathGenerator = new M13CheckboxCheckPathGenerator();

						break;
					//		case MarkType.Radio:
					//			newPathGenerator = M13CheckboxRadioPathGenerator();

					//		break;
					//case MarkType.AddRemove:
					//			newPathGenerator = M13CheckboxAddRemovePathGenerator();

					//		break;
					//case MarkType.Disclosure:
					//			newPathGenerator = M13CheckboxDisclosurePathGenerator();
					//		break;
					default:
						throw new Exception($"No path generator defined for {type}");

				}
			}

			newPathGenerator.BoxLineWidth = PathGenerator.BoxLineWidth;
			newPathGenerator.BoxType = PathGenerator.BoxType;
			newPathGenerator.CheckmarkLineWidth = PathGenerator.CheckmarkLineWidth;
			newPathGenerator.CornerRadius = PathGenerator.CornerRadius;
			newPathGenerator.Size = PathGenerator.Size;

			// Animate the change.
			if (PathGenerator.PathForMark(State) != null && animated)
			{
				var previousState = State;
				Action completion = () =>
				{
					this.PathGenerator = newPathGenerator;
					this.resetLayersForState(previousState);
					if (this.PathGenerator.PathForMark(previousState) != null)
					{
						Animate(null, previousState);
					}
				};
				Animate(State, null, completion);
			}
			else if (newPathGenerator.PathForMark(State) != null && animated)
			{
				var previousState = State;

				this.PathGenerator = newPathGenerator;

				resetLayersForState(null);

				Animate(null, toState: previousState);
			}
			else {
				this.PathGenerator = newPathGenerator;

				resetLayersForState(State);
			}

			MarkType = type;
		}

		//----------------------------
		// MARK: - Layers
		//----------------------------

		/// The layers to display in the checkbox. The top layer is the last layer in the array.
		public CALayer[] LayersToDisplay
		{
			get
			{
				return new CALayer[0];
			}
		}

		//----------------------------
		// MARK: - Animations
		//----------------------------

		/**
		 Animates the layers between the two states.
		 - parameter fromState: The previous state of the checkbox.
		 - parameter toState: The new state of the checkbox.
		 */
		void Animate(CheckState? fromState, CheckState? toState, Action completion = null)
		{
			if (toState.HasValue)
			{
				State = toState.Value;
			}
		}

		//----------------------------
		// MARK: - Layout
		//----------------------------

		/// Layout the layers.
		void layoutLayers()
		{

		}

		//----------------------------
		// MARK: - Display
		//----------------------------

		/**
		 Reset the layers to be in the given state.
		 - parameter state: The new state of the checkbox.
		 */
		void resetLayersForState(CheckState? state)
		{
			if (state.HasValue)
			{
				this.State = state.Value;

			}
			layoutLayers();

		}

	}
}