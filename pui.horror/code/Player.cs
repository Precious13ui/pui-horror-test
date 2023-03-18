using Sandbox;

namespace Horror;

partial class FpsPlayer : Player
{
	private TimeSince timeSinceDropped;
	private TimeSince timeSinceJumpReleased;

	Flashlight flashlight { get; set; }

	[Net, Predicted]
	public bool ThirdPersonCamera { get; set; }
	
	public FpsPlayer()
	{
	}

	public FpsPlayer( IClient cl ) : this()
	{
	}

	public override void ClientSpawn()
	{
		base.ClientSpawn();

		flashlight = new Flashlight
		{
			Owner = Owner,
		};
	}

	public override void Respawn()
	{
		Controller = new WalkController();

		this.ClearWaterLevel();
		EnableAllCollisions = true;
		EnableDrawing = true;
		EnableHideInFirstPerson = true;
		EnableShadowInFirstPerson = true;

		base.Respawn();
	}


	public override PawnController GetActiveController()
	{
		if ( DevController != null ) return DevController;

		return base.GetActiveController();
	}

	public override void Simulate( IClient cl )
	{
		base.Simulate( cl );

		if ( Input.Pressed( InputButton.Flashlight ) )
		{
			if ( flashlight != null )
			{
				flashlight.isLight = !flashlight.isLight;
			}
		}

		SimulateActiveChild( cl, ActiveChild );

		if ( Input.Released( InputButton.Jump ) )
		{
			timeSinceJumpReleased = 0;
		}

		if ( InputDirection.y != 0 || InputDirection.x != 0f )
		{
			timeSinceJumpReleased = 1;
		}

		FpsBase.DisplayFlash( To.Everyone );
	}

	public override void StartTouch( Entity other )
	{
		if ( timeSinceDropped < 1 ) return;

		base.StartTouch( other );
	}

	public override float FootstepVolume()
	{
		return Velocity.WithZ( 0 ).Length.LerpInverse( 0.0f, 200.0f ) * 5.0f;
	}

	public override void FrameSimulate( IClient cl )
	{
		Camera.Rotation = ViewAngles.ToRotation();
		Camera.FieldOfView = Screen.CreateVerticalFieldOfView( Game.Preferences.FieldOfView );
		Camera.Position = EyePosition;
		Camera.FirstPersonViewer = this;
	}
}
