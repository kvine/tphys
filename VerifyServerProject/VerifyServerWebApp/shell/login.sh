# !/bin/bash
cd $(dirname "$0")

source ./config_vs_team.sh

get_host_by_name "$1"

echo "IDENTITY: $IDENTITY"
echo "HOST    : $HOST"

chmod 777 *
ssh -i $IDENTITY ubuntu@${HOST}