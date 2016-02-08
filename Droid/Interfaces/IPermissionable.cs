using System;

namespace TestLocation.Droid
{
	public interface IPermissionable
	{
		Action OnPermissionGranted { get; set; }
		Action OnPermissionDisabled{ get; set; }
	}
}

