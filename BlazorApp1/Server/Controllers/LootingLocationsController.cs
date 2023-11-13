using Microsoft.AspNetCore.Mvc;
using BlazorApp1.Shared;
using BlazorApp1.Shared.Enums;

namespace BlazorApp1.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LootingLocationsController : ControllerBase
{
    private readonly ILogger<LootingLocationsController> _logger;

    public LootingLocationsController(ILogger<LootingLocationsController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public IEnumerable<LootingLocation> Get()
    {
        var lootingLocations = new List<LootingLocation>();

        #region deadCities
        
        lootingLocations.Add(new LootingLocation(6342, 4432, "raheem"));
        lootingLocations.Add(new LootingLocation(6452, 4440, "ApeBZLWW69"));
        lootingLocations.Add(new LootingLocation(6474, 4409, "CRYzeidCRY"));
        lootingLocations.Add(new LootingLocation(6374, 4345, "[TOP]Ape3DNJM9P"));
        lootingLocations.Add(new LootingLocation(6357, 4323, "Ape972W539"));
        lootingLocations.Add(new LootingLocation(6378, 4301, "[UBVH]ApeZ9WJBXJ"));
        lootingLocations.Add(new LootingLocation(6368, 4331, "Ape6WA7LP3"));
        lootingLocations.Add(new LootingLocation(6312, 4266, "ThenooK"));
        lootingLocations.Add(new LootingLocation(6252, 4304, "HUGOVG97"));
        lootingLocations.Add(new LootingLocation(6270, 4246, "Ape4NYLM8W"));
        lootingLocations.Add(new LootingLocation(6248, 4215, "BOOSIA"));
        lootingLocations.Add(new LootingLocation(6299, 4127, "[NWO]DonkeyTropical"));
        lootingLocations.Add(new LootingLocation(6269, 4143, "[NWO]ApePNLVMD3"));
        lootingLocations.Add(new LootingLocation(6298, 4144, "[NWO]Lionh3artTM"));
        lootingLocations.Add(new LootingLocation(6298, 4144, "[NWO]Lionh3artTM"));
        lootingLocations.Add(new LootingLocation(6218, 4129, "[wolf]ApeG567J5J"));
        lootingLocations.Add(new LootingLocation(6139, 4096, "ApeDNPMJ59"));
        lootingLocations.Add(new LootingLocation(5950, 4077, "[QN3]Ape2L24572"));
        lootingLocations.Add(new LootingLocation(5966, 4071, "Gregor"));
        lootingLocations.Add(new LootingLocation(6001, 4056, "xakutara"));
        lootingLocations.Add(new LootingLocation(6084, 4011, "Captin"));
        lootingLocations.Add(new LootingLocation(6023, 4086, "Ape6WVPB32"));
        lootingLocations.Add(new LootingLocation(6062, 4010, "ApeXNZ3VXZ"));
        lootingLocations.Add(new LootingLocation(6048, 3995, "Ape5YJD7WG"));
        lootingLocations.Add(new LootingLocation(6013, 3995, "ZX5KawasZX5ZX5"));
        lootingLocations.Add(new LootingLocation(6082, 3968, "ApeX"));
        lootingLocations.Add(new LootingLocation(6131, 3962, "[SVP]ApeX"));
        lootingLocations.Add(new LootingLocation(6105, 3964, "ApeX"));
        lootingLocations.Add(new LootingLocation(6135, 3949, "ApeX"));
        lootingLocations.Add(new LootingLocation(6159, 3955, "darianyigarcia"));
        lootingLocations.Add(new LootingLocation(6171, 3944, "ApeX"));
        lootingLocations.Add(new LootingLocation(6246, 3905, "[fun]ApeX"));
        lootingLocations.Add(new LootingLocation(6237, 3931, "[fun]ApeX"));
        lootingLocations.Add(new LootingLocation(6231, 3910, "ApeX"));
        lootingLocations.Add(new LootingLocation(6219, 3922, "[fun]TemA"));
        lootingLocations.Add(new LootingLocation(6214 ,3906, "[fun]VankekX"));
        lootingLocations.Add(new LootingLocation(6194, 3863, "[fun]ApeX"));
        lootingLocations.Add(new LootingLocation(6200, 3890, "[fun]ApeX"));
        lootingLocations.Add(new LootingLocation(6173, 3882, "Dadha"));
        lootingLocations.Add(new LootingLocation(6194, 3863, "[fun]ApeX"));
        lootingLocations.Add(new LootingLocation(6174, 3844, "[Osir]001BrunTOP"));
        lootingLocations.Add(new LootingLocation(1632, 943, "ApeZ9NX3XA"));
        lootingLocations.Add(new LootingLocation(1636, 870, "Amorphous"));
        lootingLocations.Add(new LootingLocation(1632, 943, "ApeZ9NX3XA"));
        lootingLocations.Add(new LootingLocation(1850, 740, "ApeL3GPXLY"));

        #endregion
        
        #region lowValue
        
        lootingLocations.Add(new LootingLocation(7023,3183,1472611, 0,0,"UBVH","Symbols",new DateTime(2023, 10, 21)));
        
        #endregion
        
        #region highValue
        
        lootingLocations.Add(new LootingLocation(6712,4487,32637749, 29758854,197793,"","bapee",new DateTime(2023, 10, 22)));
        lootingLocations.Add(new LootingLocation(6693,4321,32368823, 23066690,50177,"SVP","BHR15",new DateTime(2023, 10, 22)));
        lootingLocations.Add(new LootingLocation(6697,4277,14246767, 2295857,0,"505","AEW",new DateTime(2023, 10, 22)));
        lootingLocations.Add(new LootingLocation(6786,4169,31731981, 41473713,198002,"BFME","Monkeyleotop",new DateTime(2023, 10, 22)));
        lootingLocations.Add(new LootingLocation(6858,4087,13310466, 8117789,51922,"BHC","Ape8PJW42P",new DateTime(2023, 10, 22)));
        lootingLocations.Add(new LootingLocation(6740,4288,31926308, 16893737,49124,"505","Ahell",new DateTime(2023, 10, 22)));
        
        #endregion
        
        #region twuArea
        
        lootingLocations.Add(new LootingLocation(2101,1079,7497677, 5556605,157139,"TWU","wow34rus",new DateTime(2023, 10, 22)));
        lootingLocations.Add(new LootingLocation(2039,1121,27506674, 4332506,226682,"TWU","Bubuy378",new DateTime(2023, 10, 22)));
        lootingLocations.Add(new LootingLocation(2091,1097,2975626, 2510185,134027,"TWU","NOCCE",new DateTime(2023, 10, 22)));
        lootingLocations.Add(new LootingLocation(2106,1101,5128503, 4952398,115500,"TWU","OrangUtan",new DateTime(2023, 10, 22)));
        lootingLocations.Add(new LootingLocation(2075,1084,761448, 0,220047,"TWU","Andryuxxxa",new DateTime(2023, 10, 22)));
        lootingLocations.Add(new LootingLocation(2125,1095,6300210, 7742746,260491,"TWU","hpak123",new DateTime(2023, 10, 22)));
        lootingLocations.Add(new LootingLocation(2153,1133,3674209, 2525650,212181,"TWU","XxXrohimXxX",new DateTime(2023, 10, 22)));
        lootingLocations.Add(new LootingLocation(289026,1121,0, 0,289026,"TWU","Alan92",new DateTime(2023, 10, 22)));
        lootingLocations.Add(new LootingLocation(2172,1108,567430, 1298949,337560,"TWU","Bobdem",new DateTime(2023, 10, 22)));
        lootingLocations.Add(new LootingLocation(2185,1160,1267991, 1345019,186338,"TWU","kingjj",new DateTime(2023, 10, 22)));
        lootingLocations.Add(new LootingLocation(2153,1088,0, 0,87130,"TWU","BigRed",new DateTime(2023, 10, 22)));
        lootingLocations.Add(new LootingLocation(2211,1090,0, 0,184574,"TWU","9of11",new DateTime(2023, 10, 22)));
        lootingLocations.Add(new LootingLocation(2302,1022,2036203, 6536545,137646,"TWU","Grind",new DateTime(2023, 10, 22)));
        lootingLocations.Add(new LootingLocation(2178,1081,868711, 813152,225326,"TWU","Robdizzle562",new DateTime(2023, 10, 22)));
        lootingLocations.Add(new LootingLocation(2190,1094,540637, 827118,48100,"TWU","Shiin",new DateTime(2023, 10, 22)));
        lootingLocations.Add(new LootingLocation(2186,1117,51440347, 16856728,294798,"TWU","Haranbanjo",new DateTime(2023, 10, 22)));
        lootingLocations.Add(new LootingLocation(2212,1138,ResourceLevel.Low,95027,"TWU","himmm",new DateTime(2023, 10, 22)));

        
        lootingLocations.Add(new LootingLocation(1978,1182,5129997, 4505707,24184,"RUST","Zex",new DateTime(2023, 10, 22)));
        lootingLocations.Add(new LootingLocation(2082,1198,7274235, 4367856,70790,"RUST","AlexL",new DateTime(2023, 10, 22)));

        lootingLocations.Add(new LootingLocation(2039,1043,"ApeX"));
        lootingLocations.Add(new LootingLocation(1978,1218,"ApeX"));
        lootingLocations.Add(new LootingLocation(2039,1043,"XxXTriniVibeXxX"));
        lootingLocations.Add(new LootingLocation(2211,1331,"001TipokTOP"));
        lootingLocations.Add(new LootingLocation(2055,1145,ResourceLevel.Low,3765,"","ApeX",new DateTime(2023, 10, 22)));


        lootingLocations.Add(new LootingLocation(1194,1188,ResourceLevel.Low,0,"","Majiro",new DateTime(2023, 10, 22)));
        lootingLocations.Add(new LootingLocation(1947,1181,ResourceLevel.Medium,3955,"","ApeX",new DateTime(2023, 10, 22)));

        lootingLocations.Add(new LootingLocation(2588 ,1105,282096, 167148,3983,"TWU","ApeG59AB93",new DateTime(2023, 10, 22)));
        lootingLocations.Add(new LootingLocation(6852 ,4299,42529483, 44094417,36100,"RUST","Allawi1997",new DateTime(2023, 10, 22)));
        lootingLocations.Add(new LootingLocation(6806 ,4336,126348, 160541,29821,"NWO","Iordkrathus",new DateTime(2023, 10, 22)));
        lootingLocations.Add(new LootingLocation(6643 ,4531,7124677, 5402290,33555,"THE","VALLECO",new DateTime(2023, 10, 22)));
        lootingLocations.Add(new LootingLocation(6700 ,4508,8560859, 8127505,758,"","GrEsHnlk",new DateTime(2023, 10, 22)));

        
        #endregion

        #region oct22

        // TODO: Put this into a CSV
        var screenscrapeData = @"6697,4277,14246767,2295857,80,505,AEW
6740,4288,31926308,16893737,849124,505,Ahell
6771,4210,213392,262620,892,,Ape7XP7ZVD 
6786,4186,459541,100178,80,,ItaSkyWALLELia
6858,4087,8117789,13310466,51922,BHC,Ape8PJW42P
6846,4075,2995408,3054444,3022,Rus,Ape3DNY94P
6863,3702,596375,595431,80,,Salvocity
6948,3552,2648704,2490945,250,,Ape3DM2PG4
6899,3585,422264,398010,3669,UBVH,Ape6WAYZY2
6893,3995,1336343,3308099,59150,BFME,Ape97JLWVM
6943,3918,13230271,13307248,80,FBR,SZking
6940,3933,2268682,1209874,125,,anjoko3
6849,4038,14426489,13764002,80,BFME,Artes
6868,4031,11294630,5938070,26821,BFME,Deadshot
6899,4009,21901137,22314372,54623,UBVH,70osa
6697,4277,14246767,2295857,80,505,AEW
6740,4288,31926308,16893737,849124,505,Ahell
6771,4210,213392,262620,892,,Ape7XP7ZVD
6846,4075,2995408,3054444,3022,Rus,Ape3DNY94P
6786,4186,459541,100178,80,,ItaSkyWALLELia
6858,4087,8117789,13310466,51922,BHC,Ape8PJW42P
6899,4009,21901137,22314372,54623,UBVH,700sa
6849,4038,14426489,13764002,80,BFME,Artes
6868,4031,11294630,5938070,26821,BFME,Deadshot
6940,3933,2268682,1209874,125,,anjoko3
6893,3995,1336343,3308099,59150,BFME,Ape97JLWVM
6943,3918,13230271,13307248,80,FBR,SZking
6899,3585,422264,398010,3669,UBVH,Ape6WAYZY2
6863,3702,596375,595431,80,,Salvocity
6948,3552,2648704,2490945,250,,Ape3DM2PG4,
4465,6662,1720849,2015536,195932,X15,ksusha0890
4433,6733,6785861,3273965,31,,Sodhi
4448,6706,0,0,80,,BDRahulSM
4553,6630,3526068,2526377,105951,The,Ape4NYWG3M
4649,6629,0,0,80,Vsq,1Pipooo1 
4504,6634,0,0,80,,MORFIN174
4668,6619,0,289707,80,,ApeM2GZY5G
2502,925,508718,824002,80,,ApePNG8MJJ
4583,6608,1152410,3216591,154028,The,UTCalexalbonMdo
2540,785,708374,639881,8485,VG2,Ape3DM534A
2601,998,150076,0,80,,ApeG568NM2
2525,845,1278891,69522,209338,311,brunexter
2477,714,0,518401,80,,t90yanat90
2482,730,498796,469582,80,RUST,ApeBZNA689
2321,722,175872,184729,80,wolf,ApeM2G33XV
2330,601,196228,126221,83727,,ApePNGA5NY
2368,676,831066,1233181,85942,311,ApeG56B632
2139,745,812509,717202,218,Q80,ApeG563AAX
2195,752,242968,49495,80,,Ape3DNL27A 
2286,794,489835,489835,80,,Ape4NY227J
2147,756,80154,69114,30,,ApeZ9NZMGP
2159,766,0,85419,80,,ApeM2DNN2U 
2150,712,46197638,44392256,368077,Osir,polroncopol
595,5687,2140443,2067773,339016,Q80,Satana
595,5687,2140443,2067773,339016,Q80,Satana
2062,609,375772,280082,80,,ApeA9YB7ZZ
2160,670,0,1024259,80,,Luwyn
4583,6608,1152410,3216591,154028,The,UTCalexalbonMdo
4652,6703,0,11340313,289375,,Dman
4380,6346,994928,709719,85170,RUST,APENZGAAPY
4569,6823,946288,789435,173,,UTCTweetyMdo
4627,6807,45110,238674,3410,FBR,Ray
4647,6769,1717465,2144583,80,,XXXSerj111XXX
4474,6725,991935,873083,80,,BlakMonkey
4563,6796,254313,64578,270798,,Lilipytik
4467,6773,16316,0,84121,,Cricri
4445,6833,1444808,6697507,34473,,ApeWD7484L
4493,6743,0,0,80,,TCmaulanaTR
4483,6755,2033830,0,883870,,RUSLAN
4433,6762,8653814,7221143,50321,,[TFIJOscar 20160329
4398,6763,1174386,2185947,80,ARRA,Ape972D4M2
4462,6713,477182,2008815,80,,Roska"; // TODO: Handle blank newlines at the end if they get pasted in
        foreach (var location in screenscrapeData.Split("\r\n"))
        {
            var parts = location.Split(",");
            lootingLocations.Add(new LootingLocation(int.Parse(parts[0]), int.Parse(parts[1]), int.Parse(parts[2]), int.Parse(parts[3]), int.Parse(parts[4]), parts[5], parts[6], new DateTime(2023, 10, 22)));
        }

        lootingLocations.Add(new LootingLocation(6827,4332,119682871,99236376,95027,"BFME","Elbatlan",new DateTime(2023, 10, 22)));


        #endregion
        
        #region oct23
        
        // TODO: Put this into a CSV
        var screenscrapeData23 = @"435,3455,0,0,84055,,Ape3DNP87P
361,3438,622959,41179,239873,OBci,Ape5YLVVA5
372,3596,0,0,80,,ApeBZLX8WN
357,3682,0,0,12921,,FSBSHOWMAN46FSB
404,3761,2385289,2365485,246378,TOP,mnnCobain 13c
361,3454,0,0,80,OBci,ApeDNP64JP
407,3710,0,0,82729,,FSBArtFSB
425,3724,0,0,80,,BITIbrahm18
413,3688,0,0,80,,Ninzmike12
383,3657,92219,772282,1309,,FSBDimarikFSB
436,3652,0,0,80,Vsq,FSBmohammedFSB
413,3673,636441,0,80,,FSBAlexSTRFSB
414,3481,3706556,217901,844443,OBci,OBOblivionObL
363,3621,0,0,80,,Yokick
420,3611,0,0,83614,EMP,Fenrir
372,3596,0,0,80,,ApeBZLX8WN
375,3581,1576118,1747236,608314,SGF,Bas
366,3546,576157,34960,14940,,ApeA9Y9WNA
324,3466,0,0,80,,nygreenguy
410,3511,1921975,1287250,956,OBci,shepard46
414,3481,3706556,217901,844443,OBci,OBOblivionObL
419,3525,0,1181064,80,,Dahrlin
383,3497,0,0,80,OBci,ApeG598AG4
378,3474,0,0,80,OBci,henriquevb
378,3474,0,0,80,OBci,henriquevb
366,3485,332314,22081,84674,OBci,barrett22
393,3466,0,0,80,OBci,OBrambo14860bL
361,3454,0,0,80,OBci,ApeDNP64JP
361,3438,41179,622959,239873,OBci,Ape5YLVVA5
320,3303,0,0,80,,Dophamen
374,3403,0,0,80,,ApeDNPY6DZ
387,3330,75500,0,80,,ApeA9YVAGP
320,3303,0,0,80,,Dophamen
375,3311,0,0,80,,ANCS
393,3466,0,0,80,OBci,OBrambo14860bL
332,3359,29016400,24814389,192071,,Ape4NYWM5W
334,3259,1060471,252971,120619,TFI,TFIADMX32PRC
330,3274,0,0,887020,,trucken2
387,3330,75500,0,80,,ApeA9YVAGP
417,3317,0,0,80,,Chaosmax22
466,3361,487481,321865,85220,,Ape7X5JBW6
402,3392,13371989,16773296,55513,The,Stoves
421,3385,378102,423049,87030,MONK,ApeL3GN58M
446,3413,2812937,2589954,32859,,OBKral400bL
375,3311,0,0,80,,ANCS
324,3466,0,0,80,,nygreenguy
363,3621,0,0,80,,Yokick
366,3546,576157,34960,14940,,ApeA9Y9WNA
327,3626,0,0,127825,,Ape6WAB82N
375,3581,1576118,1747236,608314,SGF,Bas
385,2870,3924172,2403596,840345,BFME,Welly66
373,2914,2374539,2371844,10184,theG,ApeVZNWX2X
504,2943,0,0,80,,SUNPaziteIDAY
547,2913,0,1604979,80,SVP,Ape972P2WV
457,3032,3087621,3609742,564,,ApeDNP5V9Z
469,2918,225620,522887,26112,,AmurChAnIn28
493,2921,1605661,2062304,12855,TOP,Ape6WVDJ5D
487,2909,29254983,6419872,881050,BFME,SunMANCUNG
422,2968,0,0,80,,Tinnie05
490,3052,0,0,80,,Ape7XP78VP
478,3063,0,0,80,,GokuTstvxtsy
454,2903,452380,475531,5259,,Rathore
556,3096,7195040,8168977,80,,Jabas
543,3108,1453763,1867677,1859,,GuKong
524,3148,1853236,1337387,80,,AYGAR
532,3170,1298662,199322,80,,ApeDNL3JNW
517,3189,1776186,1979362,80,ARRA,Roma777
456,3218,1695721,1473132,22250,,ApeBZN95B7
422,3157,0,0,80,MDRU,CRYdhcoutCRY
516,3214,0,0,25971,,vegas
489,3215,1702847,1461055,656,,ApeVZG7AX4
461,3202,0,0,80,,andyrooo420
454,2889,65684,98322,80,,ApeG5686W9 
446,3177,144368,125252,312,,ApeNZGG452
430,3212,154153,257090,2463,,ApeG566769
362,3191,0,0,80,,ApeZ9P54V9
382,3204,0,0,80,,m4rk
395,3171,0,0,80,,decao
368,3121,0,0,80,,Freeman
419,3194,976057,1135569,1271,,ApeVZG74LG
382,3204,0,0,80,,m4rk
330,3274,0,0,887020,,trucken2
469,2918,522887,225620,26112,,AmurChAnIn28
334,3259,1060471,252971,120619,TFI,TFIADMX32PRC
366,3246,0,0,80,,felisaaa
362,3191,0,0,80,,ApeZ9P54V9
395,3171,0,0,80,,decao
371,2943,0,0,21263,,Ape2Z5GPP5
383,2960,150185,1045649,26140,Smpb,ApeL3GA8GW
428,2870,4727240,8539449,102379,BFME,Ape3DNGX3M
639,2880,0,0,3530,,t90Magomedt90
590,2897,455596,662257,84586,,ApeZ9NVX5Y
585,2881,638975,475751,88750,The,ApeZ9W4WB3
486,2952,811883,483881,89252,,ApeVZGMYPD
534,2950,2091377,1873647,13695,,Vlada"; // TODO: Handle blank newlines at the end if they get pasted in
        foreach (var location in screenscrapeData23.Split("\r\n")) // TODO: Likely to forget to change the number on this, needs improving
        {
            var parts = location.Split(",");
            // TODO: need to remember to change the date below each time
            lootingLocations.Add(new LootingLocation(int.Parse(parts[0]), int.Parse(parts[1]), int.Parse(parts[2]), int.Parse(parts[3]), int.Parse(parts[4]), parts[5], parts[6], new DateTime(2023, 10, 23)));
        }
        
        #endregion
        
        #region nov3

        var scoutingDataNov3 = @"5906,1208,198376,199444,1640,,ApeL3ZN2GY 
5942,1207,89901,82337,2314,,ApePNG93YY 
5944,1299,2860518,1652554,80,,frg
5986,1211,6631036,7038574,17135,,Shadow
5778,1211,7770122,9852187,10459,,212osbonNGM
5882,1394,5900398,5453831,10566,ARRA,Resso
5884,1248,368675,428283,4618,505,Bipo
5955,1245,258109,602840,49586,,sku4nij
5223,743,1282319,1306058,10850,SVP,Ape4NYM74W
5111,777,1098707,132211,26758,,Yojik
5692,1099,946064,882957,80,Q80,Ape972L48M
5785,1357,1681611,4,1428952,,Spacecliffgod
5867,1272,1255151,1243265,3853,,ApePNG7ABB
5840,1289,3848811,3719489,22252,,BaBale0 Saindnd
5804,1301,95302,86818,308,,ApeVZGNBYX 
5778,1211,7770122,9852187,10459,,212osbonNGM
5766,1304,23711,99668,3356,,Ape6WVYAB2 
5671,1182,2996018,2635252,34000,,Ape7X5VDPN
5704,1164,37706,37334,2357,,Ape97JBN3L 
5602,1404,748832,685432,80,,ApeVZ34732 
5083,803,2661535,2189092,80,,ApeZ9N9YBW 
5587,1427,771097,639912,3790,,ApeM2GN955
5742,1391,765429,883528,80,,Ape7XP8P8A 
5730,1368,1880718,2452412,10410,R75,Ape4NYVN9V
5656,1373,1407086,1286308,61,,Ape8PZ88D3
5668,1412,37080,49420,80,,Tntcryss
5657,1316,1116471,1163944,80,,HuyCuong
5655,1357,1124943,1174701,7830,,Dima02061994
5667,1258,111764,97896,1730,,Ape3D6JZMA 
5586,1183,986421,1354265,5741,,Ape3DNZNXY
5027,787,547409,93327,80,theG,Asl
5680,1244,291206,311250,3655,,ApeWDYAMJV
5587,1148,175085,0,80,,Semen999
5567,1117,882981,2216511,9375,311,Ape6WVMBZZ
5590,1121,112366,76526,87,,ApeM2GZYN6 
5592,1043,1659169,627387,242646,,ApeWDGMANJ
5567,996,474536,615298,87035,,ApeG56684A
5314,949,308784,277456,330,,Ape97DW7NY 
5433,1060,738498,1059014,80,,ApeL3P2PDM
5309,856,2677702,3938482,87357,R75,Ape972Z5JL
5384,799,54370,65530,311,,ApeYYDJ26Z 
5071,737,1926296,2580107,9627,ARRA,ApeZ9W8GB9
5357,843,2154689,1301369,87705,,ApePNGVJ67
5352,801,117182,84698,1615,,Ape7XPG59A 
5314,746,65414,83238,2875,,Ape3DNGW6M 
5267,743,62300,49900,8470,,ApeVZ394W4 
5282,727,1565224,1552369,5697,Osir,nansh
5281,747,749803,750995,5825,,Ape8PX3LYV
5253,743,678820,710310,4936,Osir,ApeG56AMBA
5044,741,63720,61980,1623,,ApeZ9N5AJW 
5059,792,378051,2883932,80,,Teffe
5049,776,15784,42599,534,,ApeXNZN8LX 
5111,777,1098707,132211,26758,,Yojik
6700,3113,334397,272108,80,,ApePN39BJ5 
6747,3048,67434,3255,315,,Ape7XWBB8B 
6248,3259,487174,0,80,,Ape3D6J9ZJ 
6065,2936,274930,247190,3390,,274,930
6164,4313,181603,185056,80,,ApeG59A37A 
6679,2891,2820,90,90,,2,820
6720,3073,4881511,150,6890,,Ape4NYY59J
6710,3083,3660,276849,55,,3,660
6672,3028,938656,764650,6102,,ApeBZNYM4Y 
6661,3017,2012007,2155559,87010,,ApeG56PWMJ
6515,2967,819981,1012577,87285,,Surgutcity
6589,3036,516214,548650,5505,,Ape2753XJN 
6552,3046,121243,4901,150,,ApeZ9NJLGJ 
6551,3126,3582,49102,15,,3,582
6292,3230,101360,85196,80,,Ape7XPAWYV 
6108,3914,1109200,1326129,37,Fam,ApeL3GMMG8
6867,2796,49041,3502,104,,ApeVZG68YD 
6442,3975,4006522,22800,2879544,Fam,Cerullo
6629,3010,2317,15,15,,2,317
6286,4113,1875653,1800441,254481,NWO,Daallusa
6681,3058,1584891,931060,37131,,ApeYYD7LVL
6292,3026,1329342,1071281,251,,VanQuyet
6868,4031,5938070,11294630,26821,BFME,Deadshot
6450,4033,4538848,3020578,130350,Fam,Matvei
6471,4010,3071035,146287,3750,Fam,Beliy
6464,2947,0,102579,3543,,ApePNG2MYB 
6563,3014,98172,100712,80,,ApeXNZ9BMX 
6499,2912,75426,3607,75,,ApeWDG8JZJ
6493,3097,107317,110281,3473,,ApePNG2WB5 
6012,3270,2217,45768,30452,,2,217
6105,4127,658831,0,80,,ApeZ9WGDD6 
6105,4127,658831,0,80,,ApeZ9WGDD6 
6933,3984,137994,303013,19,,amr22
6116,2920,854409,4566,475,,Ape6WAJPAV 
6472,3076,499720,3830,115,,Ape3DN2AYL 
6640,4287,63807,4305,940,SVP,ApeVZGMGNP
6621,4125,1153574,2297895,80,,ROMANNO
6932,4021,3481406,3730404,6650,,DaCom
6658,4165,8141403,7047764,45603,NWO,Ape8PJLYDZ
6616,4139,586802,574754,92,,eturned home (sweet home)
6231,3099,124984,122468,2321,,Euxix
6376,4025,414149,5103036,80,PON,ApeNZGPNLJ
6419,4051,4929084,4279482,80,Fam,Eska95
6616,4139,586802,574754,92,,Eldario
6131,3136,0,187578,534,FBR,ApeZ9W494J
6858,4087,16394891,21542568,63422,BHC,Ape8PJW42P
6899,4009,21901137,22314372,54623,UBVH,700sa
6899,4009,21901137,22314372,54623,UBVH,700sa
6557,4080,18240740,13443473,746,Fam,Ape7XPBPJA
6893,3995,1336343,3308099,59150,BFME,Ape97JLWVM
6888,4173,2277,163417,189181,,2,277
6869,3789,998,77,77,,ApeVZGN8W4 
6924,4002,459768,414162,4900,,ApeZ9NJ2GX 
5043,1477,40000,301911,80,,Ape5YJB6ZX
5160,1449,5282042,2647368,1,RBK,ApeDNP5A67
6928,2824,779606,686288,5445,one1,ApeWDG9DJV
6447,3069,4577952,3563167,80,,Ape6WVJZGB
6419,4051,4929084,4279482,80,Fam,Eska95
6876,2914,991025,835480,1325,,LOL
6808,2945,782555,776588,5499,,Agalaks
6812,2969,2690,15,15,,2,690
5054,1540,1105064,868298,138,,Ape4NYZL3M
4962,1547,1655426,1659326,30,,Tblrgbotvtvt
5210,1437,3309583,900,9945,RBK,ApeDNPW2ZD
6383,2990,2818780,1236351,46056,,Brunedson
6157,3091,51676,1388240,1273025,theG,MixonHere
6554,4121,4183363,6268714,30,Fam,DexTr
6717,4216,23204678,30,1017020,505,Gabri
6481,4165,1421663,749096,64300,Fam,Bigtankk98
6431,4159,8277309,7915605,94350,Fam,lonelymen
6693,4321,1300,3824217,54094,,1,300
6667,4121,14057120,16918377,161333,BFME,Rick
6496,4033,10287297,12034040,344350,Fam,Wael
6804,3075,128328,118870,80,,ApePNGX2WV 
6740,4288,28,66307,5359,505,Ahell
6915,2916,16705,1050,1250,,ApeYYWBWXZ
6408,3060,667782,1195647,30,FOB,alchimbek
6549,3959,63906140,30103637,307852,Fam,HaH
6703,3139,203602,229618,80,,francois
5208,1534,291130,83414,84,,Ape4NY423W 
5068,1473,1936770,151525,80,,danielbeltr
5667,1258,111764,1540,190,,Ape3D6JZMA 
5680,1244,3640,291206,311250,,3,640
5059,792,378051,2883932,80,,Teffe
5402,1250,117588,2728,395,,ApeZ9NPWJW
5766,1304,423711,99668,3326,,Ape6WVYAB2 
5742,1391,765429,883528,80,,Ape7XP8P8A
5668,1412,37080,49420,80,,Tntcryss
5656,1373,5,1407086,1286308,,Ape8PZ88D3
5314,949,330,308784,277456,,Ape97DW7NY 
5656,1373,5,1407086,1286308,,Ape8PZ88D3
5657,1316,1163944,80,1116471,,HuyCuong
5655,1357,1124943,1174701,600,,Dima02061994
5587,1148,175085,0,80,,Semen999
5567,1117,882981,2216511,300,311,Ape6WVMBZZ
5281,747,749803,750995,5825,,Ape8PX3LYV 
5253,743,710310,678820,4756,Osir,ApeG56AMBA 
5314,949,330,308784,277456,,Ape97DW7NY 
5071,737,1926296,2580107,9627,ARRA,ApeZ9W8GB9
5303,1309,942055,931213,80,,XXXLintlickXXX 
5386,1364,893518,5390,600,UBVH,Ape97DX5NY
5587,1427,771097,639912,3790,,ApeM2GN955 
5384,799,296,15,15,,ApeYYDJ26Z
5610,1160,87713,1140,1089,,monkeyking
5586,1183,1354265,986421,245,,Ape3DNZNXY
5567,996,474536,100,6290,,ApeG56684A 
5332,1242,407543,406773,87085,,1Isaiah301
5320,1268,2005503,1781385,705150,QUIT,EMGRef
5785,1357,1428952,1681611,1383,,Spacecliffgod 
5352,801,1570,45,45,,Ape7XPG59A 
5027,787,547409,93327,80,theG,Asl
6849,4038,13764002,14426489,80,BFME,Artes
6835,4015,278536,221130,2089,,AlexGold
6469,4077,1596720,80,36,Fam,SRF99
5986,1211,7038574,6631036,17135,,Shadow
5884,1248,4483,368675,15,,4,483
6224,3409,167978,476037,6690,,ApeL3P2GMZ 
6505,4080,7477099,7136399,104120,Fam,Tadas
5944,1299,1652554,2860518,80,,frg
6525,4102,10147782,46750,9723970,Fam,Ape2ZDLPD8
5882,1394,5453831,5900398,1821,ARRA,Resso
5933,1313,6717,4485,7790,Osir,ApeL3GM3VJ
5671,1182,2635252,2996018,6043,,Ape7X5VDPN
5840,1289,3848811,2057,8920,,BaBale0Saindnd
5942,1207,2269,89901,82337,,2,269
5906,1208,198376,199444,1640,,ApeL3ZN2GY 
5704,1164,2342,37706,37334,,2,342
6454,3105,593248,1540,30,,Ape2ZD857Z";
        
        foreach (var location in scoutingDataNov3.Split("\r\n")) // TODO: Likely to forget to change the number on this, needs improving
        {
            var parts = location.Split(",");
            // TODO: need to remember to change the date below each time
            lootingLocations.Add(new LootingLocation(int.Parse(parts[0]), int.Parse(parts[1]), int.Parse(parts[2]), int.Parse(parts[3]), int.Parse(parts[4]), parts[5], parts[6], new DateTime(2023, 11, 3)));
        }
        
        #endregion
        
        #region nov13

        var scoutingDataNov13 = @"4497,962,4,0,138744,,Ape7XPL5GZ
4539,935,1241822,201018,3141,,Catalin
4625,1006,73502,22318,84427,,ZetJayden1360
4667,1031,50370,65853,566,,ApeDN4ZYL3 
4627,1060,32334,67106,3400,,ApeG56JJW2
4612,1064,800188,787949,4955,,Ape2Z57W65
4581,1091,453479,456495,1529,,Ape7XPZ8NV
4582,1107,47691,863905,90207,,ApePNGXA23
4581,1122,642493,630285,4642,,Ape5YL49AX
4516,1069,2329418,3113111,80,,sempackbasah
4551,1121,94251,103808,80,BMF,ApePNGY246
4431,1166,1407591,2412627,80,RUST,Ape4NJ6PGN
4524,963,64701,133603,5360,,Ape6WV5X53
4433,1197,4,0,116090,Q80,Son
4505,1083,135361,171102,80,,Ape6WA7YYL
4464,1186,454121,513235,80,,ApeM2DNBYP 
4565,1225,374632,648041,80,,Lewk
4509,1242,1048275,915963,84270,FBR,JEMEMEJ
4483,1203,115475,0,80,,imperia
4513,1201,674068,99337,23,,TCBolu14TR
4494,1392,556665,721701,4915,,emgato101 
4495,1248,656633,695669,80,,ApeZ9PAZN3
4527,1345,915484,1172939,82,,Ape2Z5V569
4515,949,1641367,979569,18230,,Ape4NY3JYJ
4468,1301,469307,612687,5770,,caSIA just got Laurent! Q
4506,1378,250266,272274,3700,,ApeBZN9NZD
4581,1288,426501,267504,80,,ApeDNLDA7G
4563,1299,427514,3078903,322142,FBR,Officerdom59
4534,1331,170141,173908,454,,Ape6WA6AD2
4995,1485,179309,190729,1857,,ApeDNP8B32
4519,1300,1240800,1228612,80,,javad
4961,1492,0,1478172,80,,sherlock
4569,966,191968,21918,2820,,Ape3DM438Y
4979,1458,4225716,5705314,80,,ApeYYDP5ZJ
4922,1571,5051758,6668568,79897,UBVH,gajah
4902,1546,35594,73163,80,,SophiaApeGang
4607,1001,1146778,1411115,5405,,Ape5Y79AW5
4665,1047,51939,65878,846,,VIT
4566,1063,1418982,236102,8935,,NIVEK
4578,930,72218,61902,1636,,ApeBZL4AAV 
4634,1032,110746,112162,280,,Ape6WVMZYZ 
1833,6013,973495,912843,9015,,ApeWDG89BG
1599,5845,4049611,6097538,255771,Rus,HayaxLC
1572,6340,60073876,65537113,346815,theG,ApeYYWB2M7
1592,6334,7514569,2299954,498361,Q80,Watchmen
1758,6228,145434,5366,4624,,ApeBZL4XZD 
1714,6384,1396605,1693253,33419,The,Tigerking
1576,6133,925885,858527,5100,,Zhani
1576,6309,4655867,4393279,18704,MDRU,ApexPredator
1597,6186,364878,405294,80,,ApeVZGAYPY
1480,6232,24230,28670,2333,,ApeYYWXPDD
1608,6141,7591288,5282054,80,,Pawhl2246
1457,6232,861910,913768,5130,,TrungKien07 
1709,5868,3137835,11927079,180179,Rus,fang8899
1506,6234,517500,527620,87130,,Ape2ZDLBV7 
1501,6209,1017432,1618475,1765,,ApeVZG2V4Y
1485,6139,136348,132072,2326,,Dave
1492,6117,423811,671539,899,,ApePN3BMNY
1466,6112,87719,7811100,8105301,,EXOODIA
1559,5964,639999,0,80,VG2,BuXriskiNiXNİK
1496,5968,151157,0,80,,t90speirat90
1519,6164,306000,311400,212,,Ape7XPAAPD 
1511,5960,855827,0,6530,,ApeM2GAZJL 
1510,5942,1288079,0,80,FBR,RusbananenRus
1559,5964,0,639999,80,VG2,BuXriskiNiXNİK
1493,5927,120338,126886,3355,,Ape2ZD3N87
1370,5930,46000,66000,80,,Hassan
1455,5809,579879,357620,80,,ApeYYW2YN6
1586,5784,137377,115269,80,,ApeNZGJM42 
594,5753,3139380,3018056,6915,Q80,Ape5YJWPGW
648,5651,1282776,2708627,90529,Q80,Q80SultanQ80
1455,5809,579879,357620,80,,ApeYYW2YN6
581,5696,1944890,403434,1672782,Q80,Vhyckz
595,5687,1347208,1637179,155942,Q80,Satana
5134,759,480357,308648,14608,,ApeG567LPW
1873,5956,2464279,2231828,80,PON,unithekonaful
4849,667,16759923,7814843,132321,,Saverix
5145,854,1114283,792025,15142,,ApeL3GYMM3
4853,687,1893146,1169561,223598,,KingHubi
4770,892,873011,805891,87125,,ApeWDGZJ8V
4754,845,2712351,1999070,8995,,Arbuz
4535,1026,115659,117260,4200,,jorgito97
4480,997,1437240,1341318,8776,DU1,Ape4NYPMA6
4464,995,88498,85822,1541,,ApeDNL6L28 
4486,1010,24298,70982,80,,ApeYYWXJBP
4479,981,657981,715589,5300,,ApeDNPJN78 
1597,6090,685480,688256,5225,,ApeM2XA8WY
1775,6125,65202,54878,3604,,Ape5YLW93M 
1559,5964,639999,0,80,VG2,BuXriskiNiXNİK
1833,6013,973495,912843,9015,,ApeWDG89BG
1775,6125,65202,54878,3604,,Ape5YLW93M";
        
        foreach (var location in scoutingDataNov13.Split("\r\n")) // TODO: Likely to forget to change the number on this, needs improving
        {
            var parts = location.Split(",");
            // TODO: need to remember to change the date below each time
            lootingLocations.Add(new LootingLocation(int.Parse(parts[0]), int.Parse(parts[1]), int.Parse(parts[2]), int.Parse(parts[3]), int.Parse(parts[4]), parts[5], parts[6], new DateTime(2023, 11, 13)));
        }

        #endregion
        
        #region other
        lootingLocations.Add(new LootingLocation
        {
            xCoordinates = 1668,
            yCoordinates = 849,
            FoodAmount = 112868,
            IronAmount = 309052,
            ArmyCount = 0,
            Power = 36463, 
            CityLevel = 8,
            ClanName = "",
            PlayerName = "Petike21",
            DateLastScouted = new DateTime(2023, 10, 21)
        });
        
        lootingLocations.Add(new LootingLocation
        {
            xCoordinates = 7023,
            yCoordinates = 3183,
            FoodAmount = 1472611,
            IronAmount = 1639845,
            ArmyCount = 0,
            Power = 327638, 
            CityLevel = 13,
            ClanName = "UBVH",
            PlayerName = "Symbols",
            DateLastScouted = new DateTime(2023, 10, 21)
        });
        
        lootingLocations.Add(new LootingLocation
        {
            xCoordinates = 5656,
            yCoordinates = 1740,
            FoodAmount = 36900,
            IronAmount = 36900,
            ArmyCount = 150,
            Power = 17996, 
            CityLevel = 6,
            ClanName = "wolf",
            PlayerName = "ApeA9YZ2PV",
            DateLastScouted = new DateTime(2023, 10, 21)
        });
        
        lootingLocations.Add(new LootingLocation
        {
            xCoordinates = 1668,
            yCoordinates = 849,
            FoodAmount = 2030120,
            IronAmount = 414698,
            ArmyCount = 420761,
            Power = 2937034, 
            CityLevel = 16,
            ClanName = "UBVH",
            PlayerName = "battontautau",
            DateLastScouted = new DateTime(2023, 10, 21)
        });
        
        lootingLocations.Add(new LootingLocation
        {
            xCoordinates = 7027,
            yCoordinates = 3248,
            FoodAmount = 140284,
            IronAmount = 0,
            ArmyCount = 3940,
            Power = 33521, 
            CityLevel = 9,
            ClanName = "UBVH",
            PlayerName = "Refo",
            DateLastScouted = new DateTime(2023, 10, 21)
        });
        
        lootingLocations.Add(new LootingLocation
        {
            xCoordinates = 1797,
            yCoordinates = 927,
            FoodAmount = 4024942,
            IronAmount = 4291661,
            ArmyCount = 48858,
            Power = 984493, 
            CityLevel = 15,
            ClanName = "MONK",
            PlayerName = "ramm",
            DateLastScouted = new DateTime(2023, 10, 21)
        });
        
        lootingLocations.Add(new LootingLocation
        {
            xCoordinates = 1813,
            yCoordinates = 924,
            FoodAmount = 30003834,
            IronAmount = 31559272,
            ArmyCount = 215783,
            Power = 14317363, 
            CityLevel = 27,
            ClanName = "MONK",
            PlayerName = "nan1to",
            DateLastScouted = new DateTime(2023, 10, 21)
        });
        
        lootingLocations.Add(new LootingLocation
        {
            xCoordinates = 6881,
            yCoordinates = 3103,
            FoodAmount = 1041763,
            IronAmount = 3722999,
            ArmyCount = 19142,
            Power = 296380, 
            CityLevel = 12,
            ClanName = "9V0",
            PlayerName = "Ape972NY46",
            DateLastScouted = new DateTime(2023, 10, 21)
        });
        
        lootingLocations.Add(new LootingLocation
        {
            xCoordinates = 4324,
            yCoordinates = 6244,
            FoodAmount = 8874108,
            IronAmount = 7116454,
            ArmyCount = 1988,
            Power = 199705, 
            CityLevel = 12,
            ClanName = "7LZ",
            PlayerName = "Devilmonkey",
            DateLastScouted = new DateTime(2023, 10, 21)
        });
        
        lootingLocations.Add(new LootingLocation
        {
            xCoordinates = 1352,
            yCoordinates = 1802,
            FoodAmount = 607326,
            IronAmount = 990975,
            ArmyCount = 334,
            Power = 227932, 
            CityLevel = 12,
            ClanName = "",
            PlayerName = "DaryaGr",
            DateLastScouted = new DateTime(2023, 10, 21)
        });
        
        lootingLocations.Add(new LootingLocation
        {
            xCoordinates = 6016,
            yCoordinates = 3644,
            FoodAmount = 1091678,
            IronAmount = 1019342,
            ArmyCount = 13871,
            Power = 145547, 
            CityLevel = 10,
            ClanName = "",
            PlayerName = "ProHamelion",
            DateLastScouted = new DateTime(2023, 10, 21)
        });
        
        lootingLocations.Add(new LootingLocation
        {
            xCoordinates = 676,
            yCoordinates = 3249,
            FoodAmount = 2551645,
            IronAmount = 1424963,
            ArmyCount = 24706,
            Power = 254228, 
            CityLevel = 0,
            ClanName = "",
            PlayerName = "KhaledT",
            DateLastScouted = new DateTime(2023, 10, 21)
        });
        
        lootingLocations.Add(new LootingLocation
        {
            xCoordinates = 661,
            yCoordinates = 3276,
            FoodAmount = 1150303,
            IronAmount = 509931,
            ArmyCount = 14622,
            Power = 164045, 
            CityLevel = 0,
            ClanName = "",
            PlayerName = "ApeYYVGV57",
            DateLastScouted = new DateTime(2023, 10, 21)
        });
        
        lootingLocations.Add(new LootingLocation
        {
            xCoordinates = 681,
            yCoordinates = 3199,
            FoodAmount = 31781745,
            IronAmount = 23803602,
            ArmyCount = 38530,
            Power = 366911, 
            CityLevel = 0,
            ClanName = "7LZ",
            PlayerName = "Baxa",
            DateLastScouted = new DateTime(2023, 10, 21)
        });
        
        lootingLocations.Add(new LootingLocation
        {
            xCoordinates = 707,
            yCoordinates = 3194,
            FoodAmount = 12788521,
            IronAmount = 7269026,
            ArmyCount = 0,
            Power = 366911, 
            CityLevel = 0,
            ClanName = "FBR",
            PlayerName = "Kingkong1",
            DateLastScouted = new DateTime(2023, 10, 21)
        });
        
        lootingLocations.Add(new LootingLocation
        {
            xCoordinates = 5835,
            yCoordinates = 6178,
            FoodAmount = 7894470,
            IronAmount = 3835219,
            ArmyCount = 81464,
            Power = 783572, 
            CityLevel = 0,
            ClanName = "X15",
            PlayerName = "Ahmt6",
            DateLastScouted = new DateTime(2023, 10, 21)
        });
        
        lootingLocations.Add(new LootingLocation
        {
            xCoordinates = 2567,
            yCoordinates = 6702,
            FoodAmount = 11010270,
            IronAmount = 11586508,
            ArmyCount = 208164,
            Power = 1395726, 
            CityLevel = 0,
            ClanName = "",
            PlayerName = "ZetshokeeGo",
            DateLastScouted = new DateTime(2023, 10, 21)
        });
        
        lootingLocations.Add(new LootingLocation
        {
            xCoordinates = 648,
            yCoordinates = 5651,
            FoodAmount = 4750404,
            IronAmount = 2656694,
            ArmyCount = 124385,
            Power = 1774346, 
            CityLevel = 0,
            ClanName = "Q80",
            PlayerName = "Q80SultanQ80",
            DateLastScouted = new DateTime(2023, 10, 21)
        });
        
        lootingLocations.Add(new LootingLocation
        {
            xCoordinates = 428,
            yCoordinates = 2870,
            FoodAmount = 158497,
            IronAmount = 89612,
            ArmyCount = 53100,
            Power = 1077155, 
            CityLevel = 0,
            ClanName = "BFME",
            PlayerName = "Ape3DNGX3M",
            DateLastScouted = new DateTime(2023, 10, 21)
        });
        
        lootingLocations.Add(new LootingLocation
        {
            xCoordinates = 579,
            yCoordinates = 5738,
            FoodAmount = 910828,
            IronAmount = 729246,
            ArmyCount = 0,
            Power = 1689178, 
            CityLevel = 0,
            ClanName = "Q80",
            PlayerName = "ApePNLV9LV",
            DateLastScouted = new DateTime(2023, 10, 21)
        });
        
        lootingLocations.Add(new LootingLocation
        {
            xCoordinates = 1305,
            yCoordinates = 5707,
            FoodAmount = 6478739,
            IronAmount = 4887384,
            ArmyCount = 393400,
            Power = 3727637, 
            CityLevel = 0,
            ClanName = "TGK",
            PlayerName = "Ape6WVX753",
            DateLastScouted = new DateTime(2023, 10, 21)
        });
        
        lootingLocations.Add(new LootingLocation
        {
            xCoordinates = 2067,
            yCoordinates = 1993,
            FoodAmount = 40890271,
            IronAmount = 19160737,
            ArmyCount = 305399,
            Power = 3254994, 
            CityLevel = 0,
            ClanName = "H9B",
            PlayerName = "polPolMahesapol",
            DateLastScouted = new DateTime(2023, 10, 21)
        });
        
        lootingLocations.Add(new LootingLocation
        {
            xCoordinates = 2162,
            yCoordinates = 1121,
            FoodAmount = 0,
            IronAmount = 0,
            ArmyCount = 292171,
            Power = 3946965, 
            CityLevel = 0,
            ClanName = "TWU",
            PlayerName = "Alan92",
            DateLastScouted = new DateTime(2023, 10, 21)
        });
        
        lootingLocations.Add(new LootingLocation
        {
            xCoordinates = 2186,
            yCoordinates = 1117,
            FoodAmount = 53764607,
            IronAmount = 20745873,
            ArmyCount = 345253,
            Power = 7775029, 
            CityLevel = 0,
            ClanName = "TWU",
            PlayerName = "Haranbanjo",
            DateLastScouted = new DateTime(2023, 10, 21)
        });
        
        lootingLocations.Add(new LootingLocation
        {
            xCoordinates = 2172,
            yCoordinates = 1108,
            FoodAmount = 2058535,
            IronAmount = 1995769,
            ArmyCount = 325291,
            Power = 7475112, 
            CityLevel = 0,
            ClanName = "TWU",
            PlayerName = "Bobdem",
            DateLastScouted = new DateTime(2023, 10, 21)
        });
        
        lootingLocations.Add(new LootingLocation
        {
            xCoordinates = 1645,
            yCoordinates = 985,
            FoodAmount = 112618,
            IronAmount = 109502,
            ArmyCount = 0,
            Power = 8609, 
            CityLevel = 0,
            ClanName = "",
            PlayerName = "Ape5YLA3JZ",
            DateLastScouted = new DateTime(2023, 10, 21)
        });
        
        lootingLocations.Add(new LootingLocation
        {
            xCoordinates = 5741,
            yCoordinates = 1931,
            FoodAmount = 16778693,
            IronAmount = 14949474,
            ArmyCount = 16,
            Power = 288906, 
            CityLevel = 0,
            ClanName = "wolf",
            PlayerName = "cjay",
            DateLastScouted = new DateTime(2023, 10, 21)
        });
        
        lootingLocations.Add(new LootingLocation
        {
            xCoordinates = 6401,
            yCoordinates = 4467,
            FoodAmount = 2562481,
            IronAmount = 2032296,
            ArmyCount = 0,
            Power = 60704, 
            CityLevel = 0,
            ClanName = "",
            PlayerName = "sacred",
            DateLastScouted = new DateTime(2023, 10, 21),
            DateLastAttacked = new DateTime(2023, 10, 21),
            LossesOnLastAttack = 0
        });
        
        lootingLocations.Add(new LootingLocation
        {
            xCoordinates = 6361,
            yCoordinates = 4425,
            FoodAmount = 284479,
            IronAmount = 278339,
            ArmyCount = 0,
            Power = 60704, 
            CityLevel = 0,
            ClanName = "",
            PlayerName = "Ape4N3MNXW",
            DateLastScouted = new DateTime(2023, 10, 21),
            DateLastAttacked = new DateTime(2023, 10, 21),
            LossesOnLastAttack = 0
        });
        
        lootingLocations.Add(new LootingLocation
        {
            xCoordinates = 6444,
            yCoordinates = 4427,
            FoodAmount = 1318286,
            IronAmount = 933839,
            ArmyCount = 0,
            Power = 30963, 
            CityLevel = 0,
            ClanName = "",
            PlayerName = "Monke21",
            DateLastScouted = new DateTime(2023, 10, 21),
            DateLastAttacked = new DateTime(2023, 10, 21),
            LossesOnLastAttack = 0
        });
        
        lootingLocations.Add(new LootingLocation
        {
            xCoordinates = 6433,
            yCoordinates = 4350,
            FoodAmount = 151472,
            IronAmount = 92608,
            ArmyCount = 300,
            Power = 3658, 
            CityLevel = 0,
            ClanName = "",
            PlayerName = "ApeL3GA8XW",
            DateLastScouted = new DateTime(2023, 10, 21),
            DateLastAttacked = new DateTime(2023, 10, 21),
            LossesOnLastAttack = 0
        });
        
        lootingLocations.Add(new LootingLocation
        {
            xCoordinates = 6407,
            yCoordinates = 4354,
            FoodAmount = 1635698,
            IronAmount = 1783773,
            ArmyCount = 464,
            Power = 132975, 
            CityLevel = 0,
            ClanName = "",
            PlayerName = "xunix",
            DateLastScouted = new DateTime(2023, 10, 21),
            DateLastAttacked = new DateTime(2023, 10, 21),
            LossesOnLastAttack = 33
        });
        
        lootingLocations.Add(new LootingLocation
        {
            xCoordinates = 6387,
            yCoordinates = 4367,
            FoodAmount = 76658,
            IronAmount = 0,
            ArmyCount = 550,
            Power = 15066, 
            CityLevel = 0,
            ClanName = "",
            PlayerName = "ApeJP4Y7V8",
            DateLastScouted = new DateTime(2023, 10, 21)
        });
        
        lootingLocations.Add(new LootingLocation
        {
            xCoordinates = 6394,
            yCoordinates = 4316,
            FoodAmount = 79709,
            IronAmount = 65731,
            ArmyCount = 4283,
            Power = 27629, 
            CityLevel = 0,
            ClanName = "UBVH",
            PlayerName = "ApeA9DWLLN",
            DateLastScouted = new DateTime(2023, 10, 21)
        });
        
        lootingLocations.Add(new LootingLocation
        {
            xCoordinates = 6311,
            yCoordinates = 4291,
            FoodAmount = 5437135,
            IronAmount = 2037754,
            ArmyCount = 47494,
            Power = 1266430, 
            CityLevel = 0,
            ClanName = "",
            PlayerName = "TrLinhCcc",
            DateLastScouted = new DateTime(2023, 10, 21),
            DateLastAttacked = new DateTime(2023, 10, 21),
            LossesOnLastAttack = 2500
        });
        
        lootingLocations.Add(new LootingLocation
        {
            xCoordinates = 6290,
            yCoordinates = 4311,
            FoodAmount = 1631275,
            IronAmount = 1170217,
            ArmyCount = 60335,
            Power = 1015916, 
            CityLevel = 0,
            ClanName = "",
            PlayerName = "zeeAlisoo",
            DateLastScouted = new DateTime(2023, 10, 21),
            DateLastAttacked = new DateTime(2023, 10, 21),
            LossesOnLastAttack = 5000
        });
        
        lootingLocations.Add(new LootingLocation
        {
            xCoordinates = 6271,
            yCoordinates = 4269,
            FoodAmount = 334438,
            IronAmount = 297555,
            ArmyCount = 45,
            Power = 28871, 
            CityLevel = 0,
            ClanName = "",
            PlayerName = "Ape3DN5AW4",
            DateLastScouted = new DateTime(2023, 10, 21),
            DateLastAttacked = new DateTime(2023, 10, 21),
            LossesOnLastAttack = 10
        });
        
        lootingLocations.Add(new LootingLocation
        {
            xCoordinates = 6308,
            yCoordinates = 4215,
            FoodAmount = 544018,
            IronAmount = 0,
            ArmyCount = 0,
            Power = 28527, 
            CityLevel = 0,
            ClanName = "",
            PlayerName = "ApeBZLLAM3",
            DateLastScouted = new DateTime(2023, 10, 21)
        });
        
        lootingLocations.Add(new LootingLocation
        {
            xCoordinates = 6299,
            yCoordinates = 4088,
            FoodAmount = 0,
            IronAmount = 84391,
            ArmyCount = 0,
            Power = 132902, 
            CityLevel = 0,
            ClanName = "",
            PlayerName = "FSBmaximusFSB",
            DateLastScouted = new DateTime(2023, 10, 21)
        });
        
        lootingLocations.Add(new LootingLocation
        {
            xCoordinates = 6280,
            yCoordinates = 4074,
            FoodAmount = 1606086, // No resources by the time I went looting...?
            IronAmount = 865341,
            ArmyCount = 0,
            Power = 51282, 
            CityLevel = 0,
            ClanName = "",
            PlayerName = "Ape972NNNZ",
            DateLastScouted = new DateTime(2023, 10, 21),
            DateLastAttacked = new DateTime(2023, 10, 21),
            LossesOnLastAttack = 5
        });
        
        lootingLocations.Add(new LootingLocation
        {
            xCoordinates = 6337,
            yCoordinates = 4119,
            FoodAmount = 25894,
            IronAmount = 26894,
            ArmyCount = 28512,
            Power = 285723, 
            CityLevel = 0,
            ClanName = "NWO",
            PlayerName = "Juan24",
            DateLastScouted = new DateTime(2023, 10, 21)
        });
        
        lootingLocations.Add(new LootingLocation
        {
            xCoordinates = 6286,
            yCoordinates = 4113,
            FoodAmount = 4005245,
            IronAmount = 7765306,
            ArmyCount = 192092,
            Power = 2136138, 
            CityLevel = 0,
            ClanName = "NWO",
            PlayerName = "Daallusa",
            DateLastScouted = new DateTime(2023, 10, 21)
        });
        
        lootingLocations.Add(new LootingLocation
        {
            xCoordinates = 6242,
            yCoordinates = 4133,
            FoodAmount = 466845,
            IronAmount = 576056,
            ArmyCount = 0,
            Power = 82033, 
            CityLevel = 0,
            ClanName = "NWO",
            PlayerName = "Dragonoes",
            DateLastScouted = new DateTime(2023, 10, 21),
            DateLastAttacked = new DateTime(2023, 10, 21),
        });
        
        lootingLocations.Add(new LootingLocation
        {
            xCoordinates = 6092,
            yCoordinates = 4084,
            FoodAmount = 1594720,
            IronAmount = 1779151,
            ArmyCount = 5290,
            Power = 47424, 
            CityLevel = 0,
            ClanName = "7LZ",
            PlayerName = "ApeYYV88B6",
            DateLastScouted = new DateTime(2023, 10, 21),
            DateLastAttacked = new DateTime(2023, 10, 21),
            LossesOnLastAttack = 180
        });
        
        lootingLocations.Add(new LootingLocation
        {
            xCoordinates = 6192,
            yCoordinates = 3916,
            FoodAmount = 1201048,
            IronAmount = 1471430,
            ArmyCount = 262331,
            Power = 3374460, 
            CityLevel = 0,
            ClanName = "fun",
            PlayerName = "FSBAnnubizFSB",
            DateLastScouted = new DateTime(2023, 10, 21)
        });
        
        lootingLocations.Add(new LootingLocation
        {
            xCoordinates = 6167,
            yCoordinates = 3914,
            FoodAmount = 19109371,
            IronAmount = 11447159,
            ArmyCount = 0,
            Power = 502372, 
            CityLevel = 0,
            ClanName = "fun",
            PlayerName = "ApeM2D96B4",
            DateLastScouted = new DateTime(2023, 10, 21),
            DateLastAttacked = new DateTime(2023, 10, 21),
        });
        
        lootingLocations.Add(new LootingLocation
        {
            xCoordinates = 6154,
            yCoordinates = 3893,
            FoodAmount = 0,
            IronAmount = 95947,
            ArmyCount = 0,
            Power = 1752999, 
            CityLevel = 0,
            ClanName = "fun",
            PlayerName = "001AlexeyTOP",
            DateLastScouted = new DateTime(2023, 10, 21),
            DateLastAttacked = new DateTime(2023, 10, 21),
        });
        
        lootingLocations.Add(new LootingLocation
        {
            xCoordinates = 6160,
            yCoordinates = 3859,
            FoodAmount = 11395964,
            IronAmount = 6850971,
            ArmyCount = 291793,
            Power = 2810659, 
            CityLevel = 0,
            ClanName = "fun",
            PlayerName = "Gbas",
            DateLastScouted = new DateTime(2023, 10, 21)
        });
        
        lootingLocations.Add(new LootingLocation
        {
            xCoordinates = 6141,
            yCoordinates = 3827,
            FoodAmount = 585874,
            IronAmount = 678473,
            ArmyCount = 43408,
            Power = 2497096, 
            CityLevel = 0,
            ClanName = "SVP",
            PlayerName = "ApeDNPAN9J",
            DateLastScouted = new DateTime(2023, 10, 21)
        });
        
        lootingLocations.Add(new LootingLocation
        {
            xCoordinates = 6072,
            yCoordinates = 3873,
            FoodAmount = 1992189,
            IronAmount = 7022859,
            ArmyCount = 28,
            Power = 591431, 
            CityLevel = 0,
            ClanName = "UBVH",
            PlayerName = "Trida",
            DateLastScouted = new DateTime(2023, 10, 21),
            DateLastAttacked = new DateTime(2023, 10, 21),
            LossesOnLastAttack = 40
        });
        
        lootingLocations.Add(new LootingLocation
        {
            xCoordinates = 5989,
            yCoordinates = 3760,
            FoodAmount = 0,
            IronAmount = 560164,
            ArmyCount = 23476,
            Power = 282999, 
            CityLevel = 0,
            ClanName = "Osir",
            PlayerName = "ApePNLD7DN",
            DateLastScouted = new DateTime(2023, 10, 21)
        });
        
        lootingLocations.Add(new LootingLocation
        {
            xCoordinates = 6172,
            yCoordinates = 3697,
            FoodAmount = 57674,
            IronAmount = 53886,
            ArmyCount = 2397,
            Power = 18562, 
            CityLevel = 0,
            ClanName = "",
            PlayerName = "ApeG56LAG3",
            DateLastScouted = new DateTime(2023, 10, 21)
        });
        
        lootingLocations.Add(new LootingLocation
        {
            xCoordinates = 6112,
            yCoordinates = 3644,
            FoodAmount = 257000,
            IronAmount = 148222,
            ArmyCount = 16122,
            Power = 188440, 
            CityLevel = 0,
            ClanName = "",
            PlayerName = "Zaharah",
            DateLastScouted = new DateTime(2023, 10, 21)
        });
        
        lootingLocations.Add(new LootingLocation
        {
            xCoordinates = 6090,
            yCoordinates = 3645,
            FoodAmount = 566528,
            IronAmount = 573009,
            ArmyCount = 4102,
            Power = 33491, 
            CityLevel = 0,
            ClanName = "CQG",
            PlayerName = "Piorun",
            DateLastScouted = new DateTime(2023, 10, 21)
        });
        
        lootingLocations.Add(new LootingLocation
        {
            xCoordinates = 6065,
            yCoordinates = 3519,
            FoodAmount = 175054,
            IronAmount = 551079,
            ArmyCount = 0,
            Power = 72539, 
            CityLevel = 0,
            ClanName = "",
            PlayerName = "Joker",
            DateLastScouted = new DateTime(2023, 10, 21)
        });
        
        lootingLocations.Add(new LootingLocation
        {
            xCoordinates = 6138,
            yCoordinates = 3497,
            FoodAmount = 6134125,
            IronAmount = 7665103,
            ArmyCount = 13995,
            Power = 119308, 
            CityLevel = 0,
            ClanName = "",
            PlayerName = "Ape3DNZPDL",
            DateLastScouted = new DateTime(2023, 10, 21)
        });
        
        lootingLocations.Add(new LootingLocation
        {
            xCoordinates = 6236,
            yCoordinates = 3525,
            FoodAmount = 104818,
            IronAmount = 78602,
            ArmyCount = 1544,
            Power = 9139, 
            CityLevel = 0,
            ClanName = "",
            PlayerName = "Ape6WV3GV2",
            DateLastScouted = new DateTime(2023, 10, 21)
        });
        
        lootingLocations.Add(new LootingLocation
        {
            xCoordinates = 6163,
            yCoordinates = 3562,
            FoodAmount = 101379,
            IronAmount = 107215,
            ArmyCount = 1526,
            Power = 9190, 
            CityLevel = 0,
            ClanName = "",
            PlayerName = "ApeZ9NPP45",
            DateLastScouted = new DateTime(2023, 10, 21)
        });
        
        lootingLocations.Add(new LootingLocation
        {
            xCoordinates = 6155,
            yCoordinates = 3544,
            FoodAmount = 32976,
            IronAmount = 1281689,
            ArmyCount = 100750,
            Power = 1313749, 
            CityLevel = 0,
            ClanName = "BHC",
            PlayerName = "ApeVZGPLMP",
            DateLastScouted = new DateTime(2023, 10, 21)
        });
        
        lootingLocations.Add(new LootingLocation
        {
            xCoordinates = 6065,
            yCoordinates = 3519,
            FoodAmount = 175054,
            IronAmount = 551079,
            ArmyCount = 0,
            Power = 72539, 
            CityLevel = 0,
            ClanName = "",
            PlayerName = "Joker",
            DateLastScouted = new DateTime(2023, 10, 21)
        });
        
        #endregion

        var weakPlayers = lootingLocations
            .Where(x => x.ArmyCount < 25000)
            .OrderByDescending(x => x.TotalResources)
            .ToList();
        
        
        // TODO: Make this downloadable in a CSV file
        // TODO: Group them by the nearest clan HQ
        // TODO: Be able to export a journey that keeps each next target within visible range, working through in circles for each cluster to minimise teleports
        // TODO: Add stats like estimated losses given various power levels, when to recommend using unit skills etc.
        // TODO: Journeys should take into account troop load
        // Nov3 - lost 1000 troops at 27.7m power taking on 17k troops.  Need to use artillery to hit targets of this strength if they're worth it.
        
        // TODO: Add a feature so that it can group them by nearby teleport location, e.g. get me the best spot with the most resources within 1 minutes march

        var orderedLocations = lootingLocations
            .OrderBy(x => x.FoodAmount + x.IronAmount)
            .ToList();

        return orderedLocations;
    }
}