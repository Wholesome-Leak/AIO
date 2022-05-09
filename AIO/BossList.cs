﻿using System;
using System.Collections.Generic;
using AIO;

// Token: 0x0200000D RID: 13
public class BossList
{
	// Token: 0x17000004 RID: 4
	// (get) Token: 0x06000040 RID: 64 RVA: 0x0000380C File Offset: 0x00001A0C
	public static bool isboss
	{
		get
		{
			return BossList.BossListInt.Contains(Constants.Target.Entry);
		}
	}

	// Token: 0x04000027 RID: 39
	public static readonly HashSet<int> BossListInt = new HashSet<int>
	{
		11517,
		11520,
		11518,
		11519,
		17830,
		644,
		3586,
		643,
		642,
		1763,
		646,
		645,
		647,
		639,
		596,
		626,
		599,
		47162,
		47296,
		43778,
		47626,
		47739,
		49541,
		5775,
		3670,
		3673,
		3669,
		3654,
		3674,
		3653,
		3671,
		5912,
		3672,
		3655,
		3652,
		3914,
		3886,
		4279,
		3887,
		4278,
		4274,
		3927,
		14682,
		4275,
		3872,
		46962,
		46963,
		46964,
		4887,
		4831,
		12902,
		6243,
		12876,
		4830,
		4832,
		4829,
		1716,
		1663,
		1717,
		1666,
		1696,
		1720,
		4421,
		4420,
		4422,
		4428,
		4424,
		6168,
		4425,
		4842,
		7800,
		7079,
		7361,
		6235,
		6229,
		6228,
		6231,
		3983,
		6488,
		6490,
		6489,
		14693,
		4543,
		23682,
		23800,
		3974,
		6487,
		3975,
		4542,
		3976,
		3977,
		7355,
		14686,
		7356,
		7357,
		8567,
		7354,
		7358,
		7057,
		6910,
		7228,
		7023,
		7206,
		7291,
		4854,
		2748,
		6906,
		10082,
		10080,
		7272,
		8127,
		7271,
		7274,
		7275,
		7796,
		7797,
		7267,
		10081,
		7795,
		7273,
		7608,
		7606,
		7604,
		13742,
		13741,
		13740,
		13739,
		12236,
		13738,
		13282,
		12258,
		12237,
		12225,
		12203,
		13601,
		13596,
		12201,
		1063,
		5400,
		5713,
		5715,
		5714,
		5717,
		5712,
		5716,
		5399,
		5401,
		8580,
		8443,
		5711,
		5710,
		5721,
		5720,
		5719,
		5722,
		5709,
		9018,
		9025,
		9319,
		9031,
		9029,
		9027,
		9028,
		9032,
		9030,
		16059,
		9024,
		9041,
		9042,
		9476,
		9056,
		9017,
		9016,
		9033,
		8983,
		9543,
		9537,
		9502,
		9499,
		23872,
		9156,
		8923,
		17808,
		9039,
		9040,
		9037,
		9034,
		9038,
		9036,
		9938,
		10076,
		8929,
		9019,
		11447,
		11498,
		11497,
		14354,
		14327,
		14349,
		13280,
		11490,
		11492,
		16097,
		14326,
		14322,
		14321,
		14323,
		14325,
		14324,
		11501,
		11489,
		11487,
		11467,
		11488,
		14690,
		11496,
		14506,
		11486,
		10263,
		9218,
		9219,
		9217,
		9196,
		9236,
		9237,
		16080,
		9596,
		10596,
		10376,
		10584,
		9736,
		10220,
		10268,
		9718,
		9568,
		10393,
		14684,
		11058,
		10558,
		10516,
		16387,
		11143,
		10808,
		11032,
		11120,
		10997,
		10811,
		10813,
		16101,
		16102,
		10809,
		10437,
		10436,
		11121,
		10438,
		10435,
		10439,
		10440,
		17913,
		17911,
		17910,
		17914,
		17912,
		14861,
		10506,
		14695,
		10503,
		11622,
		14516,
		10433,
		10432,
		16118,
		10508,
		10505,
		11261,
		10901,
		10507,
		10504,
		10502,
		1853,
		9816,
		10264,
		10509,
		10899,
		10339,
		10429,
		10430,
		16042,
		10363,
		14517,
		14507,
		14510,
		11382,
		15114,
		14509,
		14515,
		11380,
		14834,
		15082,
		15083,
		15084,
		15085,
		10184,
		12118,
		11982,
		12259,
		12057,
		12056,
		12264,
		12098,
		11988,
		12018,
		11502,
		12435,
		13020,
		12017,
		11983,
		14601,
		11981,
		14020,
		11583,
		12557,
		10162,
		15348,
		15341,
		15340,
		15370,
		15369,
		15339,
		15263,
		15511,
		15543,
		15544,
		15516,
		15510,
		15299,
		15509,
		15276,
		15275,
		15517,
		15727,
		15589,
		30549,
		16803,
		15930,
		15929,
		15956,
		15953,
		15952,
		16028,
		15931,
		15932,
		15928,
		15954,
		15936,
		16011,
		16061,
		16060,
		16065,
		16064,
		16062,
		16063,
		15989,
		15990,
		25465,
		17306,
		17308,
		17537,
		17307,
		17536,
		17381,
		17380,
		17377,
		25740,
		17941,
		17991,
		17942,
		17770,
		18105,
		17826,
		17827,
		17882,
		18341,
		18343,
		22930,
		18344,
		18371,
		18373,
		17848,
		17862,
		18096,
		28132,
		18472,
		23035,
		18473,
		17797,
		17796,
		17798,
		18731,
		18667,
		18732,
		18708,
		16807,
		20923,
		16809,
		16808,
		17879,
		17880,
		17881,
		19218,
		19710,
		19219,
		19221,
		19220,
		17976,
		17975,
		17978,
		17980,
		17977,
		20870,
		20886,
		20885,
		20912,
		20904,
		24723,
		24744,
		24560,
		24664,
		15550,
		16151,
		28194,
		15687,
		16457,
		15691,
		15688,
		16524,
		15689,
		15690,
		17225,
		17229,
		16179,
		16181,
		16180,
		17535,
		17546,
		17543,
		17547,
		17548,
		18168,
		17521,
		17533,
		17534,
		18831,
		19044,
		18835,
		18836,
		18834,
		18832,
		17257,
		29024,
		28514,
		23576,
		23574,
		23578,
		28515,
		29023,
		23577,
		28517,
		29022,
		24239,
		24239,
		23863,
		21216,
		21217,
		21215,
		21214,
		21213,
		21212,
		21875,
		19514,
		19516,
		18805,
		19622,
		20064,
		20060,
		20062,
		20063,
		21270,
		21269,
		21271,
		21268,
		21273,
		21274,
		21272,
		17767,
		17808,
		17888,
		17842,
		17968,
		22887,
		22898,
		22841,
		22871,
		22948,
		23420,
		23419,
		23418,
		22947,
		23426,
		22917,
		22949,
		22950,
		22951,
		22952,
		24891,
		25319,
		24850,
		24882,
		25038,
		25165,
		25166,
		25741,
		25315,
		25840,
		24892,
		23953,
		27390,
		24200,
		23954,
		23980,
		27389,
		24201,
		26798,
		26796,
		26731,
		26832,
		26928,
		26929,
		26930,
		26763,
		26794,
		26723,
		28684,
		28921,
		29120,
		29309,
		29308,
		29310,
		29311,
		30258,
		26630,
		26631,
		27483,
		26632,
		27696,
		29315,
		29313,
		29312,
		29316,
		29266,
		29314,
		31134,
		29304,
		29305,
		29307,
		29306,
		29932,
		27977,
		27975,
		28234,
		27978,
		28586,
		28587,
		28546,
		28923,
		27654,
		27447,
		27655,
		27656,
		26529,
		26530,
		26532,
		32273,
		26533,
		29620,
		26668,
		26687,
		26693,
		26861,
		35617,
		35569,
		35572,
		35571,
		35570,
		34702,
		34701,
		34705,
		34657,
		34703,
		34928,
		35119,
		35451,
		36497,
		36502,
		36494,
		36477,
		36476,
		36658,
		38112,
		38113,
		37226,
		38113,
		30451,
		30452,
		30449,
		28860,
		31125,
		33993,
		35013,
		38433,
		28859,
		33113,
		33118,
		33186,
		33293,
		33670,
		33329,
		33651,
		32867,
		32927,
		32857,
		32930,
		33515,
		34035,
		32933,
		32934,
		33524,
		33350,
		32906,
		32865,
		32845,
		33271,
		33890,
		33136,
		33288,
		32915,
		32913,
		32914,
		32882,
		33432,
		34014,
		32871,
		34796,
		35144,
		34799,
		34797,
		34780,
		34461,
		34460,
		34469,
		34467,
		34468,
		34465,
		34471,
		34466,
		34473,
		34472,
		34470,
		34463,
		34474,
		34475,
		34458,
		34451,
		34459,
		34448,
		34449,
		34445,
		34456,
		34447,
		34441,
		34454,
		34444,
		34455,
		34450,
		34453,
		35610,
		35465,
		34497,
		34496,
		34564,
		36612,
		36855,
		37813,
		36626,
		36627,
		36678,
		37972,
		37970,
		37973,
		37955,
		36789,
		37950,
		37868,
		36791,
		37934,
		37886,
		37985,
		36853,
		36597,
		37217,
		37025,
		36661,
		39746,
		39747,
		39751,
		39863,
		39899,
		40142,
		39665,
		39679,
		39698,
		39700,
		39705,
		40586,
		40765,
		40825,
		40788,
		42172,
		43438,
		43214,
		42188,
		42333,
		43878,
		43873,
		43875,
		39625,
		40177,
		40319,
		40484,
		39425,
		39428,
		39788,
		39587,
		39731,
		39732,
		39378,
		44577,
		43612,
		43614,
		49045,
		44819,
		47120,
		52363,
		41570,
		42180,
		41378,
		41442,
		43296,
		41376,
		45871,
		46753,
		45992,
		45993,
		44600,
		43735,
		43324,
		45213,
		14889,
		14888,
		14890,
		14887,
		14464,
		6109,
		14461,
		15205,
		15204,
		15305,
		15203,
		14454,
		9026,
		14457,
		18728,
		12397,
		17711,
		18398,
		18069,
		18399,
		18400,
		18401,
		18402,
		52155,
		52151,
		52271,
		52059,
		52053,
		52148,
		53691,
		52558,
		52498,
		52530,
		53494,
		52571,
		52409,
		54590,
		54968,
		54938,
		54431,
		54445,
		54123,
		54544,
		54432,
		55085,
		54853,
		54969,
		55419,
		55265,
		57773,
		55308,
		55312,
		55689,
		55294,
		56427,
		56781,
		56846,
		56167,
		56168,
		57962,
		56575,
		56341,
		56710,
		56262,
		56471,
		56448,
		58826,
		59051,
		59726,
		56732,
		56762,
		56439,
		56637,
		56717,
		59479,
		56747,
		56754,
		56541,
		56719,
		56884,
		56906,
		56589,
		56636,
		61177,
		56877,
		56895,
		61442,
		61444,
		61445,
		61243,
		61337,
		61338,
		61339,
		61340,
		61398,
		61567,
		61634,
		61485,
		62205,
		59303,
		58632,
		59150,
		59789,
		59223,
		3977,
		60040,
		58633,
		59184,
		59153,
		58722,
		59080,
		56448,
		58826,
		59051,
		59726,
		56732,
		56762,
		56439,
		56637,
		56717,
		59479,
		56747,
		56754,
		56541,
		56719,
		56884,
		56906,
		56589,
		56636,
		61177,
		56877,
		56895,
		61442,
		61444,
		61445,
		61243,
		61337,
		61338,
		61339,
		61340,
		61398,
		61567,
		61634,
		61485,
		62205,
		59303,
		58632,
		59150,
		59789,
		59223,
		3977,
		60040,
		59191,
		59303,
		58633,
		59184,
		59153,
		58722,
		59080,
		59359,
		59193,
		60047,
		60051,
		60043,
		59915,
		60009,
		60143,
		60708,
		60701,
		60709,
		60710,
		60410,
		60400,
		60399,
		62980,
		62543,
		62164,
		62397,
		62511,
		62837,
		60583,
		60586,
		60585,
		62442,
		62983,
		60999,
		60491,
		62346,
		69099,
		69161,
		69465,
		68476,
		69131,
		69132,
		69134,
		69078,
		67977,
		70212,
		70235,
		70247,
		69712,
		68036,
		69017,
		69427,
		68078,
		68905,
		68904,
		68397,
		69473,
		58739,
		71259,
		59632,
		67391,
		71330,
		70963,
		71328,
		71326,
		70762,
		70893,
		71329,
		67426,
		71327,
		70468,
		70474,
		70544,
		70787,
		61707,
		61814,
		67081,
		71492,
		71123,
		70822,
		70683,
		71030,
		71543,
		71475,
		71479,
		71480,
		72276,
		71734,
		72249,
		71466,
		71859,
		71858,
		71515,
		71454,
		71529,
		71161,
		71157,
		71156,
		71155,
		71160,
		71154,
		71152,
		71158,
		71153,
		71504,
		71865
	};
}