using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

//namespace VerifyServerWebApp.PhysxVerify
//{
    public class MainLogicHelp
    {
        public const float club_min_accuracy_landPoint_f = 1.43f; //球杆最小精度 0 , landpoint效果值//
        public const float club_max_accuracy_landPoint_f = 0.2f; //球杆最大精度 100 ,landpoint效果值// 
        /// <summary>
        /// clubAccuracy 0-100;
        /// </summary>
        public static float GetClubAccuracyEffectToLandPoint(float clubAccuracy)
        {
            return club_min_accuracy_landPoint_f + clubAccuracy / 100 * (club_max_accuracy_landPoint_f - club_min_accuracy_landPoint_f);
        }
    }
//}
