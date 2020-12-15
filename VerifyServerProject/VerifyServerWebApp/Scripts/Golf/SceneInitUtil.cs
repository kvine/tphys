using System.Collections;
using System.Collections.Generic;
using com.golf.proto;
using VerifyServerWebApp.UnityEngine;
public class SceneInitUtil
{

    // private const string root_chang_jing_path = "/root_chang_jing";
    // private const string gong_ju = "gong_ju";
    // private const string water_land_point = "water_land_point";
    // private const string chu_jie_ping_mian = "chu_jie_ping_mian";
    // private const string di_xing_bian_yuan_kuo_zhan = "di_xing_bian_yuan_kuo_zhan";
    // private const string kai_qiu_dian_all = "kai_qiu_dian";
    // private const string kai_qiu_dian_zuo_biao = "zuo_biao_";
    // private const string qiu_dong_zuo_biao = "qiu_dong_zuo_biao";
    // private const string you_xi_qu = "you_xi_qu";
    // private const string jie_nei_qu = "jie_nei_qu";
    // private const string chang_cao_qu_all = "chang_cao_qu";
    // private const string fa_qiu_tai_all = "fa_qiu_tai";
    // private const string fa_qiu_tai_di_mian_all = "di_mian";
    // private const string fa_qiu_tai_qiu_zuo = "qiu_zuo";
    // private const string guo_ling = "guo_ling";
    // private const string guo_ling_bian_yuan = "guo_ling_bian_yuan";
    // private const string qiu_dao_all = "qiu_dao";
    // private const string qiu_dong = "items/qiu_dong";
    // private const string qiu_dong_chu_fa_qi = "items/qiu_dong_chu_fa_qi";
    // private const string sha_keng_all = "sha_keng";
    // private const string jie_wai_qu_di_mian = "jie_wai_qu_di_mian";
    // private const string ma_shu_pai_all = "ma_shu_pai";
    // private const string qi_gan_all = "qi_gan";
    // private const string qi_gan_gan = "gan";
    // private const string qi_gan_qi = "qi";
    // private const string shui_all = "shui";
    // private const string shui_single_main = "shui";
    // private const string shui_single_xiao_guo_all = "xiao_guo";
    // private const string shu_all = "shu";
    // private const string shu_single_main = "shu";
    // private const string shu_single_shu_gan_all_boxes = "shu_gan_all_boxes";
    // private const string shu_single_shu_zhi_box = "shu_zhi_box";
    // private const string tian_kong = "tian_kong";
    // private const string zhuang_shi_qu = "zhuang_shi_qu";
    // private const string zhuang_shi_qu_shu = "shu";
    // private const string zhuang_shi_qu_guan_mu = "guan_mu";
    // private const string zhuang_shi_qu_guan_mu_2 = "guan_mu_02";

    // private const string collides_folder_name = "Collides";
    // private const string light_direction = "light_direction";

    // public  List<Vector3> kaiQiuDians = new List<Vector3>();

    // public  float height_euler_normal_y;

    //private AssetManager assetManager;

    //private GameObject qiGan;
    //private GameObject qiGan_Gan;
    //private GameObject qiGan_Qi;
    //private GameObject qiGan_RotateNode;

    //private Transform jieNeiQuTF;

    //private Material guolingMaterial;
    //private Material guoLingBianYuanMaterial;
    //private List<Material> qiuZuoDiMianMaterials = new List<Material>();
    //private List<Material> qiuDaoMaterials = new List<Material>();
    //private List<Material> changCaoQuMaterials = new List<Material>();
    //private List<Material> shaKengMaterials = new List<Material>();

    //private Transform lightDirectTf;

    //public const string unlitTextureShaderName = "Custom/UnlitTexture";

    //private LevelCfgData levelCfgData;

    ///// <summary>
    /////  初始化场景
    ///// </summary>
    ///// <param name="assetManager"></param>
    //public void initScene(AssetManager assetManager)
    //{
    //    this.assetManager = assetManager;

    //    SetLevelCfgData();

    //    AdjustMainCameraFarClip();

    //    GameObject changJingRoot = GameObject.Find(root_chang_jing_path);

    //    initGongJu(changJingRoot.transform);

    //    initYouXiQu(changJingRoot.transform);

    //    //InitZhuangShiQu(changJingRoot.transform);
    //    Set_ZhuangShiQu_VisibleByDevicePerformance();

    //    CombineSceneStatic(changJingRoot.transform);
    //}

    //private void SetLevelCfgData()
    //{
    //    int levelCfgId = 0;
    //    if (LoadingSceneLogic.scenePlayType == ScenePlayType.tutorial)
    //    {
    //        levelCfgId = TutorialInfoUtil.tutorial_level_cfg_id;
    //    }
    //    else if (LoadingSceneLogic.scenePlayType == ScenePlayType.replay)
    //    {
    //        levelCfgId = GolfReplayInfo.GetReplayLevelId();
    //    }
    //    else if (LoadingSceneLogic.scenePlayType == ScenePlayType.pvp)
    //    {
    //        levelCfgId = GolfPVPInfo.GetLevelCfgId();
    //    }

    //    levelCfgData = LevelCfgData.GetDataById(levelCfgId);
    //}

    //private void AdjustMainCameraFarClip()
    //{
    //    SceneAttrCfgData sceneAttrCfgData = SceneAttrCfgData.GetDataBySceneName(levelCfgData.m_ScenesName);
    //    if(sceneAttrCfgData == null)
    //    {
    //        return;
    //    }

    //    Camera mainCamera = Camera.main;
    //    if(mainCamera == null)
    //    {
    //        return;
    //    }

    //    mainCamera.farClipPlane = sceneAttrCfgData.m_CameraFarClipPlane;
    //}

    ///// <summary>
    ///// 初始化场景的工具
    ///// </summary>
    ///// <param name="changJingRootTransForm"></param>
    //private void initGongJu(Transform changJingRootTransForm)
    //{
    //    Transform gongJuTransform = changJingRootTransForm.Find(gong_ju);

    //    {
    //        Transform waterLandPointTf = gongJuTransform.Find(water_land_point);
    //        if(waterLandPointTf != null)
    //        {
    //            waterLandPointTf.gameObject.layer = LayerMask.NameToLayer(Layers.water_land_point);
    //            waterLandPointTf.renderer.enabled = true;
    //            waterLandPointTf.renderer.castShadows = false;
    //            waterLandPointTf.renderer.receiveShadows = false;
    //            waterLandPointTf.renderer.sharedMaterial = SimpleAssetManager.Instance.GetLocalAsset(ResourceInfoUtil.tag_water_land_mat, "Materials/", "WaterLandMat") as Material;
    //        }
    //    }

    //    {
    //        //出界平面
    //        GameObject chuJiePingMian = gongJuTransform.Find(chu_jie_ping_mian).gameObject;
    //        chuJiePingMian.tag = Tags.jie_wai_outer_bounds;
    //        chuJiePingMian.layer = LayerMask.NameToLayer(Layers.outer_bounds);
    //    }

    //    {
    //        kaiQiuDians.Clear();

    //        //开球点
    //        Transform kaiQiuDiansTF = gongJuTransform.Find(kai_qiu_dian_all);
    //        int kaiQiuDianAmount = kaiQiuDiansTF.childCount;
    //        for (int i = 1; i <= kaiQiuDianAmount; i++)
    //        {
    //            string name = "";
    //            if (i <= 9)
    //            {
    //                name = kai_qiu_dian_zuo_biao + "0" + i;
    //            }
    //            else
    //            {
    //                name = kai_qiu_dian_zuo_biao + i;
    //            }

    //            Transform tf = kaiQiuDiansTF.Find(name);
    //            if (tf != null)
    //            {
    //                kaiQiuDians.Add(tf.position);
    //            }
    //        }
    //    }


    //    {
    //        //球洞坐标
    //        //golfHole = gongJuTransform.Find(qiu_dong_zuo_biao).transform.position;
    //        golfHole = levelCfgData.m_HolePosVec;

    //        golfBottomHole = golfHole;
    //        golfBottomHole.y -= 0.15f;
    //    }


    //    height_euler_normal_y = golfHole.y;//kaiQiuDians[0].y;

    //    {
    //        //场景的方向光
    //        lightDirectTf = gongJuTransform.Find(light_direction);
    //    }
    //}


    ///// <summary>
    ///// 初始化场景的游戏区
    ///// </summary>
    ///// <param name="changJingRootTransForm"></param>
    //private void initYouXiQu(Transform changJingRootTransForm)
    //{
    //    Transform youXiQuTF = changJingRootTransForm.Find(you_xi_qu);

    //    initJieNeiQu(youXiQuTF);

    //    initJieWaiQuDiMian(youXiQuTF);

    //    {
    //        qiGan = youXiQuTF.Find(qi_gan_all).gameObject;

    //        qiGan_Gan = qiGan.transform.Find(qi_gan_gan).gameObject;
    //        qiGan_Gan.layer = LayerMask.NameToLayer(Layers.defaultLayer);
    //        qiGan_Gan.tag = Tags.qi_gan;
    //        qiGan_Gan.collider.material = assetManager.qiGanPhyMat;

    //        qiGan_Qi = qiGan.transform.Find(qi_gan_qi).gameObject;
    //        qiGan_Qi.transform.position = qiGan_Gan.transform.position + new Vector3(0, GameConfig.qi_gan_qi_offset_y, 0);
    //        qiGan_Qi.transform.eulerAngles = GameConfig.qi_gan_qi_init_rotation;

    //        GameObject ganCopyGo = new GameObject("gan_copy");
    //        ganCopyGo.transform.parent = qiGan_Gan.transform;
    //        ganCopyGo.transform.localPosition = Vector3.zero;
    //        ganCopyGo.transform.localRotation = Quaternion.identity;
    //        ganCopyGo.transform.localScale = Vector3.one;
    //        ganCopyGo.AddComponent<MeshFilter>().sharedMesh = qiGan_Gan.GetComponent<MeshFilter>().sharedMesh;
    //        ganCopyGo.AddComponent<MeshRenderer>().sharedMaterial = qiGan_Gan.GetComponent<MeshRenderer>().sharedMaterial;

    //        qiGan_Gan.GetComponent<MeshRenderer>().enabled = false;

    //        qiGan_RotateNode = new GameObject("qi_gan_rotate_node");
    //        qiGan_RotateNode.transform.position = Vector3.zero;
    //        qiGan_RotateNode.transform.rotation = Quaternion.identity;
    //        qiGan_RotateNode.transform.localScale = Vector3.one;

    //        qiGan_RotateNode.transform.parent = qiGan_Gan.transform;
    //        qiGan_RotateNode.transform.localPosition = Vector3.zero;

    //        qiGan_RotateNode.transform.parent = qiGan.transform;
    //        Vector3 tmpPos = qiGan_RotateNode.transform.position;
    //        tmpPos.y = golfHole.y - 0.1f;
    //        qiGan_RotateNode.transform.position = tmpPos;

    //        ganCopyGo.transform.parent = qiGan_RotateNode.transform;
    //        qiGan_Qi.transform.parent = qiGan_RotateNode.transform;
    //    }

    //    {
    //        //水
    //        Transform shuiAllTF = youXiQuTF.Find(shui_all);
    //        if (shuiAllTF != null)
    //        {
    //            //int shuiAmount = shuiAllTF.childCount;
    //            //for (int i = 0; i < shuiAmount; i++)
    //            //{
    //            //    Transform shuiTF = shuiAllTF.GetChild(i);
    //            //    GameObject shuiMainGO = shuiTF.Find(shui_single_main).gameObject;
    //            //    shuiMainGO.tag = Tags.shui;
    //            //    shuiMainGO.layer = LayerMask.NameToLayer(Layers.water);
    //            //}

    //            NGUITools.SetLayer(shuiAllTF.gameObject, LayerMask.NameToLayer(Layers.water));

    //            Transform shuiCollidesTf = shuiAllTF.Find(collides_folder_name);
    //            for(int i = 0; i < shuiCollidesTf.childCount; i++)
    //            {
    //                GameObject shuiCollideGo = shuiCollidesTf.GetChild(i).gameObject;
    //                shuiCollideGo.tag = Tags.shui;
    //                shuiCollideGo.layer = LayerMask.NameToLayer(Layers.water);

    //                {
    //                    //特殊补丁,后续如果修改场景，可去掉//
    //                    if (levelCfgData.m_ScenesName == "3DGolf_DCJ_haiTan_04")
    //                    {
    //                        shuiCollideGo.transform.position = new Vector3(0, 225.1f, 0);
    //                    }
    //                }
    //            }

    //            {
    //                //修改水的渲染队列
    //                MeshRenderer[] shuiRenders = shuiAllTF.GetComponentsInChildren<MeshRenderer>();
    //                List<MeshRenderer> list = new List<MeshRenderer>();
    //                list.AddRange(shuiRenders);
    //                list.Sort(WaterRenderQueueCompare);
    //                for (int i = 0; i < list.Count; i++)
    //                {
    //                    MeshRenderer meshRender = list[i];
    //                    meshRender.material.renderQueue = 3000 + (i + 1);
    //                }
    //            }
                
    //        }
    //    }

    //    {
    //        //界内的树，树干物理碰撞， 树枝有减速

    //        Transform shuAllTF = youXiQuTF.Find(shu_all);
    //        if (shuAllTF != null)
    //        {
    //            int shuAmount = shuAllTF.childCount;
    //            for (int i = 0; i < shuAmount; i++)
    //            {
    //                Transform shuSingleTF = shuAllTF.GetChild(i);

    //                Transform allShuGanBoxesGOtf = shuSingleTF.Find(shu_single_shu_gan_all_boxes);
    //                if (allShuGanBoxesGOtf != null)
    //                {
    //                    for(int j = 0; j < allShuGanBoxesGOtf.childCount; j++)
    //                    {
    //                        Transform shuGanBoxTF = allShuGanBoxesGOtf.GetChild(j);
    //                        shuGanBoxTF.gameObject.layer = LayerMask.NameToLayer(Layers.defaultLayer);
    //                        shuGanBoxTF.gameObject.tag = Tags.shu_gan;

    //                        shuGanBoxTF.GetComponent<BoxCollider>().isTrigger = false;
    //                        shuGanBoxTF.GetComponent<BoxCollider>().material = assetManager.shuGanPhyMat;
    //                    }
    //                }

    //                Transform shuZhiBoxGOTF = shuSingleTF.Find(shu_single_shu_zhi_box);
    //                if (shuZhiBoxGOTF != null)
    //                {
    //                    shuZhiBoxGOTF.gameObject.layer = LayerMask.NameToLayer(Layers.defaultLayer);
    //                    shuZhiBoxGOTF.gameObject.tag = Tags.shu_zhi;

    //                    BoxCollider shuZhiBoxCollide = shuZhiBoxGOTF.GetComponent<BoxCollider>();
    //                    shuZhiBoxCollide.isTrigger = true;
    //                }

    //            }
    //        }
    //    }

    //}


    ///// <summary>
    ///// 初始化游戏的界内区
    ///// </summary>
    ///// <param name="youXiQuTF"></param>
    //private void initJieNeiQu(Transform youXiQuTF)
    //{
    //    jieNeiQuTF = youXiQuTF.Find(jie_nei_qu);


    //    {
    //        changCaoQuMaterials.Clear();

    //        //长草区
    //        Transform changCaoQuTFAll = jieNeiQuTF.Find(chang_cao_qu_all);
    //        int changCaoQuAmount = changCaoQuTFAll.GetChildCount();
    //        for (int i = 0; i < changCaoQuAmount; i++)
    //        {
    //            GameObject ccqGO = changCaoQuTFAll.GetChild(i).gameObject;
    //            MeshCollider ccqMC = ccqGO.GetComponent<MeshCollider>();
    //            ccqMC.material = assetManager.changCaoQuPhysicMaterial;
    //            ccqGO.tag = Tags.chang_cao_qu;
    //            ccqGO.layer = LayerMask.NameToLayer(Layers.ground);

    //            changCaoQuMaterials.Add(ccqGO.renderer.sharedMaterial);
    //        }
    //    }

    //    {
    //        //果岭
    //        GameObject guoLing = jieNeiQuTF.Find(guo_ling).gameObject;
    //        MeshCollider guoLingMC = guoLing.GetComponent<MeshCollider>();
    //        guoLingMC.material = assetManager.guoLingPhysicMaterial;
    //        guoLing.tag = Tags.guo_ling;
    //        guoLing.layer = LayerMask.NameToLayer(Layers.ground);

    //        guolingMaterial = guoLing.renderer.sharedMaterial;
    //    }

    //    {
    //        //果岭边缘
    //        GameObject guoLingBianYuan = jieNeiQuTF.Find(guo_ling_bian_yuan).gameObject;
    //        MeshCollider guoLingBianYuanMC = guoLingBianYuan.GetComponent<MeshCollider>();
    //        guoLingBianYuanMC.material = assetManager.guoLingBianYuanPhysicMaterial;
    //        guoLingBianYuan.tag = Tags.guo_ling_bian_yuan;
    //        guoLingBianYuan.layer = LayerMask.NameToLayer(Layers.ground);

    //        guoLingBianYuanMaterial = guoLingBianYuan.renderer.sharedMaterial;
    //    }

    //    {
    //        qiuDaoMaterials.Clear();

    //        //球道
    //        Transform qiuDaoTFAll = jieNeiQuTF.Find(qiu_dao_all);
    //        int qiuDaoCount = qiuDaoTFAll.GetChildCount();
    //        for (int i = 0; i < qiuDaoCount; i++)
    //        {
    //            GameObject qiuDao = qiuDaoTFAll.GetChild(i).gameObject;
    //            MeshCollider qiuDaoMC = qiuDao.GetComponent<MeshCollider>();
    //            qiuDaoMC.material = assetManager.qiuDaoPhysicMaterial;
    //            qiuDao.tag = Tags.qiu_dao;
    //            qiuDao.layer = LayerMask.NameToLayer(Layers.ground);

    //            qiuDaoMaterials.Add(qiuDao.renderer.sharedMaterial);
    //        }
    //    }

    //    {
    //        shaKengMaterials.Clear();

    //        //沙坑
    //        Transform shaKengAllTF = jieNeiQuTF.Find(sha_keng_all);
    //        if (shaKengAllTF != null)
    //        {
    //            int shaKengCount = shaKengAllTF.GetChildCount();
    //            for (int i = 0; i < shaKengCount; i++)
    //            {
    //                GameObject shaKeng = shaKengAllTF.GetChild(i).gameObject;
    //                MeshCollider shaKengMC = shaKeng.GetComponent<MeshCollider>();
    //                shaKengMC.material = assetManager.shaKengPhysicMaterial;
    //                shaKeng.tag = Tags.sha_keng;
    //                shaKeng.layer = LayerMask.NameToLayer(Layers.ground);

    //                shaKengMaterials.Add(shaKeng.renderer.sharedMaterial);
    //            }
    //        }
    //    }

    //    //球洞墙壁
    //    GameObject qiuDong = jieNeiQuTF.Find(qiu_dong).gameObject;
    //    MeshCollider qiuDongMC = qiuDong.GetComponent<MeshCollider>();
    //    qiuDongMC.material = assetManager.qiuDongQiangBiPhysicMaterial;
    //    qiuDong.tag = Tags.qiu_dong_qiang_bi;
    //    qiuDong.layer = LayerMask.NameToLayer(Layers.ground);
    //    if (!qiuDong.renderer.material.shader.name.Equals(unlitTextureShaderName))
    //    {
    //        qiuDong.renderer.material.shader = Shader.Find(unlitTextureShaderName);
    //    }

    //    //球洞触发器
    //    GameObject qiuDongChuFaQi = jieNeiQuTF.Find(qiu_dong_chu_fa_qi).gameObject;
    //    qiuDongChuFaQi.GetComponent<MeshCollider>();
    //    qiuDongChuFaQi.tag = Tags.hole;
    //    qiuDongChuFaQi.layer = LayerMask.NameToLayer(Layers.ground);
    //    if (!qiuDongChuFaQi.renderer.material.shader.name.Equals(unlitTextureShaderName))
    //    {
    //        qiuDongChuFaQi.renderer.material.shader = Shader.Find(unlitTextureShaderName);
    //    }


    //    {
    //        qiuZuoDiMianMaterials.Clear();

    //        Transform faQiuTalAllTF = jieNeiQuTF.Find(fa_qiu_tai_all);
    //        int faQiuTaiAmount = faQiuTalAllTF.GetChildCount();
    //        for (int i = 0; i < faQiuTaiAmount; i++)
    //        {
    //            GameObject faQiuTaiGO = faQiuTalAllTF.GetChild(i).gameObject;

    //            //发球台的球座
    //            GameObject qiuZuoGO = faQiuTaiGO.transform.Find(fa_qiu_tai_qiu_zuo).gameObject;
    //            qiuZuoGO.GetComponent<BoxCollider>().material = assetManager.qiuZuoPhyMat;
    //            qiuZuoGO.tag = Tags.qiu_zuo;
    //            qiuZuoGO.layer = LayerMask.NameToLayer(Layers.defaultLayer);

    //            //发球台的地面
    //            Transform faQiuTaiDiMianAll = faQiuTaiGO.transform.Find(fa_qiu_tai_di_mian_all);
    //            int fqtdmAmount = faQiuTaiDiMianAll.GetChildCount();
    //            for (int j = 0; j < fqtdmAmount; j++)
    //            {
    //                GameObject fqtdmGO = faQiuTaiDiMianAll.GetChild(j).gameObject;
    //                MeshCollider fqtdmMC = fqtdmGO.GetComponent<MeshCollider>();
    //                fqtdmMC.material = assetManager.faQiuTaiDiMianPhysicMaterial;
    //                fqtdmGO.tag = Tags.fa_qiu_tai_di_mian;
    //                fqtdmGO.layer = LayerMask.NameToLayer(Layers.ground);

    //                qiuZuoDiMianMaterials.Add(fqtdmGO.renderer.sharedMaterial);
    //            }
    //        }
    //    }
    //}

    ///// <summary>
    ///// 初始化场景界外区的地面
    ///// </summary>
    ///// <param name="youXiQuTF"></param>
    //private void initJieWaiQuDiMian(Transform youXiQuTF)
    //{
    //    Transform jieWaiQuDiMianTF = youXiQuTF.Find(jie_wai_qu_di_mian);
    //    int amount = jieWaiQuDiMianTF.childCount;
    //    for (int i = 0; i < amount; i++)
    //    {
    //        GameObject go = jieWaiQuDiMianTF.GetChild(i).gameObject;
    //        go.tag = Tags.jie_wai;
    //        go.layer = LayerMask.NameToLayer(Layers.ground);
    //    }

    //    Transform collidesTf = jieWaiQuDiMianTF.Find(collides_folder_name);
    //    for(int i = 0; i < collidesTf.childCount; i++)
    //    {
    //        GameObject go = collidesTf.GetChild(i).gameObject;
    //        MeshCollider mc = go.GetComponent<MeshCollider>();
    //        mc.material = assetManager.jieWaiPhysicMaterial;
    //        go.tag = Tags.jie_wai;
    //        go.layer = LayerMask.NameToLayer(Layers.ground);
    //    }
    //}


    ///// <summary>
    ///// 已经废弃
    ///// 根据手机性能，关闭部分场景物体
    ///// </summary>
    ///// <param name="changJingRootTf"></param>
    //private void InitZhuangShiQu(Transform changJingRootTf)
    //{
    //    Transform zhuangShiQuTf = changJingRootTf.Find(zhuang_shi_qu);
    //    Transform shuContainerTf = zhuangShiQuTf.Find(zhuang_shi_qu_shu);
    //    Transform guanMuContainerTf = zhuangShiQuTf.Find(zhuang_shi_qu_guan_mu);
    //    Transform guanMuContainer2Tf = zhuangShiQuTf.Find(zhuang_shi_qu_guan_mu_2);

    //    PerformanceManager.PerformanceLevel performanceLevel = PerformanceManager.Instance.GetCurrentPerFormanceLevel();
    //    if(performanceLevel <= PerformanceManager.PerformanceLevel.Low)
    //    {
    //        if(shuContainerTf != null)
    //        {
    //            shuContainerTf.gameObject.SetActive(false);
    //        }

    //        if(guanMuContainerTf != null)
    //        {
    //            guanMuContainerTf.gameObject.SetActive(false);
    //        }

    //        if(guanMuContainer2Tf != null)
    //        {
    //            guanMuContainer2Tf.gameObject.SetActive(false);
    //        }
    //    }
    //    else if(performanceLevel <= PerformanceManager.PerformanceLevel.Middle)
    //    {
    //        if (levelCfgData.m_ThemeName == LevelInfoUtil.theme_SenLin)
    //        {
    //            if (shuContainerTf != null)
    //            {
    //                shuContainerTf.gameObject.SetActive(false);
    //            }

    //            if (guanMuContainerTf != null)
    //            {
    //                guanMuContainerTf.gameObject.SetActive(false);
    //            }

    //            if (guanMuContainer2Tf != null)
    //            {
    //                guanMuContainer2Tf.gameObject.SetActive(false);
    //            }
    //        }
    //    }
    //}

    ///// <summary>
    ///// 根据手机性能，关闭部分场景物体
    ///// 在GameMainLogic 的 Start 方法中调用， 确保CombineStatic的Awake 方法已经执行完毕
    ///// </summary>
    //public void Set_ZhuangShiQu_VisibleByDevicePerformance()
    //{
    //    Transform changJingRootTf = GameObject.Find(root_chang_jing_path).transform;

    //    Transform zhuangShiQuTf = changJingRootTf.Find(zhuang_shi_qu);
    //    SceneAttrCfgData sceneAttrCfgData = SceneAttrCfgData.GetDataBySceneName(levelCfgData.m_ScenesName);
    //    PerformanceManager.PerformanceLevel performanceLevel = PerformanceManager.Instance.GetCurrentPerFormanceLevel();

    //    Collect_GuanMu_In_Shu(zhuangShiQuTf);

    //    if (performanceLevel <= PerformanceManager.PerformanceLevel.Low)
    //    {
    //        HideTargetNodeList(zhuangShiQuTf, sceneAttrCfgData.lowHideNodeList);
    //    }
    //    else if (performanceLevel <= PerformanceManager.PerformanceLevel.Middle)
    //    {
    //        HideTargetNodeList(zhuangShiQuTf, sceneAttrCfgData.mediumHideNodeList);
    //    }
    //    else
    //    {
    //        HideTargetNodeList(zhuangShiQuTf, sceneAttrCfgData.highHideNodeList);
    //    }
    //}

    ///// <summary>
    ///// 部分老的场景 guan_mu 存在 装饰区的shu节点下面。提取到新构造的 guan_mu节点下。方便统一使用隐藏规则
    ///// </summary>
    ///// <param name="zhuangShiQuTf"></param>
    //private void Collect_GuanMu_In_Shu(Transform zhuangShiQuTf)
    //{
    //    string guan_mu_name_start_key = "guan_mu";
    //    string new_guan_mu_node_name = "guan_mu";

    //    Transform shuContainerTf = zhuangShiQuTf.Find(zhuang_shi_qu_shu);
    //    if(shuContainerTf == null)
    //    {
    //        return;
    //    }

    //    bool hasGuanMu = false;
    //    for (int i = 0; i < shuContainerTf.childCount; i++)
    //    {
    //        Transform child = shuContainerTf.GetChild(i);
    //        if (child.gameObject.activeSelf && child.gameObject.name.StartsWith(guan_mu_name_start_key))
    //        {
    //            hasGuanMu = true;
    //            break;
    //        }
    //    }

    //    if (hasGuanMu)
    //    {
    //        GameObject newGuanMuGo = new GameObject(new_guan_mu_node_name);
    //        newGuanMuGo.transform.parent = zhuangShiQuTf;
    //        newGuanMuGo.transform.position = Vector3.zero;
    //        newGuanMuGo.transform.rotation = Quaternion.identity;
    //        newGuanMuGo.transform.localScale = Vector3.one;

    //        Transform[] children = shuContainerTf.GetComponentsInChildren<Transform>();
    //        for (int i = 0; i < children.Length; i++)
    //        {
    //            Transform child = children[i];
    //            if ((child.parent == shuContainerTf) && child.gameObject.activeSelf && child.gameObject.name.StartsWith(guan_mu_name_start_key))
    //            {
    //                child.parent = newGuanMuGo.transform;
    //            }
    //        }

    //        newGuanMuGo.AddComponent<CombineStatic>();
    //    }
    //}

    ///// <summary>
    ///// 隐藏装饰区特定的子节点列表
    ///// </summary>
    ///// <param name="nodeList"></param>
    //private void HideTargetNodeList(Transform zhuangShiQuTf, List<string> nodeList)
    //{
    //    if(nodeList.Count == 0)
    //    {
    //        return;
    //    }

    //    for(int i = 0; i < zhuangShiQuTf.childCount; i++)
    //    {
    //        Transform child = zhuangShiQuTf.GetChild(i);
    //        if (child.gameObject.activeSelf)
    //        {
    //            if (nodeList.Contains(child.gameObject.name))
    //            {
    //                child.gameObject.SetActive(false);
    //            }
    //        }
    //    }
    //}

    //public void CombineSceneStatic(Transform changjingRootTf)
    //{
    //    CombineStatic[] combineStaticAry = changjingRootTf.GetComponentsInChildren<CombineStatic>();

    //    if ((combineStaticAry != null) && (combineStaticAry.Length > 0))
    //    {
    //        for(int i = 0; i < combineStaticAry.Length; i++)
    //        {
    //            GameObject go = combineStaticAry[i].gameObject;
    //            StaticBatchingUtility.Combine(go); 
    //        }
    //    }
    //}

    //public GameObject getQiGan()
    //{
    //    return qiGan;
    //}

    //public GameObject getQiGan_Gan()
    //{
    //    return qiGan_Gan;
    //}

    //public GameObject GetQiGan_RotateNode()
    //{
    //    return qiGan_RotateNode;
    //}

    //public void changeTilingByCameraDistanceToGround(float dis)
    //{
    //    dis = Mathf.Clamp(dis, GameMainLogic.camera_min_y, 60);
    //    float factor = (dis - GameMainLogic.camera_min_y) / (60 - GameMainLogic.camera_min_y);
    //    updateSceneMaterial(factor);
    //}

    //public const string mainTexName = "_MainTex";
    //public const string bumpMapTexName = "_BumpMap";

    //private void updateSceneMaterial(float factor)
    //{
    //    guolingMaterial.SetFloat("_Height", factor);
    //    guoLingBianYuanMaterial.SetFloat("_Height", factor);

    //    for(int i = 0; i < qiuZuoDiMianMaterials.Count; i++)
    //    {
    //        qiuZuoDiMianMaterials[i].SetFloat("_Height", factor);
    //    }

    //    for (int i = 0; i < changCaoQuMaterials.Count; i++)
    //    {
    //        changCaoQuMaterials[i].SetFloat("_Height", factor);
    //    }

    //    for (int i = 0; i < qiuDaoMaterials.Count; i++)
    //    {
    //        qiuDaoMaterials[i].SetFloat("_Height", factor);
    //    }

    //    for (int i = 0; i < shaKengMaterials.Count; i++)
    //    {
    //        shaKengMaterials[i].SetFloat("_Height", factor);
    //    }

    //}

    //public Vector3 GetLightDirection()
    //{
    //    return lightDirectTf.eulerAngles;
    //}

    ///// <summary>
    ///// 水 有多个半透明的渲染， 根据Y值，来确认渲染顺序。
    ///// </summary>
    ///// <param name="left"></param>
    ///// <param name="right"></param>
    ///// <returns></returns>
    //public int WaterRenderQueueCompare(Renderer left, Renderer right)
    //{
    //    float leftY = left.transform.position.y;
    //    float rightY = right.transform.position.y;

    //    if (leftY < rightY)
    //    {
    //        return -1;
    //    }
    //    else if(leftY > rightY)
    //    {
    //        return 1;
    //    }
    //    else
    //    {
    //        return 0;
    //    }
    //}

    //public void SetFlagWaveDirByWind(CCVec3FloatToLong pV)
    //{
    //    qiGan_Qi.transform.eulerAngles = GameConfig.qi_gan_qi_init_rotation;
    //    Vector3 v = SendMsgHandle.GetFloatVector3(pV);

    //    if ((v.x != 0) || (v.y != 0))
    //    {
    //        Vector3 windDir = new Vector3(v.x, 0, v.y);
    //        Vector3 fromDir = new Vector3(-1, 0, 0);
    //        float deltaAngle = Vector3.Angle(fromDir, windDir);
    //        Vector3 crossY = Vector3.Cross(fromDir, windDir);
    //        if (crossY.y > 0)
    //        {
    //            qiGan_Qi.transform.Rotate(Vector3.up, deltaAngle, Space.World);
    //        }
    //        else if (crossY.y < 0)
    //        {
    //            qiGan_Qi.transform.Rotate(Vector3.up, -deltaAngle, Space.World);
    //        }
    //        else
    //        {
    //            //
    //        }
    //    }

    //}
}
