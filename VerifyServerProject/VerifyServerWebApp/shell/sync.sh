#!/bin/bash
cd $(dirname "$0")

### pj path
pj_path=golf3d_verify/VerifyServerWebApp
### host shell path
source ./config_vs_team.sh

# cd ..

### 同步
if [ "$1" ] && [ "$2" ] ; then
    get_host_by_name "$1"

    echo "IDENTITY: $IDENTITY"
    echo "HOST    : $HOST"
    echo "BASEDIR : $BASE_DIR"

    ubuntu=ubuntu

    chmod 777 *

    ssh -i ${IDENTITY} $ubuntu@${HOST} "mkdir -p ${BASE_DIR}/$pj_path"

    ## 同步项目文件
    cd ..
    rsync -avze "ssh -i ${IDENTITY}" \
    Controllers \
    Properties \
    SceneDatas \
    Scripts \
    shell \
    appsettings.Development.json \
    appsettings.json \
    libPhysX3_x64.so \
    libPhysX3Common_x64.so \
    libPhysX3Cooking_x64.so \
    libPxFoundation_x64.so \
    libPxPvdSDK_x64.so \
    libVerifyLibrary.so \
    Program.cs \
    Startup.cs \
    VerifyServerWebApp.csproj \
    $ubuntu@${HOST}:${BASE_DIR}/$pj_path

else 
    echo "+++++++++++++++++++++++++++++++++"
		echo "must input host name and node config name, etc:"
        echo "sync.sh verify_physics_b1 b_t1"
	echo "+++++++++++++++++++++++++++++++++"

fi

