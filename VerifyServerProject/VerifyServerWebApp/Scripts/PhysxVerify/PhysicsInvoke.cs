using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace AuthServer.PhysxVerify
{
    [StructLayout(LayoutKind.Sequential)]
    public struct RayCastHit
    {
        public IntPtr   actor;//PxRigidActor*
        public PxVec3    point;
        public PxVec3     normal;
        public float      distance;
        public IntPtr     shape;//PxShape*
    }

    enum PxForceMode
	{
		eFORCE,				//!< parameter has unit of mass * distance/ time^2, i.e. a force
		eIMPULSE,			//!< parameter has unit of mass * distance /time
		eVELOCITY_CHANGE,	//!< parameter has unit of distance / time, i.e. the effect is mass independent: a velocity change.
		eACCELERATION		//!< parameter has unit of distance/ time^2, i.e. an acceleration. It gets treated just like a force except the mass is not divided out before integration.
	};

    [StructLayout(LayoutKind.Sequential)]
    public struct PxTransform
    {
        public PxQuat q;
        public PxVec3 p;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct PxQuat
    {
        public PxQuat(float x1 = 0, float y1 = 0, float z1 = 0, float w1 = 1)
        {
            x = x1;
            y = y1;
            z = z1;
            w = w1;
        }

        public PxQuat(VerifyServerWebApp.UnityEngine.Quaternion q)
        {
            x = q.x;
            y = q.y;
            z = q.z;
            w = q.w;
        }

        public static VerifyServerWebApp.UnityEngine.Quaternion toQuat(PxQuat q)
        {
            return new VerifyServerWebApp.UnityEngine.Quaternion(q.x, q.y, q.z, q.w);
        }


        public float x, y, z, w;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct PxVec3
    {
        public PxVec3(float x1 = 0, float y1 = 0, float z1 = 0)
        {
            x = x1;
            y = y1;
            z = z1;
        }

        public PxVec3(VerifyServerWebApp.UnityEngine.Vector3 p)
        {
            x = p.x;
            y = p.y;
            z = p.z;
        }

        public static PxVec3 zero(){return new PxVec3();}

        public static VerifyServerWebApp.UnityEngine.Vector3 ToVector3(PxVec3 p)
        {
            return new VerifyServerWebApp.UnityEngine.Vector3(p.x, p.y, p.z);
        }

        public override string ToString(){
            return string.Format("({0},{1},{2})", x, y, z);
        }
        public float x, y, z;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct PxContactPoint
    {
        public PxVec3 point;
        public PxVec3 normal;
        public float separation;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct PxCollision
    {
        public int contactCount;
        public IntPtr contacts;
        public IntPtr pRigidbody;
        public IntPtr pTransform;
        public char dominanceGroup;	
        public IntPtr name;
    }
    
    [StructLayout(LayoutKind.Sequential)]
    public class PxCollider
    {
        public IntPtr pTransform;
        public char dominanceGroup;
        public IntPtr name;
    }

    public class PhysicsInvoke
    {
        public delegate void DelAction(float dt);

        public delegate void ConlisionAction(IntPtr p);

        public delegate void TriggerAction(IntPtr p);

        /// <summary>
        /// 创建指向校验管理类对象的指针
        /// </summary>
        /// <returns>VerifyMgr*</returns>
        [DllImport("libVerifyLibrary")]
        static extern IntPtr  CreateVerifyMgr(uint iSceneConcurrencyCnt);

        /// <summary>
        /// 根据场景id获得一个闲置的索引
        /// </summary>
        /// <param name="sceneId"></param>
        /// <returns></returns>
        [DllImport("libVerifyLibrary")]
        static extern int GetFreeSlotIndex(IntPtr pVerifyMgr, int sceneId);

        /// <summary>
        /// 计算结束后，归还闲置索引
        /// </summary>
        /// <param name="sceneId"></param>
        /// <param name="slotIndex"></param>
        [DllImport("libVerifyLibrary")]
        static extern void BackSlotIndex(IntPtr pVerifyMgr, int sceneId, int slotIndex);

        /// <summary>
        /// 获得一个场景模拟对象
        /// </summary>
        /// <param name="slotIndex"></param>
        /// <returns>SimulationMgr*</returns>
        [DllImport("libVerifyLibrary")]
        static extern IntPtr GetSimulationMgr(IntPtr pVerifyMgr, int slotIndex);

        /// <summary>
        /// 模拟对象初始化
        /// </summary>
        /// <param name="slotIndex"></param>
        /// <returns></returns>
        [DllImport("libVerifyLibrary")]
        [return: MarshalAs(UnmanagedType.I1)]
        static extern bool InitPhysics(IntPtr ptrSimulateMgr);

        /// <summary>
        /// 加载场景数据
        /// </summary>
        /// <param name="ptrSimulateMgr"></param>
        /// <param name="filePath"></param>
        /// <returns></returns>
        [DllImport("libVerifyLibrary")]
        [return: MarshalAs(UnmanagedType.I1)]
        static extern bool LoadXMLDataFile(IntPtr ptrSimulateMgr, IntPtr filePath);

        /// <summary>
        /// 创建球
        /// </summary>
        /// <param name="ptrSimulateMgr"></param>
        /// <param name="radius"></param>
        /// <param name="mass"></param>
        /// <param name="pos"></param>
        /// <param name="quat"></param>
        /// <returns></returns>
        [DllImport("libVerifyLibrary")]
        [return: MarshalAs(UnmanagedType.I1)]
        static extern bool CreateBall(IntPtr ptrSimulateMgr, float radius, float mass, PxVec3 pos, PxQuat quat);

        /// <summary>
        /// 物理模拟
        /// </summary>
        /// <param name="ptrSimulateMgr"></param>
        [DllImport("libVerifyLibrary")]
        static extern void StepPhysics(IntPtr ptrSimulateMgr);

        /// <summary>
        /// 设置旗杆状态
        /// </summary>
        /// <param name="pSimulationMgr">SimulationMgr*</param>
        /// <param name="active"></param>
        [DllImport("libVerifyLibrary")]
        static extern void SetFlagpoleActive(IntPtr  pSimulationMgr, bool active);

        /// <summary>
        /// 进程结束前调用，释放场景内存
        /// </summary>
        [DllImport("libVerifyLibrary")]
        static extern void Release(IntPtr pVerifyMgr);

        /// <summary>
        /// 初始化回调函数
        /// </summary>
        /// <param name="ptrSimulateMgr"></param>
        /// <param name="pFixedUpdate"></param>
        /// <param name="pCollisionEnter"></param>
        /// <param name="pCollisionStay"></param>
        /// <param name="pCollisionExit"></param>
        /// <param name="pTriggerEnter"></param>
        /// <param name="pTriggerStay"></param>
        /// <param name="pTriggerExit"></param>
        [DllImport("libVerifyLibrary")]
        static extern void InitCallback(IntPtr ptrSimulateMgr, DelAction pFixedUpdate, 
        ConlisionAction pCollisionEnter, ConlisionAction pCollisionStay, ConlisionAction pCollisionExit,
        TriggerAction pTriggerEnter, TriggerAction pTriggerStay, TriggerAction pTriggerExit);

        /// <summary>
        /// 清空回调
        /// </summary>
        /// <param name="pSimulationMgr">SimulationMgr*</param>
        [DllImport("libVerifyLibrary")]
        static extern void CleanCallback(IntPtr  pSimulationMgr);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pSimulationMgr">SimulationMgr*</param>
        /// <returns>RayCastHit*</returns>
        [DllImport("libVerifyLibrary")]
        static extern IntPtr GetRaycasthit(IntPtr pSimulationMgr);

        /// <summary>
        /// 射线检测
        /// </summary>
        /// <param name="ptrSimulateMgr"></param>
        /// <param name="origin"></param>
        /// <param name="unitDir"></param>
        /// <param name="distance"></param>
        /// <param name="layerMask"></param>
        /// <param name="pRaycasthit"></param>
        /// <returns></returns>
        [DllImport("libVerifyLibrary")]
        static extern bool RaycastHit(IntPtr ptrSimulateMgr, PxVec3 origin, PxVec3 unitDir, float distance, int layerMask, IntPtr pRaycasthit);

        /// <summary>
        /// 球形射线检测
        /// </summary>
        /// <param name="ptrSimulateMgr"></param>
        /// <param name="radius"></param>
        /// <param name="pOrigin"></param>
        /// <param name="pDir"></param>
        /// <param name="distance"></param>
        /// <param name="layerMask"></param>
        /// <param name="pRaycasthit"></param>
        /// <returns></returns>
        [DllImport("libVerifyLibrary")]
        static extern bool SphereCast(IntPtr ptrSimulateMgr, float radius, PxVec3 pOrigin, PxVec3 pDir, float distance, int layerMask, IntPtr pRaycasthit);

        /// <summary>
        /// 获取球的刚体指针
        /// </summary>
        /// <param name="ptrSimulateMgr"></param>
        /// <returns></returns>
        [DllImport("libVerifyLibrary")]
        static extern IntPtr GetRigidbody(IntPtr ptrSimulateMgr);
       
        /// <summary>
        /// 获取刚体的角速度
        /// </summary>
        /// <param name="pRigid"></param>
        /// <returns></returns>
        [DllImport("libVerifyLibrary")]
        static extern PxVec3 GetAngularVelocity(IntPtr pRigid);

        /// <summary>
        /// 设置刚体的角速度
        /// </summary>
        /// <param name="pRigid"></param>
        /// <param name="pV"></param>
        [DllImport("libVerifyLibrary")]
        static extern void SetAngularVelocity(IntPtr pRigid, PxVec3 pV);

        /// <summary>
        /// 获取刚体的线速度
        /// </summary>
        /// <param name="pRigid"></param>
        /// <returns></returns>
        [DllImport("libVerifyLibrary")]
        static extern PxVec3 GetLinearVelocity(IntPtr pRigid);

        /// <summary>
        /// 设置刚体的线速度
        /// </summary>
        /// <param name="pRigid"></param>
        /// <param name="pV"></param>
        [DllImport("libVerifyLibrary")]
        static extern void SetLinearVelocity(IntPtr pRigid, PxVec3 pV);

        /// <summary>
        /// 刚体是否睡眠
        /// </summary>
        /// <param name="pRigid"></param>
        /// <returns></returns>
        [DllImport("libVerifyLibrary")]
        [return: MarshalAs(UnmanagedType.I1)]
        static extern bool IsSleeping(IntPtr pRigid);

        /// <summary>
        /// 唤醒刚体
        /// </summary>
        /// <param name="pRigid"></param>
        [DllImport("libVerifyLibrary")]
        static extern void WakeUp(IntPtr pRigid);

        /// <summary>
        /// 设置刚体动力学状态
        /// </summary>
        /// <param name="pRigid"></param>
        /// <param name="isKinematic"></param>
        [DllImport("libVerifyLibrary")]
        static extern void SetKinematic(IntPtr pRigid, bool isKinematic);

        /// <summary>
        /// 获得刚体动力学状态
        /// </summary>
        /// <param name="pRigid"></param>
        /// <returns></returns>
        [DllImport("libVerifyLibrary")]
        [return: MarshalAs(UnmanagedType.I1)]
        static extern bool GetKinematic(IntPtr pRigid);

        /// <summary>
        /// 设置刚体质量
        /// </summary>
        /// <param name="pRigid"></param>
        /// <returns></returns>
        [DllImport("libVerifyLibrary")]
        static extern float GetMass(IntPtr pRigid);

        /// <summary>
        /// 获得刚体质量
        /// </summary>
        /// <param name="pRigid"></param>
        /// <param name="mass"></param>
        [DllImport("libVerifyLibrary")]
        static extern void SetMass(IntPtr pRigid,float mass);

        /// <summary>
        /// 设置刚体的transform
        /// </summary>
        /// <param name="pRigid"></param>
        /// <param name="pTrans"></param>
        [DllImport("libVerifyLibrary")]
        static extern void SetGlobalPos(IntPtr pRigid, PxTransform pTrans);

        /// <summary>
        /// 获得刚体的transform
        /// </summary>
        /// <param name="pRigid"></param>
        /// <returns></returns>
        [DllImport("libVerifyLibrary")]
        static extern PxTransform GetGlobalPos(IntPtr pRigid);//return PxTransform

        /// <summary>
        /// 获得actor name
        /// </summary>
        /// <param name="pActor"></param>
        /// <returns></returns>
        [DllImport("libVerifyLibrary")]
        static extern IntPtr GetActorName(IntPtr pActor);

        /// <summary>
        /// 空指针判断
        /// </summary>
        /// <param name="pRigid"></param>
        /// <returns></returns>
        [DllImport("libVerifyLibrary")]
        [return: MarshalAs(UnmanagedType.I1)]
        static extern bool IsNullRigidbody(IntPtr pRigid);
        

        [DllImport("libVerifyLibrary")]
        static extern void PrintfShapeMaterial(IntPtr pShape);

        /// <summary>
        /// 检测是否是trigger
        /// </summary>
        /// <param name="pShape">PxShape*</param>
        /// <returns></returns>
        [DllImport("libVerifyLibrary")]
        [return: MarshalAs(UnmanagedType.I1)]
        static extern bool IsTrigger(IntPtr pShape);

        //根据场景id得到空闲索引, 然后根据空闲索引去找对应的模拟对象，进行物理模拟
        public static int GetIndex(int sceneId)
        {
            return GetFreeSlotIndex(VerifyMgrProxy.Instance.ptrVerifyMgr, sceneId);
        }

        public static void BackIndex(int sceneId, int slotIndex)
        {
            BackSlotIndex(VerifyMgrProxy.Instance.ptrVerifyMgr, sceneId, slotIndex);
        }

        public static IntPtr getSimulationMgr(int slotIndex)
        {
            return GetSimulationMgr(VerifyMgrProxy.Instance.ptrVerifyMgr, slotIndex);
        }

        public static bool Init(IntPtr ptrSimulateMgr)
        {
            return InitPhysics(ptrSimulateMgr);
        }

        public static bool LoadSceneFile(IntPtr ptrSimulateMgr, string path)
        {
            IntPtr pathPtr = Marshal.StringToHGlobalAnsi(path);
            bool res = LoadXMLDataFile(ptrSimulateMgr, pathPtr);
            Marshal.FreeHGlobal(pathPtr);
            return res;
        }

        // static IntPtr PxVec3ToPtr(PxVec3 v)
        // {
        //     IntPtr ptr = Marshal.AllocHGlobal(Marshal.SizeOf(v));
        //     Marshal.StructureToPtr<PxVec3>(v, ptr, false);
        //     return ptr;
        // }

        // static IntPtr PxQuatToPtr(PxQuat quat)
        // {
        //     IntPtr ptr = Marshal.AllocHGlobal(Marshal.SizeOf(quat));
        //     Marshal.StructureToPtr<PxQuat>(quat, ptr, false);
        //     return ptr;
        // }


        // static void FreeHGPtr(IntPtr ptr)
        // {
        //     Marshal.FreeHGlobal(ptr);
        // }

        public static void createBall(IntPtr ptrSimulateMgr, float radius, float mass, PxVec3 pos, PxQuat quat)
        {
            CreateBall(ptrSimulateMgr, radius, mass, pos, quat);
        }

        public static void SimulateStep(IntPtr ptrSimulateMgr)
        {
            StepPhysics(ptrSimulateMgr);
        }

        public static void setFlagpoleActive(IntPtr  pSimulationMgr, bool active)
        {
            SetFlagpoleActive(pSimulationMgr, active);
        }

        /// <summary>
        /// 关闭服务器时，释放对象
        /// </summary>
        public static void ReleasePhysx(IntPtr pVerifyMgr)
        {
            Release(pVerifyMgr);
        }

        public static void RegisterCallback(IntPtr ptrSimulateMgr, DelAction fixedUpdate, ConlisionAction colEnter, ConlisionAction colStay, ConlisionAction colExit,
        TriggerAction trigEnter, TriggerAction trigStay, TriggerAction trigExit)
        {
            // Console.WriteLine("register call back");
            InitCallback(ptrSimulateMgr, fixedUpdate, colEnter, colStay, colExit, trigEnter, trigStay, trigExit);
        }

        public static void cleanCallback(IntPtr ptrSimulateMgr)
        {
            CleanCallback(ptrSimulateMgr);
        }

        /// <summary>
        /// 获得存取射线结果结构体
        /// </summary>
        /// <param name="ptrSimulateMgr"></param>
        /// <returns></returns>
        public static IntPtr getRaycasthit(IntPtr ptrSimulateMgr)
        {
            return GetRaycasthit(ptrSimulateMgr);
        }

        public static bool RaycastHitCall(IntPtr ptrSimulateMgr, PxVec3 origin, PxVec3 unitDir, float dis, int layerMask, IntPtr pRaycasthit)
        {
            return RaycastHit(ptrSimulateMgr, origin, unitDir, dis, layerMask, pRaycasthit);
        }

        public static bool SphereRaycastHitCall(IntPtr ptrSimulateMgr, float radius, PxVec3 origin, PxVec3 unitDir, float dis, int layerMask, IntPtr pRaycasthit)
        {
            return SphereCast(ptrSimulateMgr, radius, origin, unitDir, dis, layerMask, pRaycasthit);
        }

        public static IntPtr GetRigidbodyPtr(IntPtr ptrSimulateMgr)
        {
            return GetRigidbody(ptrSimulateMgr);
        }

        public static PxVec3 getAngularVelocity(IntPtr pRigidbody)
        {
            return GetAngularVelocity(pRigidbody);
        }
        public static void setAngularVelocity(IntPtr pRigidbody, PxVec3 value)
        {
            SetAngularVelocity(pRigidbody, value);
        }

        public static PxVec3 getLinearVelocity(IntPtr pRigidbody)
        {
            return GetLinearVelocity(pRigidbody);
        }
        public static void setLinearVelocity(IntPtr pRigidbody, PxVec3 value)
        {
            SetLinearVelocity(pRigidbody, value);
        }

        public static bool isSleeping(IntPtr pRigidbody)
        {
            return IsSleeping(pRigidbody);
        }

        public static void wakeUp(IntPtr pRigidbody)
        {
            WakeUp(pRigidbody);
        }

        public static bool getIsKinematic(IntPtr pRigidbody)
        {
            return GetKinematic(pRigidbody);
        }
           
        public static void setIsKinematic(IntPtr pRigidbody, bool value)
        {
            SetKinematic(pRigidbody, value);
        }

        public static float getMass(IntPtr pRigidbody)
        {
            return GetMass(pRigidbody);
        }
           
        public static void setMass(IntPtr pRigidbody, float value)
        {
            SetMass(pRigidbody, value);
        }

        public static void setGlobalPos(IntPtr pRigidbody, PxTransform trans)
        {
            SetGlobalPos(pRigidbody, trans);
        }


        public static PxTransform getGlobalPos(IntPtr pRigid){
            return GetGlobalPos(pRigid);;
        }

        public static string getActorName(IntPtr pActor){
            IntPtr ptrName = GetActorName(pActor);
            return Marshal.PtrToStringAnsi(ptrName);
        }

        public static bool isNullRigidbody(IntPtr pRigid){
            return IsNullRigidbody(pRigid);
        }

        public static void printfShapeMaterial(IntPtr ptrShape){
            PrintfShapeMaterial(ptrShape);
        }

        public static bool isTrigger(IntPtr pShape)
        {
            return IsTrigger(pShape);
        }

        public static IntPtr createVerifyMgr(uint iSceneConcurrencyCnt){
            return CreateVerifyMgr(iSceneConcurrencyCnt);
        }
    }

    public class VerifyMgrProxy
    {
        public IntPtr ptrVerifyMgr{set;get;}
        private static readonly object locker = new object();
        private static VerifyMgrProxy _instance;
        public static VerifyMgrProxy Instance
        {
            get
            {
                if(_instance == null)
                {
                    lock(locker)
                    {
                        if(_instance == null){
                            _instance = new VerifyMgrProxy();
                        }
                    }
                }
                return _instance;
            }
        }
        private VerifyMgrProxy(){
            //每个场景的并发请求数量为25
            ptrVerifyMgr = PhysicsInvoke.createVerifyMgr(50);
        }

        ~VerifyMgrProxy(){
            Console.WriteLine("~~VerifyMgrProxy release");
            PhysicsInvoke.ReleasePhysx(ptrVerifyMgr);
        }
    }
}
