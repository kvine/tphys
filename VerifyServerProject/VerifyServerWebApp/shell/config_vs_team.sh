#!/bin/bash

cd $(dirname "$0")
### ssh登录主机需要的key文件的路径
identity_path=/Users/doodle/Documents/unitysceneexport2physx3_4/VerifyServerProject/VerifyServerWebApp/key

### 主机ip列表
RELEASE_HOST=(
"107.21.18.161" #
)

### 主机名字对应上面host（自定义）
RELASE_HOST_NAME=(
"c-t1"
)

### 登陆主机的key
IDENTITYLIST=(
"vs-team-00-golf3d-test.pem"
)

#export HOST_USER=ubuntu
export BASE_DIR=/home/ubuntu/
export ROOT_DIR=/home/ubuntu/

get_host_by_name()
{
	if [ "$1" ]; then
		for I in "${!RELASE_HOST_NAME[@]}"; do
			if [ "${RELASE_HOST_NAME[$I]}" == "$1" ]; then
				INDEX=${I}
			fi
		done
		if [ ! "${INDEX}" ]; then
			echo "host not found! hosts: "
			echo ${RELASE_HOST_NAME[*]}
			exit 1
		fi
        if [ "$1" == "platform" ]; then
            export HOST_USER=ec2-user
        else
            export HOST_USER=ubuntu
        fi
		export HOST="${RELEASE_HOST[${INDEX}]}"
		export HOST_NAME=$1
		export IDENTITY="$identity_path/${IDENTITYLIST[${INDEX}]}"
	else
		echo "+++++++++++++++++++++++++++++++++"
		echo "host name as follows:"
		for I in "${!RELASE_HOST_NAME[@]}"; do
			echo ${RELASE_HOST_NAME[$I]}
		done
		echo "+++++++++++++++++++++++++++++++++"
	fi
}
