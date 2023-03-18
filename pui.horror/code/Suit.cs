using Editor;
using Sandbox;
using Sandbox.UI;
using System;
using System.Linq;

namespace Horror;

/// <summary>
/// Basically like the HEV suit from Half-life.
/// </summary>
[HammerEntity]
[EditorModel( "vmdls/suit_debug.vmdl" )]
public partial class Suit : ModelEntity
{
	public override void Spawn()
	{
		base.Spawn();

		SetModel( "vmdls/suit_debug.vmdl" );
		SetupPhysicsFromModel( PhysicsMotionType.Static );
		Tags.Add( "trigger" );
	}

	public override void Touch( Entity other )
	{
		base.Touch( other );

		if (!Lib.isSuit)
		{
			if ( Game.IsServer )
			{
				
				Delete();
			}
			else
			{
				Sound.FromEntity( "sfx/power_up.sound", this );
			}
			Lib.isSuit = true;
		}
	}
}
