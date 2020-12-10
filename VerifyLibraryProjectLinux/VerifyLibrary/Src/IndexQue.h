#ifndef IndexQue_h
#define IndexQue_h
#include <queue>
#include "MyMutex.h"
class IndexQue
{
public:
	IndexQue(int minIndex, int maxIndex);
	~IndexQue();
	//找到一个空置的索引,如果没有，则返回-1
	int PopSlotIndex();
	//归还索引
	void PushSlotIndex(int index);
private:
	std::queue<int>* m_pQueueSlotIndex;
	MyMutex* m_pMutex;
};

#endif