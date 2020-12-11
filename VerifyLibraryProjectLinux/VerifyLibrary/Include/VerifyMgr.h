#ifndef VerifyMgr_h
#define VerifyMgr_h
#include <map>
#include "SimulationMgr.h"
#include "IndexQue.h"
class VerifyMgr
{
public:
	VerifyMgr();
	~VerifyMgr();
	int GetFreeSlotIndex(int sceneId);
	void BackSlotIndex(int sceneId, int slotIndex);
    SimulationMgr* GetSimulationMgr(int slotIndex);
private:
	const int g_len = 25;	
	std::map<int,IndexQue*> mapIndexQueue; //key sceneId
	std::map<unsigned int,SimulationMgr*> mapSimulationMgr; //key slotIndex
	MyMutex* m_pMutex;
};

#endif
