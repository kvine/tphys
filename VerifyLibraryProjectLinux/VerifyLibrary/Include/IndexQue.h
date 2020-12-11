#ifndef IndexQue_h
#define IndexQue_h
#include <queue>
#include "MyMutex.h"
class IndexQue
{
public:
	IndexQue(int minIndex, int maxIndex);
	~IndexQue();
	//�ҵ�һ�����õ�����,���û�У��򷵻�-1
	int PopSlotIndex();
	//�黹����
	void PushSlotIndex(int index);
private:
	std::queue<int>* m_pQueueSlotIndex;
	MyMutex* m_pMutex;
};

#endif