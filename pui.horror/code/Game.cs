using Sandbox;
using System.Linq;
using System.Threading.Tasks;

namespace Horror;

public partial class FpsBase : GameManager
{
	public FpsBase()
	{
		if ( Game.IsServer )
		{
			_ = new Hud();
		} else
		{
			new Help();
		}
	}

	public override void ClientJoined( IClient cl )
	{
		base.ClientJoined( cl );
		var player = new FpsPlayer( cl );
		player.Respawn();

		cl.Pawn = player;
	}

	protected override void OnDestroy()
	{
		base.OnDestroy();
	}

	[ClientRpc]
	public static void DisplayFlash()
	{
		Event.Run( "DisplayFlash" );
	}
}
