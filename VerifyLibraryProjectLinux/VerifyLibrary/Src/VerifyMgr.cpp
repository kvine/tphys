#include "VerifyMgr.h"

VerifyMgr::VerifyMgr()
{
	mapIndexQueue = std::map<int, IndexQue*>();
	mapSimulationMgr = std::map<unsigned int, SimulationMgr*>();
	m_pMutex = new MyMutex();
}

VerifyMgr::~VerifyMgr()
{
	std::map<int, IndexQue*>::iterator iter;
	for (iter = mapIndexQueue.begin(); iter != mapIndexQueue.end(); iter++)
	{
		delete iter->second;
	}
	mapIndexQueue.clear();

	std::map<unsigned int, SimulationMgr*>::iterator iterSimulate;
	for (iterSimulate = mapSimulationMgr.begin(); iterSimulate != mapSimulationMgr.end(); iterSimulate++)
	{
		iterSimulate->second->CleanPhysics();
	}
	PhysicsSceneManager::CleanPhysxFoundation();

    for (iterSimulate = mapSimulationMgr.begin(); iterSimulate != mapSimulationMgr.end(); iterSimulate++)
    {
        delete iterSimulate->second;
    }
    
	mapSimulationMgr.clear();

	delete m_pMutex;
}

int VerifyMgr::GetFreeSlotIndex(int sceneId)
{
	int index;
	//如果不存在，则插入到map
	std::map<int, IndexQue*>::iterator iter;

	m_pMutex->Lock();

	iter = mapIndexQueue.find(sceneId);
	if (iter == mapIndexQueue.end())
	{
		int minIndex = sceneId * g_len;
		int maxIndex = (sceneId + 1) * g_len;
		IndexQue* pIndexQue = new IndexQue(minIndex, maxIndex);
		
		mapIndexQueue.insert(std::pair<int, IndexQue*>(sceneId, pIndexQue));
		printf("create scene %d indexQueue success, size %lu \n", sceneId, mapIndexQueue.size());
		index = pIndexQue->PopSlotIndex();
	}
	else
	{
		index = iter->second->PopSlotIndex();
	}

	m_pMutex->Unlock();

	return index;
}

void VerifyMgr::BackSlotIndex(int sceneId, int slotIndex)
{
	std::map<int, IndexQue*>::iterator iter;
	iter = mapIndexQueue.find(sceneId);
	if(iter != mapIndexQueue.end())
	{
		iter->second->PushSlotIndex(slotIndex);
	}
}

SimulationMgr* VerifyMgr::GetSimulationMgr(int slotIndex)
{
    std::map<unsigned int, SimulationMgr*>::iterator iter;
    iter = mapSimulationMgr.find(slotIndex);
    if (iter != mapSimulationMgr.end())
    {
        return iter->second;
    }
    else
    {
        SimulationMgr* pSimualteMgr = new SimulationMgr(slotIndex);
        mapSimulationMgr.insert(std::pair<unsigned int, SimulationMgr*>(slotIndex, pSimualteMgr));
        return pSimualteMgr;
    }
}