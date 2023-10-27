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

        var weakPlayers = lootingLocations
            .Where(x => x.ArmyCount < 25000)
            .OrderByDescending(x => x.TotalResources)
            .ToList();
        
        
        // TODO: Make this downloadable in a CSV file
        // TODO: Group them by the nearest clan HQ
        
        // TODO: Add a feature so that it can group them by nearby teleport location, e.g. get me the best spot with the most resources within 1 minutes march

        var orderedLocations = lootingLocations
            .OrderBy(x => x.FoodAmount + x.IronAmount)
            .ToList();

        return orderedLocations;
    }
}