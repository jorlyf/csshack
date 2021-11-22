using System;
using System.Collections.Generic;
using System.Numerics;

using csshack.UtilsNS;
using csshack.StructsNS;
using csshack.MemoryNS;
using System.Drawing;

namespace csshack.FeaturesNS
{
	internal class ESP
	{
		private LocalPlayer LocalPlayer { get; }
		private List<Player> Players { get; }
		private float[] GetViewMatrix()
        {
			float[] matrix = new float[16];
			byte[] viewMatrixBuffer = new byte[64];
			uint nBytesRead = uint.MinValue;
			Kernel32API.ReadProcessMemory(Memory.ProcessHandle, (IntPtr)(Offsets.ViewMatrix), viewMatrixBuffer, (uint)viewMatrixBuffer.Length, ref nBytesRead);
			Buffer.BlockCopy(viewMatrixBuffer, 0, matrix, 0, 64);
			return matrix;
		}
		public List<Rectangle> GetESPBoxes()
        {
			float[] viewmatrix = GetViewMatrix();
			List<Rectangle> espboxes = new List<Rectangle>();
			Players.ForEach(player =>
			{
				if (player.Team != LocalPlayer.Team && player.Team != 1 && player.Health > 1)
				{
					Vector2 screen = new Vector2();
					if (WorldToScreen(player.Position, ref screen, viewmatrix))
						espboxes.Add(new Rectangle((int)screen.X - 20, (int)screen.Y - 60, 40, 60));
				}
			});
			return espboxes;
		}
		public Color EnemyColor { get; } = Color.DarkRed;
		public ESP(LocalPlayer localPlayer, List<Player> players)
		{
			LocalPlayer = localPlayer;
			Players = players;
		}
		private bool IsEnemyInFOV(Vector3 enemyPos, out double angle)
		{
			float xDistance = enemyPos.X - LocalPlayer.Position.X;
			float yDistance = enemyPos.Y - LocalPlayer.Position.Y;

			angle = Trigonometry.GetAngleFromXY(xDistance, yDistance);
			angle = Trigonometry.RationalizeAngle(angle);

			bool isVisible;
			if (LocalPlayer.Fov.XLeft > 360)
			{
				double left = Trigonometry.RationalizeAngle(LocalPlayer.Fov.XLeft);
				isVisible = ((angle < left) && (angle > 0)) || ((angle < 360) && (angle > LocalPlayer.Fov.XRight));
			}
			else if (LocalPlayer.Fov.XRight < 0)
			{
				double right = Trigonometry.RationalizeAngle(LocalPlayer.Fov.XRight);
				isVisible = ((angle < LocalPlayer.Fov.XLeft) && (angle > 0)) || ((angle < 360) && (angle > right));
			}
			else
			{
				isVisible = angle < LocalPlayer.Fov.XLeft && angle > LocalPlayer.Fov.XRight;
			}

			return isVisible;
		}
		private bool WorldToScreen(Vector3 pos, ref Vector2 screen, float[] ViewMatrix)
		{
			Vector4 clipCoords;
			clipCoords.X = pos.X * ViewMatrix[0] + pos.Y * ViewMatrix[1] + pos.Z * ViewMatrix[2] + ViewMatrix[3];
			clipCoords.Y = pos.X * ViewMatrix[4] + pos.Y * ViewMatrix[5] + pos.Z * ViewMatrix[6] + ViewMatrix[7];
			clipCoords.Z = pos.X * ViewMatrix[8] + pos.Y * ViewMatrix[9] + pos.Z * ViewMatrix[10] + ViewMatrix[11];
			clipCoords.W = pos.X * ViewMatrix[12] + pos.Y * ViewMatrix[13] + pos.Z * ViewMatrix[14] + ViewMatrix[15];

			if (clipCoords.W < 0.1f)
			{
				return false;
			}

			Vector3 NDC;
			NDC.X = clipCoords.X / clipCoords.W;
			NDC.Y = clipCoords.Y / clipCoords.W;
			NDC.Z = clipCoords.Z / clipCoords.W;

			screen.X = (1920 / 2 * NDC.X) + (NDC.X + 1920 / 2);
			screen.Y = -(1080 / 2 * NDC.Y) + (NDC.Y + 1080 / 2);

			return true;
		}
	}
}
