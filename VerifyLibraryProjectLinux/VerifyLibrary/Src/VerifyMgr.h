#ifndef VerifyMgr_h
#define VerifyMgr_h
#include <map>
#include "SimulationMgr.h"
#include "IndexQue.h"
class VerifyMgr
{
public:
	VerifyMgr(unsigned int iSceneConcurrencyCnt);
	~VerifyMgr();
	int GetFreeSlotIndex(int sceneId);
	void BackSlotIndex(int sceneId, int slotIndex);
    SimulationMgr* GetSimulationMgr(int slotIndex);
private:
    unsigned int m_iSceneConcurrencyCnt;
	std::map<int,IndexQue*> mapIndexQueue; //key sceneId
	std::map<unsigned int,SimulationMgr*> mapSimulationMgr; //key slotIndex
	MyMutex* m_pMutex;
};

#endif
