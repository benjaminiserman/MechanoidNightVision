namespace MechanoidNightVision
{
	using System.Reflection;
	using HarmonyLib;
	using RimWorld;
	using Verse;

	[HarmonyPatch]
	internal class Patch_StatPart_Glow
	{
		static MethodBase TargetMethod() => typeof(StatPart_Glow).GetMethod("ActiveFor", BindingFlags.NonPublic | BindingFlags.Instance);

		[HarmonyPrefix]
		static bool Prefix(ref bool __result, bool ___humanlikeOnly, Thing t)
		{
			if (t is Pawn pawn)
			{
				if (___humanlikeOnly && !pawn.RaceProps.Humanlike)
				{
					__result = false;
					return false;
				}

				if (pawn.RaceProps.IsMechanoid)
				{
					__result = false;
					return false;
				}
			}

			__result = t.Spawned;
			return false;
		}
	}
}
