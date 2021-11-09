using System;
using System.Numerics;

namespace csshack.UtilsNS
{
	internal static class Trigonometry
	{
		public static double GetAngleFromXY(float x, float y)
		{
			return Math.Atan2(y, x) * 180 / Math.PI;
		}
		public static double RationalizeAngle(double angle) => (angle % 360 + 360) % 360;
		//public static bool WorldToScreen(Vector3 vOrigin, Vector3 vScreen, float[] matrix)
		//{
		//	vScreen.X = (matrix[0] * vOrigin.X) + (matrix[1] * vOrigin.Y) + (matrix[2] * vOrigin.Z);
		//	vScreen.Y = (matrix[4] * vOrigin.X) + (matrix[5] * vOrigin.Y) + (matrix[6] * vOrigin.Z);
		//	vScreen.Z = (matrix[8] * vOrigin.X) + (matrix[9] * vOrigin.Y) + (matrix[10] * vOrigin.Z);
		//	if (vScreen.X >= -1 && vScreen.X <= 1)
		//	{
		//		vScreen.X = ((vScreen.X + 1) * (glwidth / 2));
		//		vScreen.Y = ((vScreen.Y + 1) * (glheight / 2));
		//		return true;
		//	}
		//	return false;
		//}
	}

}
