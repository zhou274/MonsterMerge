using System;
using System.Collections.Generic;

internal static class Constants
{
	public const string firebaseDefaultKey1 = "unity_showbanner";

	public const string firebaseDefaultValue1 = "1";

	public const string firebaseDefaultKey2 = "unity_showinterstitial_frequency";

	public const string firebaseDefaultValue2 = "2";

	public const string admobAppID = "ca-app-pub-8969722984181378/2765549449";

	public const string admobInterstitialID = "ca-app-pub-8969722984181378/4242282645";

	public const string leaderboardID = "furthest_distance";

	public static Dictionary<string, object> firebaseDefaults = new Dictionary<string, object>
	{
		{
			"unity_showbanner",
			"1"
		},
		{
			"unity_showinterstitial_frequency",
			"2"
		}
	};
}
