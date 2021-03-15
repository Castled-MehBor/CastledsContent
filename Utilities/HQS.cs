using ReLogic.Reflection;
using System.Collections.Generic;

namespace CastledsContent.Utilities
{
	/// <summary>
	/// Harpy Queen Sprites
	/// </summary>

	public class HQS
	{
		public static readonly IdDictionary Search = IdDictionary.Create<HQS, short>();

		public const short Face = 0;
		public const short Fore = 1;
		public const short LegL = 2;
		public const short LegR = 3;
		public const short WingL = 4;
		public const short WingR = 5;
		public const short BodyL = 6;
		public const short BodyR = 7;
		public const short BodyM = 8;
		public const short BreastPlate_N = 9;
		public const short Hair_N = 10;
		public const short Leggings_N = 11;
		public const short Shade_N = 12;
		public const short Cape_E = 13;
		public const short Fleye1_E = 14;
		public const short Fleye2_E = 15;
		public const short Crown_E = 16;
		public const short Hair_E = 17;
		public const short Cloak1_E = 18;
		public const short CloakU_E = 19;
		public const short Cloak2_E = 20;
		//public const short EyeLid = 21;
		public const short BodyFore = 21;
		public const short LeggingsR_N = 22;
		public const short LeggingsL_N = 23;
	}
	/// <summary>
	/// Harpy Queen List.
	/// Used in conjunction with HQS
	/// </summary>
	public class HQL
	{
		public readonly static List<string> tex = new List<string>()
		{
			"Content/HarpyQueen/HQ_Head",
			"Content/HarpyQueen/HQ_ForeWing",
			"Content/HarpyQueen/HQ_LegLeft",
			"Content/HarpyQueen/HQ_LegRight",
			"Content/HarpyQueen/HQ_WingsLeft",
			"Content/HarpyQueen/HQ_WingsRight",
			"Content/HarpyQueen/HQ_BodyLeft",
			"Content/HarpyQueen/HQ_BodyRight",
			"Content/HarpyQueen/HQ_BodyMid",
			"Content/HarpyQueen/Normal/HQ_BreastPlate",
			"Content/HarpyQueen/Normal/HQ_Hair",
			"Content/HarpyQueen/Normal/HQ_Legging",
			"Content/HarpyQueen/Normal/HQ_Shade",
			"Content/HarpyQueen/Expert/HQ_Cape",
			"Content/HarpyQueen/Expert/HQ_Fleyes",
			"Content/HarpyQueen/Expert/HQ_Fleyes2",
			"Content/HarpyQueen/Expert/HQ_ReCrown",
			"Content/HarpyQueen/Expert/HQ_ReHair",
			"Content/HarpyQueen/Expert/HQ_SkywareCloak",
			"Content/HarpyQueen/Expert/HQ_SkywareCloak2",
			"Content/HarpyQueen/Expert/HQ_SkywareShatter",
			//"Content/HarpyQueen/HQ_EyeLid",
			"Content/HarpyQueen/HQ_BodyFore",
			"Content/HarpyQueen/Normal/HQ_LeggingR",
			"Content/HarpyQueen/Normal/HQ_LeggingL"
		};
	}
}