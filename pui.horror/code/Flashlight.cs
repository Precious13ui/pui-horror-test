using Sandbox;
using System;
using System.Linq;

namespace Horror;

public class Flashlight : BaseViewModel
{
	SpotLightEntity light;
	public bool isLight = false;

	public Flashlight() {
		light = new SpotLightEntity();
		light.Enabled = false;
	}

	public override void PlaceViewmodel()
	{
		base.PlaceViewmodel();

		Scale = 0.03f;
		
		if (Lib.isSuit)
		{
			if ( isLight )
			{
				light.Enabled = true;
			}
			else
			{
				light.Enabled = false;
			}
		}

		light.Position = Camera.Position;
		light.Rotation = Camera.Rotation;

		Position = Camera.Position;
		Rotation = Camera.Rotation;
		Camera.Main.SetViewModelCamera( 80f );
	}
}
