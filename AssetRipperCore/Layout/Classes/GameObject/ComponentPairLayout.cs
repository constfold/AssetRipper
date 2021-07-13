﻿using AssetRipper.Classes.GameObjects;
using AssetRipper.Converters;

namespace AssetRipper.Layout.GameObjects
{
	/// <summary>
	/// 5.5.0 - first introduction
	/// </summary>
	public sealed class ComponentPairLayout
	{
		public ComponentPairLayout(LayoutInfo info)
		{
		}

		public static void GenerateTypeTree(TypeTreeContext context, string name)
		{
			ComponentPairLayout layout = context.Layout.GameObject.ComponentPair;
			context.AddNode(layout.Name, name);
			context.BeginChildren();
			context.AddPPtr(context.Layout.Component.Name, layout.ComponentName);
			context.EndChildren();
		}

		/// <summary>
		/// All versions
		/// </summary>
		public bool HasComponent => true;

		public string Name => nameof(ComponentPair);
		public string ComponentName => "component";
	}
}