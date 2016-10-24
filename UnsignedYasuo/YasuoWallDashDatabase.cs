using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EloBuddy.SDK;
using SharpDX;

namespace UnsignedYasuo
{
    class YasuoWallDashDatabase
    {
        public static List<WallDash> wallDashDatabase = new List<WallDash>()
        {
            #region blueSide
            //raptors
            new WallDash() { unitName = "SRU_Razorbeak3.1.1", startPosition = new Vector3(6854f, 5462f, 53.52988f), dashUnitPosition = new Vector3(6823.895f, 5507.756f, 54.78283f), endPosition = new Vector3(6592.917f, 5858.813f, 0f)},
            new WallDash() { unitName = "SRU_RazorbeakMini3.1.2", startPosition = new Vector3(6806f, 5502f, 54.2442f), dashUnitPosition = new Vector3(6924f, 5608f, 58.1841f), endPosition = new Vector3(7159.362f, 5819.427f, 0f)},
            new WallDash() { unitName = "SRU_Razorbeak3.1.1", startPosition = new Vector3(6904f, 5572f, 57.34658f), dashUnitPosition = new Vector3(6824f, 5508f, 54.80014f), endPosition = new Vector3(6533.087f, 5275.27f, 0f)},
            new WallDash() { unitName = "SRU_RazorbeakMini3.1.3", startPosition = new Vector3(6988f, 5450f, 53.54608f), dashUnitPosition = new Vector3(7062f, 5500f, 55.02777f), endPosition = new Vector3(7381.58f, 5715.932f, 0f)},

            //red
            new WallDash() { unitName = "SRU_RedMini4.1.2", startPosition = new Vector3(7622f, 4108f, 54.40757f), dashUnitPosition = new Vector3(7624.128f, 4181.374f, 54.17896f), endPosition = new Vector3(7635.773f, 4582.8f, 0f)},
            new WallDash() { unitName = "SRU_RedMini4.1.2", startPosition = new Vector3(7666f, 4130f, 54.26162f), dashUnitPosition = new Vector3(7624f, 4182f, 54.17536f), endPosition = new Vector3(7367.54f, 4499.522f, 0f)},
            new WallDash() { unitName = "SRU_RedMini4.1.3", startPosition = new Vector3(7808f, 3936f, 53.91248f), dashUnitPosition = new Vector3(7918f, 3882f, 53.72088f), endPosition = new Vector3(8234.393f, 3726.68f, 0f)},

            //krugs
            new WallDash() { unitName = "SRU_KrugMini5.1.2", startPosition = new Vector3(8222f, 3158f, 51.64838f), dashUnitPosition = new Vector3(8323.471f, 2754.948f, 51.13f), endPosition = new Vector3(8337.966f, 2697.373f, 0f)},
            new WallDash() { unitName = "SRU_Krug5.1.1", startPosition = new Vector3(8536f, 2634f, 50.17058f), dashUnitPosition = new Vector3(8532f, 2738f, 50.58475f), endPosition = new Vector3(8517.744f, 3108.649f, 0f)},
            new WallDash() { unitName = "SRU_KrugMini5.1.2", startPosition = new Vector3(8368f, 2698f, 51.05234f), dashUnitPosition = new Vector3(8322f, 2756f, 51.13f), endPosition = new Vector3(8072.837f, 3070.161f, 0f)},

            //wolves
            new WallDash() { unitName = "SRU_MurkwolfMini2.1.2", startPosition = new Vector3(3904f, 6436f, 52.46461f), dashUnitPosition = new Vector3(3982f, 6444f, 52.46561f), endPosition = new Vector3(4376.521f, 6484.464f, 0f)},
            new WallDash() { unitName = "SRU_MurkwolfMini2.1.2", startPosition = new Vector3(3932f, 6568f, 52.464f), dashUnitPosition = new Vector3(3982f, 6444f, 52.46561f), endPosition = new Vector3(4109.635f, 6127.465f, 0f)},
            new WallDash() { unitName = "SRU_MurkwolfMini2.1.3", startPosition = new Vector3(3748f, 6498f, 52.46224f), dashUnitPosition = new Vector3(3732f, 6594f, 52.46144f), endPosition = new Vector3(3669.91f, 6966.537f, 0f)},

            //blue
            new WallDash() { unitName = "SRU_Blue1.1.1", startPosition = new Vector3(3832f, 7864f, 51.94576f), dashUnitPosition = new Vector3(3858f, 7886f, 51.89909f), endPosition = new Vector3(4194.608f, 8170.823f, 0f)},
            new WallDash() { unitName = "SRU_Blue1.1.1", startPosition = new Vector3(3810f, 7962f, 52.1772f), dashUnitPosition = new Vector3(3856.489f, 7886.054f, 51.89909f), endPosition = new Vector3(4057.989f, 7556.875f, 0f)},
            new WallDash() { unitName = "SRU_BlueMini21.1.3", startPosition = new Vector3(3668f, 7954f, 53.27195f), dashUnitPosition = new Vector3(3624f, 7822f, 53.89835f), endPosition = new Vector3(3517.792f, 7503.375f, 0f)},
            
            //gromp
            new WallDash() { unitName = "SRU_Gromp13.1.1", startPosition = new Vector3(2204f, 8384f, 51.77714f), dashUnitPosition = new Vector3(2110.628f, 8450.984f, 51.77732f), endPosition = new Vector3(1818.045f, 8660.883f, 0f)},
            new WallDash() { unitName = "SRU_Gromp13.1.1", startPosition = new Vector3(2184f, 8506f, 51.77744f), dashUnitPosition = new Vector3(2112f, 8450f, 51.77731f), endPosition = new Vector3(1809.058f, 8214.378f, 0f)},
            new WallDash() { unitName = "SRU_Gromp13.1.1", startPosition = new Vector3(2100f, 8512f, 51.77745f), dashUnitPosition = new Vector3(2112f, 8450f, 51.77731f), endPosition = new Vector3(2190.26f, 8045.655f, 0f)},
            #endregion
            //do second gromp pos  and hard blue dash

            #region redSide
            //raptors
            new WallDash() { unitName = "SRU_Razorbeak9.1.1", startPosition = new Vector3(7962f, 9494f, 52.3327f), dashUnitPosition = new Vector3(7986f, 9470f, 52.34794f), endPosition = new Vector3(8297.876f, 9158.124f, 0f)},
            new WallDash() { unitName = "SRU_Razorbeak9.1.1", startPosition = new Vector3(7840f, 9410f, 52.38034f), dashUnitPosition = new Vector3(7986f, 9470f, 52.34794f), endPosition = new Vector3(8279.347f, 9590.554f, 0f)},
            new WallDash() { unitName = "SRU_RazorbeakMini9.1.2", startPosition = new Vector3(7910f, 9384f, 52.41017f), dashUnitPosition = new Vector3(7886f, 9312f, 52.44501f), endPosition = new Vector3(7759.792f, 8933.375f, 0f)},
            new WallDash() { unitName = "SRU_RazorbeakMini9.1.4", startPosition = new Vector3(7788f, 9578f, 52.29733f), dashUnitPosition = new Vector3(7854f, 9610f, 52.26575f), endPosition = new Vector3(8215.412f, 9785.229f, 0f)},
            new WallDash() { unitName = "SRU_RazorbeakMini9.1.3", startPosition = new Vector3(7828f, 9482f, 52.35191f), dashUnitPosition = new Vector3(7756f, 9450f, 52.36539f), endPosition = new Vector3(7393.939f, 9289.084f, 0f)},

            //red
            new WallDash() { unitName = "SRU_RedMini10.1.2", startPosition = new Vector3(7242f, 10822f, 56.71824f), dashUnitPosition = new Vector3(7266f, 10794f, 56.73206f), endPosition = new Vector3(7551.126f, 10461.35f, 0f)},
            new WallDash() { unitName = "SRU_RedMini10.1.3", startPosition = new Vector3(7002f, 10898f, 55.99867f), dashUnitPosition = new Vector3(6918f, 11002f, 55.99971f), endPosition = new Vector3(6703.54f, 11267.52f, 0f)},
            new WallDash() { unitName = "SRU_Red10.1.1", startPosition = new Vector3(7018f, 10804f, 56.01325f), dashUnitPosition = new Vector3(7018f, 10774f, 56.01078f), endPosition = new Vector3(7018f, 10329f, 0f)},

            //krugs
            new WallDash() { unitName = "SRU_Krug11.1.1", startPosition = new Vector3(6322f, 12286f, 56.4768f), dashUnitPosition = new Vector3(6318f, 12146f, 56.4768f), endPosition = new Vector3(6308.434f, 11811.19f, 0f)},
            new WallDash() { unitName = "SRU_KrugMini11.1.2", startPosition = new Vector3(6624f, 11756f, 53.82994f), dashUnitPosition = new Vector3(6547.092f, 12156.46f, 56.4768f), endPosition = new Vector3(6534.414f, 12222.48f, 0f)},
            new WallDash() { unitName = "SRU_KrugMini11.1.2", startPosition = new Vector3(6468f, 12262f, 56.4768f), dashUnitPosition = new Vector3(6548f, 12156f, 56.4768f), endPosition = new Vector3(6754.143f, 11882.86f, 0f)},
            new WallDash() { unitName = "SRU_Krug11.1.1", startPosition = new Vector3(6330.249f, 11718.02f, 55.62539f), dashUnitPosition = new Vector3(6318f, 12146f, 56.4768f), endPosition = new Vector3(6316.66f, 12192.83f, 0f)},

            //wolves
            new WallDash() { unitName = "SRU_MurkwolfMini8.1.2", startPosition = new Vector3(11200f, 7872f, 52.20447f), dashUnitPosition = new Vector3(11058f, 8216f, 62.23421f), endPosition = new Vector3(11018.76f, 8311.063f, 0f)},
            new WallDash() { unitName = "SRU_MurkwolfMini8.1.3", startPosition = new Vector3(10422f, 8556f, 64.58703f), dashUnitPosition = new Vector3(10808.15f, 8387.408f, 63.01288f), endPosition = new Vector3(10857.32f, 8365.942f, 0f)},
            new WallDash() { unitName = "SRU_MurkwolfMini8.1.3", startPosition = new Vector3(10836f, 8374f, 62.85113f), dashUnitPosition = new Vector3(10808f, 8386f, 63.01181f), endPosition = new Vector3(10399.41f, 8561.111f, 0f)},
            new WallDash() { unitName = "SRU_MurkwolfMini8.1.2", startPosition = new Vector3(11006f, 8298f, 62.21604f), dashUnitPosition = new Vector3(11058f, 8216f, 62.23421f), endPosition = new Vector3(11260.38f, 7896.858f, 0f)},
            new WallDash() { unitName = "SRU_MurkwolfMini8.1.2", startPosition = new Vector3(11074f, 8238f, 62.02985f), dashUnitPosition = new Vector3(11058f, 8216f, 62.23421f), endPosition = new Vector3(10794.62f, 7853.851f, 0f)},
            new WallDash() { unitName = "SRU_MurkwolfMini8.1.3", startPosition = new Vector3(10872f, 8406f, 62.80222f), dashUnitPosition = new Vector3(10808f, 8386f, 63.01181f), endPosition = new Vector3(10418.62f, 8264.319f, 0f)},

            //blue
            new WallDash() { unitName = "SRU_BlueMini7.1.2", startPosition = new Vector3(11072f, 7494f, 52.20373f), dashUnitPosition = new Vector3(11140.33f, 7064.444f, 51.72464f), endPosition = new Vector3(11146.62f, 7024.897f, 0f)},
            new WallDash() { unitName = "SRU_Blue7.1.1", startPosition = new Vector3(10962f, 6908f, 51.7229f), dashUnitPosition = new Vector3(10930f, 6992f, 51.7229f), endPosition = new Vector3(10792.9f, 7351.882f, 0f)},
            new WallDash() { unitName = "SRU_Blue7.1.1", startPosition = new Vector3(10978f, 7012f, 51.7233f), dashUnitPosition = new Vector3(10930f, 6992f, 51.7229f), endPosition = new Vector3(10539.54f, 6829.308f, 0f)},
            new WallDash() { unitName = "SRU_BlueMini7.1.2", startPosition = new Vector3(11106f, 7008f, 51.72417f), dashUnitPosition = new Vector3(11140f, 7066f, 51.72464f), endPosition = new Vector3(11346.22f, 7417.782f, 0f)},

            //gromp
            new WallDash() { unitName = "SRU_Gromp14.1.1", startPosition = new Vector3(12698f, 6396f, 51.69683f), dashUnitPosition = new Vector3(12702f, 6444f, 51.69143f), endPosition = new Vector3(12737.45f, 6869.359f, 0f)},
            new WallDash() { unitName = "SRU_Gromp14.1.1", startPosition = new Vector3(12652f, 6468f, 51.712f), dashUnitPosition = new Vector3(12702f, 6444f, 51.69143f), endPosition = new Vector3(13080.22f, 6262.453f, 0f)},
            new WallDash() { unitName = "SRU_Gromp14.1.1", startPosition = new Vector3(13152f, 6444f, 54.56144f), dashUnitPosition = new Vector3(12702f, 6444f, 51.69143f), endPosition = new Vector3(12677f, 6444f, 0f)},
            #endregion
        };
    }
}
