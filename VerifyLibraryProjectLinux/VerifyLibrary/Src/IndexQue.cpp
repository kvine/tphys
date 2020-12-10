#include "IndexQue.h"

IndexQue::IndexQue(int minIndex, int maxIndex)
{
	m_pMutex = new MyMutex();
	m_pQueueSlotIndex = new std::queue<int>();
	
	for (int i = minIndex; i < maxIndex; ++i)
	{
		m_pQueueSlotIndex->push(i);
	}
}
IndexQue::~IndexQue()
{
	while (m_pQueueSlotIndex->size() > 0)
	{
		m_pQueueSlotIndex->pop();
	}

	delete m_pQueueSlotIndex;
	m_pQueueSlotIndex = NULL;

	delete m_pMutex;
}

int IndexQue::PopSlotIndex()
{
	int index;
	m_pMutex->Lock();
	if (m_pQueueSlotIndex->empty())//╤сапн╙©у
	{
		index = -1;
	}
	else
	{
		index = m_pQueueSlotIndex->front();
		m_pQueueSlotIndex->pop();
	}
	m_pMutex->Unlock();

	return index;
}


void IndexQue::PushSlotIndex(int index)
{
	if (index < 0)
	{
		return;
	}
	m_pMutex->Lock();

	m_pQueueSlotIndex->push(index);

	m_pMutex->Unlock();
}